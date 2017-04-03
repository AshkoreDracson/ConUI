using ConUI;
using ConUI.Controls;
namespace ConReddit
{
    // Declare controls
    public partial class SubredditMenu : Menu
    {
        public TextBox subredditTB, searchTB;
        public Button goBTN,
            hotBTN, newBTN, controversialBTN, bestBTN,
            searchBTN,
            nextBTN, prevBTN;
        public Label sep1, sep2, sep3, pageLabel;

        public Label[] postLabel;
        public Button[] postLinkBTN;

        public void InitializeComponents()
        {
            // Control creation
            subredditTB = new TextBox("subredditTB") { Position = new Point(0, 0), Size = new Size(75, 1), Placeholder = "Subreddit name..." };
            goBTN = new Button("goBTN", ">>>") { Position = new Point(75, 0), Size = new Size(5, 1) };

            sep1 = new Label("sep1", new string('-', 80)) { Position = new Point(0, 1), Size = new Size(80, 1) };

            hotBTN = new Button("hotBTN", "Hot") { Position = new Point(0, 2), Size = new Size(20, 1), Enabled = false };
            newBTN = new Button("newBTN", "New") { Position = new Point(20, 2), Size = new Size(20, 1), Enabled = false };
            controversialBTN = new Button("controversialBTN", "Controversial") { Position = new Point(40, 2), Size = new Size(20, 1), Enabled = false };
            bestBTN = new Button("bestBTN", "Best") { Position = new Point(60, 2), Size = new Size(20, 1), Enabled = false };

            sep2 = new Label("sep2", new string('-', 80)) { Position = new Point(0, 3), Size = new Size(80, 1) };

            searchTB = new TextBox("searchTB") { Position = new Point(0, 4), Size = new Size(70, 1), Placeholder = "Search..." };
            searchBTN = new Button("searchBTN", "Search") { Position = new Point(70, 4), Size = new Size(10, 1), Enabled = false };

            sep3 = new Label("sep3", new string('=', 80)) { Position = new Point(0, 5), Size = new Size(80, 1) };

            pageLabel = new Label("pageLabel", "Page ...") { Position = new Point(0, 6), Size = new Size(72, 1) };
            prevBTN = new Button("prevBTN", "<-") { Position = new Point(72, 6), Size = new Size(4, 1), Enabled = false };
            nextBTN = new Button("nextBTN", "->") { Position = new Point(76, 6), Size = new Size(4, 1), Enabled = false };

            postLabel = new Label[PostsPerPage];
            postLinkBTN = new Button[PostsPerPage];

            for (int i = 0; i < PostsPerPage; i++)
            {
                postLabel[i] = new Label($"postLabel{i}", "...") { Position = new Point(0, 7 + (4 * i)), Size = new Size(75, 3) };
                postLinkBTN[i] = new Button($"postLinkBTN{i}", ">") { Position = new Point(75, 7 + (4 * i)), Size = new Size(5, 3), Enabled = false };
            }

            // Event registration
            subredditTB.KeyDown += SubredditTB_KeyDown;
            goBTN.Click += GoBTN_Click;

            hotBTN.Click += HotBTN_Click;
            newBTN.Click += NewBTN_Click;
            controversialBTN.Click += ControversialBTN_Click;
            bestBTN.Click += BestBTN_Click;

            searchBTN.Click += SearchBTN_Click;
            searchTB.KeyDown += SearchTB_KeyDown;

            prevBTN.Click += PrevBTN_Click;
            nextBTN.Click += NextBTN_Click;

            postLinkBTN[0].Click += PostLinkBTN0_Click;
            postLinkBTN[1].Click += PostLinkBTN1_Click;
            postLinkBTN[2].Click += PostLinkBTN2_Click;
            postLinkBTN[3].Click += PostLinkBTN3_Click;
            postLinkBTN[4].Click += PostLinkBTN4_Click;
            postLinkBTN[5].Click += PostLinkBTN5_Click;

            // Register all controls
            Controls.AddRange(subredditTB, goBTN, sep1, hotBTN, newBTN, controversialBTN, bestBTN, sep2, searchTB, searchBTN, sep3, pageLabel, prevBTN, nextBTN);
            Controls.AddRange(postLabel);
            Controls.AddRange(postLinkBTN);
        }
    }
}
