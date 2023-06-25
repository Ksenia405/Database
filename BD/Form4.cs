using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace BD
{
    public partial class Form4 : Form
    {
        private SqlConnection sqlConnection = null;

        public static string vidTovar;
        public static string typeS;
        public static string Raz, strana, material, OblPr, minOsv, matrix, proizv;
        public static int ygol; 
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            #region Подключение таблицы
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SaveDB"].ConnectionString);
            sqlConnection.Open();
            #endregion
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            try
            {
                vidTovar = comboBox2.Text;
                typeS = textBox11.Text;
                Raz = textBox12.Text;
                strana=textBox1.Text;
                material=textBox5.Text;
                OblPr = textBox6.Text;
                minOsv = textBox4.Text;
                matrix= textBox2.Text;
                proizv = textBox7.Text;
                ygol =Convert.ToInt32(textBox3.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Проверьте данные и повторите попытку");
            }
            if ((comboBox2.Text  == "") || (textBox11.Text == "") || (textBox12.Text =="") || (textBox1.Text =="") ||(textBox5.Text =="") ||(textBox6.Text=="") ||(textBox4.Text=="")||(textBox2.Text=="")||(textBox7.Text=="") ||(Convert.ToInt32(textBox3.Text)==0)) {
                MessageBox.Show("Ошибка! Проверьте данные и повторите попытку");
                return;
            }
            Data.C1_vidTovar = vidTovar;
            Data.C1_typeS = typeS;
            Data.C1_Raz = Raz;
            Data.C1_strana = strana;
            Data.C1_material = material;
            Data.C1_OblPr = OblPr;
            Data.C1_minOsv = minOsv;
            Data.C1_matrix = matrix;
            Data.C1_proizv = proizv;
            Data.C1_ygol = ygol;
            this.Close();
        }
    }
}
