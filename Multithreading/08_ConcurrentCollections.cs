using System.Collections.Concurrent;

namespace Multithreading;

internal class _08_ConcurrentCollections
{
	static void Main(string[] args)
	{
		ConcurrentBag<int> ints = new ConcurrentBag<int>(); //List
		ints.Add(1);
		//ints[] //Kein Index

		ConcurrentDictionary<int, string> dict = new();
	}
}
