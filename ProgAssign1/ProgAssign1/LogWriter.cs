using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgAssign1
{
    using System.IO;
    using System.Reflection;
    public static class LogWriter
    {
        private static string logPath = "..\\..\\..\\Log";
        public static void LogWrite(string logMessage)
        {
            try
            {
                using (StreamWriter w = File.AppendText(logPath + "\\" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt"))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
            }
        }
    }
}
