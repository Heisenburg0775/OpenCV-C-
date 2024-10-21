using System;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using System.Diagnostics;
class Program
{
    static string Converter(Mat source, int Height, int Width)
    {
        List<string> Output = new List<string>();
        Image<Bgr, byte> ReadImage = source.ToImage<Bgr, byte>();
        for (int y = 0; y < Height; y++)
        {
            string layer = "";
            for (int x = 0; x < Width; x++)
            {
                Bgr color = ReadImage[y, x];
                string Pixel = $"\x1b[48;2;{(int)(color.Red)};{(int)(color.Green)};{(int)(color.Blue)}m  \x1b[0m";

                layer += Pixel;

            }
            Output.Add(layer);
        }
        string Resultant = String.Join("\n", Output);
        return Resultant;
    }

    static void Main(string[] args)
    {
        VideoCapture video = new VideoCapture("./video.mp4");
        double fps = video.Get(Emgu.CV.CvEnum.CapProp.Fps);
        int delay = (int)(1000/fps);
        Mat image = new Mat();
        while (video.Read(image))
        {
            double ratio = (double)(image.Size.Width / image.Size.Height);
            int newHeight = Console.WindowHeight;

            int newWidth = (int)(ratio * newHeight);
            Mat resizedImage = new Mat();
            CvInvoke.Resize(image, resizedImage, new Size(newWidth, newHeight));


            string output = Converter(resizedImage, newHeight, newWidth);
            Console.SetCursorPosition(0, 0);
            Console.Write(output);
            Thread.Sleep(delay);
            CvInvoke.WaitKey(0);
        }

    }
}