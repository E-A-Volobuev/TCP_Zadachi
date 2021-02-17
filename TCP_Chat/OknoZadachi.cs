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
    public partial class OknoZadachi : Form
    {
        public Zadacha Zd { get; set; }//задача
        public ConnectServer serv { get; set; }//соединение с сервером
        public OknoZadachi(ConnectServer sv,Zadacha zd)
        {
            InitializeComponent();
            Zd = zd;
            ShifrLabel.Text = zd.Shifr;
            NameLabel.Text = zd.Obj;
            Stadialabel.Text = zd.Stadia;
            DataLabel.Text = zd.Srok.ToString();
            SrokLabel.Text = zd.Prioritet;
            RazdelLabel.Text = zd.Otdel;
            AvtorLabel.Text = zd.Avtor;
            IspolnLabel.Text = zd.Ispolniteli;
            textBox1.ReadOnly = true;
            textBox1.Text = zd.Comment;
            serv = sv;
            if(zd.Prinyato==true)
            {
                var button=this.Controls.OfType<Button>().FirstOrDefault(x=>x.Name=="button1");
                var button_2 = this.Controls.OfType<Button>().FirstOrDefault(x => x.Name == "button3");
                this.Controls.Remove(button);
                this.Controls.Remove(button_2);
                this.Refresh();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serv.SendMessage("_PRIN");
            serv.SendMessage("ПРИНЯТО");
            serv.SendMessage(Zd.Id.ToString());//ОТПРАВЛЯЕМ ID задачи
            string mes = serv.ReceiveMessage();//получаем ответ о выполнении команды
            MessageBox.Show(mes);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            serv.SendMessage("_PRIN");
            serv.SendMessage("ОТКЛОНИТЬ");
            serv.SendMessage(Zd.Id.ToString());//ОТПРАВЛЯЕМ ID задачи
            string mes = serv.ReceiveMessage();//получаем ответ о выполнении команды
            MessageBox.Show(mes);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serv.SendMessage("_PRIN");
            serv.SendMessage("выполн");
            serv.SendMessage(Zd.Id.ToString());//ОТПРАВЛЯЕМ ID задачи
            string mes = serv.ReceiveMessage();//получаем ответ о выполнении команды
            MessageBox.Show(mes);
        }


    }
}
