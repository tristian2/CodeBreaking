using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CodeBreaking
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://www2.mps.mpg.de/homes/heller/downloads/files/SETI_message.txt";

            using (WebClient client = new WebClient())
            {
                string path = @"c:\temp\MyTest.html";
                String originalMessage = client.DownloadString(url);
                int splitPosition = originalMessage.IndexOf("0");

                /*Console.WriteLine(originalMessage.IndexOf("0"));
                Console.Read();
                Console.WriteLine(originalMessage);*/

                StringBuilder newMessage = new StringBuilder();
                newMessage.Append("<html><head><style>body { width: 80ch;font-size: 20px; font-family: Monospace; }</style></head><body>");

                List<String> chunk = new List<String>();
                for (int i = 0; i < originalMessage.Length; i += splitPosition)
                {
                    if ((i + splitPosition) < originalMessage.Length)
                        chunk.Add(originalMessage.Substring(i, splitPosition));
                    else
                        chunk.Add(originalMessage.Substring(i));
                }

                foreach(var line in chunk)
                {
                    newMessage.Append("<div>"+line+"</div>");
                }
                newMessage.Replace("0", "&#9608");
                newMessage.Replace("1", "&#9633");

                File.AppendAllText(path, newMessage.ToString());
                Console.WriteLine(newMessage.ToString());
                Console.Read();
            }


        }
    }
}
