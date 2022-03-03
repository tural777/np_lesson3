using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

namespace cs_TcpClient
{
    class Program
    {
        static void Main1(string[] args)
        {
            var client = new TcpClient();
            client.Connect("127.0.0.1", 45678);

            var stream = client.GetStream();


            // way 1
            // FileStream fs = new FileStream("novruzbayram.jpg", FileMode.Open, FileAccess.Read);
            // fs.CopyTo(stream);
            // stream.Close();
            // fs.Close();


            // way 2
            var bytes = File.ReadAllBytes("novruzbayram.jpg");
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();

        }



        static void Main(string[] args)
        {
            var client = new TcpClient();
            client.Connect("127.0.0.1", 45678);
            var stream = client.GetStream();

            BinaryReader br = new BinaryReader(stream);
            BinaryWriter bw = new BinaryWriter(stream);

            
            Command command = null;
            string input;
            string[] inputs = null;

            do
            {
                //Console.Clear();
                Console.Write("\nInput: ");
                input = Console.ReadLine().ToLower();

                inputs = input.Split(' ');


                if (inputs[0] == Command.HELP)
                {
                    Console.WriteLine(@$"
{Command.PROCLIST}
{Command.KILL} <procname>
{Command.RUN} <procname>
{Command.HELP}");
                    continue;
                }


                command = new Command { Text = inputs[0] };

                switch (command.Text)
                {
                    case Command.PROCLIST:

                        var cmdJsonStr = JsonSerializer.Serialize(command);
                        bw.Write(cmdJsonStr);

                        var processNamesStr = br.ReadString();
                        var processNamesArr = JsonSerializer.Deserialize<string[]>(processNamesStr);

                        foreach (var processName in processNamesArr)
                        {
                            Console.WriteLine(processName);
                        }
                        break;






                    case Command.KILL:
                        command.Param = inputs[1];
                        // code...
                        break;





                    case Command.RUN:
                        command.Param = inputs[1];
                        // code...
                        break;
                }

            } while (true);



        }
    }
}
