using System.Threading.Channels;

namespace Multithreading;

internal class _05_ThreadPool
{
	static void Main(string[] args)
	{
		ThreadPool.QueueUserWorkItem(Methode1, () => Console.WriteLine("Fertig")); //Daten hinzufügen (z.B. Callback)
		ThreadPool.QueueUserWorkItem(Methode2, () => Console.WriteLine("Fertig"));
		//Hintergrundthreads: Werden abgebrochen wenn alle Vordergrundthreads fertig sind

		Thread.Sleep(300);
		//Hier werden alle Threads im ThreadPool abgebrochen

		Console.ReadKey();
	}

	static void Methode1(object o)
	{
		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine(i);
			Thread.Sleep(25);
		}
		(o as Action).Invoke();
	}

	static void Methode2(object o)
	{
		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine(i);
			Thread.Sleep(25);
		}
		(o as Action).Invoke();
	}
}
