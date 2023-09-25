namespace DelegatesEvents;

public class Component
{
	//Diese Komponente verrichtet eine länger andauernde Arbeit und soll ihren Fortschritt über Events mitteilen

	public event Action ProcessStarted;

	public event EventHandler<int> Progress;

	public event Action ProcessComplete;

	public void DoWork() //Datenbank Daten holen
	{
		ProcessStarted?.Invoke();
		for (int i = 0; i < 10; i++)
		{
			Progress?.Invoke(this, i);
			Thread.Sleep(200);
		}
		ProcessComplete?.Invoke();
	}
}
