using Need_2_Upgrade_Cars.Objekte;
using Spectre.Console;

namespace Need_2_Upgrade_Cars.Menues;

public class AutoMenü : Menü
{
    public override List<Option> MenüOptionen =>
    [
        new AktionOption("Mein Auto anzeigen", () => {
            Spiel.MeinAuto.ZeigeDetails();
            Anzeigen();
            }),
        new MenüOption("Auto modifizieren", new AutoModifikationsMenü(this, Spiel.MeinAuto))
    ];
    public AutoMenü(Menü vorherigesMenü)
        : base("Auto modifizieren", "", vorherigesMenü : vorherigesMenü)
    {
    }
}