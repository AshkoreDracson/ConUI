using ConUI;
using RedditSharp;

namespace ConReddit
{
    public class Program
    {
        public static Console Console { get; set; }
        public static Reddit Reddit { get; set; }
        public static SubredditMenu SubredditMenu { get; set; }

        static void Main(string[] args)
        {
            Initialize();
        }

        static void Initialize()
        {
            Console = new Console(30, 80, "ConReddit");
            Reddit = new Reddit();

            SubredditMenu = new SubredditMenu("ConReddit");

            Console.StartingMenu = SubredditMenu;
            Console.Show();
        }
    }
}
