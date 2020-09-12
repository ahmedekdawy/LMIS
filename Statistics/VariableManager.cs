using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics
{
    public class VariableManager
    {
        public string ConnectionString12 = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        public string  CreateVariable(string variableName)
        {
           
            string NotificationLbl = string.Empty;
            SqlConnection con = new SqlConnection(ConnectionString12);
            string sqlstrg1 = "SELECT * from GeneralCode where Name = '"+ variableName+"'";
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con;
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = sqlstrg1;
            con.Open();
            SqlDataAdapter daAdapter1 = new SqlDataAdapter(cmd1);
            DataSet asd1 = new DataSet();
            daAdapter1.Fill(asd1);
            if (asd1.Tables[0].Rows.Count > 0)
            {
                NotificationLbl = "Sorry, this variable already exist";
            }
            else
            {
                string sqlstrg2= " SELECT TOP (1) GeneralID FROM GeneralCode ORDER BY GeneralID DESC ";
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = con;
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = sqlstrg2;
                SqlDataAdapter daAdapter2= new SqlDataAdapter(cmd2);
                DataSet asd2 = new DataSet();
                daAdapter2.Fill(asd2);
                if (asd2.Tables[0].Rows.Count != 0)
                {
                    int x = Convert.ToInt16(asd2.Tables[0].Rows[0][0].ToString());
                    x = x + 1;
                    string y;
                    if(x <= 9)
                    {
                        y= 00 + x.ToString();
                    }
                    else if (x <= 99)
                    {
                       
                        y = 0 + x.ToString();
                    }
                    else 
                    {
                        y = x.ToString();
                    }
                    string sqlstrg3 = "INSERT INTO GeneralCode (GeneralID, Name, ParentGeneralcodeID) VALUES ('" + y + "', '" + variableName + "', 1)";
                    SqlCommand cmd3 = new SqlCommand();
                    cmd3.Connection = con;
                    cmd3.CommandType = CommandType.Text;
                    cmd3.CommandText = sqlstrg3;
                    cmd3.ExecuteNonQuery();
                    NotificationLbl = "The Variable has been added successfully";
                   
                    
                }
               
            } 
            return NotificationLbl;
        }

    }
}
