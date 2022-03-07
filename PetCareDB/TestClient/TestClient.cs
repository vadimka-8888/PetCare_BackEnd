using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace TestClient
{
    class TestClient
    {
        static void Main(string[] args)
        {
            const string ip = "127.0.0.1";
            const int port = 8080;

            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            while (true)
            {
                var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Console.ReadLine();
                PetCareDB.UserInformation u_inf = new PetCareDB.UserInformation
                {
                    fname = "Екатерина",
                    lname = "Доц",
                    email = "ddd@mail.ru",
                    password = "rrr",
                    district = "Советский",
                    confirmation = false
                };
                string message_basic = JsonSerializer.Serialize(u_inf);
                PetCareDB.QueryInformation query = new PetCareDB.QueryInformation
                {
                    action = "reg_user",
                    data = message_basic
                };
                string message = JsonSerializer.Serialize(query);
                Console.WriteLine("Ready to send " + message);
                Console.WriteLine();

                byte[] data = Encoding.UTF8.GetBytes(message);
                tcpSocket.Connect(tcpEndPoint);
                tcpSocket.Send(data);

                byte[] buffer = new byte[256];
                int size = 0;
                var answer = new StringBuilder();

                do
                {
                    size = tcpSocket.Receive(buffer);
                    answer.Append(Encoding.UTF8.GetString(buffer));
                }
                while (tcpSocket.Available > 0);

                Console.WriteLine(answer.ToString());
                Console.WriteLine();

                tcpSocket.Shutdown(SocketShutdown.Both);
                tcpSocket.Close();
            }
        }
    }
}