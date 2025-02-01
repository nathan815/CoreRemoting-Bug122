using CoreRemoting;
using CoreRemoting.Serialization;
using CoreRemoting.Serialization.Binary;
using CoreRemotingTest.Shared;
using System;

namespace CoreRemotingTest.Client
{
    /// <summary>
    /// Client application class of hello world chat example.
    /// </summary>
    public static class HelloWorldClient
    {
        /// <summary>
        /// Application entry point.
        /// </summary>
        static void Main()
        {
            CrossFrameworkSerialization.RedirectMscorlibToPrivateCoreLib();

            // Create and configure new CoreRemoting client 
            var client = new RemotingClient(new ClientConfig()
            {
                ServerHostName = "localhost",
                //Serializer = new BinarySerializerAdapter(),
                //MessageEncryption = false,
                ServerPort = 9090
            });

            Console.WriteLine("Connecting...");

            // Establish connection to server
            client.Connect();

            Console.WriteLine("Connected.");

            // Creates proxy for remote service
            var proxy = client.CreateProxy<ISayHelloService>();

            // Subscribe MessageReceived event of remote service
            proxy.MessageReceived += (senderName, message) =>
                Console.WriteLine($"\n  {senderName} says: {message}\n");

            Console.WriteLine("What's your name?");
            var name = Console.ReadLine();

            Console.WriteLine("\nEntered chat. Type 'quit' to leave.");

            bool quit = false;

            while (!quit)
            {
                var text = Console.ReadLine();

                if (text != null && text.Equals("quit", StringComparison.InvariantCultureIgnoreCase))
                    quit = true;
                else
                {
                    // Call remote method
                    proxy.Say(name, text);
                }
            }
        }
    }
}
