using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCP_Chat
{
    public partial class Registr : Form
    {

        ConnectServer serv = new ConnectServer();
        public string Otvet { get; set; }// ответ от сервера
        public Registr()
        {
            InitializeComponent();
            serv.Process();
        }

        private void Registr_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string loginAut = textBox1.Text;
            string passwordAut = textBox2.Text;
            string[] data = new string[] { loginAut, passwordAut };
            string json = JsonConvert.SerializeObject(data);

            ConnectServer serv = new ConnectServer();
            serv.SendMessage("enter");
            serv.SendMessage(json);
            string message = serv.ReceiveMessage();
            if (message == "YesEnter")
            {
                MessageBox.Show("Вход корректный");
                message = serv.ReceiveMessage();
                Zadacha[] zadachi = JsonConvert.DeserializeObject<Zadacha[]>(message);
                MyZadachi zd = new MyZadachi(serv, zadachi, loginAut);
                zd.Show();
                Hide();
              
            }
            if (message == "NoEnter")
            {
                MessageBox.Show("Неверный логин или пароль");
            }

        }
       

        private void button2_Click(object sender, EventArgs e)
        {
            string loginAut = textBox3.Text;
            string passwordAut = textBox4.Text;
            string[] data = new string[] { loginAut, passwordAut };
            string json = JsonConvert.SerializeObject(data);

            ConnectServer serv = new ConnectServer();
            serv.SendMessage("reg");
            serv.SendMessage(json);
            string message = serv.ReceiveMessage();
            if(message== "YesReg")
            {
                MessageBox.Show("Регистрация прошла успешно!\nВведите логин и пароль!");
            }
            if (message == "NameErr")
            {
                MessageBox.Show("Логин с таким именем существует!\nВведите другой логин!");
            }
        }

    }
}

