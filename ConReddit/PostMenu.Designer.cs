using ConUI;
using ConUI.Controls;
namespace ConReddit
{
    public partial class PostMenu : Menu
    {
        public Button backBTN, prevBTN, nextBTN;
        public Label titleLabel, authorLabel;
        public ScrollableLabel bodyLabel;

        public void InitializeComponents()
        {
            backBTN = new Button("backBTN", "<- Back") { Size = new Size(9, 1) };

            titleLabel = new Label("titleLabel", $"[{Post.Score}] {Post.Title}") { Position = new Point(9, 0), Size = new Size(71, 1) };
            authorLabel = new Label("authorLabel", "???") { Position = new Point(0, 2), Size = new Size(80, 1) };
            bodyLabel = new ScrollableLabel("bodyLabel", "...") { Position = new Point(0, 4), Size = new Size(80, 23) };

            prevBTN = new Button("prevBTN", "<-") { Position = new Point(0, 27), Size = new Size(40, 3) };
            nextBTN = new Button("nextBTN", "->") { Position = new Point(40, 27), Size = new Size(40, 3) };

            // Event registration
            backBTN.Click += BackBTN_Click;

            prevBTN.Click += PrevBTN_Click;
            nextBTN.Click += NextBTN_Click;

            Controls.AddRange(backBTN, titleLabel, authorLabel, bodyLabel, prevBTN, nextBTN);
        }
    }
}
