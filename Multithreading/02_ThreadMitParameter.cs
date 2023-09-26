namespace Multithreading;

internal class _02_ThreadMitParameter
{
	static void Main(string[] args)
	{
		Thread t = new Thread(Run); //Funktion mit Object als Parameter
		t.Start(200); //Parameter hier übergeben

		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Main Thread: {i}");

		//Thread mit Callback, Rückgabewert, ...
		Thread t2 = new Thread(RunWithCallback);

		object returnValue;
		t2.Start(
			new ThreadData(
				null, //Daten für den Thread (effektiv object o)
				() => Console.WriteLine("Fertig"), //Fertig Callback
				(e) => returnValue = e) //Return Callback
			);

		t2.Join(); //Warte auf das Ergebnis
	}

	public static void Run(object o)
	{
		if (o is int x)
		{
			for (int i = 0; i < x; i++)
                Console.WriteLine($"Side Thread: {i}");
        }
	}

	public static void RunWithCallback(object o)
	{
		if (o is ThreadData dt)
		{
			int summe = 0;
			for (int i = 0; i < 100; i++)
				summe += i;
			dt.Callback();
			dt.ReturnValue(summe);
		}
	}
}

public record ThreadData(object Data, Action Callback, Action<object> ReturnValue);