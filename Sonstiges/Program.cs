using System.Collections;
using System.Diagnostics;
using System.Text;

namespace Sonstiges;

internal class Program
{
	static void Main(string[] args)
	{
		Zug z = new();
		Wagon w = new();
		z += w;
		z++;

		Wagon w1 = new() { AnzSitze = 5 };
		Wagon w2 = new() { AnzSitze = 5 };
        Console.WriteLine(w1 == w2);

		foreach (var x in z)
		{

		}

		//z[2] = new Wagon();

		//Console.WriteLine(z["Rot"]);
		//Console.WriteLine(z[10, "Rot"]);

		//var a = z.Wagons.Select(e => new { e.AnzSitze, HC = e.GetHashCode() }); //Über anonymes Objekt mehrere Werte mit Select bewegen
		//Console.WriteLine(a.First().HC);

		Stopwatch sw = Stopwatch.StartNew();
		string s = "";
		for (int i = 0; i < 100000; i++)
			s += i;
		Console.WriteLine(sw.ElapsedMilliseconds); //22s

		sw.Restart();
        StringBuilder sb = new StringBuilder();
		for (int i = 0; i < 100000; i++)
			sb.Append(i);
		//Console.WriteLine(sb.ToString());
		Console.WriteLine(sw.ElapsedMilliseconds); //3ms
	}
}

public class Zug : IEnumerable//<Wagon>
{
	public List<Wagon> Wagons = new();

	public IEnumerator<Wagon> GetEnumerator()
	{
		//IList<Wagon> wagons = new();
		//foreach (Wagon w in Wagons)
		//	wagons.Add(w);
		//return wagons;

		//foreach (Wagon w in Wagons)
		//	yield return w;

		return Wagons.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		throw new NotImplementedException();
	}

	public static Zug operator +(Zug z, Wagon w)
	{
		z.Wagons.Add(w);
		return z;
	}

	public static Zug operator +(Zug z, List<Wagon> w)
	{
		z.Wagons.AddRange(w);
		return z;
	}

	public static Zug operator ++(Zug z)
	{
		z.Wagons.Add(new Wagon());
		return z;
	}

	public Wagon this[int index]
	{
		get => Wagons[index];
		set => Wagons[index] = value;
	}

	public Wagon this[string farbe]
	{
		get => Wagons.First(e => e.Farbe == farbe);
	}

	public Wagon this[int anzSitze, string farbe]
	{
		get => Wagons.First(e => e.AnzSitze == anzSitze && e.Farbe == farbe);
	}
}

public class Wagon
{
	public int AnzSitze { get; set; }

	public string Farbe { get; set; }

	public static bool operator ==(Wagon a, Wagon b)
	{
		return GetValues(a).SequenceEqual(GetValues(b));

		IEnumerable<object?> GetValues(Wagon w) => w.GetType().GetProperties().Select(e => e.GetValue(w));

		//return (a.AnzSitze == b.AnzSitze) && a.Farbe == b.Farbe;
	}

	public static bool operator !=(Wagon a, Wagon b)
	{
		return !(a == b);
	}
}