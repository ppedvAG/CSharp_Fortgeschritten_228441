namespace Multithreading;

internal class _07_Mutex
{
	static void Main(string[] args)
	{
		bool b = Mutex.TryOpenExisting("M", out Mutex r);
		Mutex m = null;
		if (b)
		{
            Console.WriteLine("Programm läuft bereits");
			Environment.Exit(0);
        }
		else
		{
			m = new Mutex(true, "M");
		}
		Console.ReadKey();
		m?.ReleaseMutex();
	}
}
