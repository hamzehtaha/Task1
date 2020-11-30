using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseConnection;
using Question;
using BaseLog;
using Global; 
namespace OperationManger
{
    public class Operation
    {
        public static Qustions AddQustion(Qustions Question)
        {
            try
            {
                if (Question is Slider)
                {
                    return DataBaseConnections.AddNewSlider(Question);
                }
                else if (Question is Smiles)
                {
                    return DataBaseConnections.AddNewSmile(Question);
                }
                else
                {
                    return DataBaseConnections.AddNewStar(Question);
                }
            }
            catch (Exception ex)
            {
                StaticObjects.Erros.Log(ex);
                StaticObjects.SuccOfFail = 0;
                return null;
            }
        }
        public static Qustions EditQustion(Qustions Question)
        {
            try
            {
                if (Question is Slider)
                {
                    return DataBaseConnections.EditSlider(Question);
                }
                else if (Question is Smiles)
                {
                    return DataBaseConnections.EditSmile(Question);
                }
                else
                {
                    return DataBaseConnections.EditStar(Question);
                }
            }
            catch (Exception ex)
            {
                StaticObjects.Erros.Log(ex);
                StaticObjects.SuccOfFail = 0;
                return null;
            }
        }
        public static int DeleteQustion(Qustions Question)
        {
            try
            {
                if (Question is Slider)
                {
                    return DataBaseConnections.DeleteSlider(Question);
                }
                else if (Question is Smiles)
                {
                    return DataBaseConnections.DeleteSmile(Question);
                }
                else
                {
                    return DataBaseConnections.DeleteStar(Question);
                }
            }
            catch (Exception ex)
            {
                StaticObjects.Erros.Log(ex);
                StaticObjects.SuccOfFail = 0;
                return 0;
            }
        }
        public static List<Qustions> GetQustion()
        {
            try
            {
                return DataBaseConnections.GetQuestionFromDataBase();
            }
            catch (Exception ex)
            {
                StaticObjects.Erros.Log(ex);
                return null;
            }
        }

    }
}
