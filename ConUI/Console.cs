using SunshineConsole;
using System.Collections.Generic;

namespace ConUI
{
    public class Console : ConsoleWindow
    {
        public delegate void OnShownHandler();
        public delegate void OnHideHandler();

        public event OnShownHandler OnShown;
        public event OnHideHandler OnHide;

        public Menu CurrentMenu
        {
            get
            {
                if (ShownMenus.Count <= 0)
                    return null;

                return ShownMenus.Peek();
            }
        }
        public InputSystem InputSystem { get; private set; }
        public RenderSystem RenderSystem { get; private set; }
        public Menu StartingMenu { get; set; }
        public bool Shown { get; private set; }

        private Stack<Menu> ShownMenus = new Stack<Menu>();

        public Console(int rows, int columns, string title) : base(rows, columns, title)
        {
            Initialize();
        }

        void Initialize()
        {
            InputSystem = new InputSystem(this);
            RenderSystem = new RenderSystem(this);
            Visible = Shown;
        }

        void UpdateWindow()
        {
            while (Shown && WindowUpdate())
            {
                RenderSystem.Render();
            }

            if (Shown)
                Hide();
        }

        public void Show()
        {
            Shown = Visible = true;
            ShowMenu(StartingMenu);
            OnShown?.Invoke();
            UpdateWindow();
        }
        public void Hide()
        {
            Shown = Visible = false;
            ShownMenus.Clear();
        }

        public void ShowMenu(Menu menu)
        {
            if (menu == null)
                return;

            ShownMenus.Push(menu);
        }
        public void HideMenu()
        {
            if (ShownMenus.Count > 0)
                ShownMenus.Pop();

            OnHide?.Invoke();
        }
    }
}
