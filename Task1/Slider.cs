using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Slider : Qustions
    {
        public int IdForType{get; set;}
        public int StartV { get; set; }
        public int EndV { get; set; }
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
            this.IdForType = idForType; 
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
            this.IdForType = idForType;
        }
        public Slider()
        {

        }
        public static  void ShowQuestion()
        {
            
            SqlConnection con = new SqlConnection(@"data source=HAMZEH; database=Survey; integrated security=SSPI");
            SqlCommand cmd = new SqlCommand("sp_Qustions_SelectAll2", con);
            cmd.CommandType = CommandType.StoredProcedure; 

            SqlCommand cmd1 = new SqlCommand("sp_Slider_SelectAll2", con);
            cmd1.CommandType = CommandType.StoredProcedure; 
            Slider obj;
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Type_Of_Qustion", "Slider");
                SqlDataReader rd = cmd.ExecuteReader();
                 
                obj  = new Slider();
                List<Slider> li1 = new List<Slider>(); 
                while (rd.Read())
                {
                    obj.Id = Convert.ToInt32(rd["ID"]);
                    obj.Qustion = rd["Qustions_text"].ToString(); 
                    obj.TypeOfQuestion = "Slider";
                    obj.Order = Convert.ToInt32(rd["Qustion_order"]);
                    li1.Add(obj);
                    obj = new Slider(); 

                }
                rd.Close();
               
                for (int i = 0; i<li1.Count; ++i)
                {

                    cmd1.Parameters.AddWithValue("@Qus_ID", li1.ElementAt(i).Id);
                    SqlDataReader rd1 = cmd1.ExecuteReader();
                    while (rd1.Read())
                    {
                        li1.ElementAt(i).StartV= Convert.ToInt32(rd1["Start_Value"]);
                        li1.ElementAt(i).EndV = Convert.ToInt32(rd1["End_Value"]);
                        li1.ElementAt(i).StartC = rd1["Start_Value_Cap"].ToString();
                        li1.ElementAt(i).EndC = rd1["End_Value_Cap"].ToString();
                        li1.ElementAt(i).IdForType = Convert.ToInt32(rd1["ID"]);
                        Qustions.lissSlid.Add(li1.ElementAt(i));
                        //Console.WriteLine(li1.ElementAt(i).Qustion); 
                    }
                    cmd1.Parameters.Clear(); 
                    rd1.Close(); 
                }
                cmd.Parameters.Clear();
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
            finally
            {
                con.Close();
            }
        }

    }
}
