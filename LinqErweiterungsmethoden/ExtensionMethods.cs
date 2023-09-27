using System.Text;
using System.Text.Json;

namespace LinqErweiterungsmethoden;

public static class ExtensionMethods
{
	public static int Quersumme(this int x)
	{
		return x.ToString().Sum(x => (int) char.GetNumericValue(x));
	}

	public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> x)
	{
		//Dictionary<T, int> elementeMitZahl = new();
		//foreach (T element in x)
		//{
		//	elementeMitZahl.Add(element, Random.Shared.Next());
		//}
		//IOrderedEnumerable<KeyValuePair<T, int>> order = elementeMitZahl.OrderBy(e => e.Value);
		//return order.Select(e => e.Key);

		return x.OrderBy(e => Random.Shared.Next()); //Weist jedem Element eine Zufallszahl zu und sortiert dann nach dieser
	}

	public static T GetValue<T>(this JsonElement x)
	{
		return (T) Convert.ChangeType(x, typeof(T));
	}

	public static JsonElement GetPropertyChain(this JsonElement x, string prop)
	{
		JsonElement result = default;
		string[] properties = prop.Split(".");
		foreach (string property in properties)
		{
			result = x.GetProperty(property);
		}
		return result;
	}


	public static void PrintList<T>(this IEnumerable<T> x)
	{
		StringBuilder sb = new StringBuilder();
		sb.Append("[");
		sb.Append(string.Join(", ", x));
		sb.Append("]");
        Console.WriteLine(sb.ToString());
    }

	/// <summary>
	/// Printet alle Elemente einer Liste in Form der Selektors
	/// Der Selektor gibt die Form vor und nach der Form werden die Elemente mittels Komma zusammengebaut
	/// </summary>
	/// <typeparam name="TElement">Der Typ der Liste</typeparam>
	/// <typeparam name="TSelector">Der selektierte Wert (e => ...)</typeparam>
	public static void PrintList<TElement, TSelector>(this IEnumerable<TElement> x, Func<TElement, TSelector> selector)
	{
		StringBuilder sb = new StringBuilder();
		sb.Append("[");
		sb.Append(string.Join(", ", x.Select(e => selector(e))));
		sb.Append("]");
		Console.WriteLine(sb.ToString());
	}
}
