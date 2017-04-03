using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
namespace ConUI
{
    using Controls;

    public class Menu
    {
        public delegate void KeyDownHandler(KeyboardKeyEventArgs e);
        public delegate void KeyPressHandler(KeyPressEventArgs e);
        public delegate void KeyUpHandler(KeyboardKeyEventArgs e);
        public delegate void MouseDownHandler(MouseButtonEventArgs e);
        public delegate void MouseEnterHandler();
        public delegate void MouseLeaveHandler();
        public delegate void MouseMoveHandler(MouseMoveEventArgs e);
        public delegate void MouseUpHandler(MouseButtonEventArgs e);
        public delegate void MouseWheelHandler(MouseWheelEventArgs e);

        public event KeyDownHandler KeyDown;
        public event KeyPressHandler KeyPress;
        public event KeyUpHandler KeyUp;
        public event MouseDownHandler MouseDown;
        public event MouseEnterHandler MouseEnter;
        public event MouseLeaveHandler MouseLeave;
        public event MouseMoveHandler MouseMove;
        public event MouseUpHandler MouseUp;
        public event MouseWheelHandler MouseWheel;

        public Color4 BackColor { get; set; } = Color4.Black;
        public ControlCollection Controls { get; private set; }
        public Control FocusedControl { get; set; }
        public Color4 ForeColor { get; set; } = Color4.LightGray;
        public string Title { get; set; }

        private Control highlightedControl = null;

        public Menu(string title)
        {
            Controls = new ControlCollection(this);
            Title = title;
        }

        public void PerformKeyDown(KeyboardKeyEventArgs e)
        {
            KeyDown?.Invoke(e);
            FocusedControl?.PerformKeyDown(e);
        }
        public void PerformKeyPress(KeyPressEventArgs e)
        {
            KeyPress?.Invoke(e);
            FocusedControl?.PerformKeyPress(e);
        }
        public void PerformKeyUp(KeyboardKeyEventArgs e)
        {
            KeyUp?.Invoke(e);
            FocusedControl?.PerformKeyUp(e);
        }

        public void PerformMouseDown(MouseButtonEventArgs e)
        {
            MouseDown?.Invoke(e);

            MouseButtonEventArgs e2 = new MouseButtonEventArgs(e.X / 8, e.Y / 12, e.Button, e.IsPressed);
            Control topMostControl = Controls.GetAtPosition(e2.X, e2.Y);
            topMostControl?.PerformMouseDown(e2);
        }
        public void PerformMouseEnter()
        {
            MouseEnter?.Invoke();
        }
        public void PerformMouseLeave()
        {
            MouseLeave?.Invoke();
        }
        public void PerformMouseMove(MouseMoveEventArgs e)
        {
            MouseMove?.Invoke(e);

            Control curHighlightedControl = Controls.GetAtPosition(e.X / 8, e.Y / 12);
            if (curHighlightedControl == null)
            {
                highlightedControl?.PerformMouseLeave();
                highlightedControl = curHighlightedControl;
                return;
            }

            MouseMoveEventArgs e2 = new MouseMoveEventArgs(e.X - (curHighlightedControl.Position.X * 8), e.Y - (curHighlightedControl.Position.Y * 12), e.XDelta, e.YDelta);

            if (highlightedControl != curHighlightedControl)
            {
                highlightedControl?.PerformMouseLeave();
                curHighlightedControl?.PerformMouseEnter();
            }

            highlightedControl = curHighlightedControl;
        }
        public void PerformMouseUp(MouseButtonEventArgs e)
        {
            MouseUp?.Invoke(e);

            Control topMostControl = Controls.GetAtPosition(e.X / 8, e.Y / 12);
            if (topMostControl == null) return;

            MouseButtonEventArgs e2 = new MouseButtonEventArgs(e.X - (topMostControl.Position.X * 8), e.Y - (topMostControl.Position.Y * 12), e.Button, e.IsPressed);

            if (topMostControl != null)
            {
                if (FocusedControl != null)
                    FocusedControl.Focused = false;

                FocusedControl = topMostControl;
                topMostControl.Focused = true;
                topMostControl.PerformMouseUp(e2);
            }
        }
        public void PerformMouseWheel(MouseWheelEventArgs e)
        {
            MouseWheel?.Invoke(e);

            if (FocusedControl != null)
            {
                MouseWheelEventArgs e2 = new MouseWheelEventArgs(e.X - (FocusedControl.Position.X * 8), e.Y - (FocusedControl.Position.Y * 12), e.Value, e.Delta);
                FocusedControl.PerformMouseWheel(e2);
            }
        }
    }
}
