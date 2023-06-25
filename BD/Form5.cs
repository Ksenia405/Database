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
    public partial class Form5 : Form
    {
        private SqlConnection sqlConnection = null;

        string IDKlcamera=null;
        string IDKldatchik = null;
        string IDKlkeyboard = null;
        string IDKlcentre = null;

        string IDclass = null;
        string IDtype = null;
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            #region Подключение таблицы
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SaveDB"].ConnectionString);
            sqlConnection.Open();
            #endregion
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt32(textBox10.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка, проверьте данные");
            }

            if ((textBox13.Text == "")|| (textBox10.Text=="")||(comboBox2.Text=="")) {
                MessageBox.Show("Введите данные");
                return;
            }
            if ((!checkBox1.Checked) && (!checkBox2.Checked) && (!checkBox3.Checked) && (!checkBox4.Checked)) {
                MessageBox.Show("Введите данные");
                return;
            }

            button2.Enabled = false;

            if (checkBox1.Checked) {
                Form4 d = new Form4();
                d.ShowDialog();
                SqlCommand sqlCommand1 = new SqlCommand($"SELECT [id Записи] FROM [Классификатор камер]  WHERE [Вид]=N'{Data.C1_vidTovar}' AND [Тип съемки]=N'{Data.C1_typeS}' AND [Разрешение]=N'{Data.C1_Raz}' AND [Страна]=N'{Data.C1_strana}' AND [Материал]=N'{Data.C1_material}' AND [Область применения]=N'{Data.C1_OblPr}' AND [Минимальная освещенность ]=N'{Data.C1_minOsv}' AND [Угол обзора]={Data.C1_ygol} AND [Матрица]=N'{Data.C1_matrix}' AND [Производитель]=N'{Data.C1_proizv}'", sqlConnection);
                if (sqlCommand1.ExecuteScalar() != null)
                {
                    SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
                    dataReader1.Read();
                    IDKlcamera = Convert.ToString(dataReader1["id Записи"]);
                    dataReader1.Close();
                }
                else
                {
                    SqlCommand sqlCommand = new SqlCommand($"INSERT INTO [Классификатор камер] ([Вид], [Тип съемки], [Разрешение], [Страна], [Материал], [Область применения], [Минимальная освещенность ], [Угол обзора], [Матрица], [Производитель]) VALUES (N'{Data.C1_vidTovar}', N'{Data.C1_typeS}', N'{Data.C1_Raz}', N'{Data.C1_strana}', N'{Data.C1_material}', N'{Data.C1_OblPr}', N'{Data.C1_minOsv}', {Data.C1_ygol}, N'{Data.C1_matrix}', N'{Data.C1_proizv}') ", sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    SqlCommand sqlCommand2 = new SqlCommand($"SELECT [id Записи] FROM [Классификатор камер]  WHERE [Вид]=N'{Data.C1_vidTovar}' AND [Тип съемки]=N'{Data.C1_typeS}' AND [Разрешение]=N'{Data.C1_Raz}' AND [Страна]=N'{Data.C1_strana}' AND [Материал]=N'{Data.C1_material}' AND [Область применения]=N'{Data.C1_OblPr}' AND [Минимальная освещенность ]=N'{Data.C1_minOsv}' AND [Угол обзора]={Data.C1_ygol} AND [Матрица]=N'{Data.C1_matrix}' AND [Производитель]=N'{Data.C1_proizv}'", sqlConnection);
                    SqlDataReader dataReader1 = sqlCommand2.ExecuteReader();
                    dataReader1.Read();
                    IDKlcamera = Convert.ToString(dataReader1["id Записи"]);
                    dataReader1.Close();
                } 
            }
            if (checkBox2.Checked) {
                Form6 d = new Form6();
                d.ShowDialog();

               
                    SqlCommand sqlCommand = new SqlCommand($"INSERT INTO [Классификатор датчиков] ([Тип датчика], [Угол обнаружения], [Время доставки сигнала], [Материал], [Страна], [Производитель]) VALUES (N'{Data.C2_vidTovar}', {Data.C2_ygol}, {Data.C2_timeD}, N'{Data.C2_material}', N'{Data.C2_strana}', N'{Data.C2_proizv}') ", sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    SqlCommand sqlCommand2 = new SqlCommand($"SELECT [id Записи] FROM [Классификатор датчиков]  WHERE [Тип датчика]=N'{Data.C2_vidTovar}' AND [Угол обнаружения]={Data.C2_ygol} AND [Время доставки сигнала]={Data.C2_timeD} AND [Материал]=N'{Data.C2_material}' AND [Страна]=N'{Data.C2_strana}' AND [Производитель]=N'{Data.C2_proizv}'", sqlConnection);
                    SqlDataReader dataReader1 = sqlCommand2.ExecuteReader();
                    dataReader1.Read();
                    IDKldatchik = Convert.ToString(dataReader1["id Записи"]);
                    dataReader1.Close();
                

            }
            if (checkBox3.Checked) 
            {
                Form7 d = new Form7();
                d.ShowDialog();

                SqlCommand sqlCommand1 = new SqlCommand($"SELECT [id Записи] FROM [Классификатор клавиатур]  WHERE [Тип клавиатуры]=N'{Data.C3_vidTovar}' AND [Страна]=N'{Data.C3_strana}' AND [Производитель]=N'{Data.C3_proizv}'", sqlConnection);
                if (sqlCommand1.ExecuteScalar() != null)
                {
                    SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
                    dataReader1.Read();
                    IDKlkeyboard = Convert.ToString(dataReader1["id Записи"]);
                    dataReader1.Close();
                }
                else
                {
                    SqlCommand sqlCommand = new SqlCommand($"INSERT INTO [Классификатор клавиатур] ([Тип клавиатуры], [Страна], [Производитель]) VALUES (N'{Data.C3_vidTovar}', N'{Data.C3_strana}', N'{Data.C3_proizv}') ", sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    SqlCommand sqlCommand2 = new SqlCommand($"SELECT [id Записи] FROM [Классификатор клавиатур]  WHERE [Тип клавиатуры]=N'{Data.C3_vidTovar}' AND [Страна]=N'{Data.C3_strana}' AND [Производитель]=N'{Data.C3_proizv}'", sqlConnection);
                    SqlDataReader dataReader1 = sqlCommand2.ExecuteReader();
                    dataReader1.Read();
                    IDKlkeyboard = Convert.ToString(dataReader1["id Записи"]);
                    dataReader1.Close();
                }
            }
            if (checkBox4.Checked) 
            {
                Form8 d = new Form8();
                d.ShowDialog();

                SqlCommand sqlCommand1 = new SqlCommand($"SELECT [id Записи] FROM [Классификатор центров]  WHERE [Тип]=N'{Data.C4_vidTovar}' AND [Страна]=N'{Data.C4_strana}' AND [Производитель]=N'{Data.C4_proizv}' AND [Кол-во подключаемых устр]={Data.C4_count}", sqlConnection);
                if (sqlCommand1.ExecuteScalar() != null)
                {
                    SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
                    dataReader1.Read();
                    IDKlcentre = Convert.ToString(dataReader1["id Записи"]);
                    dataReader1.Close();
                }
                else
                {
                    SqlCommand sqlCommand = new SqlCommand($"INSERT INTO [Классификатор центров] ([Тип], [Страна], [Производитель], [Кол-во подключаемых устр]) VALUES (N'{Data.C4_vidTovar}', N'{Data.C4_strana}', N'{Data.C4_proizv}', {Data.C4_count}) ", sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    SqlCommand sqlCommand2 = new SqlCommand($"SELECT [id Записи] FROM [Классификатор центров]  WHERE [Тип]=N'{Data.C4_vidTovar}' AND [Страна]=N'{Data.C4_strana}' AND [Производитель]=N'{Data.C4_proizv}'AND [Кол-во подключаемых устр]={Data.C4_count}", sqlConnection);
                    SqlDataReader dataReader1 = sqlCommand2.ExecuteReader();
                    dataReader1.Read();
                    IDKlcentre = Convert.ToString(dataReader1["id Записи"]);
                    dataReader1.Close();
                }
            }
            Save();

        }
        void Save() {            
            string commandText = "INSERT INTO [РазделКлассификатор] ([id_Записи_кл1], [id_Записи_кл2], [id_Записи_кл3], [id_Записи_кл4]) VALUES (@IDc, @IDd, @IDk, @IDch)";
            SqlCommand command = new SqlCommand(commandText, sqlConnection);
            command.Parameters.Add("@IDc", SqlDbType.UniqueIdentifier);
            command.Parameters.Add("@IDd", SqlDbType.UniqueIdentifier);
            command.Parameters.Add("@IDk", SqlDbType.UniqueIdentifier);
            command.Parameters.Add("@IDch", SqlDbType.UniqueIdentifier);

            if((IDKlcamera is null))
                command.Parameters["@IDc"].Value = Guid.Empty;
            else
                command.Parameters["@IDc"].Value = Guid.Parse(IDKlcamera);

            if ((IDKldatchik is null))
                command.Parameters["@IDd"].Value = Guid.Empty;
            else
                command.Parameters["@IDd"].Value = Guid.Parse(IDKldatchik);


            if ((IDKlkeyboard is null))
                command.Parameters["@IDk"].Value = Guid.Empty;
            else
                command.Parameters["@IDk"].Value = Guid.Parse(IDKlkeyboard);


            if ((IDKlcentre is null))
                command.Parameters["@IDch"].Value = Guid.Empty;
            else
                command.Parameters["@IDch"].Value = Guid.Parse(IDKlcentre);

             SqlCommand sqlCommand;

            command.ExecuteNonQuery();
            commandText = "SELECT [Id Классификатора] FROM [РазделКлассификатор]  WHERE [id_Записи_кл1]=@IDc AND [id_Записи_кл2]=@IDd AND [id_Записи_кл3]=@IDk AND [id_Записи_кл4]=@IDch";
            command.CommandText = commandText;
            command.ExecuteNonQuery();
            SqlDataReader dataReader1 = command.ExecuteReader();
            dataReader1.Read();
            IDclass= Convert.ToString(dataReader1["Id Классификатора"]);
            dataReader1.Close();

            sqlCommand = new SqlCommand($"INSERT INTO [Тип] ([Название], [id Классификатора]) VALUES (N'{comboBox2.Text}', '{IDclass}')", sqlConnection);
            sqlCommand.ExecuteNonQuery();

            sqlCommand = new SqlCommand($"SELECT [id ] FROM [Тип]  WHERE [Название]=N'{comboBox2.Text}' AND [id Классификатора]='{IDclass}'", sqlConnection); 
            dataReader1 = sqlCommand.ExecuteReader();
            dataReader1.Read();
            IDtype = Convert.ToString(dataReader1["id "]);
            dataReader1.Close();

            sqlCommand = new SqlCommand($"INSERT INTO [Товар] ([Название], [Цена], [Тип товара]) VALUES (N'{textBox13.Text}', N'{textBox10.Text}', '{IDtype}')", sqlConnection);
            sqlCommand.ExecuteNonQuery();

            this.Close();
        }
    }
}
