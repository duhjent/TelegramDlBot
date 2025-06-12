// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

var startInfo = new ProcessStartInfo
{
    CreateNoWindow = true,
    UseShellExecute = false,
    RedirectStandardOutput = true,
    RedirectStandardInput = true,
    RedirectStandardError = true,
    FileName = "yt-dlp",
    Arguments = "https://youtu.be/fH7GAt-MbAc -t mp4 -o test.mp4"
};

using var process = new Process
{
    StartInfo = startInfo,
    EnableRaisingEvents = true,
};
process.OutputDataReceived += (sender, args) =>
{
    Console.WriteLine(args.Data);
};

process.Start();
await process.WaitForExitAsync();

