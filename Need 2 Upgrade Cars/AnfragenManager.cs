using Need_2_Upgrade_Cars.Menues;
using Need_2_Upgrade_Cars.Objekte;
using Spectre.Console;

namespace Need_2_Upgrade_Cars;

public class AnfragenManager
{
    public List<Anfrage> Anfragen { get; set; } = [];
}

public class AnfragenMenü : Menü
{
    public AnfragenMenü(Menü vorherigesMenü)
        : base("Need 2 Upgrade Cars: Anfragen-Manager", "", [], vorherigesMenü)
    {
        MenüOptionen.Add(new AktionOption("Anfragen anzeigen", ZeigeAnfragen));
        MenüOptionen.Add(new AktionOption("Neue Anfrage erstellen", ErstelleAnfrage));
        MenüOptionen.Add(new AktionOption("Anfragen bearbeiten", BearbeiteAnfrage));
        MenüOptionen.Add(new AktionOption("Anfragen prüfen", AnfragenPrüfen));
        MenüOptionen.Add(new AktionOption("Kundenautos anzeigen", ZeigeKundenAuto));
        MenüOptionen.Add(new AktionOption("Zufällige Anfragen generieren", GeneriereZufälligeAnfragen));
        MenüOptionen.Add(new AktionOption("Anfragen ablehnen", AnfrageAblehnen));
    }

    // Methode zum Anzeigen von Anfragen
    private void ZeigeAnfragen()
    {
        AnsiConsole.Clear();

        var anfragen = Spiel.AnfragenManager.Anfragen;
        // Wenn keine Anfragen gespeichert sind
        if (anfragen.Count == 0)
        {
            AnsiConsole.MarkupLine("[bold yellow]Keine Anfragen vorhanden![/]");
            Anzeigen();
            return;
        }
        // Frage, welche Anfrage näher angezeigt werden soll
        var auswahl = AnsiConsole.Prompt(
            new SelectionPrompt<Anfrage>()
            .Title("Wähle eine Anfrage aus:")
            .AddChoices(anfragen)
        );
        auswahl.ZeigeDetails();


        AnsiConsole.MarkupLine("\n[bold yellow]Drücke eine beliebige Taste, um zurückzukehren...[/]");
        Console.ReadKey();
        AnsiConsole.Clear();

        Anzeigen();
    }

