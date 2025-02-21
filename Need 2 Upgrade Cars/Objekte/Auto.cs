using Need_2_Upgrade_Cars.Menues;
using Spectre.Console;

namespace Need_2_Upgrade_Cars.Objekte;

public class Auto
{
    // Deklaration von Eigenschaften eines Autos
    public int Leistung { get; set; }
    public bool HatTurbolader { get; set; }
    public Lackierung Lackierung { get; set; }
    public Reifen Reifen { get; set; }
    public Motorart Motor { get; set; }
    public Getriebeart Getriebe { get; set; }
    public Antrieb Antrieb { get; set; }
    public Radio Radio { get; private set; }
    public Person Besitzer { get; }

    public Auto()
    {
        Radio = new Radio();
        Lackierung = new Lackierung(LackierungsArt.Hochglanz, Farbe.Schwarz);
        Reifen = new Reifen(Hersteller.Goodyear, 20, Farbe.Schwarz, ReifenArt.Sommerreifen);
        Besitzer = Person.Fabrik.ErstelleZufällig();
    }

    // Konstruktor mit Initialisierungssicherungen
    public Auto(int leistung, bool hatTurbolader, Lackierung? lackierung, Reifen? reifen, Motorart motor, Getriebeart getriebe, Antrieb antrieb, Person? besitzer)
    {
        Leistung = leistung;
        HatTurbolader = hatTurbolader;
        Lackierung = lackierung ?? new Lackierung(LackierungsArt.Hochglanz, Farbe.Schwarz);
        Reifen = reifen ?? new Reifen(Hersteller.Goodyear, 20, Farbe.Schwarz, ReifenArt.Sommerreifen);
        Motor = motor;
        Getriebe = getriebe;
        Antrieb = antrieb;
        Besitzer = besitzer ?? Person.Fabrik.ErstelleZufällig();
        Radio = new Radio();
    }

    public void ZeigeDetails()
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine($"[bold cyan]Details des Autos von {(Besitzer != null ? Besitzer : "Unbekannt")}[/]");

        // Erstellen einer leeren Tabelle

        var tabelleAutoDetails = new Table();

        // Hinzufügen von Spalten
        tabelleAutoDetails.AddColumn("Eigenschaft");
        tabelleAutoDetails.AddColumn("Wert");

        // Hinzufügen von Zeilen
        tabelleAutoDetails.AddRow("Leistung", Leistung.ToString() + " PS");
        tabelleAutoDetails.AddRow("Motorart", Motor.ToString());
        tabelleAutoDetails.AddRow("Getriebe", Getriebe.ToString());
        tabelleAutoDetails.AddRow("Turbolader", HatTurbolader ? "[green]Ja[/]" : "[red]Nein[/]");
        tabelleAutoDetails.AddRow("Lackierung", $"{Lackierung.Art} - {Lackierung.Farbe}");
        tabelleAutoDetails.AddRow("Antrieb", Antrieb.ToString());
        tabelleAutoDetails.AddRow("Reifen", $"{Reifen.Hersteller}, {Reifen.Felgendurchmesser} Zoll, {Reifen.ReifenArt}, {Reifen.FelgenFarbe}");

        // Ausgabe der Tabelle
        AnsiConsole.Write(tabelleAutoDetails);

        AnsiConsole.MarkupLine("\n[bold yellow]Drücke eine beliebige Taste, um fortzufahren...[/]");
        Console.ReadKey();

        AnsiConsole.Clear();
    }

    public class Fabrik
    {
        public static Auto ErstelleZufällig()
        {
            return new Auto(
                Spiel.Zufall.Next(100, 400), // Leistung zufällig zwischen 100-400 PS
                Spiel.Zufall.Next(2) == 1,   // Turbolader ja/nein
                Lackierung.Fabrik.ErstelleZufällig(),
                Reifen.Fabrik.ErstelleZufällig(),
                (Motorart)Spiel.Zufall.Next(Enum.GetValues<Motorart>().Length),
                (Getriebeart)Spiel.Zufall.Next(Enum.GetValues<Getriebeart>().Length),
                (Antrieb)Spiel.Zufall.Next(Enum.GetValues<Antrieb>().Length),
                Person.Fabrik.ErstelleZufällig()
            );
        }
    }
}