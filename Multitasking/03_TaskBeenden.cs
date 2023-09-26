namespace Multitasking;

internal class _03_TaskBeenden
{
	static void Main(string[] args)
	{
		CancellationTokenSource cts = new CancellationTokenSource();
		CancellationToken token = cts.Token;
		Task t = new Task(Run, token);
		t.Start();

		Thread.Sleep(500);

		cts.Cancel();
	}

	static void Run(object o) //Hier auch wieder object (wie bei Threads)
	{
		if (o is CancellationToken ct)
		{
			for (int i = 0; i < 100; i++)
			{
                Console.WriteLine($"Side Task: {i}");
				ct.ThrowIfCancellationRequested(); //Task wirft Exception, aber diese ist nicht sichtbar
				Thread.Sleep(25);
            }
		}
	}
}
