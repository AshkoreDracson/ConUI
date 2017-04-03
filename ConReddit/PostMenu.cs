using ConUI;
using RedditSharp.Things;
namespace ConReddit
{
    public partial class PostMenu : Menu
    {
        private int _postIndex = -1;

        public Post Post { get; private set; }
        public int PostIndex
        {
            get
            {
                return _postIndex;
            }
            set
            {
                if (value < -1)
                    value = -1;
                else if (value >= Post.CommentCount)
                    value = Post.CommentCount - 1;

                _postIndex = value;

                if (value == -1)
                {
                    authorLabel.Text = Post.AuthorName;
                    bodyLabel.Text = Post.SelfText;
                }
                else
                {
                    try
                    {
                        Comment c = Post.Comments[value];
                        authorLabel.Text = $"[{c.Score}] {c.Author}";
                        bodyLabel.Text = c.Body;
                    }
                    catch (System.Exception)
                    {
                        _postIndex--;
                    }
                }
            }
        }

        public PostMenu(string title, Post post) : base(title)
        {
            Post = post;

            InitializeComponents();
            PostIndex = -1;
        }

        private void BackBTN_Click()
        {
            Program.Console.HideMenu();
        }
        private void PrevBTN_Click()
        {
            PostIndex--;
        }
        private void NextBTN_Click()
        {
            PostIndex++;
        }
    }
}
