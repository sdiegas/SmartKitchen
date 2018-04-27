using System;
using System.ServiceModel;
using Hsr.CloudSolutions.SmartKitchen.Util;

namespace Hsr.CloudSolutions.SmartKitchen.Service
{
    class Program
    {
        static void Main()
        {
            using (var host = new ServiceHost(typeof (SmartKitchenService)))
            {
                try
                {
                    host.Open();
                    Console.WriteLine("SmartKitchenService is running...");

                    Console.WriteLine();
                    Console.WriteLine("Press ENTER to stop the service!");
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An exception occurred while running the SmartKitchenService...");
                    Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");

                    Exception innerstException = ex.GetInnerstException();
                    if (innerstException != null)
                    {
                        Console.WriteLine($"Innerst Exception: {innerstException.GetType().Name} => {innerstException.Message}");
                    }

                    Console.WriteLine();
                    Console.WriteLine("Press ENTER to kill the service!");
                    Console.ReadLine();
                }
            }
        }
    }
}
