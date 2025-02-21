using Spectre.Console;

namespace Need_2_Upgrade_Cars.Objekte;

public class Lackierung
{
    // Deklaration von Eigenschaften
    public LackierungsArt Art { get; set; }
    public Farbe Farbe { get; set; }
    // Verpacken der Eigenschaften in einen Konstruktoren
    public Lackierung(LackierungsArt art, Farbe farbe)
    {
        Art = art;
        Farbe = farbe;
    }

    public class Fabrik
    {
        public static Lackierung ErstelleZufällig()
        {
            return new Lackierung(
                (LackierungsArt)Spiel.Zufall.Next(Enum.GetValues<LackierungsArt>().Length),
                (Farbe)Spiel.Zufall.Next(Enum.GetValues<Farbe>().Length)
            );
        }

        public static Lackierung ErstelleViaInput() => new(
                art: AnsiConsole.Prompt(
                new SelectionPrompt<LackierungsArt>()
                    .Title("Bitte die neue Art der Lackierung wählen:")
                    .AddChoices(Enum.GetValues<LackierungsArt>())
                ),
                farbe: AnsiConsole.Prompt(
                new SelectionPrompt<Farbe>()
                    .Title("Bitte die neue Lackierungsfarbe wählen:")
                    .AddChoices(Enum.GetValues<Farbe>())
                )
        );
    }
}