using Spectre.Console;
using Need_2_Upgrade_Cars;
using Need_2_Upgrade_Cars.Objekte;

namespace Need_2_Upgrade_Cars.Menues;

public class RadioMenü : Menü
{
    private Radio radio;

    public override List<Option> MenüOptionen =>
    [
        new AktionOption("Radio ein-/ausschalten", () => {
            AnsiConsole.Clear();

            if (radio.IstEingeschaltet)
                radio.Ausschalten();
            else
                radio.Einschalten();

            Anzeigen();
        }),
        new AktionOption("Sender wechseln", () => {
            AnsiConsole.Clear();

            radio.NaechsterSender();

            Anzeigen();
        }),
        new AktionOption("Favoriten anzeigen", () => {
            AnsiConsole.Clear();

            radio.FavoritenAnzeigen();

            Anzeigen();
        }),
        new AktionOption("Sender aus Favoriten auswählen", () => {
            AnsiConsole.Clear();

            radio.DirektwahlFavoriten();

            Anzeigen();
        }),
        new AktionOption("Sender als Favorit hinzufügen", () => {
            AnsiConsole.Clear();

            radio.FavoritHinzufuegen();

            Anzeigen();
        }),
        new AktionOption("Favoriten verwalten", () => {
            AnsiConsole.Clear();

            radio.FavoritEntfernen();

            Anzeigen();
        }),
        new AktionOption("Sleep-Timer setzen", () => {
            AnsiConsole.Clear();
            int minuten = AnsiConsole.Ask<int>("[green]Gib die Minuten für den Sleep-Timer ein:[/]");

            radio.SleepTimer(minuten);

            Anzeigen();
        }),
        new AktionOption("Sleep-Timer abbrechen", () => {
            AnsiConsole.Clear();
            radio.SleepTimerAbbrechen();
            AnsiConsole.MarkupLine("[yellow]Sleep-Timer wurde abgebrochen.[/]");

            Anzeigen();
        }),
        new AktionOption("Verkehrsmeldungen", () => {
            radio.ZeigeVerkehrsInfo();
            Anzeigen();
        }),
        new AktionOption("Wetterinfo anzeigen", () => {
            radio.ZeigeWetterInfo();
            Anzeigen();
        }),
        new AktionOption("Manuelle Frequenz eingeben", () => {
            AnsiConsole.Clear();
            radio.SetzeFrequenz();

            Anzeigen();
        })
    ];

    public RadioMenü(Menü vorherigesMenü, Radio radio)
        : base("Radio: VW MIB 2", "", vorherigesMenü : vorherigesMenü)
    {
        this.radio = radio;
    }
}
