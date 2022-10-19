using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ProgAssign1
{
    public class Program
    {
        static string inputPath = "..\\..\\..\\Input";
        static string outputPath = "..\\..\\..\\Output";
        static void Main(string[] args)
        {
            try
            {
                CreateInputOutputFolderIfNotExists();

                DateTime startTime = DateTime.Now;
                Console.WriteLine("Application Start At: " + startTime.ToString("dd-MM-yyyy HH:mm"));

                LogWriter.LogWrite("Execution Started.");

                DirWalker dirWalker = new DirWalker();

                dirWalker.walk(inputPath);
                dirWalker.ConvertListToCSV(outputPath);

                LogWriter.LogWrite("Execution Completed.");

                DateTime endTime = DateTime.Now;
                TimeSpan totalTime = endTime - startTime;
                LogWriter.LogWrite("Approx. execution time is " + totalTime.Minutes + " Minutes and " + totalTime.Seconds + " Seconds.");

                int skippedRows = dirWalker.GetSkipCount();
                int successRows = dirWalker.GetCustomerList().Count();

                LogWriter.LogWrite("Total rows: " + (successRows + skippedRows) + " , Successfully merged rows: " + successRows + " , Skipped rows: " + skippedRows + ".");

            }
            catch (Exception ex)
            {
                LogWriter.LogWrite("Exception : " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        static void CreateInputOutputFolderIfNotExists()
        {
            if (!Directory.Exists(inputPath))
            {
                Directory.CreateDirectory(inputPath);
            }

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            string logPath = "..\\..\\..\\Log";

            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
        }
    }
}
