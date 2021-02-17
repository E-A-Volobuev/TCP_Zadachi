using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCP_Chat
{
    public class ConnectServer
    {

        private const string host = "192.168.0.142";
        private const int port = 8888;
        static TcpClient client;
        static NetworkStream stream;


        // получение сообщений
        public string ReceiveMessage()
        {
  
            BinaryReader reader = new BinaryReader(stream);
           
                string message = reader.ReadString();
                return message;

        }
        //отправка сообщений
        public void SendMessage(string text)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(text);
            writer.Flush();

        }
        //подключение к серверу 
        public void Process()
        {
            client = new TcpClient();
         
                client.Connect(host, port); //подключение клиента
                stream = client.GetStream(); // получаем поток

        }
        //получение списка клиентов,которые онлайн
        //public string  GetOnlineUser()
        //{

            


        //}


    }
}
