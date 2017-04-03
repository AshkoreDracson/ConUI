using ConUI;
using ConUI.Controls;
using OpenTK.Graphics;
using RedditSharp;
using RedditSharp.Things;
using System.Linq;
using System.Threading;
namespace ConReddit
{
    public partial class SubredditMenu : Menu
    {
        private SubredditFilter _filter = SubredditFilter.None;
        private Listing<Post> _posts = null;
        private Post[] _currentPosts = null;
        private int _page = -1;
        private Subreddit _subreddit = null;

        private Thread fetchThread;

        public Color4 ActiveButtonColor { get; set; } = Color4.DarkBlue;
        public Color4 InactiveButtonColor { get; set; } = Color4.Black;

        public SubredditFilter Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                _filter = value;

                switch (_filter)
                {
                    case SubredditFilter.None:
                        SetActiveButton(null);
                        _posts = null;
                        if (!Searching)
                            return;
                        break;
                    case SubredditFilter.Hot:
                        SetActiveButton(hotBTN);
                        _posts = Subreddit.Hot;
                        break;
                    case SubredditFilter.New:
                        SetActiveButton(newBTN);
                        _posts = Subreddit.New;
                        break;
                    case SubredditFilter.Controversial:
                        SetActiveButton(controversialBTN);
                        _posts = Subreddit.Controversial;
                        break;
                    case SubredditFilter.Best:
                        SetActiveButton(bestBTN);
                        _posts = Subreddit.GetTop(FromTime.All);
                        break;
                    default:
                        break;
                }

                if (Searching)
                {
                    _posts = Subreddit.Search(SearchText);
                }

                Page = 0;
            }
        }
        public int Page
        {
            get
            {
                return _page;
            }
            set
            {
                if (value < 0)
                    value = 0;

                _page = value;

                if (_page >= 0)
                {
                    pageLabel.Text = $"Page {_page + 1}";
                }
                else
                {
                    pageLabel.Text = "Page ...";
                }

                fetchThread?.Abort();
                fetchThread = new Thread(new ThreadStart(Fetch));
                fetchThread.Start();
            }
        }
        public int PostsPerPage { get; set; } = 6;
        public bool Searching { get; set; }
        public string SearchText { get; set; } = string.Empty;
        public Subreddit Subreddit
        {
            get
            {
                return _subreddit;
            }
            set
            {
                _subreddit = value;

                Searching = false;

                if (_subreddit != null)
                {
                    SetButtonsEnabled(true);

                    Filter = SubredditFilter.Hot;
                    Page = 0;
                }
                else
                {
                    SetButtonsEnabled(false);

                    Filter = SubredditFilter.None;
                    Page = -1;
                }
            }
        }

        public SubredditMenu(string title) : base(title)
        {
            InitializeComponents();
        }

        void Fetch()
        {
            if (_posts == null)
                return;

            foreach (Button button in postLinkBTN)
            {
                button.Enabled = false;
            }
            foreach (Label label in postLabel)
            {
                label.Text = "...";
            }

            int i = 0;
            _currentPosts = _posts.Skip(Page * PostsPerPage).Take(PostsPerPage).ToArray();
            foreach (Post p in _currentPosts)
            {
                postLabel[i].Text = $"{i + 1}) [{p.Score}] By {p.AuthorName} - {p.Title}";
                i++;
            }

            foreach (Button button in postLinkBTN)
            {
                button.Enabled = true;
            }
        }

        void ShowPost(int index)
        {
            Post p = _currentPosts[index];

            if (p != null)
            {
                Program.Console.ShowMenu(new PostMenu("Post", p));
            }
        }

        void SetButtonsEnabled(bool b)
        {
            hotBTN.Enabled = b;
            newBTN.Enabled = b;
            controversialBTN.Enabled = b;
            bestBTN.Enabled = b;

            searchBTN.Enabled = b;

            prevBTN.Enabled = b;
            nextBTN.Enabled = b;

            foreach (Button button in postLinkBTN)
            {
                button.Enabled = b;
            }
        }
        void SetActiveButton(Button b)
        {
            hotBTN.BackColor = (hotBTN == b ? ActiveButtonColor : InactiveButtonColor);
            newBTN.BackColor = (newBTN == b ? ActiveButtonColor : InactiveButtonColor);
            controversialBTN.BackColor = (controversialBTN == b ? ActiveButtonColor : InactiveButtonColor);
            bestBTN.BackColor = (bestBTN == b ? ActiveButtonColor : InactiveButtonColor);
        }

        private void GoBTN_Click()
        {
            Subreddit = Program.Reddit.GetSubreddit(subredditTB.Text);
        }

        private void SubredditTB_KeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
        {
            if (e.Key == OpenTK.Input.Key.Enter)
            {
                goBTN.PerformClick();
            }
        }

        private void HotBTN_Click()
        {
            Searching = false;
            Filter = SubredditFilter.Hot;
        }
        private void NewBTN_Click()
        {
            Searching = false;
            Filter = SubredditFilter.New;
        }
        private void ControversialBTN_Click()
        {
            Searching = false;
            Filter = SubredditFilter.Controversial;
        }
        private void BestBTN_Click()
        {
            Searching = false;
            Filter = SubredditFilter.Best;
        }

        private void SearchBTN_Click()
        {
            Searching = true;
            SearchText = searchTB.Text;
            Filter = SubredditFilter.None;
        }
        private void SearchTB_KeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
        {
            if (e.Key == OpenTK.Input.Key.Enter)
            {
                searchBTN.PerformClick();
            }
        }

        private void PrevBTN_Click()
        {
            Page--;
        }
        private void NextBTN_Click()
        {
            Page++;
        }

        private void PostLinkBTN0_Click()
        {
            ShowPost(0);
        }
        private void PostLinkBTN1_Click()
        {
            ShowPost(1);
        }
        private void PostLinkBTN2_Click()
        {
            ShowPost(2);
        }
        private void PostLinkBTN3_Click()
        {
            ShowPost(3);
        }
        private void PostLinkBTN4_Click()
        {
            ShowPost(4);
        }
        private void PostLinkBTN5_Click()
        {
            ShowPost(5);
        }
    }
}
