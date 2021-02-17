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
    public partial class MyZadachi : Form
    {
        public Zadacha [] zadachi { get; set; } // массив задач,полученный с сервера
        public Zadacha[] ishod { get; set; }// массив исходящих задач
        public Zadacha[] vyp{ get; set; }// массив выполненных задач
        public Zadacha[] tek { get; set; }// массив текущих задач
        public Zadacha[] vhod { get; set; }// массив входящих задач
        public string User { get; set; } // имя пользователя
        public ConnectServer serv { get; set; }//соединение с сервером
        public MyZadachi(ConnectServer sv,Zadacha[]zd, string username)
        {
            User = username;
            InitializeComponent();
            zadachi = zd;
            serv = sv;
            DataGrid(zd,dataGridView1);
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            linkLabel3.LinkClicked += linkLabel3_LinkClicked;
            linkLabel4.LinkClicked += linkLabel4_LinkClicked;
            this.Closing += new CancelEventHandler(MyZadachi_Closing);
            label7.Text = username;
            CountZadach();
        }
        private void CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ( e.RowIndex >= 0&& e.RowIndex<zadachi.Count())
            {
                var dt = this.Controls.OfType<DataGridView>().FirstOrDefault();
                var a = dt[0, e.RowIndex].Value.ToString();//отдел
                var b = dt[1, e.RowIndex].Value.ToString();//стадия
                var c = dt[2, e.RowIndex].Value.ToString();//шифр
                var d = dt[3, e.RowIndex].Value.ToString();//приоритет
                var f = dt[4, e.RowIndex].Value.ToString();//автор
                Zadacha zd_ = zadachi.FirstOrDefault(x => x.Otdel == a && x.Stadia == b && x.Shifr == c && x.Prioritet == d && x.Avtor == f);
                OknoZadachi okno = new OknoZadachi(serv, zd_);
                okno.Show();
            }
            else
            {
                MessageBox.Show("Задача не найдена");
            }
        }
        public void DataGrid(Zadacha[] zd,DataGridView date1)
        {

            string[] nameColumn = new string[] { "Исх.отдел", "Стадия", "Шифр", "Приоритет", "Автор","Готово","Принято" };
            DataGridViewTextBoxColumn[] column = new DataGridViewTextBoxColumn[nameColumn.Count()];
            for (int i = 0; i < nameColumn.Count(); i++)
            {
                column[i] = new DataGridViewTextBoxColumn();
                column[i].HeaderText = nameColumn[i];
                column[i].Name = nameColumn[i];
            }
            var stolb = column.Where(x => x.Name == "Шифр").FirstOrDefault();
            stolb.Width = 200;
            var stolb1 = column.Where(x => x.Name == "Автор").FirstOrDefault();
            stolb1.Width = 100;
            int size = 0;
            foreach (var s in column)
            {
                size += s.Width;
            }
            if(zd.Count()>0)
            {
                date1.Columns.AddRange(column);
                date1.Rows.Add(zd.Count());

                for (int i = 0; i < zd.Count(); i++)
                {


                    date1.Rows[i].Cells[0].Value = zd[i].Otdel;
                    date1.Rows[i].Cells[1].Value = zd[i].Stadia;
                    date1.Rows[i].Cells[2].Value = zd[i].Shifr;
                    date1.Rows[i].Cells[3].Value = zd[i].Prioritet;
                    date1.Rows[i].Cells[4].Value = zd[i].Avtor;
                    date1.Rows[i].Cells[5].Value = zd[i].Gotovnost;
                    date1.Rows[i].Cells[6].Value = zd[i].Prinyato;
                }
            }
           
            date1.CellMouseDoubleClick += CellMouseDoubleClick;
            date1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            date1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            date1.Size = new Size(size + 42, 250);
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            Obnovl(tek);

        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Obnovl(ishod);
        }
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Obnovl(vyp);

        }
        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (vhod.Count()>0)
            {
                Obnovl(vhod);
            }

        }
        public void CountZadach()
        {
            var listPrinyato = zadachi.Where(x => x.Prinyato == true&&x.Gotovnost==false);// список принятых задач
            tek = listPrinyato.ToArray();
            var listIshod = zadachi.Where(x => x.Avtor == User);// список исходящих задач
            ishod = listIshod.ToArray();
            var listVyp = zadachi.Where(x => x.Gotovnost == true);// список готовых задач
            vyp = listVyp.ToArray();
            var listVhod = zadachi.Where(x => x.Prinyato == false);// список входящих задач
            vhod = listVhod.ToArray();
            label2.Text = "(" + listPrinyato.Count().ToString() + ")";
            label3.Text = "(" + listIshod.Count().ToString() + ")";
            label4.Text = "(" + listVyp.Count().ToString() + ")";
            label6.Text = "(" + listVhod.Count().ToString() + ")";
        }// количество задач
        private void MyZadachi_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serv.SendMessage("полз.");
            string message = serv.ReceiveMessage();
            string[] names= JsonConvert.DeserializeObject<string[]>(message);
            SozdZad zd = new SozdZad(serv,names);
            zd.Show();

        }
        //обновление поля таблицы
        public void Obnovl(Zadacha[] zd)
        {
            var dt = this.Controls.OfType<DataGridView>().FirstOrDefault();
            this.Controls.Remove(dt);
            this.Refresh();
            DataGridView date1 = new DataGridView();
            date1.Location = new Point(166, 144);
            this.Controls.Add(date1);
            DataGrid(zd, date1);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            serv.SendMessage("обнов.зад.");
            string message = serv.ReceiveMessage();
            Zadacha[] new_zadachi = JsonConvert.DeserializeObject<Zadacha[]>(message);
            zadachi = new_zadachi;
            CountZadach();
            Obnovl(zadachi);
        }
    }
}
