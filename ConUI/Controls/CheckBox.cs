using OpenTK.Graphics;
namespace ConUI.Controls
{
    public class CheckBox : Control
    {
        public delegate void CheckedChangedHandler(bool value);

        public event CheckedChangedHandler CheckedChanged;

        private bool _checked = false;
        public bool Checked
        {
            get
            {
                return _checked;
            }
            set
            {
                _checked = value;
                CheckedChanged?.Invoke(value);
            }
        }
        public char CheckedChar { get; set; } = (char)7;

        public Color4 HighlightedBackColor { get; set; } = Color4.White;
        public Color4 HighlightedForeColor { get; set; } = Color4.Black;
        public Color4 ClickingBackColor { get; set; } = Color4.LightGray;
        public Color4 ClickingForeColor { get; set; } = Color4.Black;

        public string Text { get; set; }

        public CheckBox(string name, string text) : base(name)
        {
            Text = text;

            Click += CheckBox_Click;
        }

        private void CheckBox_Click()
        {
            Toggle();
        }

        public void Check()
        {
            Checked = true;
        }
        public void Uncheck()
        {
            Checked = false;
        }
        public void Toggle()
        {
            Checked = !Checked;
        }

        public override ColoredChar[,] Draw(ColoredChar[,] buffer)
        {
            if (!Visible)
            {
                ColoredChar cc = new ColoredChar('\0', Parent.ForeColor, Parent.BackColor);
                buffer.Populate(cc);
                return buffer;
            }

            Color4 fBackColor = BackColor;
            Color4 fForeColor = ForeColor;

            if (Highlighted)
            {
                fBackColor = HighlightedBackColor;
                fForeColor = HighlightedForeColor;
            }

            if (Clicked)
            {
                fBackColor = ClickingBackColor;
                fForeColor = ClickingForeColor;
            }

            if (!Enabled)
            {
                fForeColor = DisabledForeColor;
            }

            string fText = $"({(Checked ? CheckedChar : ' ')}) {Text}";
            int yMiddle = Size.Height / 2;

            for (int y = 0; y < Size.Height; y++)
            {
                for (int x = 0; x < Size.Width; x++)
                {
                    char chr = '\0';

                    if (y == yMiddle && x >= 0 && x < fText.Length)
                    {
                        chr = fText[x];
                    }

                    ColoredChar c = new ColoredChar(chr, fForeColor, fBackColor);
                    buffer[x, y] = c;
                }
            }

            return buffer;
        }
    }
}
