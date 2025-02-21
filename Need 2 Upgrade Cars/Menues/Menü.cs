using Spectre.Console;

namespace Need_2_Upgrade_Cars.Menues;

public class Menü
{
    public string Titel { get; set; }
    public string Beschreibung { get; set; }
    public Menü? VorherigesMenü {  get; set; }
    public virtual List<Option> MenüOptionen { get;  }
    public List<Option> Optionen
    {
        get
        {
            if (VorherigesMenü == null)
                return MenüOptionen;
            var alleOptionen = new List<Option>(MenüOptionen)
            {
                new MenüOption("Zurück", VorherigesMenü)
            };
            return alleOptionen;
        }
    }

    public Menü(string titel, string beschreibung, List<Option>? optionen = null, Menü? vorherigesMenü = null )
    {
        Titel = titel;
        Beschreibung = beschreibung;
        MenüOptionen = optionen ?? [];
        VorherigesMenü = vorherigesMenü;
    }

    public void Anzeigen()
    {
        var ausgewählteOption = AnsiConsole.Prompt(
            new SelectionPrompt<Option>()
            .Title(Titel)
            .AddChoices(Optionen)
        );
        ausgewählteOption.Auswerten();
    }
    public void TitelAnzeigen()
    {
        Console.WriteLine(Titel);
    }
    public void OptionenAnzeigen()
    {
        foreach (var option in Optionen)
        {
            Console.WriteLine(option);
        }
    }
}
