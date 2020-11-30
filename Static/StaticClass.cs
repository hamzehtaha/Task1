using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log; 
namespace Static
{
    public class StaticObjects
    {
        /// <summary>
        /// This static objects and use it everwhere 
        /// </summary>
        public static Logger Erros = new Logger();
        public static string AddOrEdit = "";
        public static int SuccOfFail = 0;
    }
}
