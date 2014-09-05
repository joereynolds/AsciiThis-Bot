using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            RedditAccess reddit = new RedditAccess();
            
            //ImgurAccess imgur = new ImgurAccess();
            //ImageProcessor im = new ImageProcessor("c:/users/joe reynolds/desktop/test5.jpg");
            //im.DrawAsciiImage();
            //Process.Start(imgur.UploadImage("c:/users/joe reynolds/desktop/result.jpg"));
        
            while (true)
            {
             reddit.ListenForPrompt();
            }
        }
    }
}
