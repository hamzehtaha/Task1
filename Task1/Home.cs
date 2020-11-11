using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task1;

namespace Survey
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            Slider.ShowQuestion();
            Smiles.ShowQuestion();
            Stars.ShowQuestion();
            Qustions.GetData(ListOfQuestion);
        }
        private void Add_Click(object sender, EventArgs e)
        {
            // to go to the Add page
            try
            {
                QuestionsInformation f = new QuestionsInformation(ListOfQuestion, -1, "", 1);
                f.Show();
            }catch (Exception ex)
            {

            }
        }
        private void Edit_Click(object sender, EventArgs e)
        {
            // this function for go to the edit page and give me the id 
            try
            {
                List<int> ListOfId = new List<int>();
                List<string> types = new List<string>();
                foreach (DataGridViewRow row in ListOfQuestion.Rows)
                {
                    if ((bool)ListOfQuestion.Rows[row.Index].Cells[0].Value == true)
                    {
                        string s = ListOfQuestion.Rows[row.Index].Cells[1].Value.ToString();
                        for (int i = 0; i < Attributes.ListOfAllQuestion.Count; ++i)
                            if (s.Equals(Attributes.ListOfAllQuestion.ElementAt(i).NewText))
                            {
                                ListOfId.Add(Attributes.ListOfAllQuestion.ElementAt(i).Id);
                                types.Add(Attributes.ListOfAllQuestion.ElementAt(i).TypeOfQuestion);
                            }
                    }
                }
                if (ListOfId.Count > 1)
                {
                    MessageBox.Show("You Can not choose more than one Question for Edit", Attributes.Variables.Error.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ListOfId.Count == 0)
                {
                    MessageBox.Show("Please Choose one Question for Edit", Attributes.Variables.Error.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ListOfId.Count == 1)
                {
                    QuestionsInformation f = new QuestionsInformation(ListOfQuestion, ListOfId.ElementAt(0), types.ElementAt(0), 2);
                    f.Show();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void Delete_Click(object sender, EventArgs e)
        {
            // this function for delete the answer 
            try
            {
                List<int> ListOfId = new List<int>();
                Slider slider = null;
                Stars stars = null;
                Smiles smiles = null;
                foreach (DataGridViewRow row in ListOfQuestion.Rows)
                {
                    if ((bool)ListOfQuestion.Rows[row.Index].Cells[0].Value == true)
                    {
                        string s = ListOfQuestion.Rows[row.Index].Cells[1].Value.ToString();
                        for (int i = 0; i < Attributes.ListOfAllQuestion.Count; ++i)
                            if (s.Equals(Attributes.ListOfAllQuestion.ElementAt(i).NewText))
                                ListOfId.Add(Attributes.ListOfAllQuestion.ElementAt(i).Id);
                    }
                }
                if (ListOfId.Count == 0)
                {
                    MessageBox.Show("This is not select any Question !", Attributes.Variables.Error.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure to Delete this answer ?", "Delete", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes && ListOfId.Count > 0)
                    {
                        for (int j = 0; j < ListOfId.Count; ++j)
                        {
                            for (int i = 0; i < Attributes.ListOfAllQuestion.Count; ++i)
                            {
                                if (Attributes.ListOfAllQuestion.ElementAt(i).Id == ListOfId.ElementAt(j))
                                {
                                    if (Attributes.ListOfAllQuestion.ElementAt(i) is Slider)
                                    {
                                        slider = (Slider)Attributes.ListOfAllQuestion.ElementAt(i);
                                        slider.Delete(slider.Id, slider.IdForType);
                                        Attributes.ListOfAllQuestion.Remove(slider);
                                        break;
                                    }
                                    else if (Attributes.ListOfAllQuestion.ElementAt(i) is Smiles)
                                    {
                                        smiles = (Smiles)Attributes.ListOfAllQuestion.ElementAt(i);
                                        smiles.Delete(smiles.Id, smiles.IdForType);
                                        Attributes.ListOfAllQuestion.Remove(smiles);
                                        break;

                                    }
                                    else if (Attributes.ListOfAllQuestion.ElementAt(i) is Stars)
                                    {
                                        stars = (Stars)Attributes.ListOfAllQuestion.ElementAt(i);
                                        stars.Delete(stars.Id, stars.IdForType);
                                        Attributes.ListOfAllQuestion.RemoveAt(i);
                                        break;

                                    }
                                }
                            }

                        }
                        Qustions.GetData(ListOfQuestion);
                        MessageBox.Show("Your Questions is deleted !");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
