using System;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
class Program{

    static void Main(string[] args){

        string image_path = @"./eren1.jpeg";

        Mat image = CvInvoke.Imread(image_path, Emgu.CV.CvEnum.ImreadModes.Color);

        int ratio= image.Size.Width / image.Size.Height;
        int newHeight = Console.WindowHeight;

        int newWidth = ratio * newHeight;
        Mat resizedImage = new Mat();
        List<string> Output = new List<string>();
        CvInvoke.Resize(image,resizedImage, new Size(newWidth, newHeight) );
        Image<Bgr, byte> ReadImage = resizedImage.ToImage<Bgr, byte>();
        for (int y = 0; y < newHeight; y++){
            string layer = "";
            for (int x = 0;x < newWidth; x++) {
                Bgr color = ReadImage[y,x];
                string Pixel = $"\x1b[48;2;{(int)(color.Red)};{(int)(color.Green)};{(int)(color.Blue)}m  \x1b[0m";

                layer += Pixel;

            }
            Output.Add(layer);
        }
        Console.WriteLine(Output);
        string Resultant = String.Join("\n", Output);
        Console.WriteLine(Resultant);
        CvInvoke.WaitKey(0);
    }
}