namespace DelegatesEvents;

internal class Program
{
	delegate void Vorstellung(string name); //Definition von Delegate, speichert Referenzen auf Methoden (Methodenzeiger)

	static void Main(string[] args)
	{
		Vorstellung vorstellungen = new Vorstellung(VorstellungDE); //Erstellung von Delegate mit Initialmethode
		vorstellungen("Max");

		vorstellungen += VorstellungEN; //Ein Delegate Objekt mit zwei Methodenzeigern
		vorstellungen("Lukas");

		vorstellungen += VorstellungEN;
		vorstellungen += VorstellungEN;
		vorstellungen += VorstellungEN; //Selbe Methode kann mehrmals angehängt werden
		vorstellungen("Tim");

		//vorstellungen += VorstellungDE;
		vorstellungen -= VorstellungDE; //Mit -= können Methoden abgenommen
		vorstellungen("Micha");

		vorstellungen -= VorstellungDE; //Methode abnehmen die nicht angehängt ist gibt keinen Fehler
		vorstellungen -= VorstellungDE;
		vorstellungen -= VorstellungDE;
		vorstellungen("Max");

		vorstellungen -= VorstellungEN;
		vorstellungen -= VorstellungEN;
		vorstellungen -= VorstellungEN;
		vorstellungen -= VorstellungEN; //Delegate wird null wenn alle Zeiger weg sind
		vorstellungen("Lukas");

		if (vorstellungen is not null)
			vorstellungen("Lukas");

		vorstellungen?.Invoke("Lukas"); //Null Propagation: Wenn das Objekt nicht null ist, wird die Funktion ausgeführt

		//Delegate auflisten
		foreach (Delegate dg in vorstellungen.GetInvocationList())
		{
            Console.WriteLine(dg.Method.Name);
        }
	}

	static void VorstellungDE(string name)
	{
        Console.WriteLine($"Hallo mein Name ist {name}");
    }

	static void VorstellungEN(string name)
	{
		Console.WriteLine($"Hello my name is {name}");
	}
}