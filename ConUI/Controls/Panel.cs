namespace ConUI.Controls
{
    public class Panel : Control
    {
        public Panel(string name) : base(name) { }

        public override void Draw(ref ColoredChar[,] buffer)
        {
            ColoredChar c = new ColoredChar(' ', ForeColor, BackColor);
            buffer.Populate(c);
        }
    }
}
