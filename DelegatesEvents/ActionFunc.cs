using System.Threading.Channels;

namespace DelegatesEvents;

internal class ActionFunc
{
	static List<int> TestList = new();

	static void Main(string[] args)
	{
		//Action, Predicate, Func: Vordefinierte Delegates die an verschiedenen Stellen in C# existieren
		//z.B. Linq, Multitasking, Reflection, ...
		//Essentiell für die fortgeschrittene Programmierung
		//Können alles was in dem vorherigen Teil vorkommt

		//Action: Delegate mit void als Rückgabewert und bis zu 16 Parametern
		Action<int, int> action = Addiere;
		action(3, 4);
		action?.Invoke(5, 7);

		//Methode konfigurieren über Action Parameter
		DoAction(4, 8, action);
		DoAction(3, 6, Addiere);
		DoAction(7, 2, Subtrahiere);

		//Praktische Beispiele
		//Task t = new Task(Action);
		TestList.ForEach(Print); //for Schleife die bei jedem Element die Action ausführt
		void Print(int x) => Console.WriteLine(x);

		//Func: Delegate mit einem beliebigen Rückgabewert und bis zu 16 Parametern
		Func<int, int, double> func = Multipliziere; //Rückgabetyp ist hier das letzte Argument
		double d = func(4, 5); //func gibt ein Ergebnis zurück
		double? d2 = func?.Invoke(5, 6); //Hier könnte null zurück kommen wenn die Func null ist
		//<Typ>?: Nullable Type
		double d3 = func?.Invoke(5, 6) ?? double.NaN;

		//Methode konfigurieren über Func Parameter
		DoFunc(5, 6, func);
		DoFunc(3, 4, Multipliziere);
		DoFunc(3, 4, Dividiere);

		//Praktische Beispiele
		TestList.Where(TeilbarDurch2);
		bool TeilbarDurch2(int x) => x % 2 == 0;

		//Anonyme Methoden: Funktionen für den einmaligen Gebrauch, ohne sie anlegen zu müssen
		func += delegate (int x, int y) { return x + y; }; //Anonyme Methode

		func += (int x, int y) => { return x + y; }; //Kürzere Form

		func += (x, y) => { return x - y; };

		func += (x, y) => (double) x / y; //Kürzeste, häufigste Form

		//Anwenden von Anonymen Funktionen
		TestList.ForEach(e => Console.WriteLine(e)); //void als Ergebnis: CW
		TestList.Where(e => e % 2 == 0); //bool als Ergebnis: ==
		DoFunc(4, 8, (x, y) => x % y); //double als Ergebnis: %
	}

	#region Action
	static void Addiere(int x, int y) => Console.WriteLine($"{x} + {y} = {x + y}");

	static void Subtrahiere(int x, int y) => Console.WriteLine($"{x} - {y} = {x - y}");

	static void DoAction(int x, int y, Action<int, int> action) => action?.Invoke(x, y);
	#endregion

	#region Func
	static double Multipliziere(int x, int y) => x * y; //return fällt weg bei Expression Body

	static double Dividiere(int x, int y) => (double) x / y;

	static double DoFunc(int x, int y, Func<int, int, double> func) => func?.Invoke(x, y) ?? double.NaN;
	#endregion
}
