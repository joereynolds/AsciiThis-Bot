using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using RedditSharp;
using System.IO;
using System.Net;

namespace ConsoleApplication2
{
    class RedditAccess
    {
        private Reddit reddit = new Reddit();
        private List<string> commentIds = new List<string>();
        private ImgurAccess imgur = new ImgurAccess();
        private string commentIdFilePath = "c:/users/joe reynolds/desktop/idlist.txt";
        
        /// <summary>
        /// Logs a user in and then returns True if 
        /// the login was successful
        /// </summary>
        /// <returns>Boolean</returns>
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

        private void AddIdToList(string commentId)
        {
            if (!commentIds.Contains(commentId))
            {
                commentIds.Add(commentId);
            }
        }

        private void WriteIdsToFile()
        {
            using (StreamWriter file = new StreamWriter(commentIdFilePath))
            {
                foreach (string commentId in commentIds)
                {
                    file.Write(" {0}",commentId);
                }
            }
        }

        /// <summary>
        /// Waits until a certain sentence is posted
        /// and then will reply with a message
        /// </summary>
        public void ListenForPrompt(string ResultFileFullPath)
        {
            if (!HasLoggedIn("AsciiThis", "thisisjustapassword"))
            {
                return;
            }

            var subreddit = reddit.GetSubreddit("/r/learnprogramming");

            foreach (var post in subreddit.New.Take(25))
            {
                Console.WriteLine("THREAD : {0}", post.Title);
                string url;
                try
                {
                    foreach (var comment in post.Comments)
                    {
                        if (!commentIds.Contains(comment.Id) && comment.Body.Contains("hello ascii! "))
                        {
                            using (WebClient client = new WebClient())
                            {
                                url = comment.Body.Substring(comment.Body.IndexOf("!")+2);
                                string filename = @"C:/users/joe reynolds/desktop/image.jpg";
                                ImageProcessor im = new ImageProcessor(filename);
                                client.DownloadFile(url,filename);
                                im.DrawAsciiImage(ResultFileFullPath);
                                comment.Reply(imgur.UploadImage("c:/users/joe reynolds/desktop/result.jpg") + System.Environment.NewLine  
                                                                + " ^I ^am ^a ^bot. ^I'm ^still ^being ^tested. ^Im ^very ^unreliable ^at ^^the ^^^moment");
                                AddIdToList(comment.Id);
                                WriteIdsToFile();
                                Thread.Sleep(5000); //Don't spam reddit
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
