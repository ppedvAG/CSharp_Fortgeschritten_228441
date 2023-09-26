namespace Multithreading;

internal class _03_ThreadBeenden
{
	static void Main(string[] args)
	{
		try
		{
			Thread t = new Thread(Run);
			t.Start();

			Thread.Sleep(500);

			t.Interrupt(); //Thread beenden

			//Solange noch mindestens ein Thread läuft (Main Thread oder Side Thread) wird das Programm nicht beendet
			Console.ReadKey(); //Blockt den Main Thread
		}
		catch (Exception)
		{
			//Exception muss unten gefangen werden
		}
	}

	static void Run()
	{
		try
		{
			for (int i = 0; i < 10000; i++)
			{
                Console.WriteLine($"Side Thread: {i}");
                //Thread.Sleep(10);
			}
		}
		catch (Exception)
		{
            Console.WriteLine("Thread beendet");
        }
	}
}
