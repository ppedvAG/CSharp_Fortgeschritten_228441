using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsyncAwaitWPF
{
	public partial class MainWindow : Window
	{
        public string Buch { get; set; }

        public MainWindow()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Progress.Value = 0;
			for (int i = 0; i < Progress.Maximum; i++)
			{
				Progress.Value++;
				Thread.Sleep(25);
				//Thread.Sleep blockiert den Main Thread
				//Thread.Sleep kann hier auch durch eine längere Arbeit ersetzt werden (z.B. Datenbankabfrage)
			}
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			Task.Run(() =>
			{
				Dispatcher.Invoke(() => Progress.Value = 0); //UI Updates sind von Side Threads/Tasks nicht erlaubt
				for (int i = 0; i < Dispatcher.Invoke(() => Progress.Maximum); i++)
				{
					Dispatcher.Invoke(() => Progress.Value++); //Mit Dispatcher das Update auf den Main Thread legen
					Thread.Sleep(25);
				}
			});
		}

		private async void Button_Click_2(object sender, RoutedEventArgs e)
		{
			Progress.Value = 0;
			for (int i = 0; i < Progress.Maximum; i++)
			{
				Progress.Value++;
				//Task.Run(() => Thread.Sleep(25)).Wait();
				await Task.Delay(25);
			}
		}

		private async void Button1_Click(object sender, RoutedEventArgs e)
		{
			using HttpClient client = new();
			Task<HttpResponseMessage> get = client.GetAsync(@"http://www.gutenberg.org/files/54700/54700-0.txt"); //Hier Request starten
			//UI Updates
			TB.Text = "Text wird geladen...";
			Button1.IsEnabled = false;
			HttpResponseMessage response = await get; //Auf das Ergebnis warten
			if (response.IsSuccessStatusCode)
			{
				TB.Text = "Text wird ausgelesen...";
				string text = await response.Content.ReadAsStringAsync(); //Text aus Response auslesen
				await Task.Delay(500);
				TB.Text = text;

				Buch = Enumerable.Range(0, 20).Aggregate(new StringBuilder(), (agg, zahl) => agg.Append(text)).ToString();

				Button1.IsEnabled = true;
			}
		}

		private async void Button_Click_3(object sender, RoutedEventArgs e)
		{
			int x = Buch.Length;
			ConcurrentBag<string> paths = new(Enumerable.Range(0, 100).Select(e => $"Output\\Buch{e}.txt"));

			if (!Directory.Exists("Output"))
				Directory.CreateDirectory("Output");

			//await Parallel.ForEachAsync(paths, (element, ct) =>
			//{
			//	File.WriteAllText(element, Buch);
			//	return ValueTask.CompletedTask;
			//});

			foreach (string path in paths) //1s ForEachAsync, 20s ForEach normal
				await File.WriteAllTextAsync(path, Buch);
		}
	}
}
