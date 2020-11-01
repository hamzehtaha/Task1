using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Smiles :Qustions 
    {
        public static Boolean[] f = new Boolean[10];
        public Smiles(int Id,string Qustion,string TypeOfQuestion, int order,int NumberOfSmiles) {
            this.Qustion = Qustion;
            this.NumberOfSmiles = NumberOfSmiles;
            this.Order = order;
            this.TypeOfQuestion = TypeOfQuestion;
            this.Id = Id; 
        }
        public Smiles(string Qustion, string TypeOfQuestion, int order, int NumberOfSmiles)
        {
            this.Qustion = Qustion;
            this.NumberOfSmiles = NumberOfSmiles;
            this.Order = order;
            this.TypeOfQuestion = TypeOfQuestion;
            
        }
        private int numberOfSmiles; 
        public int NumberOfSmiles { get; set; }

    }
}
