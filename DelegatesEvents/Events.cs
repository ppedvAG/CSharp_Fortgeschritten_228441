namespace DelegatesEvents;

internal class Events
{
	//Event: Statischer Punkt (muss nicht static sein), an den Methoden angehängt werden können (Delegate Mechaniken)
	//Kann nicht instanziert werden

	//Zweigeteilte Entwicklung
	//1. Entwicklerseite: Definiert das Event und ruft es auf
	//2. Anwenderseite: Hängt eine Methode an und bestimmt dadurch den Effekt des Events
	//Beispiel: Button -> Click: Wir können selbst definieren, was der Button beim Click tun soll

	static event EventHandler TestEvent; //Entwicklerseite

	static event EventHandler<TestEventArgs> ArgsEvent; //Eigene Daten die bei dem Event übergeben werden

	static event EventHandler<int> IntEvent; //EventArgs kann einen beliebigen Typ haben

	static void Main(string[] args)
	{
		TestEvent += Events_TestEvent; //Anwenderseite
		TestEvent(null, EventArgs.Empty); //Entwicklerseite

		ArgsEvent += Events_ArgsEvent;
		ArgsEvent(null, new TestEventArgs("Event ausgeführt")); //Daten durch Events bewegen

		IntEvent += Events_IntEvent;
		IntEvent(null, 10); //Normalerweise würde beim Sender this stehen (aber Main Methode deswegen keine Objekte)
	}

	private static void Events_TestEvent(object? sender, EventArgs e) //Anwenderseite
	{
        Console.WriteLine("Event ausgeführt");
	}

	private static void Events_ArgsEvent(object? sender, TestEventArgs e)
	{
		Console.WriteLine(e.Status);
	}

	private static void Events_IntEvent(object? sender, int e)
	{
		Console.WriteLine(e);
	}
}

public class TestEventArgs : EventArgs
{
	public string Status { get; set; }

    public TestEventArgs(string status)
    {
		Status = status;
    }
}