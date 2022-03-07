using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using PetCareDB.EF;
using static System.Console;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace PetCareDB
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            string mes = PetCareMethods.RegisterUser("Валерия", "Мороз", "YYY.ru", "fff", "Советский", false);
            WriteLine(mes);
            */

            /*
            Regex r = new Regex(@"\d+");
            int k = int.Parse(r.Match(mes).ToString());
            DateTime d = DateTime.Parse("01.09.2021");
            mes = PetCareMethods.RegisterPet(1002, "Ёж", "Куки", null, d, "M", 3, "Серый", null);
            WriteLine(mes);
            ReadLine();
            */
            
            const string ip = "127.0.0.1";
            const int port = 8080;

            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            tcpSocket.Bind(tcpEndPoint);
            tcpSocket.Listen(6);

            while (true)
            {
                var listener = tcpSocket.Accept();
                var buffer = new byte[256];
                var size = 0;
                var data = new StringBuilder();

                do
                {
                    size = listener.Receive(buffer);
                    data.Append(Encoding.UTF8.GetString(buffer, 0, size));
                }
                while (listener.Available > 0);

                string query_to_base = data.ToString();
                WriteLine("Recieved query is " + query_to_base);
                WriteLine();
                QueryInformation query = JsonSerializer.Deserialize<QueryInformation>(query_to_base);
                string response = "";
                
                switch (query.action)
                {
                    case "reg_user":
                        UserInformation u_inf = JsonSerializer.Deserialize<UserInformation>(query.data);
                        response = PetCareMethods.RegisterUser(u_inf.fname, u_inf.lname, u_inf.email, u_inf.password, u_inf.district, u_inf.confirmation);
                        break;
                    default:
                        break;
                }

                listener.Send(Encoding.UTF8.GetBytes(response));

                listener.Shutdown(SocketShutdown.Both);
                listener.Close();
            }
            

        }
    }
}
