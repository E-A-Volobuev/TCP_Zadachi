using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCP_Chat
{
    public partial class SozdZad : Form
    {
        public ConnectServer serv { get; set; }//соединение с сервером
        public SozdZad(ConnectServer sv,string [] names)
        {
            InitializeComponent();
            serv = sv;
            comboBox1.Items.AddRange(names);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string otdel = textBox3.Text;
            string stadia = "";
            if (radioButton1.Checked == true)
            {
                stadia = "ПД";
            }
            else
            {
                stadia = "РД";
            }
            string shifr = textBox2.Text;
            string obj = textBox1.Text;
            string priorit = textBox4.Text;
            DateTime time = dateTimePicker1.Value;
            string ispoln = comboBox1.SelectedItem.ToString();
            string comment = textBox6.Text;
            string[] data = new string[] { otdel, stadia, shifr, obj, priorit, time.ToString(), ispoln, comment };
            string json = JsonConvert.SerializeObject(data);
            serv.SendMessage("_нов.зад");
            serv.SendMessage(json);
            string mess = serv.ReceiveMessage();

        }
    }
}
