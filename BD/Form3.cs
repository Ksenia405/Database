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
    public partial class Form3 : Form
    {
        private SqlConnection sqlConnection = null;
        List<string[]> list56;
        public Form3(string name, string zakaz, string s)
        {
            InitializeComponent();
            label1.Text = name;
            label2.Text = zakaz;
            label4.Text = s;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

            #region Подключение таблицы
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SaveDB"].ConnectionString);
            sqlConnection.Open();
            #endregion
            LoadSatelitesList1();
        }

        public void LoadSatelitesList1()
        {
            dataGridView1.Rows.Clear();
            int sum = 0;
            string zakaz = label2.Text;
            list56 = new List<string[]>();
            string[] page1 = null;
            SqlCommand sqlCommand = new SqlCommand("DELETE FROM СписокТоваров WHERE [Кол-во]=0", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            SqlCommand sqlCommand1 = new SqlCommand($"SELECT Товар.Название, Товар.Цена, СписокТоваров.[Кол-во], СписокТоваров.[id] FROM Товар INNER JOIN СписокТоваров ON [СписокТоваров].[id Товара]=[Товар].[Id Товара] INNER JOIN Заказ ON [СписокТоваров].[id Заказа]=[Заказ].[Id Заказа] WHERE [Заказ].[Номер заказа]='{zakaz}'", sqlConnection); ; ;
            SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
            while (dataReader1.Read())
            {
                page1 = new string[]
                {
                    Convert.ToString(dataReader1["Название"]),
                    Convert.ToString(dataReader1["Цена"]),
                    Convert.ToString(dataReader1["Кол-во"]),
                    Convert.ToString(dataReader1["id"])

                };
                list56.Add(page1);

            }
            foreach (var s in list56)
            {
                dataGridView1.Rows.Add(s[0], s[1], s[2], s[3]);
                sum += Convert.ToInt32(s[1])*Convert.ToInt32(s[2]);
            }
            textBox1.Text = sum.ToString();
            dataReader1.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadSatelitesList1();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView1.CurrentRow.Index;
            string zakaz;
            zakaz = list56[rowindex][3];
            SqlCommand sqlCommand = new SqlCommand($"UPDATE СписокТоваров SET [Кол-во]=[Кол-во]+1 WHERE [id]='{zakaz}'", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            LoadSatelitesList1();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView1.CurrentRow.Index;
            string zakaz;
            zakaz = list56[rowindex][3];
            SqlCommand sqlCommand = new SqlCommand($"UPDATE СписокТоваров SET [Кол-во]=[Кол-во]-1 WHERE [id]='{zakaz}'", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            LoadSatelitesList1();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                SqlCommand sqlCommand = new SqlCommand($"UPDATE Заказ SET Состояние=N'{textBox2.Text}' WHERE [Номер заказа]=N'{label2.Text}'", sqlConnection);
                sqlCommand.ExecuteNonQuery();
                label4.Text = textBox2.Text;
            }
        }


    }
}
