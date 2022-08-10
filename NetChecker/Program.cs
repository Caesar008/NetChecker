using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace NetChecker
{
    static class Program
    {
        // defines for commandline output
        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;

        /// <summary>
        /// Hlavní vstupní bod aplikace.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            AttachConsole(ATTACH_PARENT_PROCESS);
            
            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else if (args[0].ToLower() == "-console" || args[0].ToLower() == "/console" || args[0].ToLower() == "-c" || args[0].ToLower() == " /c")
            {
                Verze v = new Verze();
                Console.WriteLine("\r\n\r\n" + v.verze());
                if (args.Length > 1 && (args[1].ToLower() == "-file" || args[1].ToLower() == "/file"))
                    File.WriteAllText("NetVersion.txt", "\r\n\r\n" + v.verze());
                SendKeys.SendWait("{ENTER}");
            }
            else
                Console.WriteLine("Neznámý parametr");

        }


    }
}
