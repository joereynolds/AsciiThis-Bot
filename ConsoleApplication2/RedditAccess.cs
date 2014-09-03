using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class RedditAccess
    {
        /// <summary>
        /// Waits until a certain sentence is posted
        /// and then will reply with a message
        /// </summary>
        public void ListenForPrompt()
        {
            var reddit = new RedditSharp.Reddit();

            try
            {
                var user = reddit.LogIn("AsciiThis", "thisisjustapassword");
                Console.WriteLine("Logged in successfully");
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var subreddit = reddit.GetSubreddit("/r/test");

            foreach (var post in subreddit.New.Take(25))
            {
                try
                {
                    foreach (var comment in post.Comments)
                    {
                        if (comment.Body.Contains("hello ascii!"))
                        {
                            comment.Reply("Hello!");
                            Console.WriteLine("Replied to message");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("No more comments to reply to"); //I think that's what the exception means...(???)
                }
            }
        }
    }
}
