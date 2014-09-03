using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using RedditSharp;

namespace ConsoleApplication2
{
    class RedditAccess
    {

        private Reddit reddit = new Reddit();
        private List<string> commentIds = new List<string>();

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


        /// <summary>
        /// Waits until a certain sentence is posted
        /// and then will reply with a message
        /// </summary>
        /// 
        public void ListenForPrompt()
        {
            if (!HasLoggedIn("AsciiThis", "ppp"))
            {
                return;
            }

            var subreddit = reddit.GetSubreddit("/r/test");

            foreach (var post in subreddit.New.Take(25))
            {
                Console.WriteLine("==========================");
                Console.WriteLine("THREAD NAME: {0}",post.Title);

                try
                {
                    foreach (var comment in post.Comments)
                    {
                        Console.WriteLine("comment body: {0}", comment.Body);
                        if (!commentIds.Contains(comment.Id) && comment.Body.Contains("hello ascii!"))
                        {
                            commentIds.Add(comment.Id);
                            comment.Reply("Hello!");
                            Console.WriteLine("    Replied to message");
                            Thread.Sleep(600000);
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
