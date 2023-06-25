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
    public partial class Form2 : Form
    {
        private SqlConnection sqlConnection = null;
        List<string[]> list56;
        string IDPok;
        int h;
        public Form2(string name, string ID, int i)
        {
            InitializeComponent();
            label1.Text = name;
            IDPok = ID;
            h = i;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            #region Подключение таблицы
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SaveDB"].ConnectionString);
            sqlConnection.Open();
            #endregion
            LoadSatelitesList1();
        }

        void LoadSatelitesList1()
        {
            dataGridView1.Rows.Clear();
            list56 = new List<string[]>();
            string[] page1 = null;
            SqlCommand sqlCommand1 = new SqlCommand($"SELECT [Заказ].[Номер заказа], Заказ.Состояние FROM Заказ INNER JOIN Покупатель ON [Заказ].[id Покупателя]=[Покупатель].[Id Покупателя] WHERE [Заказ].[id Покупателя]='{IDPok}'", sqlConnection); 
            SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
            while (dataReader1.Read())
            {
                page1 = new string[]
                {
                    Convert.ToString(dataReader1["Номер заказа"]),
                    Convert.ToString(dataReader1["Состояние"]),

                };
                list56.Add(page1);

            }
            if (list56.Count ==0) { dataGridView1.Visible = false; }
            else {
                foreach (var s in list56)
                {
                    dataGridView1.Rows.Add(s[0], s[1]);
                } }
            dataReader1.Close();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = dataGridView1.CurrentRow.Index;
            this.Hide();
            Form3 d = new Form3(label1.Text, list56[rowindex][0], list56[rowindex][1]);
            if ((h == 1) || (h == 3)) { if ((h == 1) || (h == 3)) { d.button3.Parent = null; d.button4.Parent = null; d.button2.Parent = null; d.button1.Parent = null; } }
            d.ShowDialog();
        }
    }
}
