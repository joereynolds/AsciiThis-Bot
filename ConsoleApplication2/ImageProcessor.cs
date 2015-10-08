using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net;
using System.Diagnostics;

/* To Do
 * Document the methods
 * Allow the image to be declared in the constructor 
 * */

namespace ConsoleApplication2
{
    class ImageProcessor
    {
        private string path;

        public ImageProcessor(string filepath)
        {
            path = filepath;
        }

        public string fpath { get { return path; } set { path = value; } }


        /// <summary>
        /// Gets the average RGB colour value
        /// of a given pixel. 
        /// </summary>
        int AverageRgb(int x, int y, Bitmap image)
        {

            var col = image.GetPixel(x, y);
            int average = (col.R + col.G + col.B) / 3;
            return average;
        }


        /// <summary>
        /// Returns a new image based on the image you give it
        /// with the image file path string.
        /// </summary>
        /// 
        public void DrawAsciiImage(string ResultFileFullPath)
        {
            Bitmap image = new Bitmap(this.fpath);
            Bitmap AsciiImage = new Bitmap(image.Width, image.Height);
            Font drawFont = new Font("Arial", 5);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            int spacing = 6;

            for (int y = 0; y < image.Height; y += spacing)
            {
                for (int x = 0; x < image.Width; x += spacing)
                {
                    using (Graphics g = Graphics.FromImage(AsciiImage))
                    {
                        if (AverageRgb(x, y, image) > 240)
                        {
                            g.DrawString(".", drawFont, drawBrush, new PointF((float)x, (float)y));
                        }
                        else if (AverageRgb(x, y, image) > 225)
                        {
                            g.DrawString(":", drawFont, drawBrush, new PointF((float)x, (float)y));
                        }
                        else if (AverageRgb(x, y, image) > 200)
                        {
                            g.DrawString("$", drawFont, drawBrush, new PointF((float)x, (float)y));
                        }
                        else if (AverageRgb(x, y, image) > 175)
                        {
                            g.DrawString("C", drawFont, drawBrush, new PointF((float)x, (float)y));
                        }
                        else if (AverageRgb(x, y, image) > 100)
                        {
                            g.DrawString("V", drawFont, drawBrush, new PointF((float)x, (float)y));
                        }
                        else if (AverageRgb(x, y, image) > 50)
                        {
                            g.DrawString("O", drawFont, drawBrush, new PointF((float)x, (float)y));
                        }
                        else if (AverageRgb(x, y, image) > 25)
                        {
                            g.DrawString("Q", drawFont, drawBrush, new PointF((float)x, (float)y));
                        }

                        else
                        {
                            g.DrawString("M", drawFont, drawBrush, new PointF((float)x, (float)y));
                        }
                    }
                }
            }
            AsciiImage.Save(ResultFileFullPath);
        }
    }
}

