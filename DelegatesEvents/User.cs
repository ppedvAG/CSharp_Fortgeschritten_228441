namespace DelegatesEvents;

internal class User
{
	static void Main(string[] args)
	{
		Component comp = new();
		comp.ProcessStarted += Comp_ProcessStarted;
		comp.Progress += Comp_Progress;
		comp.ProcessComplete += () => Console.WriteLine("Fertig");
		comp.DoWork();
	}

	private static void Comp_ProcessStarted()
	{
		//Hier können beliebige Dinge beim Start durchgeführt werden
        Console.WriteLine("Prozess gestartet");
	}

	private static void Comp_Progress(object? sender, int e)
	{
		Console.WriteLine($"Fortschritt: {e}");
	}
}
