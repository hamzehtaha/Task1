using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Qustions
    {
        public static List<Qustions> lissSlid = new List<Qustions>(); 
        private string qustion;
        private int order = 0;
        private int id;
        private string typeOfQuestion; 
        public string Qustion { get; set; }
        public int Order { get; set; }
        public int Id { get; set; }
        public string TypeOfQuestion { get; set;  }

    }
}
