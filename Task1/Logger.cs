﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Survey
{
    // This Class using  AbstractLog For Error 
    class Logger : AbstractLog
    {
        private string CurrentDirectory {
            get;
            set;
        }
        private string FileName
        {
            get;
            set;
        }
        private string FilePath
        {
            get;
            set;
        }
        public Logger()
        {
            this.CurrentDirectory = Directory.GetCurrentDirectory();
            this.FileName = "Log.txt";
            this.FilePath = this.CurrentDirectory + "/" + this.FileName;
        }

        public override void Log(string Message,int LineNumber,string MethodName)
        {
            //This Function OVERRIDE FROM AbstractLog that is write Errors and dates in log file  

            StreamWriter writer = new StreamWriter(this.FilePath);
            try
            {
                writer.Write("\r\nLog Entry : ");
                writer.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                writer.WriteLine("{0}", Message);
                writer.WriteLine("{0} {1}","Method name is", MethodName);
                writer.WriteLine("{0} {1}", "The Number of line :", LineNumber); 
                writer.WriteLine("------------------------------------");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                writer.Close();
            }
        }
    }
}
