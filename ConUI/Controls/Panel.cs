namespace ConUI.Controls
{
    public class Panel : Control
    {
        public Panel(string name) : base(name) { }

        public override void Draw(ref ColoredChar[,] buffer)
        {
            ColoredChar c = new ColoredChar('\0', ForeColor, BackColor);
            buffer.Populate(c);
        }
    }
}
