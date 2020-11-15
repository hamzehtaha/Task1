using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task1;

namespace Survey
{
    public partial class Home : Form
    {
        public Home()
        {
            StartFunction(); 
        }
        private void StartFunction()
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
                
                QuestionsInformation f = new QuestionsInformation(ListOfQuestion, -1, "", "Add");
                f.Show();
            }catch (Exception ex)
            {
                MessageBox.Show("Smomething went wrong!");
                Attributes.Erros.Log(ex.Message);
            }
        }
        private void Edit_Click(object sender, EventArgs e)
        {
            // this function for go to the edit page and give me the id 
            try
            {
                List<int> ListOfId = new List<int>();
                List<string> Types = new List<string>();
                foreach (DataGridViewRow row in ListOfQuestion.Rows)
                {
                    if ((bool)ListOfQuestion.Rows[row.Index].Cells[0].Value == true)
                    {
                        string TempText = ListOfQuestion.Rows[row.Index].Cells[1].Value.ToString();
                        for (int i = 0; i < Attributes.ListOfAllQuestion.Count; ++i)
                            if (TempText.Equals(Attributes.ListOfAllQuestion.ElementAt(i).NewText))
                            {
                                ListOfId.Add(Attributes.ListOfAllQuestion.ElementAt(i).Id);
                                Types.Add(Attributes.ListOfAllQuestion.ElementAt(i).TypeOfQuestion);
                                
                            }
                    }
                }
                if (ListOfId.Count > 1)
                {
                    MessageBox.Show("You Can not choose more than one Question for Edit", Attributes.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ListOfId.Count == 0)
                {
                    MessageBox.Show("Please Choose one Question for Edit", Attributes.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ListOfId.Count == 1)
                {
                    QuestionsInformation f = new QuestionsInformation(ListOfQuestion, ListOfId.ElementAt(0), Types.ElementAt(0), "Edit");
                    f.Show();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(Attributes.MessageError);
                Attributes.Erros.Log(ex.Message);
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
                    MessageBox.Show("This is not select any Question !", Attributes.ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                MessageBox.Show(Attributes.MessageError);
                Attributes.Erros.Log(ex.Message);
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void changeToArabicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (Thread.CurrentThread.CurrentUICulture.IetfLanguageTag) {
                case "ar-EG":
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                    Attributes.Languge = "Englsih"; 
                    break;
                case "en-US":
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
                    Attributes.Languge = "Arabic"; 
                    break;
                default:
                    break;
            }
            this.Controls.Clear();
            StartFunction(); 
        }
    }
}
