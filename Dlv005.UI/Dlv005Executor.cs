using System;
using System.Windows.Forms;

namespace Dlv005.UI
{
    internal static class Dlv005Executor
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Dlv005View());
        }
    }
}