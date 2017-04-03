using OpenTK.Graphics;
namespace ConUI
{
    public struct ColoredChar
    {
        public char Char;
        public Color4 BackColor;
        public Color4 ForeColor;

        public ColoredChar(char chr)
        {
            Char = chr;
            BackColor = Color4.Black;
            ForeColor = Color4.Black;
        }
        public ColoredChar(char chr, Color4 foreColor, Color4 backColor)
        {
            Char = chr;
            BackColor = backColor;
            ForeColor = foreColor;
        }
    }
}
