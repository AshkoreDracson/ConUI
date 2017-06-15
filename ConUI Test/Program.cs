using ConUI;
using ConUI.Controls;
using OpenTK.Graphics;

namespace ConUI_Test
{
    class Program
    {
        public static Console Console { get; private set; } = new Console(30, 80, "ConUI Test");
        static Menu mainMenu = new Menu("Main Menu");

        [System.STAThread]
        static void Main(string[] args)
        {
            InitializeMenu();
            Console.StartingMenu = mainMenu;
            Console.Show();
        }

        static void InitializeMenu()
        {
            mainMenu.BackColor = Color4.DarkBlue;

            mainMenu.Controls.AddRange(
                new Label("lb1", "Name") { Size = new Size(4, 1), Position = new Point(0, 1) },
                new TextBox("tb1") { Size = new Size(75, 1), Position = new Point(5, 1) },

                new Label("lb2", "Age") { Size = new Size(3, 1), Position = new Point(0, 3) },
                new TextBox("tb2") { Size = new Size(75, 1), Position = new Point(5, 3) },

                new Label("lb3", "DOB") { Size = new Size(3, 1), Position = new Point(0, 4) },
                new DateBox("tb3") { Size = new Size(75, 1), Position = new Point(5, 4) },

                new CheckBox("cb1", "Male") { Size = new Size(40, 1), Position = new Point(0, 6) },
                new CheckBox("cb1", "Female") { Size = new Size(40, 1), Position = new Point(40, 6) },

                new Button("b1", "Sign up") { Size = new Size(40, 1), Position = new Point(0, 8) },
                new Button("b2", "Cancel") { Size = new Size(40, 1), Position = new Point(40, 8) }
                );
        }
    }
}
