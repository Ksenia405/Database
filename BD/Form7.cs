using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BD
{
    public partial class Form7 : Form
    {
        public static string vidTovar;
        public static string strana, proizv;

        public Form7()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string vidTovar;
            string strana, proizv;

            try
            {
                vidTovar = comboBox2.Text;
                strana = textBox3.Text;
                proizv = textBox2.Text;
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Проверьте данные и повторите попытку");
            }
            vidTovar = comboBox2.Text;
            strana = textBox3.Text;
            proizv = textBox2.Text;
            if ((comboBox2.Text == "") || (textBox2.Text == "") || (textBox3.Text == ""))
            {
                MessageBox.Show("Ошибка! Проверьте данные и повторите попытку");
                return;
            }

         Data.C3_vidTovar=vidTovar;
         Data.C3_strana=strana; 
         Data.C3_proizv=proizv;

            this.Close();
        }
    }
}
