using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Survey
{
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

        public override void Log(string Message)
        {

            StreamWriter writer = new StreamWriter(this.FilePath);
            try
            {
                writer.Write("\r\nLog Entry : ");
                writer.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                writer.WriteLine("{0}", Message);
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
