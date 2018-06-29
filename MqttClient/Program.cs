using System;

namespace MqttClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string error = null;
            var mqtt = new Mqtt();
            uint[] buffer = { 14,  };
            uint[] data = { 255, 255, 255 };
            var output = mqtt.EncodeRemainingLength(data);
            var lenght = mqtt.DecodeRemainingLength(output, out error);
            if (error != null)
                Console.WriteLine("Error : " + error);
            Console.WriteLine("Lenght : " + lenght);
            Console.ReadLine();
        }
    }
}
