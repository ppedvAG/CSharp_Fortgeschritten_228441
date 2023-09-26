namespace Multithreading;

internal class _06_Lock
{
	static void Main(string[] args)
	{
		List<Thread> queue = new();
		for (int i = 0; i < 100; i++)
		{
			queue.Add(new Thread(Run));
		}
		queue.ForEach(e => e.Start());
    }

	static int Zaehler = 0;

	static object LockObject = new();

	static void Run()
	{
		//for (int i = 0; i < 1000; i++)
		//{
		//	Zaehler += 1;
		//}
		//Console.WriteLine(Zaehler); //Irreguläre Muster


		for (int i = 1; i < 1001; i++)
		{
			lock (LockObject)
			{
				Zaehler += 1; //Nach Locking ist der Zähler am Ende immer 100000
				if (i % 100 == 0) 
					Console.WriteLine(Zaehler);
			}
		}


		for (int i = 1; i < 1001; i++)
		{
			Monitor.Enter(LockObject); //Lock und Monitor haben 1:1 den selben Code dahinter
			Zaehler += 1; //Nach Monitor ist der Zähler am Ende immer 100000
			if (i % 100 == 0)
				Console.WriteLine(Zaehler);
			Monitor.Exit(LockObject);
		}
	}
}
