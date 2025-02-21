using Need_2_Upgrade_Cars.Menues;
using Need_2_Upgrade_Cars.Objekte;
using Spectre.Console;

namespace Need_2_Upgrade_Cars;

public class Spiel
{
    public static Auto MeinAuto { get; set; }
    public static AnfragenManager AnfragenManager { get; set; }
    public static Random Zufall { get; } 

    static Spiel()
    {
        MeinAuto = new Auto(170, true, new Lackierung(LackierungsArt.Hochglanz, Farbe.Grün),
                        new Reifen(Hersteller.Goodyear, 23, Farbe.Rot, ReifenArt.Sommerreifen), Motorart.Diesel, Getriebeart.Automatik, Antrieb.Allrad, new("Kevin", "Engel"));
        AnfragenManager = new AnfragenManager();
        Zufall = new Random();
    }
    public static void Starten()
    {
        AnsiConsole.Clear();
        Menü hauptMenü = new("Need 2 Upgrade Cars 2: Hauptmenü", "", []);

        Menü anfragenMenü = new AnfragenMenü(hauptMenü);

        Menü autoMenü = new AutoMenü(hauptMenü);

        hauptMenü.MenüOptionen.Add(new MenüOption("Autos modifizieren", autoMenü));
        hauptMenü.MenüOptionen.Add(new MenüOption("Meine Anfragen", anfragenMenü));
        hauptMenü.MenüOptionen.Add(new ProgrammBeendenOption());

        hauptMenü.Anzeigen();
    }
}