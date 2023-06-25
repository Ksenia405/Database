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
    public partial class Form6 : Form
    {
        public static string vidTovar;
        public static  string strana, material, proizv;
        public static  int ygol;
        public static float timeD;
        public Form6()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
           

            try
            {
                material = textBox1.Text;
                vidTovar = comboBox2.Text;
                strana = textBox3.Text;
                timeD = float.Parse(textBox6.Text);
                proizv = textBox2.Text;
                ygol = Convert.ToInt32(textBox11.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Проверьте данные и повторите попытку");
            }
            if ((comboBox2.Text == "")  || (textBox1.Text == "") || (textBox2.Text == "") || (textBox3.Text == "") )
            {
                MessageBox.Show("Ошибка! Проверьте данные и повторите попытку");
                return;
            }

           Data.C2_vidTovar=vidTovar;
           Data.C2_strana = strana;
           Data.C2_material = material;
           Data.C2_proizv=proizv;
           Data.C2_ygol=ygol;
           Data.C2_timeD=timeD;

            this.Close();
        }
    }
}
