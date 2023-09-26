namespace Multitasking;

internal class _04_TaskExceptions
{
	static void Main(string[] args)
	{
		try
		{
			Task<int> t1 = Task.Run(Exc1);
			Task<int> t2 = Task.Run(Exc2);
			Task<int> t3 = Task.Run(Exc3);

			Console.WriteLine(t1.IsFaulted);

			//AggregateException
			//Sammelobjekt für alle Exceptions die in Tasks auftreten
			//t1.Wait(); //Wirft AggregateException
			Task.WaitAll(t1, t2, t3); //Wirft AggregateException
			Console.WriteLine(t1.Result); //Wirft AggregateException
		}
		catch (AggregateException e)
		{
			foreach (Exception t in e.InnerExceptions)
			{
				Console.WriteLine(t.Message);
			}
		}
		Console.ReadKey();
	}

	public static int Exc1()
	{
		Thread.Sleep(1000);
		throw new DivideByZeroException();
	}

	public static int Exc2()
	{
		Thread.Sleep(2000);
		throw new OutOfMemoryException();
	}

	public static int Exc3()
	{
		Thread.Sleep(3000);
		throw new InvalidDataException();
	}
}
