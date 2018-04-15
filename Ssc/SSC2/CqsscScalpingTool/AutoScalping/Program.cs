using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace AutoScalping
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                GloableParams.AppCurrentVersion = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }
            catch
            {
                GloableParams.AppCurrentVersion = Assembly.GetExecutingAssembly().GetName().Version;
            }


            Application.Run(new fmsscMainWin());
        }
    }
}
