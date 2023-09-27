using System.Reflection;
using System.Threading.Channels;

namespace Reflection;

internal class Program
{
	private int X;

	static void Main(string[] args)
	{
		Program p = new();
		Type pt = p.GetType(); //Type über Objekt holen
		Type t = typeof(Program); //Type über Klassenname holen

		//Über ein Type Objekt können alle möglichen Dinge herausgefunden werden

		Convert.ChangeType(p, t); //Cast eines Objekts auf einen bestimmten Typen

		Activator.CreateInstance(t); //Erzeugt ein Objekt vom Typ t

		/////////////////////////////////////////////

		//private Feld angreifen
		FieldInfo fi = pt.GetField("X", BindingFlags.NonPublic | BindingFlags.Instance); //100100
		fi.SetValue(p, 123); //Muss ein Objekt angegeben werden

        Console.WriteLine(fi.GetValue(p));

		/////////////////////////////////////////////
		
		Assembly a = Assembly.GetExecutingAssembly(); //Die derzeitige Projektumgebung

		Assembly loadedDLL = Assembly.LoadFrom(@"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2023_09_25\DelegatesEvents\bin\Debug\net7.0\DelegatesEvents.dll"); //Externe DLL laden

		Type compType = loadedDLL.DefinedTypes.First(e => e.Name == "Component").AsType();

		object comp = Activator.CreateInstance(compType);
		//compType.GetEvent("Progress").AddEventHandler(comp, new EventHandler<int>(comp, x)); //Hier wissen wir dank Reflection nicht was das Delegate für einen Typen hat
		compType.GetEvent("ProcessStarted").AddEventHandler(comp, () => Console.WriteLine("Start")); //Hier wissen wir dank Reflection nicht was das Delegate für einen Typen hat
		compType.GetEvent("ProcessComplete").AddEventHandler(comp, () => Console.WriteLine("Ende")); //Hier wissen wir dank Reflection nicht was das Delegate für einen Typen hat
		compType.GetMethod("DoWork").Invoke(comp, null);
	}
}