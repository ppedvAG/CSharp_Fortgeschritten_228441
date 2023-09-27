namespace PluginBase;

public interface IPlugin
{
	string Name { get; }

	string Description { get; }

	string Version { get; }

	string Author { get; }
}