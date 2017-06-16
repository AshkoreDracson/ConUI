using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace ConUI.Controls
{
    public class ControlCollection : IEnumerable<Control>
    {
        public Menu Parent { get; private set; }

        private List<Control> _controls = new List<Control>();

        public ControlCollection(Menu parent)
        {
            Parent = parent;
        }

        public Control this[int index] => _controls[index];
        public Control this[string name] => Get<Control>(name);

        public void Add(Control control)
        {
            if (control == null)
                return;

            if (!_controls.Contains(control))
            {
                control.Parent = Parent;
                _controls.Add(control);
            }
        }
        public void AddRange(params Control[] controls)
        {
            if (controls == null || controls.Length <= 0)
                return;

            foreach (Control control in controls)
            {
                if (!_controls.Contains(control))
                {
                    control.Parent = Parent;
                    _controls.Add(control);
                }
            }
        }
        public void Clear()
        {
            foreach (Control control in _controls)
            {
                control.Parent = null;
            }
            _controls.Clear();
        }
        public T Get<T>(string name) where T : Control
        {
            T control = _controls.Find(o => o.Name == name) as T;
            return control;
        }
        public Control GetAtPosition(int x, int y)
        {
            var orderedControls = _controls.OrderByDescending(o => o.Layer);
            foreach (Control control in orderedControls)
            {
                Point pos = control.Position;
                Size size = control.Size;

                if (x >= pos.X && x < pos.X + size.Width && y >= pos.Y && y < pos.Y + size.Height)
                    return control;
            }
            return null;
        }
        public void Remove(Control control)
        {
            if (control == null)
                return;

            control.Parent = null;
            _controls.Remove(control);
        }

        public IEnumerator<Control> GetEnumerator()
        {
            return ((IEnumerable<Control>)_controls).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Control>)_controls).GetEnumerator();
        }
    }
}
