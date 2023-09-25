public class Program
{
	static void Main(string[] args)
	{
		//Eigenen Code hier schreiben
	}

	public static void Addition(double zahl1, double zahl2)
	{
		Console.WriteLine($"{zahl1} + {zahl2} = {zahl1 + zahl2}");
	}

	public static void Subtraktion(double zahl1, double zahl2)
	{
		Console.WriteLine($"{zahl1} - {zahl2} = {zahl1 - zahl2}");
	}

	public static void Multiplikation(double zahl1, double zahl2)
	{
		Console.WriteLine($"{zahl1} * {zahl2} = {zahl1 * zahl2}");
	}
}

public class DivisionsCalculator
{
	public static void Division(double zahl1, double zahl2)
	{
		Console.WriteLine($"{zahl1} : {zahl2} = {zahl1 /  zahl2}");
	}
}

/// <summary>
/// Diese Methode kann für das dritte Event bearbeitet werden
/// </summary>
/// <param name="num"></param>
/// <returns></returns>
//public bool CheckPrime(int num)
//{
//	if (num % 2 == 0)
//	{
//		return false;
//	}

//	for (int i = 3; i <= num / 2; i += 2)
//	{
//		if (num % i == 0)
//		{
//			return false;
//		}
//	}
//	return true;
//}