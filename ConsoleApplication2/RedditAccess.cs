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
        void ListenForPrompt()
        {
            var reddit = new RedditSharp.Reddit;
            var user = reddit.LogIn("AsciiThis","password");
            var subreddit = reddit.GetSubreddit("/r/test");

            foreach (var post in subreddit.New.Take(25))
            {
                if (post.SelfText.Contains("hello ascii!"))
                {
                    var comment = post.Comment("Hello!");
                }

            }

        }
    }
}
