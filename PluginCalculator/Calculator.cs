using PluginBase;

namespace PluginCalculator;

public class Calculator : IPlugin
{
	public string Name => "Rechner";

	public string Description => "Ein einfacher Rechner";

	public string Version => "1.0";

	public string Author => "Lukas Kern";

	[ReflectionVisible("Add")]
	public double Addiere(double x, double y) => x + y;

	[ReflectionVisible("Sub")]
	public double Subtrahiere(double x, double y) => x - y;
}