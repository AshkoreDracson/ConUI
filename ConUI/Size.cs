namespace ConUI
{
    public struct Size
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"[W: {Width}, H: {Height}]";
        }
    }
}
