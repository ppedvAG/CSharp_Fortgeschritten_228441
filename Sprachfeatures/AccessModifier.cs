using System.Runtime.CompilerServices;

namespace M008;

public class AccessModifier //Member ohne Modifier sind internal
{
	public string Name { get; set; } //Kann überall gesehen werden, auch außerhalb vom Projekt

	internal int Alter { get; set; } //Kann überall gesehen werden, aber nur innerhalb vom Projekt

	private string Wohnort { get; set; } //Kann nur innerhalb dieser Klasse gesehen werden

	protected string Spitzname { get; set; } //Kann nur innerhalb dieser Klasse gesehen werden (private) aber auch in Unterklassen (auch außerhalb vom Projekt)

	protected private decimal Gehalt { get; set; } //Kann nur innerhalb dieser Klasse gesehen werden (private) aber auch in Unterklassen (nur innerhalb vom Projekt)

	protected internal string Haarfarbe { get; set; } //Kann überall gesehen werden, aber nur innerhalb vom Projekt, und zusätzlich in Unterklassen außerhalb vom Projekt
}
