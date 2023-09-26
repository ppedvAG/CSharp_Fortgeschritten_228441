using System.Diagnostics;

namespace Multitasking;

internal class _06_ParallelForDemo
{
	static void Main(string[] args)
	{
		int[] durchgänge = { 1000, 10_000, 50_000, 100_000, 250_000, 500_000, 1_000_000, 5_000_000, 10_000_000, 100_000_000 };
		for (int i = 0; i < durchgänge.Length; i++)
		{
			int d = durchgänge[i];

			Stopwatch sw = Stopwatch.StartNew();
			RegularFor(d);
			sw.Stop();
			Console.WriteLine($"For Durchgänge: {d}, {sw.ElapsedMilliseconds}ms");

			Stopwatch sw2 = Stopwatch.StartNew();
			ParallelFor(d);
			sw2.Stop();
			Console.WriteLine($"ParallelFor Durchgänge: {d}, {sw2.ElapsedMilliseconds}ms");
		}

		/*
			For Durchgänge: 1000, 1ms
			ParallelFor Durchgänge: 1000, 34ms
			For Durchgänge: 10000, 3ms
			ParallelFor Durchgänge: 10000, 1ms
			For Durchgänge: 50000, 12ms
			ParallelFor Durchgänge: 50000, 11ms
			For Durchgänge: 100000, 22ms
			ParallelFor Durchgänge: 100000, 7ms
			For Durchgänge: 250000, 67ms
			ParallelFor Durchgänge: 250000, 80ms
			For Durchgänge: 500000, 126ms
			ParallelFor Durchgänge: 500000, 52ms
			For Durchgänge: 1000000, 257ms
			ParallelFor Durchgänge: 1000000, 107ms
			For Durchgänge: 5000000, 2696ms
			ParallelFor Durchgänge: 5000000, 559ms
			For Durchgänge: 10000000, 3022ms
			ParallelFor Durchgänge: 10000000, 674ms
			For Durchgänge: 100000000, 18521ms
			ParallelFor Durchgänge: 100000000, 9085ms
		 */
	}

	static void RegularFor(int iterations)
	{
		double[] erg = new double[iterations];
		for (int i = 0; i < iterations; i++)
			erg[i] = (Math.Pow(i, 0.333333333333) * Math.Sin(i + 2) / Math.Exp(i) + Math.Log(i + 1)) * Math.Sqrt(i + 100);
	}

	static void ParallelFor(int iterations)
	{
		double[] erg = new double[iterations];
		//int i = 0; i < iterations; i++
		Parallel.For(0, iterations, i => erg[i] = (Math.Pow(i, 0.333333333333) * Math.Sin(i + 2) / Math.Exp(i) + Math.Log(i + 1)) * Math.Sqrt(i + 100));
	}
}
