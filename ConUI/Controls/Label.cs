using OpenTK.Graphics;

namespace ConUI.Controls
{
    public class Label : Control
    {
        public string Text { get; set; }

        public Label(string name, string text) : base(name)
        {
            Text = text;
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

            int i = 0;

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
    }
}
