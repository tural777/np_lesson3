using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

namespace cs_TcpServer
{
    class Program
    {
        static void Main1()
        {

            var ip = IPAddress.Parse("127.0.0.1");
            var port = 45678;

            var listener = new TcpListener(ip, port);

            listener.Start(10);
            var client = listener.AcceptTcpClient();
            var stream = client.GetStream();


            FileStream fs = new FileStream("img.jpg", FileMode.Create, FileAccess.Write);
            stream.CopyTo(fs);

            stream.Close();
            fs.Close();

        }




        static void Main()
        {
            var ip = IPAddress.Parse("127.0.0.1");
            var port = 45678;

            var listener = new TcpListener(ip, port);

            listener.Start(10);

            var client = listener.AcceptTcpClient();
            var stream = client.GetStream();

            BinaryReader br = new BinaryReader(stream);
            BinaryWriter bw = new BinaryWriter(stream);

            Command command = null;

            while (true)
            {
                var commandJsonStr = br.ReadString();
                command = JsonSerializer.Deserialize<Command>(commandJsonStr);


                switch (command.Text)
                {
                    case Command.PROCLIST:
                        var processNames = Process.GetProcesses().Select(p => p.ProcessName);
                        var jsonProcessNames = JsonSerializer.Serialize(processNames);
                        bw.Write(jsonProcessNames);
                        break;






                    case Command.KILL:
                        break;

                    case Command.RUN:
                        break;
                }
            }







        }
    }
}
