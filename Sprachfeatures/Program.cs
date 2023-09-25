using System.Diagnostics.Metrics;

namespace Sprachfeatures;

internal class Program
{
	static void Main(string[] args)
	{
		object o = 123;
		if (o.GetType() == typeof(int)) //Genauer Typvergleich: Prüft ob die Typen genau übereinstimmen (keine Unterklassen)
		{
            Console.WriteLine("o ist int");
        }

		if (o is int x) //Vererbungshiearchie Typvergleich: Prüft auch auf Unterklassen (true auch bei Unterklassen)
		{
			//Direkter Cast zu x
			Console.WriteLine($"o ist int {x}");
		}


		(int, int, int) y = (1, 2, 3);
        Console.WriteLine(y.Item1);

		List<(int ID, string Name, string Adresse)> list;

		Person p = new Person();
		var(id, name) = p; //Strg + .: Generate Deconstruct Method (funktioniert nur wenn das Tupel zuerst existiert)


		string str = "Ein Text";
		str = Convert(str);

		string Convert(string x)
		{
			x = x.ToUpper();
			x = x.Trim();
			x = x.GetHashCode().ToString();
			return x;
		}

		double d = 2_397_845.2_35782_3_9;

		//Wertetypen
		//struct
		//Wenn ein Struct in eine Variable geschrieben wird, wird der Wert kopiert
		//Wenn zwei Structs verglichen werden, werden die Inhalte verglichen
		int zahl = 5;
		int k = zahl;
		zahl = 10;

		//Referenztypen
		//class
		//Wenn eine Klasse in eine Variable geschrieben wird, wird eine Referenz angelegt auf das entsprechende Objekt
		//Wenn zwei Klassen verglichen werden, werden die Speicheradressen verglichen
		Person p1 = new Person();
		Person p2 = p1; //p2 --> [Person] <-- p1
		p1.ID = 10;

        Console.WriteLine(p1.GetHashCode());
        Console.WriteLine(p2.GetHashCode());


		//Objekt konfigurierbar machen, nur die Felder angeben die ich wirklich brauche
		Person p3 = new Person(Name: "Ein Name", GebDat: DateTime.MinValue);
		Person p4 = new Person(ID: 4, GebDat: DateTime.MinValue);
		Person p5 = new Person(ID: 4, Adresse: "Zuhause");

		unsafe
		{
			int* r = stackalloc int[] { 1, 2, 3 };
			//int* h;
			//h = **r;
		}


		string zahlText = zahl switch
		{
			1 => "Eins",
			2 => "Zwei",
			3 => "Drei",
			_ => "Andere Zahl" //Default
		};

		object g = p5 switch
		{
			{ ID: 1 } => "ID ist eins",
			{ ID: 2, Name: "Ein Name" } => "",
			_ => "Anderer Wert"
		};

		//object z = p5 switch
		//{
		//	var (id2, name2) when id2 > 5 && name2 != "" => ""
		//};

		//Null-Coalescing Operator (??): Wenn der Linke Wert nicht null ist, nimm den linken Wert, sonst den rechten Wert
		Person p6 = null;
		Person p7 = p6 ?? new Person(); //Wenn p6 null ist, erstelle eine neue Person, ansonsten nimm p6
		//Person p8 = p6 == null ? new Person() : p6;

		//Interpolated String ($-String): Code in einem String einzubauen
		Console.WriteLine($"Die Zahl ist {zahl}, p5 heißt {p5.Name}");
		Console.WriteLine("Die Zahl ist " + zahl + ", p5 heißt " + p5.Name);
        Console.WriteLine($"Die Zahl ist {(zahl == 5 ? "Fünf" : "Nicht Fünf")}");
		Console.WriteLine($"{zahl switch { 1 => "Eins",	2 => "Zwei", 3 => "Drei", _ => "Andere Zahl"}}");

		//Verbatim String (@-String): Ignoriert Escape-Sequenzen, nimmt alles genau so wie es geschrieben ist
		string pfad = @"C:\Program Files\dotnet\shared\Microsoft.NETCore.App\7.0.5\System.Text.Encoding.Extensions.dll";
		string pfad2 = "C:\\Program Files\\dotnet\\shared\\Microsoft.NETCore.App\\7.0.5\\System.Text.Encoding.Extensions.dll";
		string umbruch = @"Umbruch
Umbruch";
        Console.WriteLine(umbruch);

		//.NET Source Code: https://github.com/dotnet/runtime

		// sum([len(open(file, "r").readlines()) for file in [f for f in os.listdir(".")]])
		// Directory.GetFiles(@"C:\Users\ppedv\Desktop\Richard\py").Select(e => File.ReadAllLines(e).Length).Sum();

		DateTime start = DateTime.Now;
		DateTime ende = DateTime.Now;
        Console.WriteLine((ende - start).Ticks);

        Console.WriteLine($"Das Hochkomma: \" {{{zahl}}} ");
    }

	public void Test(Person p) { }

	public void Test(int x) => Console.WriteLine(x);

	//public ref Person Test()
	//{
	//	Person p = new Person();
	//	return ref p;
	//}
}

public class Person
{
	public int ID { get; set; }

	public string Name { get; set; }

	public string Adresse { get; set; }

	public DateTime Geburtsdatum { get; set; }

	/// <summary>
	/// Methode/Konstruktor konfigurierbar machen über optionale Parameter
	/// 20+ Parameter...
	/// </summary>
    public Person(int ID = 0, string Name = "", string Adresse = "", DateTime GebDat = default)
    {
        //...
    }

    public void Deconstruct(out int ID, out string Name)
	{
		ID = this.ID;
		Name = this.Name;
	}
}

public record Person2
(
	//field: Um auf Felder von Records Attribute anzuhängen
	[field: Obsolete] int ID, string Name, string Adresse, DateTime GebDat
)
{
	public void Test()
	{

	}
}