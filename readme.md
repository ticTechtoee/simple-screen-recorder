# Screen Recorder

A simple screen recording application built using **C#.NET** that captures both video and audio, and merges them into a final MP4 output file. Perfect for personal use, content creators, or anyone who needs to record their screen with audio.

## Features
- **High-quality screen recording**: Capture your entire screen as a video in MP4 format.
- **Audio capture**: Record system audio in WAV format.
- **Seamless video-audio merging**: Merge the recorded video and audio files into a final MP4 file using **FFmpeg**.

## Requirements
- **.NET Framework** (C#.NET 4.7 or higher)
- **FFmpeg** (Make sure to download and install it from [FFmpeg](https://ffmpeg.org/download.html))
- **NAudio** library for audio recording

## Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/ticTechtoee/simple-screen-recorder.git
   ```

2. **Install FFmpeg:**
   Download and install **FFmpeg** from the official site [here](https://ffmpeg.org/download.html). Make sure the `ffmpeg.exe` file is accessible in your system's PATH, or update the `VideoAudioMerger.cs` file to point to the location of your FFmpeg executable.

3. **Run the application:**
   Open the solution in **Visual Studio**, build the project, and run the application.

## How It Works

1. **Start Recording**:
   - Click the "Start Recording" button to begin capturing both video and audio.

2. **Stop Recording**:
   - Click the "Stop Recording" button to stop the screen and audio recording.
   - The video and audio files are automatically merged into a single MP4 file.

3. **Final Output**:
   - The final merged video is saved in the same directory as the application with the name `final_output.mp4`.

## Usage

Once the application is running, you can easily start and stop your recordings with the buttons provided in the user interface.

### Example:
- Start recording by clicking the **Start Recording** button.
- Stop recording and merge the video and audio by clicking the **Stop Recording** button.

## Contributing

This project is open-source and free to use. Developers are welcome to collaborate and improve the project by adding new features, fixing bugs, or optimizing the code. To contribute:
1. Fork the repository.
2. Create a new branch.
3. Make your changes.
4. Open a pull request with a description of your changes.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact

If you have any questions, suggestions, or ideas, feel free to reach out or create an issue in the repository!
