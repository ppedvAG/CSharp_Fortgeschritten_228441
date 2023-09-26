namespace Multitasking;

internal class _01_TaskStarten
{
	static void Main(string[] args)
	{
		Task t = new Task(Run);
		t.Start();

		Task.Factory.StartNew(Run); //ab .NET 4.0

		Task.Run(Run); //ab .NET 4.5 (Factory.StartNew == Run)

		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Main Thread: {i}");

		//Nachdem Tasks am ThreadPool liegen werden diese abgebrochen wenn der Main Thread fertig ist
		Console.ReadKey(); //Main Thread blockieren
	}

	static void Run()
	{
		for (int i = 0; i < 100; i++)
            Console.WriteLine($"Side Task: {i}");
    }
}