using System;
using System.Threading;
using System.Threading.Tasks;
using Thrift;
using Thrift.Processor;
using Thrift.Protocol;
using Thrift.Server;
using Thrift.Transport;
using Thrift.Transport.Server;
using ThriftSpecification.Shared;

namespace Server
{
    internal class AppServer
    {
        private static readonly TConfiguration configuration = new();

        static void Main(string[] args)
        {
            using var source = new CancellationTokenSource();
            Console.WriteLine("Starting server");
            RunAsync(source.Token);
            Console.ReadKey();
            source.Cancel();
        }

        public static async Task RunAsync(CancellationToken token)
        {
            var serverTransport = new TServerSocketTransport(6789, configuration);
            var transportFactory = new TBufferedTransport.Factory();
            var protocolFactory = new TJsonProtocol.Factory();


            var handler = new CalculatorAsyncHandler();
            var processor = new CalculatorService.AsyncProcessor(handler);

            var server = new TThreadPoolAsyncServer(
                new TSingletonProcessorFactory(processor),
                serverTransport,
                transportFactory,
                transportFactory,
                protocolFactory,
                protocolFactory,
                new TThreadPoolAsyncServer.Configuration());

            await server.ServeAsync(token);
        }
    }
}
