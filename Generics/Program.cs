namespace Generics;

internal class Program
{
	static void Main(string[] args)
	{
		List<int> x = new();
		x.Add(1); //T ist jetzt int

		DataStore<int> ds;
	}
}

public class DataStore<T> : IProgress<T>
{
	public T[] data;

	public List<T> List => data.ToList();

	public T GetIndex(int index)
	{
		if (index < 0 || index > data.Length)
			return default; //default: Nimmt den Standardwert von T
		return data[index];
	}

	public void Add(T item, int index)
	{
		data[index] = item; 
	}

	public void Report(T value)
	{
        //...
    }

	public void Methode<TT>()
	{
        Console.WriteLine(default(TT));
        Console.WriteLine(typeof(TT)); //Konkreter Typ hinter TT
        Console.WriteLine(nameof(TT)); //Name des Typs als String (hinter TT)

		object o = 123;
		Convert.ChangeType(o, typeof(TT)); //Cast mit Generic
    }
}

public class Person { }

public class Mensch : Person { }