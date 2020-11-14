using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Chathost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(WCF_CHAT.ServiceChater)))
            {
                host.Open();
                Console.ReadLine();
                host.Close();
            }
            
            Console.ReadLine();
        }
    }
}
