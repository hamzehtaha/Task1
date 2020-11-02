using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        public Smiles() { }
        public int NumberOfSmiles { get; set; }
        public int idForType { get; set; }
        public static void ShowQuestion()
        {

            SqlConnection con = new SqlConnection(@"data source=HAMZEH; database=Survey; integrated security=SSPI");
            SqlCommand cmd = new SqlCommand("sp_Qustions_SelectAll2", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlCommand cmd1 = new SqlCommand("sp_Smily_SelectAll2", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            Smiles obj;
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Type_Of_Qustion", "Smily");
                SqlDataReader rd = cmd.ExecuteReader();

                obj = new Smiles();
                List<Smiles> li1 = new List<Smiles>();
                while (rd.Read())
                {
                    obj.Id = Convert.ToInt32(rd["ID"]);
                    obj.Qustion = rd["Qustions_text"].ToString();
                    obj.TypeOfQuestion = "Smily";
                    obj.Order = Convert.ToInt32(rd["Qustion_order"]);
                    li1.Add(obj);
                    obj = new Smiles();
                }
                rd.Close();

                for (int i = 0; i < li1.Count; ++i)
                {

                    cmd1.Parameters.AddWithValue("@Qus_ID", li1.ElementAt(i).Id);
                    SqlDataReader rd1 = cmd1.ExecuteReader();
                    while (rd1.Read())
                    {
                        li1.ElementAt(i).NumberOfSmiles = Convert.ToInt32(rd1["Number_of_smily"]);
                        li1.ElementAt(i).idForType = Convert.ToInt32(rd1["ID"]);
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
