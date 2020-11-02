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
        public Smiles(int Id,int idForType, string Qustion,string TypeOfQuestion, int order,int NumberOfSmiles) {
            this.Qustion = Qustion;
            this.NumberOfSmiles = NumberOfSmiles;
            this.Order = order;
            this.TypeOfQuestion = TypeOfQuestion;
            this.Id = Id;
            this.idForType = idForType; 
        }
        public Smiles(int idForType, string Qustion, string TypeOfQuestion, int order, int NumberOfSmiles)
        {
            this.Qustion = Qustion;
            this.NumberOfSmiles = NumberOfSmiles;
            this.Order = order;
            this.TypeOfQuestion = TypeOfQuestion;
            this.idForType = idForType; 


        }
        public int NumberOfSmiles { get; set; }
        public int idForType { get; set; }

    }
}
