using OpenTK.Graphics;
using System.Linq;
namespace ConUI
{
    using Controls;

    public class RenderSystem : BaseSystem
    {
        private ColoredChar[,] Buffer;

        public RenderSystem(Console parent) : base(parent)
        {
            Buffer = new ColoredChar[parent.Cols, parent.Rows];
        }

        public void Clear()
        {
            Color4 backColor = Parent.CurrentMenu?.BackColor ?? default(Color4);
            Color4 foreColor = Parent.CurrentMenu?.ForeColor ?? default(Color4);
            ColoredChar cc = new ColoredChar('\0', foreColor, backColor);

            Buffer.Populate(cc);
        }
        public void Render()
        {
            Parent.HoldUpdates();

            Clear();
            var orderedControls = Parent.CurrentMenu?.Controls.OrderBy(o => o.Layer);

            if (orderedControls != null)
            {
                foreach (Control control in orderedControls)
                {
                    Point pos = control.Position;
                    Size size = control.Size;
                    ColoredChar[,] controlBuffer = new ColoredChar[size.Width, size.Height];
                    control.Draw(ref controlBuffer);

                    for (int y = 0; y < size.Height; y++)
                    {
                        for (int x = 0; x < size.Width; x++)
                        {
                            int fX = x + pos.X;
                            int fY = y + pos.Y;
                            if (fX < 0 || fX >= Parent.Cols || fY < 0 || fY >= Parent.Rows)
                                continue;

                            Buffer[fX, fY] = controlBuffer[x, y];
                        }
                    }
                }
            }

            for (int y = 0; y < Buffer.GetLength(1); y++)
            {
                for (int x = 0; x < Buffer.GetLength(0); x++)
                {
                    ColoredChar cc = Buffer[x, y];
                    Parent.Write(y, x, cc.Char, cc.ForeColor, cc.BackColor);
                }
            }

            Parent.ResumeUpdates();
        }
    }
}
