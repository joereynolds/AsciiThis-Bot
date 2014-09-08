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
        
            while (true)
            {
             reddit.ListenForPrompt();
            }
        }
    }
}
