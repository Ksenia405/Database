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
    public partial class Form8 : Form
    {

        public static string vidTovar;
        public static  string strana, proizv;
        public static int count;
        public Form8()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string vidTovar;
            string strana, proizv;
            int count;

            try
            {
                vidTovar = comboBox2.Text;
                strana = textBox3.Text;
                proizv = textBox2.Text;
                count = Convert.ToInt32(textBox11.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Проверьте данные и повторите попытку");
            }
            vidTovar = comboBox2.Text;
            strana = textBox3.Text;
            proizv = textBox2.Text;
            count = Convert.ToInt32(textBox11.Text);
            if ((comboBox2.Text == "") || (textBox2.Text == "") || (textBox3.Text == "")||(Convert.ToInt32(textBox11.Text) == 0))
            {
                MessageBox.Show("Ошибка! Проверьте данные и повторите попытку");
                return;
            }
            Data.C4_vidTovar=vidTovar;
            Data.C4_strana=strana;
            Data.C4_proizv=proizv;
            Data.C4_count=count;
            this.Close();
        }
    }
}
