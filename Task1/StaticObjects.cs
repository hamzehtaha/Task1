using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1;

namespace Survey
{
    class StaticObjects
    {
        /// <summary>
        /// This static objects and use it everwhere 
        /// </summary>
        public static AbstractLog Erros = new Logger();
        public static string AddOrEdit = "";
        public static int SuccOfFail = 0;
    }
}
