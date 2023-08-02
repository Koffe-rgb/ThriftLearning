using System;
using System.Threading;
using System.Threading.Tasks;
using ThriftSpecification.Shared;

namespace Server
{
    internal class CalculatorAsyncHandler : CalculatorService.IAsync
    {
        public Task<int> Evaluate(Work work, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Evaluating: {work.A} {work.Operation} {work.B}");

            int result;
            switch (work.Operation)
            {
                case Operation.ADD: result = work.A + work.B; break;
                case Operation.SUBTRACT: result = work.A - work.B; break;
                case Operation.MULTIPLY: result = work.A * work.B; break;
                case Operation.DIVIDE:
                    if (work.B == 0)
                    {
                        return Task.FromException<int>(new ThriftSpecification.Shared.InvalidOperationException { Operation = work.Operation, Comment = "Can't divide by zero" });
                    }
                    result = work.A / work.B;
                    break;
                default:
                    return Task.FromException<int>(new ThriftSpecification.Shared.InvalidOperationException { Operation = work.Operation, Comment = "Unknown operation" });
            }

            return Task.FromResult(result);
        }

        public Task SayHello(string name, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Hello, {name}");
            return Task.Delay(0, CancellationToken.None);
        }
    }
}
