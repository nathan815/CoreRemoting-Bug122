﻿using CoreRemoting;
using CoreRemoting.DependencyInjection;
using CoreRemoting.Serialization;
using CoreRemoting.Serialization.Binary;
using CoreRemotingTest.Shared;
using System;

namespace CoreRemotingTest.Server
{
    /// <summary>
    /// Hello world chat service.
    /// </summary>
    public class SayHelloService : ISayHelloService
    {
        /// <summary>
        /// Event: Fired when a chat message is received.
        /// </summary>
        public event Action<string, string> MessageReceived;

        /// <summary>
        /// Say something in the chat.
        /// </summary>
        /// <param name="name">User name</param>
        /// <param name="message">Message to post in chat</param>
        public void Say(string name, string message)
        {
            MessageReceived?.Invoke(name, message);
        }
    }

    /// <summary>
    /// Hello world chat server application class.
    /// </summary>
    public static class HelloWorldServer
    {
        /// <summary>
        /// Server application entry point.
        /// </summary>
        static void Main()
        {
            CrossFrameworkSerialization.RedirectPrivateCoreLibToMscorlib();

            // Create an configure a CoreRemoting server
            var server = new RemotingServer(new ServerConfig()
            {
                HostName = "localhost",
                NetworkPort = 9090,
                //MessageEncryption = false,
                //Serializer = new BinarySerializerAdapter(),
                RegisterServicesAction = container =>
                {
                    container.RegisterService<ISayHelloService, SayHelloService>(ServiceLifetime.Singleton);
                }
            });

            server.Error += (sender, exception) =>
            {
                Console.WriteLine("--[Error]--------------------------");
                Console.WriteLine(exception.Message);
                Console.WriteLine(exception.StackTrace);

                if (exception.InnerException != null)
                {
                    Console.WriteLine(exception.InnerException.Message);
                    Console.WriteLine(exception.InnerException.StackTrace);
                }

                Console.WriteLine("-----------------------------------");
            };

            // Start server
            server.Start();

            Console.WriteLine("\nRegistered services");
            Console.WriteLine("-------------------");

            // List registered services
            foreach (var registration in server.ServiceRegistry.GetServiceRegistrations())
            {
                Console.WriteLine($"ServiceName = '{registration.ServiceName}', InterfaceType = {registration.InterfaceType.FullName}, UsesFactory = {registration.UsesFactory}, Lifetime = {registration.ServiceLifetime}");
            }

            Console.WriteLine("\nServer is running.");
            Console.ReadLine();
        }
    }
}
