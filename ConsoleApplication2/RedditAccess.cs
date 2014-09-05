using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using RedditSharp;
using System.IO;
using System.Diagnostics;
using System.Net;

namespace ConsoleApplication2
{
    class RedditAccess
    {
        private Reddit reddit = new Reddit();
        private List<string> commentIds = new List<string>();

        private ImgurAccess imgur = new ImgurAccess();


        /// <summary>
        /// Logs a user in and then returns True if 
        /// the login was successful
        /// </summary>
        /// <returns></returns>
        private bool HasLoggedIn(string username,string password)
        {
            try
            {
                var user = reddit.LogIn(username, password);
                Console.WriteLine("User logged in");
                return true;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private void WriteListToFile(List<String> list)
        {
            StreamWriter file = new StreamWriter("c:/users/joe reynolds/desktop/idlist.txt");
            foreach (string commentid in list)
            {
                if (!file.ToString().Contains(commentid))
                {
                    file.Write(" " +commentid);
                }
            }
            file.Close();
        }

        /// <summary>
        /// Waits until a certain sentence is posted
        /// and then will reply with a message
        /// </summary>
        public void ListenForPrompt()
        {
            if (!HasLoggedIn("AsciiThis", "password"))
            {
                return;
            }

            var subreddit = reddit.GetSubreddit("/r/pics");

            foreach (var post in subreddit.New.Take(25))
            {
                Console.WriteLine("==========================");
                Console.WriteLine("THREAD NAME: {0}",post.Title);

                string url;

                try
                {
                    foreach (var comment in post.Comments)
                    {
                        Console.WriteLine("comment body: {0}", comment.Body);
                        if (!commentIds.Contains(comment.Id) && comment.Body.Contains("hello ascii! "))
                        {
                            using (WebClient client = new WebClient())
                            {
                                url = comment.Body.Substring(comment.Body.IndexOf("!")+2);
                                string filename = @"C:/users/joe reynolds/desktop/image.jpg";

                                ImageProcessor im = new ImageProcessor(filename);
                                
                                client.DownloadFile(url,filename);
                                //Console.WriteLine(url);
                                commentIds.Add(comment.Id);
                                WriteListToFile(commentIds);
                                im.DrawAsciiImage();

                                comment.Reply(imgur.UploadImage("c:/users/joe reynolds/desktop/result.jpg") + System.Environment.NewLine  + " ^I ^am ^a ^bot. ^I'm ^still ^being ^tested. ^Im ^very ^unreliable ^at ^^the ^^^moment"); 
                                                                 //+System.Environment.NewLine +  "[^source ^code](https://github.com/JoeReynolds1/AsciiThis-Bot)");
                                Console.WriteLine("Replied to message");
                                //File.Delete(im.fpath);
                                //Console.WriteLine("Removed file from machine");
                                Thread.Sleep(600000);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("exception: {0}", ex.Message);
                }
            }
        }
    }
}
