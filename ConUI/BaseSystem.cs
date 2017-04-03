namespace ConUI
{
    public abstract class BaseSystem
    {
        public Console Parent { get; protected set; }

        public BaseSystem(Console parent)
        {
            Parent = parent;
        }
    }
}
