using Spectre.Console;

namespace Need_2_Upgrade_Cars.Objekte;

public class Reifen
{
    // Deklaration von Eigenschaften von Reifen
    public Hersteller Hersteller { get; set; }
    public int Felgendurchmesser { get; set; }
    public Farbe FelgenFarbe { get; set; }
    public ReifenArt ReifenArt { get; set; }
    // Konstruktor zum Verpacken aller Eigenschaften
    public Reifen(Hersteller hersteller, int felgendurchmesser, Farbe felgenfarbe, ReifenArt reifenArt)
    {
        Hersteller = hersteller;
        Felgendurchmesser = felgendurchmesser;
        FelgenFarbe = felgenfarbe;
        ReifenArt = reifenArt;
    }

    public class Fabrik
    {
        public static Reifen ErstelleZufällig()
        {
            return new Reifen(
                (Hersteller)Spiel.Zufall.Next(Enum.GetValues<Hersteller>().Length),
                Spiel.Zufall.Next(16, 22), // Felgendurchmesser zwischen 16 und 22 Zoll
                (Farbe)Spiel.Zufall.Next(Enum.GetValues<Farbe>().Length),
                (ReifenArt)Spiel.Zufall.Next(Enum.GetValues<ReifenArt>().Length)
            );
        }

        public static Reifen ErstelleViaInput() => new(
                hersteller: AnsiConsole.Prompt(
                    new SelectionPrompt<Hersteller>()
                        .Title("Bitte den neuen Reifenhersteller wählen:")
                        .AddChoices(Enum.GetValues<Hersteller>())
                ),
                felgendurchmesser: AnsiConsole.Prompt(
                    new TextPrompt<int>("Gib den neuen Felgendurchmesser ein (Zoll):")
                ),
                felgenfarbe: AnsiConsole.Prompt(
                    new SelectionPrompt<Farbe>()
                        .Title("Wähle die neue Felgenfarbe:")
                        .AddChoices(Enum.GetValues<Farbe>())
                ),
                reifenArt: AnsiConsole.Prompt(
                     new SelectionPrompt<ReifenArt>()
                        .Title("Bitte die neue Art vom Reifen wählen:")
                        .AddChoices(Enum.GetValues<ReifenArt>())
                )

        );
    }
}