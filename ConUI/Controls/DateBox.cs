using OpenTK.Graphics;
using OpenTK.Input;
using System;
using System.Linq;

namespace ConUI.Controls
{
    public class DateBox : Control
    {
        private static char[] validChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '/', '.', ':' };

        public Color4 CaretBackColor { get; set; } = Color4.White;
        public Color4 CaretForeColor { get; set; } = Color4.Black;
        private int _caretPosition;
        public int CaretPosition
        {
            get
            {
                return _caretPosition;
            }
            set
            {
                if (value < 0)
                    value = 0;
                else if (value > Text.Length)
                    value = Text.Length;

                _caretPosition = value;

                if (_caretPosition > renderOffset + TextRenderLength - 1)
                {
                    renderOffset = _caretPosition - TextRenderLength + 1;
                }
                else if (_caretPosition < renderOffset + 1)
                {
                    renderOffset = _caretPosition - 1;
                }

                if (renderOffset < 0)
                    renderOffset = 0;
                else if (renderOffset >= Text.Length)
                    renderOffset = Text.Length - 1;
            }
        }
        public int MaxLength { get; set; } = 32767;
        public string Placeholder { get; set; } = string.Empty;
        public Color4 PlaceholderBackColor { get; set; }
        public Color4 PlaceholderForeColor { get; set; }
        public string Text { get; set; } = string.Empty;

        private int TextRenderLength => Size.Width - 2;
        private int renderOffset = 0;

        public DateTime? Date
        {
            get
            {
                if (DateTime.TryParse(Text, out DateTime date))
                    return date;

                return null;
            }
        }

        public DateBox(string name) : base(name)
        {
            KeyDown += DateBox_KeyDown;
            KeyPress += DateBox_KeyPress;
        }

        private void DateBox_KeyPress(OpenTK.KeyPressEventArgs e)
        {
            if (Text.Length >= MaxLength || !validChars.Contains(e.KeyChar)) return;
            Text = Text.Insert(CaretPosition, e.KeyChar.ToString());
            CaretPosition++;
        }

        private void DateBox_KeyDown(KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.BackSpace)
            {
                if (Text.Length > 0 && CaretPosition > 0)
                {
                    Text = Text.Remove(CaretPosition - 1, 1);
                    CaretPosition--;
                }
            }
            else if (e.Key == Key.Delete)
            {
                if (CaretPosition < Text.Length)
                    Text = Text.Remove(CaretPosition, 1);
            }
            else if (e.Key == Key.Left)
            {
                CaretPosition--;
            }
            else if (e.Key == Key.Right)
            {
                CaretPosition++;
            }
        }

        public override void Draw(ref ColoredChar[,] buffer)
        {
            if (!Visible)
            {
                ColoredChar cc = new ColoredChar('\0', Parent.ForeColor, Parent.BackColor);
                buffer.Populate(cc);
                return;
            }

            bool showPlaceholder = (!Focused && Placeholder.Length > 0 && Text.Length <= 0);

            for (int y = 0; y < Size.Height; y++)
            {
                for (int x = 0; x < Size.Width; x++)
                {
                    char chr = '\0';
                    int index = (x - 1) + renderOffset;
                    bool showCaret = (CaretPosition == index && Focused);

                    Color4 fForeColor = ForeColor;
                    Color4 fBackColor = BackColor;

                    if (x == 0)
                    {
                        chr = '[';
                    }
                    else if (x == Size.Width - 1)
                    {
                        chr = ']';
                    }
                    else if (index >= 0 && index < Text.Length)
                    {
                        chr = Text[index];
                    }
                    else if (showPlaceholder && index >= 0 && index < Placeholder.Length)
                    {
                        chr = Placeholder[index];
                    }

                    if (x != 0 && x != Size.Width - 1)
                    {
                        fForeColor = (showCaret ? CaretForeColor : ForeColor);
                        fBackColor = (showCaret ? CaretBackColor : BackColor);

                        fForeColor = (showPlaceholder ? PlaceholderForeColor : fForeColor);
                        fBackColor = (showPlaceholder ? PlaceholderBackColor : fBackColor);
                    }

                    buffer[x, y] = new ColoredChar(chr, fForeColor, fBackColor);
                }
            }
        }
    }
}
