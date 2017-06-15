using OpenTK.Graphics;
namespace ConUI.Controls
{
    public class Button : Control
    {
        public Color4 HighlightedBackColor { get; set; } = Color4.White;
        public Color4 HighlightedForeColor { get; set; } = Color4.Black;
        public Color4 ClickingBackColor { get; set; } = Color4.LightGray;
        public Color4 ClickingForeColor { get; set; } = Color4.Black;

        public string Text { get; set; }
        public TextAlignement TextAlign { get; set; } = TextAlignement.Middle;

        public Button(string name, string text) : base(name)
        {
            Text = text;
        }

        public override void Draw(ref ColoredChar[,] buffer)
        {
            if (!Visible)
            {
                ColoredChar cc = new ColoredChar('\0', Parent.ForeColor, Parent.BackColor);
                buffer.Populate(cc);
                return;
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

            int yMiddle = Size.Height / 2;
            for (int y = 0; y < Size.Height; y++)
            {
                for (int x = 0; x < Size.Width; x++)
                {
                    char chr = '\0';

                    if (y == yMiddle)
                    {
                        var index = 0;
                        switch (TextAlign)
                        {
                            case TextAlignement.Left:
                                index = x;
                                chr = (index >= 0 && index < Text.Length ? Text[index] : '\0');
                                break;
                            case TextAlignement.Middle:
                                index = x - Size.Width / 2 + Text.Length / 2;
                                chr = (index >= 0 && index < Text.Length ? Text[index] : '\0');
                                break;
                            case TextAlignement.Right:
                                index = x - (Size.Width - Text.Length);
                                chr = (index >= 0 && index < Text.Length ? Text[index] : '\0');
                                break;
                            default:
                                break;
                        }
                    }

                    ColoredChar c = new ColoredChar(chr, fForeColor, fBackColor);
                    buffer[x, y] = c;
                }
            }
        }
    }
}
