using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;

namespace ConUI.Controls
{
    public abstract class Control
    {
        public delegate void ClickHandler();
        public delegate void KeyDownHandler(KeyboardKeyEventArgs e);
        public delegate void KeyPressHandler(KeyPressEventArgs e);
        public delegate void KeyUpHandler(KeyboardKeyEventArgs e);
        public delegate void MouseDownHandler(MouseButtonEventArgs e);
        public delegate void MouseEnterHandler();
        public delegate void MouseLeaveHandler();
        public delegate void MouseMoveHandler(MouseMoveEventArgs e);
        public delegate void MouseUpHandler(MouseButtonEventArgs e);
        public delegate void MouseWheelHandler(MouseWheelEventArgs e);

        public event ClickHandler Click;
        public event KeyDownHandler KeyDown;
        public event KeyPressHandler KeyPress;
        public event KeyUpHandler KeyUp;
        public event MouseDownHandler MouseDown;
        public event MouseEnterHandler MouseEnter;
        public event MouseLeaveHandler MouseLeave;
        public event MouseMoveHandler MouseMove;
        public event MouseUpHandler MouseUp;
        public event MouseWheelHandler MouseWheel;

        private Color4 _backColor;
        private Color4 _foreColor;

        public Color4 BackColor
        {
            get => _backColor;
            set
            {
                _backColor = value;
                DefaultColors = false;
            }
        }
        public Color4 ForeColor
        {
            get => _foreColor;
            set
            {
                _foreColor = value;
                DefaultColors = false;
            }
        }

        public Color4 DisabledForeColor { get; set; } = Color4.Gray;

        public bool DefaultColors { get; set; } = true;

        private Menu _parent;
        public Menu Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;

                if (DefaultColors)
                {
                    _backColor = Parent.BackColor;
                    _foreColor = Parent.ForeColor;
                }
            }
        }
        public int Layer { get; set; }

        private bool _focused = false;
        public bool Focused
        {
            get
            {
                return _focused;
            }
            set
            {
                _focused = value;
                Parent.FocusedControl = this;
            }
        }
        public bool Clicked { get; private set; }
        private bool _enabled = true;
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;

                if (!_enabled)
                {
                    Clicked = false;
                    Highlighted = false;
                }
            }
        }
        public bool Highlighted { get; private set; }
        public bool Interactable => Enabled && Visible;

        public string Name { get; set; }

        public Point Position { get; set; }
        public Size Size { get; set; }
        private bool _visible = true;
        public bool Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;

                if (!_visible)
                {
                    Clicked = false;
                    Highlighted = false;
                }
            }
        }

        public Control(string name)
        {
            Name = name;
        }

        public abstract void Draw(ref ColoredChar[,] buffer);

        public void Focus()
        {
            Focused = true;
        }

        public void PerformClick()
        {
            if (Interactable)
                Click?.Invoke();
        }
        public void PerformKeyDown(KeyboardKeyEventArgs e)
        {
            if (Interactable)
                KeyDown?.Invoke(e);
        }
        public void PerformKeyPress(KeyPressEventArgs e)
        {
            if (Interactable)
                KeyPress?.Invoke(e);
        }
        public void PerformKeyUp(KeyboardKeyEventArgs e)
        {
            if (Interactable)
                KeyUp?.Invoke(e);
        }

        public void PerformMouseDown(MouseButtonEventArgs e)
        {
            if (Interactable)
            {
                Clicked = true;
                MouseDown?.Invoke(e);
            }
        }
        public void PerformMouseEnter()
        {
            if (Interactable)
            {
                Highlighted = true;
                MouseEnter?.Invoke();
            }
        }
        public void PerformMouseLeave()
        {
            if (Interactable)
            {
                Clicked = false;
                Highlighted = false;
                MouseLeave?.Invoke();
            }
        }
        public void PerformMouseMove(MouseMoveEventArgs e)
        {
            if (Interactable)
            {
                MouseMove?.Invoke(e);
            }
        }
        public void PerformMouseUp(MouseButtonEventArgs e)
        {
            if (Interactable)
            {
                if (Clicked)
                    Click?.Invoke();

                Clicked = false;
                MouseUp?.Invoke(e);
            }

        }
        public void PerformMouseWheel(MouseWheelEventArgs e)
        {
            if (Interactable)
            {
                MouseWheel?.Invoke(e);
            }
        }
    }
}
