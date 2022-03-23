using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

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
                Console.WriteLine("Press something to start tests");
                Console.ReadLine();
                Console.WriteLine("You are suggested to send differen applications. To choose press a b c d e f g h i j k");
                string test = Console.ReadLine();

                string message = "NOTEST";
                switch (test)
                {
                    case "a":
                        message = Test1();
                        break;
                    case "b":
                        message = Test3();
                        break;
                    case "c":
                        message = Test4();
                        break;
                    case "d":
                        message = Test5();
                        break;
                    case "e":
                        message = Test6();
                        break;
                    case "f":
                        message = Test7();
                        break;
                    case "g":
                        message = Test8();
                        break;
                    case "h":
                        message = Test9();
                        break;
                    case "i":
                        message = Test10();
                        break;
                    case "j":
                        message = Test2();
                        break;
                    case "k":
                        message = Test11();
                        break;
                    default:
                        break;
                }

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

        static string Test1()
        {
            //Test 1
            PetCareDB.UserInformation u_inf = new PetCareDB.UserInformation
            {
                fname = "Катя",
                lname = "Веселова",
                email = "ddd@mail.ru",
                password = "kat",
                district = "Ворошиловский",
                confirmation = true
            };
            string message_basic = JsonSerializer.Serialize(u_inf);
            Console.WriteLine("temporary test-message 1" + message_basic);

            PetCareDB.QueryInformation query = new PetCareDB.QueryInformation
            {
                action = "reg_user",
                data = message_basic
            };
            string mes = JsonSerializer.Serialize(query);
            Console.WriteLine("Ready to send");
            Console.WriteLine();
            return mes;
        }

        static string Test2()  //test for receiving data for account
        {
            PetCareDB.EmailPassword ep = new PetCareDB.EmailPassword
            {
                email = "ddd@mail.ru",
                password = "rrr"
            };
            string message_basic = JsonSerializer.Serialize(ep);
            Console.WriteLine("temporary test-message 2" + message_basic);

            PetCareDB.QueryInformation query = new PetCareDB.QueryInformation
            {
                action = "ent_user",
                data = message_basic
            };
            string mes = JsonSerializer.Serialize(query);
            Console.WriteLine("Ready to send");
            Console.WriteLine();
            return mes;
        }

        static string Test3()  //test for erorr due to wrong password
        {
            PetCareDB.EmailPassword ep = new PetCareDB.EmailPassword
            {
                email = "XXX.ru",
                password = "kuku"
            };
            string message_basic = JsonSerializer.Serialize(ep);
            Console.WriteLine("temporary test-message 3" + message_basic);

            PetCareDB.QueryInformation query = new PetCareDB.QueryInformation
            {
                action = "ent_user",
                data = message_basic
            };
            string mes = JsonSerializer.Serialize(query);
            Console.WriteLine("Ready to send");
            Console.WriteLine();
            return mes;
        }

        static string Test4()
        {
            PetCareDB.PetInformation p_inf = new PetCareDB.PetInformation
            {
                animal = "Собака",
                breed = "Хаски",
                user_id = 5,
                name = "Аристотель",
                date_of_birth = new DateTime(2015, 5, 15),
                gender = "M",
                weight = 5,
                color = "Серый",
                photo = null
            };
            string message_basic = JsonSerializer.Serialize(p_inf);
            Console.WriteLine("temporary test-message 4" + message_basic);

            PetCareDB.QueryInformation query = new PetCareDB.QueryInformation
            {
                action = "reg_animal",
                data = message_basic
            };
            string mes = JsonSerializer.Serialize(query);
            Console.WriteLine("Ready to send");
            Console.WriteLine();
            return mes;
        }

        static string Test5()
        {
            PetCareDB.NoteInformation n_inf = new PetCareDB.NoteInformation
            {
                user_id = 2002,
                date = new DateTime(2022,03,20),
                n_text = "Завести нового питомца. Срочно!"
            };
            string message_basic = JsonSerializer.Serialize(n_inf);
            Console.WriteLine("temporary test-message 5" + message_basic);

            PetCareDB.QueryInformation query = new PetCareDB.QueryInformation
            {
                action = "reg_note",
                data = message_basic
            };
            string mes = JsonSerializer.Serialize(query);
            Console.WriteLine("Ready to send");
            Console.WriteLine();
            return mes;
        }

        static string Test6()
        {
            PetCareDB.OverexposureInformation o_inf = new PetCareDB.OverexposureInformation
            {
                animal = "Кот/Кошка",
                cost = 1200,
                o_note = "Превезите с собой наполнитель",
                user_id = 1002
            };
            string message_basic = JsonSerializer.Serialize(o_inf);
            Console.WriteLine("temporary test-message 6" + message_basic);

            PetCareDB.QueryInformation query = new PetCareDB.QueryInformation
            {
                action = "reg_overexposure",
                data = message_basic
            };
            string mes = JsonSerializer.Serialize(query);
            Console.WriteLine("Ready to send");
            Console.WriteLine();
            return mes;
        }

        static string Test7()
        {
            PetCareDB.OverexposureInformation o_inf = new PetCareDB.OverexposureInformation
            {
                animal = "Собака",
                cost = 1400,
                o_note = "Оплата только наличными",
                user_id = 1002
            };
            string message_basic = JsonSerializer.Serialize(o_inf);
            Console.WriteLine("temporary test-message 7" + message_basic);

            PetCareDB.QueryInformation query = new PetCareDB.QueryInformation
            {
                action = "reg_overexposure",
                data = message_basic
            };
            string mes = JsonSerializer.Serialize(query);
            Console.WriteLine("Ready to send");
            Console.WriteLine();
            return mes;
        }

        static string Test8()
        {
            PetCareDB.OverexposureInformation o_inf = new PetCareDB.OverexposureInformation
            {
                animal = "Кот/Кошка",
                cost = 700,
                o_note = "Могу оставить вашего питомца не более, чем на неделю",
                user_id = 2002
            };
            string message_basic = JsonSerializer.Serialize(o_inf);
            Console.WriteLine("temporary test-message 8" + message_basic);

            PetCareDB.QueryInformation query = new PetCareDB.QueryInformation
            {
                action = "reg_overexposure",
                data = message_basic
            };
            string mes = JsonSerializer.Serialize(query);
            Console.WriteLine("Ready to send");
            Console.WriteLine();
            return mes;
        }

        static string Test9()
        {
            PetCareDB.UpdateBoolField field = new PetCareDB.UpdateBoolField
            {
                id = 5,
                inf_for_update = true
            };
            string message_basic = JsonSerializer.Serialize(field);
            Console.WriteLine("temporary test-message 9" + message_basic);

            PetCareDB.QueryInformation query = new PetCareDB.QueryInformation
            {
                action = "upd_user_state",
                data = message_basic
            };
            string mes = JsonSerializer.Serialize(query);
            Console.WriteLine("Ready to send");
            Console.WriteLine();
            return mes;
        }

        static string Test10()
        {
            PetCareDB.UpdateBoolField field = new PetCareDB.UpdateBoolField
            {
                id = 1002,
                inf_for_update = true
            };
            string message_basic = JsonSerializer.Serialize(field);
            Console.WriteLine("temporary test-message 10" + message_basic);

            PetCareDB.QueryInformation query = new PetCareDB.QueryInformation
            {
                action = "upd_user_state",
                data = message_basic
            };
            string mes = JsonSerializer.Serialize(query);
            Console.WriteLine("Ready to send");
            Console.WriteLine();
            return mes;
        }

        static string Test11()
        {
            PetCareDB.EmailPassword ep = new PetCareDB.EmailPassword
            {
                email = "XXX.ru",
                password = "t1t1"
            };
            string message_basic = JsonSerializer.Serialize(ep);
            Console.WriteLine("temporary test-message 2" + message_basic);

            PetCareDB.QueryInformation query = new PetCareDB.QueryInformation
            {
                action = "ent_user",
                data = message_basic
            };
            string mes = JsonSerializer.Serialize(query);
            Console.WriteLine("Ready to send");
            Console.WriteLine();
            return mes;
        }
    }
}