    // Methode zum Erstellen einer neuen Anfrage
    private void ErstelleAnfrage()
    {
        AnsiConsole.Clear();
        var anfragen = Spiel.AnfragenManager.Anfragen;

        var anfrage = Anfrage.Fabrik.ErstelleViaInput();

        // Die Anfrage wird mit den zuvor angegebenen Vorgaben der Anfragen-Liste hinzugefügt

        if (anfrage != null)
        {
            int neueId = anfragen.Count > 0 ? anfragen.Max(a => a.Id) + 1 : 1;

            Auto kundenAuto = Auto.Fabrik.ErstelleZufällig();

            var anforderungen = new AnforderungsProfil(
            anfrage.Anforderungen.MinLeistung ?? 100,  // Standardwert: 100 PS
            anfrage.Anforderungen.MaxLeistung ?? 300,  // Standardwert: 300 PS
            anfrage.Anforderungen.ErforderlicheReifenArt ?? ReifenArt.Sommerreifen, // Standard: Sommerreifen
            anfrage.Anforderungen.ErforderlichesGetriebe ?? Getriebeart.Manuell, // Falls nullable, dann Standardwert setzen
            anfrage.Anforderungen.ErforderlicherMotor ?? Motorart.Benzin, // Standard: Benziner
            anfrage.Anforderungen.ErfordertTurbolader ?? false, // Falls null → Kein Turbolader
            anfrage.Anforderungen.ErforderlicheLackierungsArt ?? LackierungsArt.Speziallack, // Standard: Einfarbig
            anfrage.Anforderungen.ErforderlicheLackierungsFarbe ?? Farbe.Schwarz // Standard: Schwarz
            );

            anfragen.Add(new Anfrage(
                neueId,
                anfrage.Kunde,
                anfrage.Beschreibung ?? "Keine Beschreibung angegeben",
                kundenAuto,
                anforderungen
            ));

            AnsiConsole.Clear();
            // Bestätigung, dass die Erstellung erfolgreich war mit Anzeige seiner ID
            AnsiConsole.MarkupLine($"[bold green]Anfrage erstellt! ID: {neueId}[/]");
        }
        else
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold yellow]Erstellung der Anfrage wurde abgebrochen.[/]");
        }

        Anzeigen();
    }

    // Methode zum Löschen von Anfragen
    private void AnfrageAblehnen()
    {
        AnsiConsole.Clear();
        var anfragen = Spiel.AnfragenManager.Anfragen;
        // Wenn keine Anfragen vorhanden sind
        if (anfragen.Count == 0)
        {
            AnsiConsole.MarkupLine("[bold yellow]Keine Anfragen vorhanden![/]");
            Anzeigen();
            return;
        }

        // Frage nach der Anfrage, die entfernt werden soll
        var auswahl = AnsiConsole.Prompt(
            new SelectionPrompt<Anfrage>()
                .Title("Wähle eine Anfrage zum Löschen aus:")
                .AddChoices(anfragen)
        );
        
        // Die ausgewählte Anfrage wird von der Liste entfernt
        anfragen.Remove(auswahl);

        // Bestätigung, dass das Entfernen abgeschlossen ist
        AnsiConsole.MarkupLine($"[bold green]Anfrage mit ID {auswahl.Id} gelöscht![/]");

        Anzeigen();
    }

    // Methode zum Erstellen zufälliger Anfragen 
    private void GeneriereZufälligeAnfragen()
    {
        AnsiConsole.Clear();
        
        // Frage nach der Anzahl der zu erstellenden Anfragen
        int anzahl = AnsiConsole.Prompt(
            new TextPrompt<int>("Gib die Anzahl an Anfragen ein, die erstellt werden sollen:")
            .ValidationErrorMessage("Bitte eine gültige Zahl eingeben!")
            .Validate(num => num > 0 ? ValidationResult.Success() : ValidationResult.Error("Muss mindestens 1 betragen!"))
        );
        
        var anfragenListe = Spiel.AnfragenManager.Anfragen;
        
        for (int i = 0; i < anzahl; i++)
        {
            int neueId = anfragenListe.Count > 0 ? anfragenListe.Max(a => a.Id) + 1 : 1;
            
            var neueAnfrage = Anfrage.Fabrik.ErstelleZufällig(neueId);
            neueAnfrage.Kunde = Person.Fabrik.ErstelleZufällig();


            anfragenListe.Add(neueAnfrage);
        }

        AnsiConsole.Clear();

        AnsiConsole.MarkupLine($"[bold green]{anzahl} zufällige Anfragen erstellt![/]");
        Anzeigen();
    }

    // Methode zum Bearbeiten einer Anfrage
    private void BearbeiteAnfrage()
    {
        AnsiConsole.Clear();

        var anfragenListe = Spiel.AnfragenManager.Anfragen;

        // Wenn keine Anfragen gespeichert sind
        if (anfragenListe.Count == 0)
        {
            AnsiConsole.MarkupLine("[bold yellow]Keine Anfragen vorhanden![/]");
            Anzeigen();
            return;
        }

        // Frage, welche Anfrage bearbeitet werden soll
        var auswahl = AnsiConsole.Prompt(
            new SelectionPrompt<Anfrage>()
                .Title("Wähle eine Anfrage zum Bearbeiten aus:")
                .AddChoices(anfragenListe)
        );

        // Initialisierung des Modifikationsmenüs der Kundenautos
        var modifikationsMenü = new AutoModifikationsMenü(this, auswahl.KundenAuto);
        modifikationsMenü.Anzeigen();

        Anzeigen();
    }

    private void AnfragenPrüfen()
    {
        AnsiConsole.Clear();

        var offeneAnfragen = Spiel.AnfragenManager.Anfragen;

        if (offeneAnfragen.Count == 0)
        {
            AnsiConsole.MarkupLine("[bold yellow]Keine Anfragen vorhanden![/]");
            Anzeigen();
            return;
        }

        var auswahl = AnsiConsole.Prompt(
            new SelectionPrompt<Anfrage>()
                .Title("Wähle eine Anfrage zum Bearbeiten aus:")
                .AddChoices(offeneAnfragen)
        );

        if (auswahl.IstErfüllt())
        {
            AnsiConsole.MarkupLine($"[green]Anfrage von {auswahl.Kunde} erfolgreich abgeschlossen![/]");
            Spiel.AnfragenManager.Anfragen.Remove(auswahl);
        }

        else
        {
            AnsiConsole.MarkupLine($"[red]FEHLER: Das modifizierte Auto von {auswahl.Kunde} entspricht nicht den Anforderungen in der Anfrage![/]");
        }

        Anzeigen();
    }

    private void ZeigeKundenAuto()
    {
        AnsiConsole.Clear();

        var anfragen = Spiel.AnfragenManager.Anfragen;
        if (anfragen.Count == 0)
        {
            AnsiConsole.MarkupLine("[bold yellow]Keine Anfragen vorhanden![/]");
            Anzeigen();
            return;
        }

        var auswahl = AnsiConsole.Prompt(
            new SelectionPrompt<Anfrage>()
                .Title("Wähle eine Anfrage aus, um das Kundenauto zu sehen:")
                .AddChoices(anfragen)
        );

        auswahl.KundenAuto.ZeigeDetails();

        Anzeigen();
    }
}