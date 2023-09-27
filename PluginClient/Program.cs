using PluginBase;
using System.Reflection;

namespace PluginClient;

internal class Program
{
	static void Main(string[] args)
	{
		IPlugin calcPlugin = LoadPlugin(@"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2023_09_25\PluginCalculator\bin\Debug\net7.0\PluginCalculator.dll"); //Liste der zu ladenden Plugins sollte in einer Config stehen
		MethodInfo[] methods = calcPlugin
			.GetType()
			.GetMethods()
			.Where(e => e.GetCustomAttribute<ReflectionVisible>() != null)
			.ToArray();
		for (int i = 0; i < methods.Length; i++)
		{
			Console.WriteLine($"{i}: {methods[i].GetCustomAttribute<ReflectionVisible>().Name}");
        }

        Console.WriteLine("Gib eine Zahl ein: ");
		int auswahl = int.Parse(Console.ReadLine());
        Console.WriteLine(methods[auswahl].Invoke(calcPlugin, new object[] { 2.2, 5.5 }));
    }

	static IPlugin LoadPlugin(string path)
	{
		Assembly loadedDLL = Assembly.LoadFrom(path);
		Type loadedType = loadedDLL.DefinedTypes.First(e => e.ImplementedInterfaces.Contains(typeof(IPlugin))).AsType();
		IPlugin plugin = Activator.CreateInstance(loadedType) as IPlugin;
		return plugin;
	}
}