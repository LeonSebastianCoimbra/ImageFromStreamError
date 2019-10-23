using System;
using System.IO;
using System.Drawing;

namespace TestEmptyPNGGDI
{
    class Program
    {
        static void Main(string[] args)
        {
            // This one will works
            using (Bitmap image = new Bitmap("/mnt/share/callout.png"))
            {
                Console.WriteLine("ImageSize: [" + image.Width + "," + image.Height + "]");
            }

            // This one will works too
            using (Image image = Image.FromFile("/mnt/share/callout.png"))
            {
                Console.WriteLine("ImageSize: [" + image.Width + "," + image.Height + "]");
            }

            // This one will not work... In a Linux container (created with the attached Dockerfile) it will break at a c++ level,
            // resulting on an interruption of the program. It is not an exception, but something related to the implementation of
            // the library libgdiplus (or one of its dependency). Since it is a problem at native code, the program will end without
            // the possibility to manage the error.
            using (var stream = new FileStream("/mnt/share/callout.png", FileMode.Open))
            {
                using (var image = Image.FromStream(stream))
                {
                    Console.WriteLine("ImageSize: [" + image.Width + "," + image.Height + "]");
                }
            }

            // We will not reach this point!!
            Console.WriteLine("Hello World!");
        }
    }
}
