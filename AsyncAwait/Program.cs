using System.Diagnostics;

namespace AsyncAwait;

internal class Program
{
	static async Task Main(string[] args)
	{
		//Stopwatch sw = Stopwatch.StartNew();
		//Toast();
		//Tasse();
		//Kaffee();
		//Console.WriteLine(sw.ElapsedMilliseconds); //7s, synchron

		//Stopwatch sw = Stopwatch.StartNew();
		//Task t1 = Task.Run(Toast);
		//Task t2 = Task.Run(Tasse);
		//Task t3 = Task.Run(Kaffee);
		//Task.WaitAll(t1, t2, t3);
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s, aber WaitAll

		//Stopwatch sw = Stopwatch.StartNew();
		//Task t1 = Task.Run(Toast);
		//Task t2 = new Task(Tasse); //Abhängigkeit zwischen Tasse und Kaffee herstellen
		//t2.ContinueWith(_ => Kaffee());
		//t2.Start();
		//Task.WaitAll(t1, t2);
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s, aber WaitAll

		//async und await
		//Eine Methode die mit async definiert wurde, kann awaited werden (mit await)
		//await hat die Eigenschaft, das nicht blockiert wird

		//Verschiedene Rückgabetypen
		//async void: kann selbst await verwenden, kann aber nicht awaited werden
		//async Task: kann selbst await verwenden und awaited werden (hat kein return)
		//async Task<T>: kann selbst await verwenden und awaited werden, hat zusätzlich ein Ergebnis

		//Wenn eine Async Methode gestartet wird, wird diese als Task gestartet

		//Stopwatch sw = Stopwatch.StartNew();
		//Task t1 = ToastAsync(); //Wenn eine Async Methode gestartet wird, wird diese als Task gestartet
		//Task t2 = TasseAsync();
		//Task t3 = KaffeeAsync();
		//await t1; //Warte hier bis der Task fertig ist (nicht blockierend)
		//await t2;
		//await t3;
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		//Stopwatch sw = Stopwatch.StartNew();
		//Task<Toast> t1 = ToastObjectAsync(); //Starte den Toast
		//Task<Tasse> t2 = TasseObjectAsync(); //Starte die Tasse
		//Tasse tasse = await t2; //Warte hier, bis die Tasse fertig ist
		//Task<Kaffee> t3 = KaffeeObjectAsync(tasse); //Starte den Kaffee mit der fertigen Tasse
		//Kaffee kaffee = await t3; //Warte auf den Kaffee
		//Toast toast = await t1; //Längster Task sollte am Ende sein (schätzen)
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		//Kurzschreibweise
		Stopwatch sw = Stopwatch.StartNew();
		Task<Toast> t1 = ToastObjectAsync(); //Starte den Toast
		Kaffee kaffee = await KaffeeObjectAsync(await TasseObjectAsync()); //Warte auf den Kaffee
		Toast toast = await t1; //Längster Task sollte am Ende sein (schätzen)
		Console.WriteLine(sw.ElapsedMilliseconds); //4s

		//Mit Task.Run können alle Methoden awaitable werden
		Stopwatch sw2 = Stopwatch.StartNew();
		Task t1s = Task.Run(Toast);
		Task t2s = Task.Run(Tasse);
		Task t3s = Task.Run(Kaffee);
		await t1s;
		await t2s;
		await t3s;
		Console.WriteLine(sw2.ElapsedMilliseconds); //4s

		//Task.WhenAll, Task.WhenAny
		await Task.WhenAll(t1s, t2s, t3s); //await Statements konsolidieren, bis alle fertig sind
		await Task.WhenAny(t1s, t2s, t3s); //await Statements konsolidieren, bis der erste fertig ist
	}

	#region Synchron
	public static void Toast()
	{
		Thread.Sleep(4000);
        Console.WriteLine("Toast fertig");
    }

	public static void Tasse()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Tasse fertig");
	}

	public static void Kaffee()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Kaffee fertig");
	}
	#endregion

	#region Asynchron
	public static async Task ToastAsync()
	{
		await Task.Delay(4000); //Hier wird auf das Ergebnis von dem Task gewartet -> Delay(4000)
		Console.WriteLine("Toast fertig");
	}

	public static async Task TasseAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Tasse fertig");
	}

	public static async Task KaffeeAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
	}
	#endregion

	#region Asynchron mit Objekt
	public static async Task<Toast> ToastObjectAsync()
	{
		await Task.Delay(4000); //Hier wird auf das Ergebnis von dem Task gewartet -> Delay(4000)
		Console.WriteLine("Toast fertig");
		return new Toast(); //Hier kein Task zurückgeben, sondern ein Objekt, dass mit dem Generic zusammenpasst
	}

	public static async Task<Tasse> TasseObjectAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Tasse fertig");
		return new Tasse();
	}

	public static async Task<Kaffee> KaffeeObjectAsync(Tasse t)
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
		return new Kaffee();
	}
	#endregion
}

public class Toast { }

public class Tasse { }

public class Kaffee { }