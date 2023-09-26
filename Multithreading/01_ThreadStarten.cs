namespace Multithreading;

internal class _01_ThreadStarten
{
	static void Main(string[] args)
	{
		Thread t = new Thread(Run); //ThreadStart: Delegate mit void und 0 Parameter
		t.Start();

		Thread t2 = new Thread(Run); //ThreadStart: Delegate mit void und 0 Parameter
		t2.Start();

		Thread t3 = new Thread(Run); //ThreadStart: Delegate mit void und 0 Parameter
		t3.Start();

		//Ab hier parallel

		t.Join(); //Effektiv Wait
		t2.Join();
		t3.Join(); //Warte auf alle Threads

		//Effektiv Sequentiell

		for (int i = 0; i < 100; i++)
            Console.WriteLine($"Main Thread: {i}");
    }

	static void Run()
	{
		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Side Thread: {i}");
	}
}