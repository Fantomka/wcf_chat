using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace chat_host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(wcf_chat.ChatService)))
            {
                host.Open();
                Console.WriteLine("Host starts!");
                Console.ReadLine();
            }
        }
    }
}
