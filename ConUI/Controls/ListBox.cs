using OpenTK.Graphics;
using System.Collections.Generic;

namespace ConUI.Controls
{
    public class ListBox : Control
    {
        public List<object> Items { get; } = new List<object>();

        private int _renderYOffset = 0;

        public int RenderYOffset
        {
            get => _renderYOffset;
            set
            {
                int maxY = Items.Count - Size.Height;

                if (maxY < 0)
                    maxY = 0;

                if (value < 0)
                    value = 0;
                else if (value > maxY)
                    value = maxY;

                _renderYOffset = value;
            }
        }

        public ListBox(string name) : base(name)
        {
            MouseWheel += ListBox_MouseWheel;
        }

        public override void Draw(ref ColoredChar[,] buffer)
        {
            if (!Visible)
            {
                ColoredChar cc = new ColoredChar('\0', Parent.ForeColor, Parent.BackColor);
                buffer.Populate(cc);
                return;
            }

            Color4 fForeColor = ForeColor;

            if (!Enabled)
            {
                fForeColor = DisabledForeColor;
            }

            for (int y = 0; y < Size.Height; y++)
            {
                string s = y + RenderYOffset < Items.Count ? Items[y + RenderYOffset].ToString() : null;
                for (int x = 0; x < Size.Width; x++)
                {
                    if (s != null && x < s.Length)
                    {
                        char c = s[x];
                        buffer[x, y] = new ColoredChar(c, fForeColor, BackColor);
                    }
                    else
                    {
                        buffer[x, y] = new ColoredChar('\0', fForeColor, BackColor);
                    }
                }
            }
        }

        private void ListBox_MouseWheel(OpenTK.Input.MouseWheelEventArgs e)
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
