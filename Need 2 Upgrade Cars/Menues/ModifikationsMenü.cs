using Need_2_Upgrade_Cars.Objekte;
using Spectre.Console;

namespace Need_2_Upgrade_Cars.Menues;

public class AutoModifikationsMenü : Menü
{
    private Auto auto;
    public override List<Option> MenüOptionen =>
    [
        new AktionOption("Leistung ändern", LeistungÄndern),
        new AktionOption("Getriebe ändern", GetriebeÄndern),
        new AktionOption("Turbolader ein/aus", TurboladerÄndern),
        new AktionOption("Lackierung ändern", LackierungÄndern),
        new AktionOption("Reifen ändern", ReifenÄndern),
        new AktionOption("Antrieb ändern", AntriebÄndern),
        new AktionOption("Motor ändern", MotorartÄndern),
        new MenüOption("Radio starten", new RadioMenü(this, auto.Radio))
    ];
    public AutoModifikationsMenü(Menü vorherigesMenü, Auto auto)
    : base("Auto modifizieren", "",[], vorherigesMenü)
    {
        this.auto = auto;
    }

    private void MotorartÄndern()
    {
        AnsiConsole.Clear();
        auto.Motor = AnsiConsole.Prompt(
            new SelectionPrompt<Motorart>()
                .Title("Wähle die neue Motorart:")
                .AddChoices(Enum.GetValues<Motorart>())
        );

        Console.Clear();
        AnsiConsole.MarkupLine($"[green]Motor erfolgreich auf {auto.Motor} umgerüstet![/]");

        Anzeigen();
    }
    private void LeistungÄndern()
    {
        AnsiConsole.Clear();
        int neueLeistung = AnsiConsole.Prompt(
            new TextPrompt<int>($"Aktuelle Leistung: {auto.Leistung} PS. Gib neue Leistung ein:")
                .DefaultValue(auto.Leistung)
            );

        auto.Leistung = neueLeistung;
        Console.Clear();
        AnsiConsole.MarkupLine("[green]Leistung erfolgreich geändert![/]");

        Anzeigen();
    }

    private void TurboladerÄndern()
    {
        AnsiConsole.Clear();
        auto.HatTurbolader = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Turbolader aktivieren/deaktivieren")
                .AddChoices("Aktivieren", "Deaktivieren")
        ) == "Aktivieren";

        Console.Clear();
        AnsiConsole.MarkupLine($"[green]Turbolader wurde {(auto.HatTurbolader ? "aktiviert" : "deaktiviert")}![/]");

        Anzeigen();
    }

    private void LackierungÄndern()
    {
        AnsiConsole.Clear();

        auto.Lackierung = Lackierung.Fabrik.ErstelleViaInput();
        Console.Clear();
        AnsiConsole.MarkupLine("[green]Lackierung erfolgreich geändert![/]");

        Anzeigen();
    }

    private void ReifenÄndern()
    {
        AnsiConsole.Clear();

        auto.Reifen = Reifen.Fabrik.ErstelleViaInput();
        Console.Clear();
        AnsiConsole.MarkupLine("[green]Reifen erfolgreich geändert![/]");

        Anzeigen();
    }
    private void GetriebeÄndern()
    {
        AnsiConsole.Clear();
        auto.Getriebe = AnsiConsole.Prompt(
            new SelectionPrompt<Getriebeart>()
                .Title("Wähle die neue Getriebeart aus:")
                .AddChoices(Enum.GetValues<Getriebeart>())
        );

        Console.Clear();
        AnsiConsole.MarkupLine($"[green]Getriebe erfolgreich auf {auto.Getriebe} umgerüstet![/]");

        Anzeigen();
    }
    private void AntriebÄndern()
    {
        AnsiConsole.Clear();
        auto.Antrieb = AnsiConsole.Prompt(
            new SelectionPrompt<Antrieb>()
                .Title("Wähle die neue Antriebsart aus:")
                .AddChoices(Enum.GetValues<Antrieb>())
        );

        Console.Clear();
        AnsiConsole.MarkupLine($"Antrieb erfolgreich auf {auto.Antrieb} geändert!");

        Anzeigen();
    }
}