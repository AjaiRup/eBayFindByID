using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using eBayFindByID.FindingAPI;

namespace eBayFindByID
{

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                UserInput.Start();
                Request.New();
            }
            catch (Exception e)
            {
                Console.WriteLine("Apologies, the following error has occurred:\n");
                Console.WriteLine(e);
                Console.ReadLine();
            }

        }       
    }
}
