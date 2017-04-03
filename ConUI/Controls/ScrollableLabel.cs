using OpenTK.Graphics;

namespace ConUI.Controls
{
    public class ScrollableLabel : Control
    {
        private int _renderYOffset = 0;

        public int RenderYOffset
        {
            get
            {
                return _renderYOffset;
            }
            set
            {
                int maxY = RealHeight - Size.Height;

                if (maxY < 0)
                    maxY = 0;

                if (value < 0)
                    value = 0;
                else if (value > maxY)
                    value = maxY;

                _renderYOffset = value;
            }
        }
        public int RealHeight
        {
            get
            {
                if (Text.Contains("\n"))
                {

                    string[] lines = Text.Split('\n');
                    int height = lines.Length;

                    for (int i = 0; i < lines.Length; i++)
                    {
                        int increment = (lines[i].Length / (Size.Width + 1));
                        height += increment;
                    }

                    return height;
                }
                else
                {
                    double height = (double)Text.Length / Size.Width;
                    return (int)System.Math.Ceiling(height);
                }
            }
        }
        public string Text { get; set; }

        public ScrollableLabel(string name, string text) : base(name)
        {
            Text = text;

            MouseWheel += ScrollableLabel_MouseWheel;
        }

        public override ColoredChar[,] Draw(ColoredChar[,] buffer)
        {
            if (!Visible)
            {
                ColoredChar cc = new ColoredChar('\0', Parent.ForeColor, Parent.BackColor);
                buffer.Populate(cc);
                return buffer;
            }

            string t = Text;
            if (t.Length <= 0)
                return buffer;

            int i = RenderYOffset * Size.Width;

            Color4 fForeColor = ForeColor;

            if (!Enabled)
            {
                fForeColor = DisabledForeColor;
            }

            for (int y = 0; y < Size.Height; y++)
            {
                for (int x = 0; x < Size.Width; x++)
                {
                    if (i < Text.Length)
                    {
                        char c = Text[i++];

                        if (c == '\n')
                        {
                            break;
                        }

                        buffer[x, y] = new ColoredChar(c, fForeColor, BackColor);
                    }
                    else
                    {
                        buffer[x, y] = new ColoredChar('\0', fForeColor, BackColor);
                    }
                }
            }

            return buffer;
        }

        private void ScrollableLabel_MouseWheel(OpenTK.Input.MouseWheelEventArgs e)
        {
            if (e.Delta < 0)
            {
                RenderYOffset++;
            }
            else if (e.Delta > 0)
            {
                RenderYOffset--;
            }
        }
    }
}
