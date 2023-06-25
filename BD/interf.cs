using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BD
{
    public partial class interf : Form
    {
        private SqlConnection sqlConnection = null;

        public interf()
        {
            InitializeComponent();
        }

        private void interf_Load(object sender, EventArgs e)
        {
            #region Подключение таблицы
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SaveDB"].ConnectionString);
            sqlConnection.Open();
            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox7.Text != "") || (textBox1.Text != ""))
            {
                if ((textBox7.Text == "admin") && (textBox1.Text == "admin"))
                {

                    Form1 d = new Form1(1);
                    d.ShowDialog();
                }
                else if ((textBox7.Text == "salesman") && (textBox1.Text == "salesman"))
                {

                    Form1 d = new Form1(2);
                    d.ShowDialog();
                }
                else
                {

                    string name = textBox7.Text;
                    string login = textBox1.Text;
                    string pass;
                    bool flag = false;
                    string dh=null;
                    char[] g;

                    SqlCommand sqlCommand1 = new SqlCommand($"SELECT [id Покупателя] FROM Покупатель WHERE [ФИО/Название компании]=N'{name}'", sqlConnection);
                    SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
                    while (dataReader1.Read())
                    {
                        dh=Convert.ToString(dataReader1["id Покупателя"]);
                        g =new char[4] { dh.ToCharArray()[0],  dh.ToCharArray()[1],  dh.ToCharArray()[2], dh.ToCharArray()[3] };
                        pass = (g[0].ToString() + g[1].ToString() + g[2].ToString() + g[3].ToString());
                        if (pass == login) { flag = true; break; }
                        
                    }
                    dataReader1.Close();
                    if (flag) {
                        Form2 d = new Form2(name, dh, 1);
                        d.ShowDialog(); 
                    }
                    else
                    {
                        MessageBox.Show("Ошибка, повторите попытку");
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Ошибка, повторите попытку");
            }
        }


    }
}
