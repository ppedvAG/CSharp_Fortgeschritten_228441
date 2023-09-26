using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Serialisierung;

internal class Program
{
	static void Main(string[] args)
	{
		//File, Directory, Path
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		string filePath = Path.Combine(desktop, "Test.txt");

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new PKW(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		//SystemJson();

		//NewtonsoftJson();

		//XML();

		//CSV();
	}

	public static void SystemJson()
	{
		//File, Directory, Path
		//string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		//string filePath = Path.Combine(desktop, "Test.txt");

		//List<Fahrzeug> fahrzeuge = new()
		//{
		//	new Fahrzeug(0, 251, FahrzeugMarke.BMW),
		//	new Fahrzeug(1, 274, FahrzeugMarke.BMW),
		//	new Fahrzeug(2, 146, FahrzeugMarke.BMW),
		//	new Fahrzeug(3, 208, FahrzeugMarke.Audi),
		//	new Fahrzeug(4, 189, FahrzeugMarke.Audi),
		//	new Fahrzeug(5, 133, FahrzeugMarke.VW),
		//	new Fahrzeug(6, 253, FahrzeugMarke.VW),
		//	new Fahrzeug(7, 304, FahrzeugMarke.BMW),
		//	new Fahrzeug(8, 151, FahrzeugMarke.VW),
		//	new Fahrzeug(9, 250, FahrzeugMarke.VW),
		//	new Fahrzeug(10, 217, FahrzeugMarke.Audi),
		//	new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		//};

		///////////////////////////////////////////////
		////			  System.Text.Json             //
		///////////////////////////////////////////////

		////Teil 2: Einstellungen (Options)
		//JsonSerializerOptions options = new JsonSerializerOptions(); //Wichtig: Options beim (De-)Serialisieren übergeben
		//options.WriteIndented = true; //Json schön schreiben
		//options.ReferenceHandler = ReferenceHandler.IgnoreCycles; //Zirkelbezüge ignorieren

		////Teil 1: Json schreiben und lesen
		//string json = JsonSerializer.Serialize(fahrzeuge, options);
		//File.WriteAllText(filePath, json);

		//string readJson = File.ReadAllText(filePath);
		//List<Fahrzeug> readFzg = JsonSerializer.Deserialize<List<Fahrzeug>>(readJson, options);

		////Teil 3: Json per Hand durchgehen
		//JsonDocument doc = JsonDocument.Parse(readJson);
		//JsonElement.ArrayEnumerator ae = doc.RootElement.EnumerateArray(); //Enumerator erzeugen aus dem Json File -> Liste

		//double summe = 0;
		//int count = 0;
		//foreach (JsonElement fzg in ae) //ae = 12 Fahrzeuge
		//{
		//	summe += fzg.GetProperty("MaxV").GetInt32();
		//	count++;
		//}
		//Console.WriteLine($"Durchschnittsgeschwindigkeit: {summe / count}");

		//Teil 4: Attribute
		//Alle Felder die Serialisiert werden sollen, müssen Properties sein
		//JsonIgnore: Feld ignorieren
		//JsonPropertyName: Name von Feld ändern
		//JsonDerivedType: Ermöglicht das Serialisieren von Vererbungsstrukturen, benötigt den Type Discriminator
	}

	public static void NewtonsoftJson()
	{
		//File, Directory, Path
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		string filePath = Path.Combine(desktop, "Test.txt");

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new PKW(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		//SystemJson();

		/////////////////////////////////////////////
		//			   Newtonsoft.Json             //
		/////////////////////////////////////////////

		//Teil 2: Einstellungen (Options)
		JsonSerializerSettings settings = new JsonSerializerSettings(); //Wichtig: Options beim (De-)Serialisieren übergeben
		settings.Formatting = Newtonsoft.Json.Formatting.Indented; //Json schön schreiben
		settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; //Zirkelbezüge ignorieren
		settings.TypeNameHandling = TypeNameHandling.Objects; //Vererbung ermöglichen

		//Teil 1: Json schreiben und lesen
		string json = JsonConvert.SerializeObject(fahrzeuge, settings);
		File.WriteAllText(filePath, json);

		string readJson = File.ReadAllText(filePath);
		List<Fahrzeug> readFzg = JsonConvert.DeserializeObject<List<Fahrzeug>>(readJson, settings);

		//Teil 3: Json per Hand durchgehen
		JToken doc = JToken.Parse(readJson);
		double summe = 0;
		int count = 0;
		foreach (JToken fzg in doc) //ae = 12 Fahrzeuge
		{
			summe += fzg["MaxV"].Value<int>();
			count++;
		}
		Console.WriteLine($"Durchschnittsgeschwindigkeit: {summe / count}");

		//Teil 4: Attribute
		//Alle Felder die Serialisiert werden sollen, müssen Properties sein
		//JsonIgnore: Feld ignorieren
		//JsonProperty: Name von Feld ändern
	}

	public static void XML()
	{
		//File, Directory, Path
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		string filePath = Path.Combine(desktop, "Test.txt");

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new PKW(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		/////////////////////////////////////////////
		//			   System.Xml                  //
		/////////////////////////////////////////////

		//Teil 1: XML schreiben und lesen
		XmlSerializer xml = new XmlSerializer(fahrzeuge.GetType());
		StreamWriter sw = new StreamWriter(filePath);
		xml.Serialize(sw, fahrzeuge);
		sw.Close();

		using StreamReader sr = new StreamReader(filePath);
		List<Fahrzeug> readFzg = xml.Deserialize(sr) as List<Fahrzeug>;
		sr.BaseStream.Position = 0; //Stream zurücksetzen: Wenn ein Stream ausgelesen wird, steht der Cursor am Ende -> Zurück auf 0

		//Teil 2: XML per Hand durchgehen
		XmlDocument doc = new XmlDocument();
		doc.Load(sr);

		foreach (XmlNode node in doc.DocumentElement.ChildNodes) //Header überspringen
		{
			//Console.WriteLine(node["MaxV"].InnerText);
			Console.WriteLine(node.Attributes["MaxV"].InnerText); //Wenn ein Feld ein Attribut ist
		}

		//Teil 3: Attribute
		//XmlIgnore: Feld ignorieren
		//XmlAttribute: Konvertiert ein Feld zu einem Attribut anstatt einer eigener Node
		//XmlInclude: Vererbung serialisieren mit XML
	}

	public static void CSV()
	{
		//File, Directory, Path
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		string filePath = Path.Combine(desktop, "Test.txt");

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new PKW(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		TextFieldParser csv = new(filePath);
		csv.SetDelimiters(";");

		//csv.ReadToEnd();

		List<string[]> lines = new();
		while (!csv.EndOfData)
		{
			lines.Add(csv.ReadFields());
		}
	}
}

//System.Text.Json
//[JsonDerivedType(typeof(Fahrzeug), "F")]
//[JsonDerivedType(typeof(PKW), "P")]

[XmlInclude(typeof(Fahrzeug))]
[XmlInclude(typeof(PKW))]
public class Fahrzeug
{
	//System.Text.Json
	//[JsonIgnore]
	//[JsonPropertyName("Identifier")]
	[XmlAttribute]
	public int ID { get; set; }

	//Newtonsoft.Json
	//[JsonIgnore]
	//[JsonProperty("Maximalgeschwindigkeit")]
	[XmlAttribute]
	public int MaxV { get; set; }

	//[XmlIgnore]
	[XmlAttribute]
	public FahrzeugMarke Marke { get; set; }

	public Fahrzeug(int iD, int maxV, FahrzeugMarke marke)
	{
		ID = iD;
		MaxV = maxV;
		Marke = marke;
	}

    public Fahrzeug()
    {
        
    }
}

public class PKW : Fahrzeug
{
	public PKW(int iD, int maxV, FahrzeugMarke marke) : base(iD, maxV, marke)
	{
	}

    public PKW()
    {
        
    }
}

public enum FahrzeugMarke { Audi, BMW, VW }