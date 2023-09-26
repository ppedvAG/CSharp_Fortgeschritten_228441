namespace Multitasking;

internal class _05_ContinueWith
{
	static void Main(string[] args)
	{
		Task<int> t1 = new Task<int>(() =>
		{
			Thread.Sleep(1000);
			return 123456;
		});

		//Tasks verketten
		//An Tasks können Folgetasks angehängt werden

		//Task erstellt -> Folgetask konfiguriert -> Gestartet

		t1.ContinueWith(vorherigerTask => Console.WriteLine(vorherigerTask.Result)); //Folgetask (asynchron), Ergebnis wird irgendwann in die Konsole geschrieben
		t1.Start();

		////////////////////////////////////////////////////////

		//Taskabzweigungen
		Task<int> t2 = new Task<int>(() =>
		{
			Thread.Sleep(1000);
			throw new InvalidOperationException();
			return 5;
		});
		t2.ContinueWith(vorherigerTask =>
		{
			try
			{
				Console.WriteLine(t2.Result);
			}
			catch (AggregateException e)
			{
				foreach (Exception ex in e.InnerExceptions) //Fehler von oben wird hier weitergegeben
					Console.WriteLine(ex.Message);
            }
		}); //Erfolgstask
		t2.ContinueWith(vorherigerTask => Console.WriteLine("Fehler"), TaskContinuationOptions.OnlyOnFaulted); //Fehlertask
		t2.ContinueWith(vorherigerTask => Console.WriteLine("Erfolg"), TaskContinuationOptions.OnlyOnRanToCompletion); //Erfolgstask
		t2.Start();

		for (int i = 0; i < 20; i++)
		{
            Console.WriteLine($"Main Thread: {i}");
			Thread.Sleep(100);
        }

		Console.ReadKey();
	}
}
