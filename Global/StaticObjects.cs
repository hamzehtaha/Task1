﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseLog; 
namespace Global
{
    public class StaticObjects
    { 
        /// <summary>
        /// This static objects and use it everwhere 
        /// </summary>
        public static Logger Erros = new Logger();
        public static int SuccOfFail = 0;
    }
}
