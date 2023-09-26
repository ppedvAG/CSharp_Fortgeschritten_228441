namespace Multitasking;

internal class _02_TaskMitReturn
{
	static void Main(string[] args)
	{
		Task<int> intTask = new Task<int>(Run);
		intTask.Start();

        Console.WriteLine(intTask.Result); //Result blockiert den Main Thread

		for (int i = 0; i < 100; i++)
            Console.WriteLine($"Main Thread: {i}");

		Task t2 = Task.Run(() => Console.WriteLine("Anonyme Methode"));

		Task t3 = Task.Run(() =>
		{
			Console.WriteLine("Mehrzeilige");
			Console.WriteLine("anonyme");
			Console.WriteLine("Methode");
		});

		t2.Wait(); //Blockiert auch den Main Thread

		Task.WaitAll(intTask, t2, t3); //Warte auf alle Tasks (Blockiert den Main Thread)
		Task.WaitAny(intTask, t2, t3); //Wartet auf einen der gegebenen Tasks (Blockiert den Main Thread), gibt den Index des zuerst fertig gewordenen Tasks zurück
    }

	static int Run()
	{
        Console.WriteLine("Start Task");
        Thread.Sleep(1000);
		return 10;
	}
}
