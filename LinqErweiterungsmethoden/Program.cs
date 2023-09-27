using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace LinqErweiterungsmethoden;

internal class Program
{
	static void Main(string[] args)
	{
		#region Listentheorie
		//IEnumerable: Interface, dass die Grundstruktur von allen Listen darstellt
		//IEnumerable enthält eine Anleitung zum Erstellen der Liste (mit konkretem Typen) am Ende

		IEnumerable<int> x = Enumerable.Range(1, 1000); //Expanding the Results View will enumerate the IEnumerable
		//-> IEnumerable enthält keine Elemente
		//Mit eine Umwandlungsfunktion (ToList(), ToArray(), ...) kann das IEnumerable generiert werden
		List<int> y = x.ToList(); //Mit ToList() werden die Elemente tatsächich erzeugt

		x.Where(e => e % 2 == 0); //Hier wird eine Anleitung mit Predicate erzeugt

		//ToList() arbeiten lassen und später Ergebnis angreifen
		//var task = Task.Run(() => x.Where(e => e % 2 == 0).ToList());
		//await task;

		//Alle Linq Funktionen haben eine Func als Parameter um sie zu konfigurieren
		//Über die Func kann eine anonyme Methode übergeben werden, die bei jedem Listenelement ausgeführt wird
		x.Where(TeilbarDurch3);

		//Enumerator:
		//Komponente die bei jedem IEnumerable intergriert ist
		//3 Teile:
		//- Zeiger: object -> Zeigt auf genau ein Element
		//- MoveNext(): bool -> Bewegt den Zeiger um ein Element weiter und gibt die Möglichkeit den Wert anzugreifen
		//- Reset(): void -> Bewegt den Zeiger auf Position 0 zurück
		//GetEnumerator(): Bietet die Funktion um auf den Enumerator zuzugreifen
		foreach (int i in x) //Hier wird die GetEnumerator() Funktion angegriffen
		{

		}
		Dictionary<int, string> dict = new();

		foreach (KeyValuePair<int, string> kv in dict) //Hier gibt der Enumerator ein KeyValuePair<T1, T2> zurück
		{

		}

		bool TeilbarDurch3(int x) => x % 3 == 0;
		#endregion

		#region Einfaches Linq
		List<int> ints = Enumerable.Range(1, 20).ToList();

		Console.WriteLine(ints.Average());
		Console.WriteLine(ints.Min());
		Console.WriteLine(ints.Max());
		Console.WriteLine(ints.Sum());

		ints.First(); //Gibt das erste Element zurück, Exception wenn kein Element gefunden wurde
		ints.FirstOrDefault(); //Gibt das erste Element zurück, default wenn kein Element gefunden wurde

		ints.Last(); //Gibt das letzte Element zurück, Exception wenn kein Element gefunden wurde
		ints.LastOrDefault(); //Gibt das letzte Element zurück, default wenn kein Element gefunden wurde

		//Console.WriteLine(ints.First(e => e % 50 == 0)); //Finde das erste Element, dass restlos durch 50 teilbar ist (-> Exception)
		Console.WriteLine(ints.FirstOrDefault(e => e % 50 == 0)); //Finde das erste Element, dass restlos durch 50 teilbar ist (-> 0)
		#endregion

		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new Fahrzeug(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		};

		#region Vergleich Linq Schreibweisen
		//Alle BMWs finden
		List<Fahrzeug> bmwsForEach = new();
		foreach (Fahrzeug f in fahrzeuge)
			if (f.Marke == FahrzeugMarke.BMW)
				bmwsForEach.Add(f);

		//Standard-Linq: SQL-ähnliche Schreibweise (alt)
		List<Fahrzeug> bmwsAlt = (from f in fahrzeuge
								  where f.Marke == FahrzeugMarke.BMW
								  select f).ToList();

		//Methodenkette
		List<Fahrzeug> bmwsNeu = fahrzeuge.Where(e => e.Marke == FahrzeugMarke.BMW).ToList();
		#endregion

		#region Linq mit Objektliste
		//Predicate und Selector
		//Predicate: Func die einen bool zurück gibt, und generell weniger Elemente zurückgibt als verarbeitet werden
		//Selector: Func die einen beliebigen Wert zurück gibt, und immer die gleiche Anzahl Elemente zurückgibt

		//Select
		//Ermöglicht eine Transformation der Liste in eine beliebige Form
		//2 Fälle:

		//Fall 1: Extrahieren einzelner Felder
		//Nur die Geschwindigkeiten der Fahrzeuge
		fahrzeuge.Select(e => e.MaxV); //Liste mit Geschwindigkeiten

		//Fall 2: Transformieren der Liste
		//Schönen Output aus den Fahrzeugen erzeugen
		fahrzeuge.Select(e => $"Das Fahrzeug hat die Marke {e.Marke} und kann maximal {e.MaxV} fahren."); //String Liste

		//Schönen Output aus den Fahrzeugen erzeugen und das Fahrzeugobjekt mitnehmen
		fahrzeuge.Select(e => (e, $"Das Fahrzeug hat die Marke {e.Marke} und kann maximal {e.MaxV} fahren.")); //Liste von Tupeln

		//Alle Dateien aus einem Ordner ohne Pfad einlesen
		string[] pfade = Directory.GetFiles(@"C:\Windows");
		List<string> pfadeOhnePfad = new();
		foreach (string p in pfade)
			pfadeOhnePfad.Add(Path.GetFileNameWithoutExtension(p));

		Directory.GetFiles(@"C:\Windows")
			.Select(Path.GetFileNameWithoutExtension)
			.ToList();

		//Alle Dateien aus einem Ordner nehmen und die Zeilenanzahlen aufsummieren

		//Python
		//sum([len(open(file, "r").readlines()) for file in [f for f in os.listdir(".")]])

		//int summe = 0;
		//string[] readPfade = Directory.GetFiles(@"C:\Users\ppedv\Desktop\Richard\py");
		//foreach (string pfad in readPfade)
		//{
		//	string[] lines = File.ReadAllLines(pfad);
		//	summe += lines.Length;
		//}
		//Console.WriteLine(summe);

		//Console.WriteLine(Directory.GetFiles(@"Pfad").Select(e => File.ReadAllLines(e).Length).Sum());
		//Console.WriteLine(Directory.GetFiles(@"Pfad").Select(File.ReadAllLines).Sum(e => e.Length));

		///////////////////////////////////////////////////////

		fahrzeuge.Min(e => e.MaxV); //Die kleinste Geschwindigkeit (int)
		fahrzeuge.MinBy(e => e.MaxV); //Das Fahrzeug mit der kleinsten Geschwindigkeit (Fahrzeug)

		//Except
		//Liste A - Liste B (Alle Elemente die in Liste A aber nicht in Liste B vorkommen)

		//Intersect
		//List A ∩ List B (Alle Elemente die in beiden Listen vorkommen)

		//SelectMany
		//Liste von Listen glätten
		//Verschachtelte Listen: List<List<T>> -> List<T> -> Die neue Liste enthält alle Elemente

		fahrzeuge.Chunk(10); //Liste in X große Teile aufteilen (10, 2)

		//GroupBy
		//Erzeugt Gruppen anhand eines Kriteriums
		//Diese Gruppen enthalten alle Elemente die zum entsprechenden Kriterium dazugehören
		fahrzeuge.GroupBy(e => e.Marke); //3 Gruppen: Audi-Gruppe, BMW-Gruppe, VW-Gruppe -> Dictionary

		Dictionary<FahrzeugMarke, List<Fahrzeug>> group = fahrzeuge
			.GroupBy(e => e.Marke)
			.ToDictionary(k => k.Key, fzg => fzg.ToList()); //2 Lambdas: Key-Lambda, Value-Lambda

		Dictionary<FahrzeugMarke, double> durchschnittsVProMarke = fahrzeuge
			.GroupBy(e => e.Marke)
			.ToDictionary(k => k.Key, fzg => fzg.Average(e => e.MaxV)); //Über den Value Selektor kann der Wert noch angepasst werden
        
		foreach (KeyValuePair<FahrzeugMarke, double> kv in durchschnittsVProMarke)
            Console.WriteLine(kv);

		//Aggregate
		//Wendet eine Funktion auf jedes Element der Liste an und danach kann das Ergebnis in den Aggregator geschrieben werden
		fahrzeuge.Aggregate(0, (agg, fzg) => agg + fzg.MaxV); //Simples Aggregate Beispiel

		//Ausgabe erzeugen am Ende einer Linq-Query um nicht eine separate Schleife machen zu müssen
		string output =	fahrzeuge.Aggregate(string.Empty, (agg, fzg) =>
			agg + $"Das Fahrzeug hat die Marke {fzg.Marke} und kann maximal {fzg.MaxV} fahren.\n");

        Console.WriteLine(output);

		Console.WriteLine
		(
			fahrzeuge
				.GroupBy(e => e.Marke)
				.ToDictionary(k => k.Key, fzg => fzg.Average(e => e.MaxV))
				.Aggregate(new StringBuilder(), (agg, kv) =>
					agg.AppendLine($"Die Fahrzeuge der Marke {kv.Key} fahren im Durchschnitt {kv.Value}km/h schnell."))
				.ToString()
		);

		//Zip
		//Kombiniert zwei Listen zu einer Tupelliste
		fahrzeuge.Zip(Enumerable.Range(0, fahrzeuge.Count));
		Dictionary<int, Fahrzeug> fahrzeugeMitIDs = 
			Enumerable.Range(0, fahrzeuge.Count)
			.Zip(fahrzeuge)
			.ToDictionary(e => e.First, e => e.Second);
		#endregion

		#region Erweiterunsmethoden
		int zahl = 72539732;
        Console.WriteLine(zahl.Quersumme());
		Console.WriteLine(239579.Quersumme());

		fahrzeuge.Shuffle();

		//string json = JsonSerializer.Serialize(fahrzeuge);
		//JsonDocument doc = JsonDocument.Parse(json);
		//JsonElement.ArrayEnumerator ae = doc.RootElement.EnumerateArray(); //Enumerator erzeugen aus dem Json File -> Liste

		//double summe = 0;
		//List<FahrzeugMarke> marken = new();
		//foreach (JsonElement fzg in ae)
		//{
		//	summe += fzg.GetProperty("MaxV").GetProperty("Prop2").GetProperty("Prop3").GetValue<int>();
		//	marken.Add(fzg.GetProperty("Marke").GetValue<FahrzeugMarke>());
		//	//fzg.GetPropertyChain("MaxV.Prop2.Prop3");
		//}

		fahrzeuge.PrintList();
		ints.PrintList();
		Enumerable.Repeat(new Person(), 20).PrintList();

		List<Person> personen = new();
		for (int i = 0; i < 20; i++)
			personen.Add(new Person());
		personen.PrintList(e => (e.GetHashCode(), e.id));
		#endregion
	}
}

public record Fahrzeug(int MaxV, FahrzeugMarke Marke);

public class Person
{
	public int id = Random.Shared.Next();
}

public enum FahrzeugMarke { Audi, BMW, VW }