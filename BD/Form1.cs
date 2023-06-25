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
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;

        List<string[]> list56;

        string IDpokepatel;
        string NAMEpok;
        string Yzakaz;

        int gl; 
        public Form1(int i)
        {
            InitializeComponent();
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            dataGridView3.Visible = false;
            dataGridView4.Visible = false;
            gl = i;
            if (i == 1) { tabPage2.Parent = null; button4.Parent = null; }
            if (i == 2) { button5.Enabled = false; button5.Parent = null; }
        }

 



          private void Form1_Load(object sender, EventArgs e)
           {

            #region Подключение таблицы
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SaveDB"].ConnectionString);
               sqlConnection.Open();
               if (sqlConnection.State == ConnectionState.Open)
                   MessageBox.Show("Подключение установлено!");
            #endregion

            SearchPage();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ((comboBox2.Text == "") && (comboBox3.Text == ""))
            {
                MessageBox.Show("Выберите покупателя или номер заказа");
            }
            else if (comboBox3.Text == "") 
            {
                string name = comboBox2.Text;

                SqlCommand sqlCommand1 = new SqlCommand($"SELECT [id Покупателя] FROM Покупатель WHERE [ФИО/Название компании]=N'{name}'", sqlConnection); 
                SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
                dataReader1.Read();
                IDpokepatel = Convert.ToString(dataReader1["id Покупателя"]);
                this.Hide();
                Form2 d = new Form2(name, IDpokepatel, gl);
                d.ShowDialog();
            }
            else
            {
                string name = comboBox2.Text;
                string zakaz = comboBox3.Text;
                string sostoynie = Sos(zakaz);
                this.Hide();
                Form3 d = new Form3(name, zakaz, sostoynie);
                if ((gl == 1)||(gl==3)) { d.button3.Parent = null; d.button4.Parent = null; d.button2.Parent = null; d.button1.Parent = null; }
                d.ShowDialog();
                
            }
        }
        private string Sos(string z)
        {
            string page;
            SqlCommand sqlCommand1 = new SqlCommand($"SELECT [Состояние] FROM Заказ WHERE [Номер заказа]='{z}'", sqlConnection);
            SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
            dataReader1.Read();
            page = Convert.ToString(dataReader1["Состояние"]);
            dataReader1.Close();
            return page;
        }

        private void SearchPage()
        {
            #region Раздел - поиск ФИО 
            List<string> list = new List<string>();
            string page;
            SqlCommand sqlCommand = new SqlCommand("SELECT [ФИО/Название компании] FROM Покупатель", sqlConnection);
            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            while (dataReader.Read())
            {
                page = Convert.ToString(dataReader["ФИО/Название компании"]);
                list.Add(page);
            }
            RefComBox2(list);
            dataReader.Close();
            #endregion

            #region Раздел - поиск заказ 
            List<string> list1 = new List<string>();
            string page1;
            SqlCommand sqlCommand1 = new SqlCommand("SELECT[Номер заказа] FROM Заказ", sqlConnection);
            SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
            while (dataReader1.Read())
            {
                page1 = Convert.ToString(dataReader1["Номер заказа"]);
                list1.Add(page1);
            }
            RefComBox3(list1);
            dataReader1.Close();
            #endregion
        }
        private void RefComBox2(List<string> list)
        {
            comboBox2.Items.Clear();
            foreach (string s in list)
            {
                comboBox2.Items.Add(s);
            }
        }
        private void RefComBox3(List<string> list)
        {
            comboBox3.Items.Clear();
            foreach (string s in list)
            {
                comboBox3.Items.Add(s);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
                List<string> list1 = new List<string>();
                string page1;
                string d = comboBox2.Text;
                SqlCommand sqlCommand1 = new SqlCommand($"SELECT [Номер заказа], [Заказ].[id Покупателя] FROM Заказ INNER JOIN Покупатель ON [Заказ].[id Покупателя]=[Покупатель].[id Покупателя] WHERE [ФИО/Название компании]=N'{d}'", sqlConnection);
                SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
                while (dataReader1.Read())
                {
                 page1 = Convert.ToString(dataReader1["Номер заказа"]);
              IDpokepatel = Convert.ToString(dataReader1["id Покупателя"]);
                list1.Add(page1);
                }
                RefComBox3(list1);
                dataReader1.Close();   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            #region Новый покупатель
            string name = textBox7.Text;
            long number = Convert.ToInt64(textBox5.Text);
            string adress = textBox6.Text;
            SqlCommand sqlCommand1 = new SqlCommand($"SELECT [id Покупателя] FROM Покупатель  WHERE [ФИО/Название компании]=N'{name}' AND [Номер телефона]={number}", sqlConnection);
            if (sqlCommand1.ExecuteScalar() != null)
            {
                MessageBox.Show("Покупатель найден!");
                button2.Enabled=false;

            }
            else
            {
                var result = MessageBox.Show("Создать нового покупателя?", name, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    return;
                }
                else
                {
                    SqlCommand sqlCommand = new SqlCommand($"INSERT INTO [Покупатель] ([ФИО/Название компании], [Номер телефона], Адрес) VALUES (N'{name}', {number}, N'{adress}')", sqlConnection);
                    button2.Enabled = false;
                    sqlCommand.ExecuteNonQuery();
                }
            }
            sqlCommand1 = new SqlCommand($"SELECT [id Покупателя] FROM Покупатель  WHERE [ФИО/Название компании]=N'{name}' AND [Номер телефона]={number}", sqlConnection);
            SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
            dataReader1.Read();
            IDpokepatel = Convert.ToString(dataReader1["id Покупателя"]);
            dataReader1.Close();

            string pass;
            char[] g;

                g = new char[4] { IDpokepatel.ToCharArray()[0], IDpokepatel.ToCharArray()[1], IDpokepatel.ToCharArray()[2], IDpokepatel.ToCharArray()[3] };
                pass = (g[0].ToString() + g[1].ToString() + g[2].ToString() + g[3].ToString());
            MessageBox.Show($"Пароль для входя в личный кабинет: {pass}") ;

            NAMEpok = name;
            #endregion 
        }

        private void comboBox2_TextUpdate(object sender, EventArgs e)
        {
            if (comboBox2.Text == "") {
                List<string> list1 = new List<string>();
                string page1;
                SqlCommand sqlCommand1 = new SqlCommand("SELECT[Номер заказа], [id Покупателя] FROM Заказ", sqlConnection);
                SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
                while (dataReader1.Read())
                {
                    page1 = Convert.ToString(dataReader1["Номер заказа"]);
                    IDpokepatel = Convert.ToString(dataReader1["id Покупателя"]);
                    list1.Add(page1);
                }
                RefComBox3(list1);
                dataReader1.Close();
            }
            else
            {
            
                    List<string> list1 = new List<string>();
                    string page1;
                    string d = comboBox2.Text;
                    SqlCommand sqlCommand1 = new SqlCommand($"SELECT[Номер заказа], [Заказ].[id Покупателя] FROM Заказ INNER JOIN Покупатель ON [Заказ].[id Покупателя]=[Покупатель].[id Покупателя] WHERE [ФИО/Название компании]=N'{d}'", sqlConnection);
                    SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
                    while (dataReader1.Read())
                    {
                        page1 = Convert.ToString(dataReader1["Номер заказа"]);
                        IDpokepatel = Convert.ToString(dataReader1["id Покупателя"]);
                        list1.Add(page1);
                    }
                    RefComBox3(list1);
                    dataReader1.Close();
                
            }
        }

        private void comboBox3_TextUpdate(object sender, EventArgs e)
        {             
            string d = comboBox3.Text;
            SqlCommand sqlCommand1 = new SqlCommand($"SELECT [ФИО/Название компании], [Id Покупателя] FROM Покупатель INNER JOIN Заказ ON [Покупатель].[id Покупателя]=[Заказ].[id Покупателя] WHERE [Номер заказа]='{d}'", sqlConnection);
            if (sqlCommand1.ExecuteScalar()!=null)
            {
                SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
                dataReader1.Read();
                comboBox2.Items.Clear();
                comboBox2.Text = Convert.ToString(dataReader1["ФИО/Название компании"]);
                IDpokepatel= Convert.ToString(dataReader1["Id Покупателя"]);
                dataReader1.Close();
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string d = comboBox3.Text;
            SqlCommand sqlCommand1 = new SqlCommand($"SELECT [ФИО/Название компании], [Заказ].[Id Покупателя] FROM Покупатель INNER JOIN Заказ ON [Покупатель].[id Покупателя]=[Заказ].[id Покупателя] WHERE [Номер заказа]='{d}'", sqlConnection);
            SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
            dataReader1.Read();
            comboBox2.Items.Clear();
            comboBox2.Text = Convert.ToString(dataReader1["ФИО/Название компании"]);
            IDpokepatel = Convert.ToString(dataReader1["Id Покупателя"]);
            dataReader1.Close();
        }
        //
        private void button1_Click(object sender, EventArgs e)
        {
            if (button2.Enabled == false)
            {
                string typeH;
                int countDoor, countRoom, countFloor, startFloor, S;
                if (checkBox1.Checked == true) typeH = checkBox1.Text;
                else if (checkBox2.Checked == true) typeH = checkBox2.Text;
                else if (checkBox3.Checked == true) typeH = checkBox3.Text;
                else if (checkBox4.Checked == true) typeH = checkBox4.Text;
                else
                {
                    MessageBox.Show("Выберите наиболее подходящий тип здания");
                    return;
                }
                try
                {
                    S = Convert.ToInt32(textBox1.Text);
                    countDoor = Convert.ToInt32(numericUpDown3.Value);
                    countFloor = Convert.ToInt32(numericUpDown1.Value);
                    countRoom = Convert.ToInt32(numericUpDown2.Value);
                    startFloor = Convert.ToInt32(textBox3.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка! Повторите правильность введенных данных");
                    return;
                }
                //создание проекта и заказа
                SqlCommand sqlCommand = new SqlCommand($"INSERT INTO Проект ([Тип здания], [Общий метраж], [Кол-во входных дверей], [Кол-во комнат], [Кол-во этажей], [Начальный этаж]) VALUES (N'{typeH}', {S}, {countDoor}, {countRoom}, {countFloor}, {startFloor})", sqlConnection);
                sqlCommand.ExecuteNonQuery();

                string IDProject;
                SqlCommand sqlCommand1 = new SqlCommand($"SELECT [id Проекта] FROM Проект INNER JOIN [Тип здания] ON [Проект].[Тип здания]=[Тип здания].[Тип здания] WHERE [Проект].[Тип здания]=N'{typeH}' AND [Общий метраж]={S} AND [Кол-во входных дверей]={countDoor} AND [Кол-во комнат]={countRoom} AND [Кол-во этажей]={countFloor} AND [Начальный этаж]={startFloor}", sqlConnection);
                SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
                dataReader1.Read();
                IDProject = Convert.ToString(dataReader1["id Проекта"]);
                dataReader1.Close();

                string NumberZakaz = ("PDJ-"+ IDpokepatel[0]+IDpokepatel[IDpokepatel.Length-1]+IDProject[0]+IDProject[IDpokepatel.Length-1]);

                sqlCommand = new SqlCommand($"INSERT INTO Заказ ([id Покупателя], [id Проекта], [Номер заказа], [Состояние]) VALUES ('{IDpokepatel}', '{IDProject}', '{NumberZakaz}', N'Создан')", sqlConnection);
                sqlCommand.ExecuteNonQuery();
                Yzakaz =NumberZakaz;



                string IDzkz;
                SqlCommand sqlCommand3 = new SqlCommand($"SELECT [id Заказа] FROM Заказ  WHERE [Номер заказа]='{Yzakaz}'", sqlConnection);
                SqlDataReader dataReader3 = sqlCommand3.ExecuteReader();
                dataReader3.Read();
                IDzkz = Convert.ToString(dataReader3["id Заказа"]);
                dataReader3.Close();

                sqlCommand = new SqlCommand($"INSERT INTO СписокТоваров ([id Заказа], [id Товара], [Кол-во]) VALUES ('{IDzkz}', 'd59ca77c-fa95-4a4b-bb61-b329c41bddab', {countDoor})", sqlConnection);
                sqlCommand.ExecuteNonQuery();

                sqlCommand = new SqlCommand($"INSERT INTO СписокТоваров ([id Заказа], [id Товара], [Кол-во]) VALUES ('{IDzkz}', '6320bb67-8d39-4b22-a43c-45e130885fd2', {countRoom})", sqlConnection);
                sqlCommand.ExecuteNonQuery();

                if (startFloor<3) {

                    sqlCommand = new SqlCommand($"INSERT INTO СписокТоваров ([id Заказа], [id Товара], [Кол-во]) VALUES ('{IDzkz}', '688c6da4-0cbb-4127-a17c-2b6417b97c83', {countRoom})", sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                }

                sqlCommand = new SqlCommand($"INSERT INTO СписокТоваров ([id Заказа], [id Товара], [Кол-во]) VALUES ('{IDzkz}', 'd2954fc3-6a27-4766-9ed4-1bbb838a6ce2', {Convert.ToInt32(S/150+1)})", sqlConnection);
                sqlCommand.ExecuteNonQuery();

                if ((S > 400) || (countRoom > 49))
                {
                    sqlCommand = new SqlCommand($"INSERT INTO СписокТоваров ([id Заказа], [id Товара], [Кол-во]) VALUES ('{IDzkz}', '2f93885e-89b4-4639-81a0-66985c4c5c4d', {countRoom})", sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                }



                tabPage2.Parent = null;
                tabPage3.Parent = null;
                Form3 d = new Form3(NAMEpok, NumberZakaz, "Создан");
                if ((gl == 1) || (gl == 3)) { d.button3.Parent = null; d.button4.Parent = null; d.button2.Parent = null; d.button1.Parent = null; }
                d.ShowDialog();
                button4.Enabled = true;
                tabPage2.Parent = tabControl1;
                tabPage3.Parent = tabControl1;
            }
            else
            {
                MessageBox.Show("Ошибка! Введите пользователя");
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
            checkBox1.Checked = false;
            checkBox4.Checked = false;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox1.Checked = false;
        }
        public void LoadSatelitesList1()
        {
            dataGridView2.Rows.Clear();
            list56 = new List<string[]>();
            string[] page1 = null;
            SqlCommand sqlCommand1 = new SqlCommand("SELECT Товар.Название, Цена, [Тип].[Название], [Классификатор камер].[Вид], [Классификатор камер].[Тип съемки], [Классификатор камер].[Разрешение], [Классификатор камер].[Область применения], [Классификатор камер].[Страна], [Классификатор камер].[Производитель], [Классификатор камер].[Угол обзора], [Классификатор камер].[Минимальная освещенность], [Классификатор камер].[Матрица], [Классификатор камер].[Материал], Товар.[Id Товара] FROM Товар INNER JOIN Тип ON[Товар].[Тип товара] =[Тип].[id] INNER JOIN РазделКлассификатор ON[Тип].[id Классификатора] =[РазделКлассификатор].[Id Классификатора] INNER JOIN[Классификатор камер] ON[РазделКлассификатор].[id_Записи_кл1] =[Классификатор камер].[id Записи] ", sqlConnection);
            SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
            while (dataReader1.Read())
            {
                page1 = new string[]
                {
                    Convert.ToString(dataReader1["Название"]),
                    Convert.ToString(dataReader1["Цена"]),
                    Convert.ToString(dataReader1["Название"]),
                    Convert.ToString(dataReader1["Вид"]),
                    Convert.ToString(dataReader1["Тип съемки"]),
                    Convert.ToString(dataReader1["Разрешение"]),
                    Convert.ToString(dataReader1["Область применения"]),
                    Convert.ToString(dataReader1["Страна"]),
                    Convert.ToString(dataReader1["Производитель"]),
                    Convert.ToString(dataReader1["Угол обзора"]),
                    Convert.ToString(dataReader1["Минимальная освещенность"]),
                    Convert.ToString(dataReader1["Матрица"]),
                    Convert.ToString(dataReader1["Материал"]),
                    Convert.ToString(dataReader1["Id Товара"])

                };
                list56.Add(page1);

            }
            foreach (var s in list56)
            {
                dataGridView2.Rows.Add(s[0], s[1], s[2], s[3], s[4], s[5], s[6], s[7], s[8], s[9], s[10], s[11], s[12]);
            }
            dataReader1.Close();
        }
        public void LoadSatelitesList2()
        {
            dataGridView1.Rows.Clear();
            list56= new List<string[]>();
            string [] page1=null;
            SqlCommand sqlCommand1 = new SqlCommand("SELECT Товар.Название, Цена, [Тип].[Название], [Классификатор датчиков].[Тип датчика], [Классификатор датчиков].[Угол обнаружения], [Классификатор датчиков].[Время доставки сигнала], [Классификатор датчиков].[Материал], [Классификатор датчиков].[Страна], [Классификатор датчиков].[Производитель], Товар.[Id Товара] FROM Товар INNER JOIN Тип ON[Товар].[Тип товара] =[Тип].[id] INNER JOIN РазделКлассификатор ON[Тип].[id Классификатора] =[РазделКлассификатор].[Id Классификатора] INNER JOIN[Классификатор датчиков] ON[РазделКлассификатор].[id_Записи_кл2] =[Классификатор датчиков].[Id Записи]", sqlConnection);
            SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
            while (dataReader1.Read())
            {
                page1 = new string[]
                {
                    Convert.ToString(dataReader1["Название"]),
                    Convert.ToString(dataReader1["Цена"]),
                    Convert.ToString(dataReader1["Название"]),
                    Convert.ToString(dataReader1["Тип датчика"]),
                    Convert.ToString(dataReader1["Угол обнаружения"]),
                    Convert.ToString(dataReader1["Время доставки сигнала"]),
                    Convert.ToString(dataReader1["Материал"]),
                    Convert.ToString(dataReader1["Страна"]),
                    Convert.ToString(dataReader1["Производитель"]),
                    Convert.ToString(dataReader1["Id Товара"])
                };
                list56.Add(page1);

            }
            foreach(var s in list56)
            {
                dataGridView1.Rows.Add(s[0], s[1], s[2], s[3], s[4], s[5], s[6], s[7], s[8]);
            }
            dataReader1.Close();
        }
        public void LoadSatelitesList3()
        {
            dataGridView4.Rows.Clear();
            list56 = new List<string[]>();
            string[] page1 = null;
            SqlCommand sqlCommand1 = new SqlCommand("SELECT Товар.Название, Цена, [Тип].[Название], [Классификатор центров].[Тип], [Классификатор центров].[Страна], [Классификатор центров].[Производитель], [Классификатор центров].[Кол-во подключаемых устр], Товар.[Id Товара] FROM Товар INNER JOIN Тип ON [Товар].[Тип товара] =[Тип].[id ] INNER JOIN РазделКлассификатор ON[Тип].[id Классификатора] =[РазделКлассификатор].[Id Классификатора] INNER JOIN[Классификатор центров] ON[РазделКлассификатор].[id_Записи_кл4] =[Классификатор центров].[Id Записи]", sqlConnection);
            SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
            while (dataReader1.Read())
            {
                page1 = new string[]
                {
                    Convert.ToString(dataReader1["Название"]),
                    Convert.ToString(dataReader1["Цена"]),
                    Convert.ToString(dataReader1["Название"]),
                    Convert.ToString(dataReader1["Тип"]),
                    Convert.ToString(dataReader1["Страна"]),
                    Convert.ToString(dataReader1["Производитель"]),
                    Convert.ToString(dataReader1["Кол-во подключаемых устр"]),
                    Convert.ToString(dataReader1["Id Товара"])
                };
                list56.Add(page1);

            }
            foreach (var s in list56)
            {
                dataGridView4.Rows.Add(s[0], s[1], s[2], s[3], s[4], s[5], s[6]);
            }
            dataReader1.Close();
        }
        public void LoadSatelitesList4()
        {
            dataGridView3.Rows.Clear();
            list56 = new List<string[]>();
            string[] page1 = null;
            SqlCommand sqlCommand1 = new SqlCommand("SELECT Товар.Название, Цена, [Тип].[Название], [Классификатор клавиатур].[Тип клавиатуры], [Классификатор клавиатур].[Страна], [Классификатор клавиатур].[Производитель], Товар.[Id Товара] FROM Товар INNER JOIN Тип ON [Товар].[Тип товара] =[Тип].[id ] INNER JOIN РазделКлассификатор ON [Тип].[id Классификатора] =[РазделКлассификатор].[Id Классификатора] INNER JOIN [Классификатор клавиатур] ON [РазделКлассификатор].[id_Записи_кл3] =[Классификатор клавиатур].[Id Записи]", sqlConnection);
            SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
            while (dataReader1.Read())
            {
                page1 = new string[]
                {
                    Convert.ToString(dataReader1["Название"]),
                    Convert.ToString(dataReader1["Цена"]),
                    Convert.ToString(dataReader1["Название"]),
                    Convert.ToString(dataReader1["Тип клавиатуры"]),
                    Convert.ToString(dataReader1["Страна"]),
                    Convert.ToString(dataReader1["Производитель"]),
                    Convert.ToString(dataReader1["Id Товара"])
                };
                list56.Add(page1);

            }
            foreach (var s in list56)
            {
                dataGridView3.Rows.Add(s[0], s[1], s[2], s[3], s[4], s[5]);
            }
            dataReader1.Close();
        }
            private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
            {
            Load89();
            }

        private void Load89()
        {
            if (comboBox1.Text == "Камеры") { dataGridView4.Visible = false; dataGridView3.Visible = false; dataGridView1.Visible = false; dataGridView2.Visible = true; LoadSatelitesList1(); }
            if (comboBox1.Text == "Датчики") { dataGridView4.Visible = false; dataGridView3.Visible = false; dataGridView2.Visible = false; dataGridView1.Visible = true; LoadSatelitesList2(); }
            if (comboBox1.Text == "Клавиатуры") { dataGridView4.Visible = false; dataGridView1.Visible = false; dataGridView2.Visible = false; dataGridView3.Visible = true; LoadSatelitesList4(); }
            if (comboBox1.Text == "Центры") { dataGridView1.Visible = false; dataGridView2.Visible = false; dataGridView3.Visible = false; dataGridView4.Visible = true; LoadSatelitesList3(); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string zkz;
            SqlCommand sqlCommand1 = new SqlCommand($"SELECT [Id Заказа] FROM Заказ WHERE [Номер заказа]='{Yzakaz}'", sqlConnection);
            SqlDataReader dataReader1 = sqlCommand1.ExecuteReader();
            dataReader1.Read();
            zkz = Convert.ToString(dataReader1["Id Заказа"]);
            dataReader1.Close();
            if (dataGridView1.Visible) 
            { 
                int rowindex = dataGridView1.CurrentRow.Index;
                string tovar = list56[rowindex][9];
                SqlCommand sqlCommand = new SqlCommand($"INSERT INTO СписокТоваров ([id Заказа], [id Товара], [Кол-во]) VALUES ('{zkz}', '{tovar}', 1)", sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }
            else if (dataGridView2.Visible)
            {
                int rowindex = dataGridView2.CurrentRow.Index;
                string tovar = list56[rowindex][13];
                SqlCommand sqlCommand = new SqlCommand($"INSERT INTO СписокТоваров ([id Заказа], [id Товара], [Кол-во]) VALUES('{zkz}', '{tovar}', 1)", sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }
            else
            if (dataGridView3.Visible)
            {
                int rowindex = dataGridView3.CurrentRow.Index;
                string tovar = list56[rowindex][6];
                SqlCommand sqlCommand = new SqlCommand($"INSERT INTO СписокТоваров ([id Заказа], [id Товара], [Кол-во]) VALUES('{zkz}', '{tovar}', 1)", sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }
            else
            if (dataGridView4.Visible)
            {
                int rowindex = dataGridView4.CurrentRow.Index;
                string tovar = list56[rowindex][7];
                SqlCommand sqlCommand = new SqlCommand($"INSERT INTO СписокТоваров ([id Заказа], [id Товара], [Кол-во]) VALUES('{zkz}', '{tovar}', 1)", sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }
            button4.Enabled = true;
            tabPage2.Parent = tabControl1;
            tabPage3.Parent = tabControl1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form5 d = new Form5();
            d.ShowDialog();
            Load89();
        }
    }
}
