using Spectre.Console;

namespace Need_2_Upgrade_Cars.Objekte;

public class Radio
{
    private List<string> senderListe;
    private List<string> favoriten;
    private int aktuellerSenderIndex;
    public int Lautstaerke { 
        get => lautstaerke;
        set
        {
            if (value > 10)
                lautstaerke = 10;
            if (value < 0)
                lautstaerke = 0;
            else
                lautstaerke = value;
        }
    }
    public bool istEingeschaltet;
    public bool BluetoothAktiv { get; private set; }
    private List<string> MP3Dateien = new List<string> { "Song1.mp3", "Song2.mp3", "Song3. mp3" };

    private CancellationTokenSource? sleepTimerCancellationTokenSource;
    private int lautstaerke;

    public Radio()
    {
        senderListe = ["N-JOY", "ffn", "NDR 2", "NDR 1 NDS", "ANTENNE", "ENERGY", "RADIO BOB!", "NDR Info", "os radio"];
        favoriten = new List<string>();
        aktuellerSenderIndex = 0;
        Lautstaerke = 5;
        istEingeschaltet = false;
    }

    public bool IstEingeschaltet => istEingeschaltet;

    public void Einschalten()
    {
        istEingeschaltet = true;
        AnsiConsole.MarkupLine($"[green]Radio eingeschaltet: {senderListe[aktuellerSenderIndex]}[/]");
    }

    public void Ausschalten()
    {
        istEingeschaltet = false;
        AnsiConsole.MarkupLine("[red]Radio ausgeschaltet![/]");
    }

    public void NaechsterSender()
    {
        if (!istEingeschaltet) return;
        aktuellerSenderIndex = (aktuellerSenderIndex + 1) % senderListe.Count;
        AnsiConsole.MarkupLine($"[blue]Sender gewechselt: {senderListe[aktuellerSenderIndex]}[/]");
    }

    public void LautstaerkeErhoehen()
    {
        Lautstaerke++;
        AnsiConsole.MarkupLine($"[yellow]Lautstärke: {Lautstaerke}[/]");
    }

    public void LautstaerkeVerringern()
    {
        Lautstaerke--;
        AnsiConsole.MarkupLine($"[yellow]Lautstärke: {Lautstaerke}[/]");
    }

    public void FavoritHinzufuegen()
    {
        string aktuellerSender = senderListe[aktuellerSenderIndex];
        if (!favoriten.Contains(aktuellerSender))
        {
            favoriten.Add(aktuellerSender);
            AnsiConsole.MarkupLine($"[green]{aktuellerSender} zu Favoriten hinzugefügt![/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[yellow]{aktuellerSender} ist bereits ein Favorit.[/]");
        }
    }

    public void FavoritEntfernen()
    {
        string aktuellerSender = senderListe[aktuellerSenderIndex];
        if (favoriten.Remove(aktuellerSender))
        {
            AnsiConsole.MarkupLine($"[red]{aktuellerSender} aus Favoriten entfernt.[/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[yellow]{aktuellerSender} ist kein Favorit.[/]");
        }
    }

    public void FavoritenAnzeigen()
    {
        if (favoriten.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]Keine Favoriten gespeichert.[/]");
            return;
        }

        var favoritenTabelle = new Table();
        favoritenTabelle.AddColumn("Nr.");
        favoritenTabelle.AddColumn("Sender");

        for (int i = 0; i < favoriten.Count; i++)
        {
            favoritenTabelle.AddRow((i + 1).ToString(), favoriten[i]);
        }

        AnsiConsole.Write(favoritenTabelle);
    }

    public void DirektwahlFavoriten()
    {
        if (favoriten.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]Keine Favoriten verfügbar![/]");
            return;
        }

        FavoritenAnzeigen();

        int auswahl = AnsiConsole.Prompt(
            new SelectionPrompt<int>()
                .Title("Wähle einen Favoriten aus:")
                .AddChoices(Enumerable.Range(1, favoriten.Count))
        );

        AnsiConsole.Clear();

        string gewaehlterSender = favoriten[auswahl - 1];
        aktuellerSenderIndex = senderListe.IndexOf(gewaehlterSender);

        if (aktuellerSenderIndex != -1)
        {
            AnsiConsole.MarkupLine($"[bold green]Wechsle zu: {senderListe[aktuellerSenderIndex]}[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]FEHLER: Sender nicht gefunden![/]");
        }
    }
    public void SleepTimer(int minuten)
    {
        if (!istEingeschaltet)
        {
            AnsiConsole.MarkupLine("[red]Das Radio ist ausgeschaltet![/]");
            return;
        }

        if (minuten <= 0)
        {
            AnsiConsole.MarkupLine("[yellow]Ungültige Zeit![/]");
            return;
        }
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine($"[green]Sleep-Timer gesetzt: {minuten} Minuten[/]");

        Task.Run(async () =>
        {
            try
            {
                await Task.Delay(minuten * 60000);
                if (istEingeschaltet)
                {
                    AnsiConsole.Clear();
                    Ausschalten();
                    AnsiConsole.MarkupLine("[blue]Sleep-Timer abgelaufen: Radio ausgeschaltet![/]");
                }
            }
            catch (TaskCanceledException)
            {
                AnsiConsole.MarkupLine("[yellow]Sleep-Timer abgebrochen.[/]");
            }
        });
    }
    public void SleepTimerAbbrechen()
    {
        if (sleepTimerCancellationTokenSource != null)
        {
            sleepTimerCancellationTokenSource.Cancel();
            sleepTimerCancellationTokenSource = null;
        }
    }

    public void ZeigeVerkehrsInfo()
    {
        AnsiConsole.Clear();
        // Simulierte Verkehrsinfo – Echtzeitmeldungen erfordern hier eine API
        AnsiConsole.MarkupLine("[bold cyan]Aktuelle Verkehrsinfo:[/]");
        AnsiConsole.MarkupLine("Stau auf der A3, ca. 20 Minuten Verzögerung.");
        AnsiConsole.MarkupLine("\nDrücke eine beliebige Taste, um fortzufahren...");
        Console.ReadKey();
        AnsiConsole.Clear();
    }

    public void ZeigeWetterInfo()
    {
        AnsiConsole.Clear();
        // Simulierte Wetterinfo - Echtzeitinfos erfordern hier eine API
        AnsiConsole.MarkupLine("[bold cyan]Aktuelle Wetterinfo:[/]");
        AnsiConsole.MarkupLine("Leicht bewölkt, 18°C, vereinzelte Regenschauer möglich.");
        AnsiConsole.MarkupLine("\nDrücke eine beliebige Taste, um fortzufahren...");
        Console.ReadKey();
        AnsiConsole.Clear();
    }

    public void SetzeFrequenz()
    {
        AnsiConsole.Clear();
        double frequenz = AnsiConsole.Ask<double>("[green]Gib eine Frequenz ein (z. B. 101.1 MHz):[/]");


        if (frequenz < 87.5 || frequenz > 108.0)
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[red]Ungültige Frequenz! Bitte gib eine Frequenz zwischen 87.5 und 108.0 MHz ein.[/]");
        }
        else
        {
            senderListe.Add($"{frequenz} MHz");
            aktuellerSenderIndex = senderListe.Count - 1;
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine($"[blue]Manuelle Frequenz eingestellt: {frequenz} MHz[/]");
        }
    }
}