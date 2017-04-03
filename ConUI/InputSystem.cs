namespace ConUI
{
    public class InputSystem : BaseSystem
    {
        public InputSystem(Console parent) : base(parent)
        {
            parent.KeyDown += Parent_KeyDown;
            parent.KeyPress += Parent_KeyPress;
            parent.KeyUp += Parent_KeyUp;

            parent.MouseDown += Parent_MouseDown;
            parent.MouseEnter += Parent_MouseEnter;
            parent.MouseLeave += Parent_MouseLeave;
            parent.MouseMove += Parent_MouseMove;
            parent.MouseUp += Parent_MouseUp;
            parent.MouseWheel += Parent_MouseWheel;
        }

        private void Parent_KeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            Parent.CurrentMenu?.PerformKeyDown(e);
        }
        private void Parent_KeyPress(object sender, OpenTK.KeyPressEventArgs e)
        {
            Parent.CurrentMenu?.PerformKeyPress(e);
        }
        private void Parent_KeyUp(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            Parent.CurrentMenu?.PerformKeyUp(e);
        }

        private void Parent_MouseDown(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            Parent.CurrentMenu?.PerformMouseDown(e);
        }
        private void Parent_MouseEnter(object sender, System.EventArgs e)
        {
            Parent.CurrentMenu?.PerformMouseEnter();
        }
        private void Parent_MouseLeave(object sender, System.EventArgs e)
        {
            Parent.CurrentMenu?.PerformMouseLeave();
        }
        private void Parent_MouseMove(object sender, OpenTK.Input.MouseMoveEventArgs e)
        {
            Parent.CurrentMenu?.PerformMouseMove(e);
        }
        private void Parent_MouseUp(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            Parent.CurrentMenu?.PerformMouseUp(e);
        }
        private void Parent_MouseWheel(object sender, OpenTK.Input.MouseWheelEventArgs e)
        {
            Parent.CurrentMenu?.PerformMouseWheel(e);
        }
    }
}
