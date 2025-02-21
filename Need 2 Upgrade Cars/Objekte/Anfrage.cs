using System.Linq.Expressions;
using System.Text;
using Spectre.Console;

namespace Need_2_Upgrade_Cars.Objekte;

public class Anfrage
{
    public int Id { get; private set; }
    public Person Kunde { get; set; }
    public string? Beschreibung { get; private set; }
    public Auto KundenAuto { get; }
    public AnforderungsProfil Anforderungen { get; set; }

    private Anfrage()
    {
        Kunde = new("","");
        KundenAuto = Auto.Fabrik.ErstelleZufällig();
        Anforderungen = new();
    }
    public Anfrage(int id, Person kunde, string beschreibung, Auto auto, AnforderungsProfil anforderungen)
    {
        Id = id;
        Kunde = kunde;
        Beschreibung = beschreibung;
        KundenAuto = auto;
        Anforderungen = anforderungen;
    }

    public override string ToString()
    {
        return $"[[ID: {Id}]] {Kunde}: {Beschreibung}";
    }

    public void ZeigeDetails()
    {
        Console.Clear();
        AnsiConsole.WriteLine($"[ID: {Id}] {Kunde}");
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"Anfrage: {Beschreibung}");

        AnsiConsole.MarkupLine("\n[bold cyan]Anforderungsprofil[/]");

        var tabelleAnforderungen = new Table();
        tabelleAnforderungen.AddColumn("Eigenschaft");
        tabelleAnforderungen.AddColumn("Wert");

        tabelleAnforderungen.AddRow("Min. Leistung in PS", (Anforderungen.MinLeistung?.ToString() ?? "Keine Angabe"));
        tabelleAnforderungen.AddRow("Max. Leistung in PS", (Anforderungen.MaxLeistung?.ToString() ?? "Keine Angabe"));
        tabelleAnforderungen.AddRow("Reifenart", (Anforderungen.ErforderlicheReifenArt?.ToString() ?? "Keine Angabe"));
        tabelleAnforderungen.AddRow("Getriebe", Anforderungen.ErforderlichesGetriebe?.ToString() ?? "Keine Angabe");
        tabelleAnforderungen.AddRow("Motor", Anforderungen.ErforderlicherMotor?.ToString() ?? "Keine Angabe");
        tabelleAnforderungen.AddRow("Turbolader", Anforderungen.ErfordertTurbolader.GetValueOrDefault(false) ? "[green]Ja[/]" : "[red]Nein[/]");
        tabelleAnforderungen.AddRow("Lackierungsart", Anforderungen.ErforderlicheLackierungsArt?.ToString() ?? "Keine Angabe");
        tabelleAnforderungen.AddRow("Lackierungsfarbe", Anforderungen.ErforderlicheLackierungsFarbe?.ToString() ?? "Keine Angabe");

        AnsiConsole.Write(tabelleAnforderungen);
    }

    public bool IstErfüllt()
    {
        return Anforderungen.IstErfüllt(KundenAuto);
    }

    public class Fabrik
    {
        public static Anfrage? ErstelleViaInput()
        {
            var anfrage = new Anfrage();

            while (true)
            {
                anfrage.ZeigeDetails();
                var auswahl = AnsiConsole.Prompt(
                   new SelectionPrompt<SelectionOption>()
                       .Title("Wähle eine Eigenschaft zum Bearbeiten oder 'Speichern' zum Fertigstellen")
                       .AddChoices(
                           new SelectionOption("Vorname", () => anfrage.Kunde.Vorname = AnsiConsole.Prompt(new TextPrompt<string>("Gib den Vornamen ein:"))),
                           new SelectionOption("Nachname", () => anfrage.Kunde.Nachname = AnsiConsole.Prompt(new TextPrompt<string>("Gib den Nachnamen ein:"))),
                           new SelectionOption("Beschreibung", () => anfrage.Beschreibung = AnsiConsole.Prompt(new TextPrompt<string>("Gib eine Beschreibung ein:"))),
                           new SelectionOption("Minimale Leistung", () => anfrage.Anforderungen.MinLeistung = AnsiConsole.Prompt(new TextPrompt<int>("Gib die minimale Leistung ein:"))),
                           new SelectionOption("Maximale Leistung", () => anfrage.Anforderungen.MaxLeistung = AnsiConsole.Prompt(new TextPrompt<int>("Gib die maximale Leistung ein:"))),
                           new SelectionOption("Reifenart", () => anfrage.Anforderungen.ErforderlicheReifenArt = AnsiConsole.Prompt(new SelectionPrompt<ReifenArt>().Title("Wähle die Reifenart").AddChoices(Enum.GetValues<ReifenArt>()))),
                           new SelectionOption("Getriebeart", () => anfrage.Anforderungen.ErforderlichesGetriebe = AnsiConsole.Prompt(new SelectionPrompt<Getriebeart>().Title("Wähle die Getriebeart").AddChoices(Enum.GetValues<Getriebeart>()))),
                           new SelectionOption("Motorart", () => anfrage.Anforderungen.ErforderlicherMotor = AnsiConsole.Prompt(new SelectionPrompt<Motorart>().Title("Wähle die Motorart").AddChoices(Enum.GetValues<Motorart>()))),
                           new SelectionOption("Turbolader", () => anfrage.Anforderungen.ErfordertTurbolader = AnsiConsole.Confirm("Soll das Auto einen Turbolader haben?")),
                           new SelectionOption("Lackierungsart", () => anfrage.Anforderungen.ErforderlicheLackierungsArt = AnsiConsole.Prompt(new SelectionPrompt<LackierungsArt>().Title("Wähle die Lackierungsart").AddChoices(Enum.GetValues<LackierungsArt>()))),
                           new SelectionOption("Lackierungsfarbe", () => anfrage.Anforderungen.ErforderlicheLackierungsFarbe = AnsiConsole.Prompt(new SelectionPrompt<Farbe>().Title("Wähle die Lackierungsfarbe").AddChoices(Enum.GetValues<Farbe>()))),
                           new SelectionOption("Speichern", () =>
                           {
                               AnsiConsole.MarkupLine("[bold green]Anfrage gespeichert![/]");
                           }),
                           new SelectionOption("Zurück", () =>
                           {
                               throw new OperationCanceledException();
                           })
                       )
                );

                if (auswahl.Text == "Speichern")
                {
                    return anfrage;
                }
                else if (auswahl.Text == "Zurück")
                {
                    return null;
                }

                auswahl.Aktion();
                AnsiConsole.Clear();
            }
        }


        public static Anfrage ErstelleZufällig(int id)
        {
            Person person = Person.Fabrik.ErstelleZufällig();
            AnfrageTyp typ = (AnfrageTyp)Spiel.Zufall.Next(Enum.GetValues<AnfrageTyp>().Length);

            Auto kundenAuto = Auto.Fabrik.ErstelleZufällig();

            string beschreibung = typ switch
            {
                AnfrageTyp.Rennstrecke => "Ich brauche mehr Leistung für die Rennstrecke!",
                AnfrageTyp.Umweltfreundlich => "Ich will mein Auto umweltfreundlicher machen.",
                AnfrageTyp.Winter => "Ich bereite mein Auto für den Winter vor.",
                AnfrageTyp.OptikTuning => "Ich möchte, dass mein Auto auffällt!",
                _ => "Ich will mein Auto für den Alltag verbessern."
            };

            AnforderungsProfil anforderungen = AnforderungsProfil.ErstelleZufällig();

            return new Anfrage(id, person, beschreibung, kundenAuto, anforderungen);
        }
    }
}
