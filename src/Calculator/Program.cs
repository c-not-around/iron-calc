using System;
using System.IO;
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
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			if (!(File.Exists("IronPython.dll")        && File.Exists("IronPython.Modules.dll") &&
				  File.Exists("Microsoft.Dynamic.dll") && File.Exists("Microsoft.Scripting.dll")))
			{
				MessageBox.Show
				(
					"There are no libraries:\r\n"  +
                    "  IronPython.dll\r\n"         +
                    "  IronPython.Modules.dll\r\n" +
                    "  Microsoft.Dynamic.dll\r\n"  +
                    "  Microsoft.Scripting.dll", "Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error
				);

				Application.Exit();
			}

			if (!File.Exists("calc.py"))
			{
				File.WriteAllBytes("calc.py", Properties.Resources.calc);
			}

			Application.CurrentCulture       = new CultureInfo("en-US", false);
            Application.CurrentInputLanguage = InputLanguage.FromCulture(Application.CurrentCulture);
            Application.Run(new MainForm());
        }
    }
}
