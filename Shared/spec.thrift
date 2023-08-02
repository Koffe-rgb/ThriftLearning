namespace netstd ThriftSpecification.Shared

typedef i32 int

enum Operation {
	ADD = 1,
	SUBTRACT = 2,
	MULTIPLY = 3,
	DIVIDE = 4
}

struct Work {
	1: int a = 0,
	2: int b,
	3: Operation operation
}

exception InvalidOperationException {
	1: Operation operation,
	2: string comment
}

service CalculatorService {
	int Evaluate(1: Work work) throws (1: InvalidOperationException e),
	oneway void SayHello(1:string name)
}