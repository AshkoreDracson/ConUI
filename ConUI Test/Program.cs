using ConUI;
using ConUI.Controls;
using OpenTK.Graphics;

namespace ConUI_Test
{
    class Program
    {
        static Console Console { get; } = new Console(30, 80, "ConUI Test");
        static Menu mainMenu = new Menu("Main Menu");

        [System.STAThread]
        static void Main()
        {
            InitializeMenu();
            Console.StartingMenu = mainMenu;
            Console.Show();
        }

        static void InitializeMenu()
        {
            mainMenu.BackColor = Color4.DarkBlue;

            mainMenu.Controls.AddRange(
                new Label("lb1", "Text To Add") { Size = new Size(11, 1), Position = new Point(0, 1) },
                new TextBox("tb1") { Size = new Size(68, 1), Position = new Point(12, 1) },

                new Button("b1", "Add") { Size = new Size(80, 1), Position = new Point(0, 2) },
                new ListBox("listbox1") { Size = new Size(80, 20), Position = new Point(0, 4) }
                );

            Button b1 = mainMenu.Controls["b1"] as Button;
            ListBox listbox1 = mainMenu.Controls["listbox1"] as ListBox;
            TextBox tb1 = mainMenu.Controls["tb1"] as TextBox;

            if (b1 != null && listbox1 != null && tb1 != null)
            {
                b1.Click += () =>
                {
                    listbox1.Items.Add($"{listbox1.Items.Count + 1}) {tb1.Text}");
                };
            }
        }
    }
}
