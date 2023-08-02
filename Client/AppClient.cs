using System;
using System.Threading;
using System.Threading.Tasks;
using Thrift;
using Thrift.Protocol;
using Thrift.Transport.Client;
using ThriftSpecification.Shared;

namespace Client
{
    internal class AppClient
    {
        static void Main(string[] args)
        {
            using var source = new CancellationTokenSource();
            RunAsync(source.Token);
            Console.ReadKey();
            source.Cancel();
        }

        public static async Task RunAsync(CancellationToken token)
        {
            var config = new TConfiguration();
            var transport = new TSocketTransport("localhost", 6789, config);
            var protocol = new TJsonProtocol(transport);
            var client = new CalculatorService.Client(protocol);

            var rng = new Random();

            var work = new Work
            {
                A = rng.Next(100),
                B = rng.Next(100),
                Operation = (Operation)(rng.Next(4)+1)
            };

            try
            {
                Console.WriteLine($"Sending {work.A} {work.Operation} {work.B}");
                var result = await client.Evaluate(work, token);
                Console.WriteLine(result);
            }
            catch (ThriftSpecification.Shared.InvalidOperationException e)
            {
                Console.WriteLine($"Error: \"{e.Message}\" while trying perform {e.Operation} operation");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
