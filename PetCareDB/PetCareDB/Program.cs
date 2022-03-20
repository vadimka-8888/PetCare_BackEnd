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
            /*
            DateTime d = DateTime.Parse("05.03.2018");
            string mes = PetCareMethods.RegisterPet(2002, "Кот/Кошка", "Рита", null, d, "W", 3, "Чёрный", null);
            WriteLine(mes);
            mes = PetCareMethods.RegisterIllness(3, "Простуда", DateTime.Parse("12.05.2020"), DateTime.Parse("17.05.2020"));
            WriteLine(mes);
            mes = PetCareMethods.RegisterMention(2002, "Покормить Риту", DateTime.Parse("16.02.2022"), TimeSpan.Parse("17:00:00"));
            WriteLine(mes);
            mes = PetCareMethods.RegisterNote(2002, "Купить когтеточку для Риты", DateTime.Parse("12.03.2022"));
            WriteLine(mes);
            */

            /*
            string mes = PetCareMethods.EnterUserProfile("YYY.ru", "fff");
            //Console.OutputEncoding = Encoding.Unicode;

            SendQuery qqq = JsonSerializer.Deserialize<SendQuery>(mes);
            WriteLine(qqq.result);
            foreach (var x in qqq.data)
                WriteLine(x);

            ReadLine();
            
            string response;
            string query_to_base = Test1();
            QueryInformation query = JsonSerializer.Deserialize<QueryInformation>(query_to_base);
            UserInformation u_inf = JsonSerializer.Deserialize<UserInformation>(query.data);
            response = PetCareMethods.RegisterUser(u_inf.fname, u_inf.lname, u_inf.email, u_inf.password, u_inf.district, u_inf.confirmation);
            WriteLine(response);
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
                    case "ent_user":
                        EmailPassword ep_inf = JsonSerializer.Deserialize<EmailPassword>(query.data);
                        response = PetCareMethods.EnterUserProfile(ep_inf.email, ep_inf.password);
                        break;
                    case "reg_animal":
                        PetInformation p_inf = JsonSerializer.Deserialize<PetInformation>(query.data);
                        response = PetCareMethods.RegisterPet(p_inf.user_id, p_inf.animal, p_inf.name, p_inf.breed, p_inf.date_of_birth, p_inf.gender, p_inf.weight, p_inf.color, p_inf.photo);
                        break;
                    case "reg_note":
                        NoteInformation n_inf = JsonSerializer.Deserialize<NoteInformation>(query.data);
                        response = PetCareMethods.RegisterNote(n_inf.user_id, n_inf.n_text, n_inf.date);
                        break;
                    case "reg_overexposure":
                        OverexposureInformation o_inf = JsonSerializer.Deserialize<OverexposureInformation>(query.data);
                        response = PetCareMethods.RegisterOverexposure(o_inf.user_id, o_inf.animal, o_inf.o_note, o_inf.cost);
                        break;
                    case "upd_user_email":
                        UpdateStringField f1_inf = JsonSerializer.Deserialize<UpdateStringField>(query.data);
                        response = PetCareMethods.UpdateEmail(f1_inf.id, f1_inf.inf_for_update);
                        break;
                    case "upd_user_district":
                        UpdateStringField f2_inf = JsonSerializer.Deserialize<UpdateStringField>(query.data);
                        response = PetCareMethods.UpdateDistrict(f2_inf.id, f2_inf.inf_for_update);
                        break;
                    case "upd_over_list":
                        int id = JsonSerializer.Deserialize<int>(query.data);
                        response = PetCareMethods.UpdateOverexposureDataList(id);
                        break;
                    case "upd_user_state":
                        UpdateBoolField f3_inf = JsonSerializer.Deserialize<UpdateBoolField>(query.data);
                        response = PetCareMethods.UpdateOverexposureState(f3_inf.id, f3_inf.inf_for_update);
                        break;
                    default:
                        break;
                }

                SendQuery q = JsonSerializer.Deserialize<SendQuery>(response);
                foreach (var x in q.data)
                    WriteLine(x);
                listener.Send(Encoding.UTF8.GetBytes(response));

                listener.Shutdown(SocketShutdown.Both);
                listener.Close();
            }
            
        }

    }
}
