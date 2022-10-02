using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Priprema
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 form1 = new Form1();
            Application.Run(form1);
            if (form1.isLoggedInMain)
            {
                Main main = new Main();
                Application.Run(main);
                if (main.isLoggedInTableView)
                {
                    TableView tableView = new TableView();
                    Application.Run(tableView);
                }
            }
        }
    }
}
