using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using Accord.Video.FFMPEG;
using System.Diagnostics;
using System;
using System.IO;
using NAudio.Wave;

namespace Screen_Recorder
{
    public partial class Form1 : Form
    {
        private ScreenRecorder screenRecorder;
        private AudioRecorder audioRecorder;
        private string videoFilePath = "screen_video.mp4";
        private string audioFilePath = "audio.wav";
        private string outputFilePath = "final_output.mp4";
        public Form1()
        {
            InitializeComponent();
            screenRecorder = new ScreenRecorder();
            audioRecorder = new AudioRecorder();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            screenRecorder.StartRecording(videoFilePath);
            audioRecorder.StartRecording(audioFilePath);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            screenRecorder.StopRecording();
            audioRecorder.StopRecording();

            VideoAudioMerger.Merge(videoFilePath, audioFilePath, outputFilePath);

            MessageBox.Show("Recording completed and merged.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }


    public class ScreenRecorder
    {
        private VideoFileWriter writer;
        private int frameRate = 10;
        private Thread recordingThread;
        private bool isRecording;

        public ScreenRecorder()
        {
            writer = new VideoFileWriter();
        }

        public void StartRecording(string outputFilePath)
        {
            writer.Open(outputFilePath, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, frameRate, VideoCodec.MPEG4);
            isRecording = true;

            recordingThread = new Thread(() =>
            {
                while (isRecording)
                {
                    Bitmap screenshot = CaptureScreen();
                    writer.WriteVideoFrame(screenshot);
                    screenshot.Dispose();
                    Thread.Sleep(1000 / frameRate);
                }
            });
            recordingThread.Start();
        }

        public void StopRecording()
        {
            isRecording = false;
            recordingThread.Join();
            writer.Close();
        }

        private Bitmap CaptureScreen()
        {
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
            return bmp;
        }
    }

    public class AudioRecorder
    {
        private WaveInEvent waveIn;
        private WaveFileWriter writer;

        public void StartRecording(string outputFilePath)
        {
            waveIn = new WaveInEvent();
            writer = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);

            waveIn.DataAvailable += (s, a) =>
            {
                writer.Write(a.Buffer, 0, a.BytesRecorded);
            };

            waveIn.StartRecording();
        }

        public void StopRecording()
        {
            waveIn.StopRecording();
            writer.Close();
        }
    }

    public class VideoAudioMerger
    {
        public static void Merge(string videoFilePath, string audioFilePath, string outputFilePath)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = @"C:\ffmpeg\ffmpeg.exe"; // Use the full path to ffmpeg.exe
            process.StartInfo.Arguments = $"-i \"{videoFilePath}\" -i \"{audioFilePath}\" -c:v copy -c:a aac -strict experimental \"{outputFilePath}\"";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true; // Optional: Redirect standard error to capture FFmpeg output
            process.StartInfo.UseShellExecute = false;

            // Start the process
            process.Start();

            // Optionally, read the output (for debugging purposes)
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            // Optionally log the output and error
            if (!string.IsNullOrEmpty(output))
            {
                Console.WriteLine("FFmpeg Output: " + output);
            }
            if (!string.IsNullOrEmpty(error))
            {
                Console.WriteLine("FFmpeg Error: " + error);
            }
        }
    }






}
