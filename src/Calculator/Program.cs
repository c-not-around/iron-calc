using System;
using System.Globalization;
using System.Windows.Forms;

namespace Calculator
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.CurrentCulture = new CultureInfo("en-US", false);
            Application.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("en-US", false));
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
