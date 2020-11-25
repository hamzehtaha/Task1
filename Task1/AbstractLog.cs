using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey
{
    //This Abstract Class For Logger 
    public abstract class  AbstractLog
    {
        public abstract void Log(string Message,int LineNumber, string MethodName);
    }
}
