using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Slider : Qustions
    {

        private int startV;
        private int endV;
        private string startC;
        private string endC;
        private int idForType;
        public int IdForType{get; set;}
        public int StartV { get; set; }
        public int EndV {
            get {
                return endV; 
            }
            set {
                if (value <= 100)
                {
                    endV = value;
                }
                else {
                    Console.WriteLine("Error");  
                }
            }
        
        }
        public string StartC { get; set; }
        public string EndC { get; set; }

        public Slider(int Id, int idForType, string Qustion ,string TypeOfQuestion, int order, int  StartV,int EndV , string StartC,string EndC) {
            this.Qustion = Qustion;
            this.Order = order; 
            this.StartV = StartV;
            this.EndV = EndV;
            this.StartC = StartC;
            this.EndC = EndC;
            this.Id = Id; 
            this.TypeOfQuestion = TypeOfQuestion;
            this.idForType = idForType; 
        }
        public Slider(string Qustion, string TypeOfQuestion, int idForType,int order, int StartV, int EndV, string StartC, string EndC)
        {
            this.Qustion = Qustion;
            this.Order = order;
            this.StartV = StartV;
            this.EndV = EndV;
            this.StartC = StartC;
            this.EndC = EndC;
            this.TypeOfQuestion = TypeOfQuestion;
            this.idForType = idForType;
        }

    }
}
