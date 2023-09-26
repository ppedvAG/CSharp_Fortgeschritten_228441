namespace Multithreading;

internal class _04_CancellationToken
{
	static void Main(string[] args)
	{
		CancellationTokenSource source = new(); //Sender
		CancellationToken token = source.Token; //Empfänger

		Thread t = new Thread(Run);
		t.Start(token);

		Thread.Sleep(500);

		source.Cancel(); //Beende alle Tokens die an diesem Task angehängt sind
	}

	public static void Run(object o)
	{
		try
		{
			if (o is CancellationToken ct)
			{
				Thread t = new Thread(Cancel);
				t.Start(ct);

				for (int i = 0; i < 100; i++)
				{
                    Console.WriteLine($"Side Thread: {i}");
                    Thread.Sleep(10);
				}
			}
		}
		catch (Exception)
		{
            Console.WriteLine("Thread beendet mit CT");
			//throw;
        }
	}

	private static void Cancel(object o)
	{
		if (o is CancellationToken ct)
			while (true)
				ct.ThrowIfCancellationRequested();
	}
}
