using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net;
using System.Diagnostics;

/* To Do
 * Document the methods
 * Allow the image to be declared in the constructor 
 * 
 * */

namespace ConsoleApplication2
{
    class ImageProcessor
    {
        Bitmap image = new Bitmap("C:/users/joe reynolds/desktop/test1.jpg");
        Bitmap AsciiImage = new Bitmap(400, 400);

        static void Main(string[] args)
        {
            ImageProcessor im = new ImageProcessor();
            ImgurAccess imgur = new ImgurAccess();
            RedditAccess reddit = new RedditAccess();

            //im.DrawAsciiImage();
            //im.AsciiImage.Save("C:/users/joe reynolds/desktop/result.bmp");
            //Process.Start(imgur.UploadImage("SomeImage"));

            while (true)
            {
                reddit.ListenForPrompt();
            }
        }

        /// <summary>
        /// Gets the average RGB colour value
        /// of a given pixel. 
        /// </summary>
        int AverageRgb(int x, int y)
        {
            var col = image.GetPixel(x, y);
            int average = (col.R + col.G + col.B) / 3;
            return average;
        }

        /// <summary>
        /// Saves an image from a specified URL
        /// </summary>
        void SaveLinkedImage(string url)
        {

        }

        /// <summary>
        /// Put docs here
        /// </summary>
        void DrawAsciiImage()
        {
            Font drawFont = new Font("Arial", 3);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            int spacing = 5;

            for (int y = 0; y < image.Height; y += spacing)
            {
                for (int x = 0; x < image.Width; x += spacing)
                {
                    using (Graphics g = Graphics.FromImage(AsciiImage))
                    {
                        if (AverageRgb(x, y) > 225)
                        {
                            g.DrawString(" ", drawFont, drawBrush, new PointF((float)x, (float)y));
                        }
                        else if (AverageRgb(x,y) > 200)
                        {
                            g.DrawString("\"", drawFont, drawBrush, new PointF((float)x, (float)y));
                        }
                        else if (AverageRgb(x, y) > 175)
                        {
                            g.DrawString("░", drawFont, drawBrush, new PointF((float)x, (float)y));
                        }
                        else if (AverageRgb(x, y) > 100)
                        {
                            g.DrawString("@", drawFont, drawBrush, new PointF((float)x, (float)y));
                        }
                        else if (AverageRgb(x, y) > 75)
                        {
                            g.DrawString("▒", drawFont, drawBrush, new PointF((float)x, (float)y));
                        }
                        else if (AverageRgb(x, y) > 25)
                        {
                            g.DrawString("▓", drawFont, drawBrush, new PointF((float)x, (float)y));
                        }
                        else
                        {
                            g.DrawString("█", drawFont, drawBrush, new PointF((float)x, (float)y));
                        }
                    }
                }
            }
        }
    }
}
