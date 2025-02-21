using Spectre.Console;
using Need_2_Upgrade_Cars.Objekte;

namespace Need_2_Upgrade_Cars;

public class AnforderungsProfil
{
    public int? MinLeistung { get; set; }
    public int? MaxLeistung { get; set; }
    public ReifenArt? ErforderlicheReifenArt { get; set; }
    public Getriebeart? ErforderlichesGetriebe { get; set; }
    public Motorart? ErforderlicherMotor { get; set; }
    public bool? ErfordertTurbolader { get; set; }
    public LackierungsArt? ErforderlicheLackierungsArt { get; set; }
    public Farbe? ErforderlicheLackierungsFarbe { get; set; }

    public AnforderungsProfil() { }
    public AnforderungsProfil(
        int? minLeistung, int? maxLeistung, ReifenArt? reifenArt,
        Getriebeart? getriebe, Motorart? motor, bool turbolader,
        LackierungsArt? lackierungsArt, Farbe? lackierungsFarbe)
    {
        MinLeistung = minLeistung;
        MaxLeistung = maxLeistung;
        ErforderlicheReifenArt = reifenArt;
        ErforderlichesGetriebe = getriebe;
        ErforderlicherMotor = motor;
        ErfordertTurbolader = turbolader;
        ErforderlicheLackierungsArt = lackierungsArt;
        ErforderlicheLackierungsFarbe = lackierungsFarbe;
    }

    public static AnforderungsProfil ErstelleZufällig()
    {
        int minLeistung = Spiel.Zufall.Next(100, 300);
        int maxLeistung = minLeistung + Spiel.Zufall.Next(50, 150);
        ReifenArt erforderlicherReifenTyp = (ReifenArt)Spiel.Zufall.Next(Enum.GetValues<ReifenArt>().Length);

        Getriebeart? getriebe = (Getriebeart)Spiel.Zufall.Next(Enum.GetValues<Getriebeart>().Length);
        Motorart? motor = (Motorart)Spiel.Zufall.Next(Enum.GetValues<Motorart>().Length);
        bool turbolader = Spiel.Zufall.Next(0, 2) == 0;
        LackierungsArt? lackierungsArt = (LackierungsArt)Spiel.Zufall.Next(Enum.GetValues<LackierungsArt>().Length);
        Farbe? lackierungsFarbe = (Farbe)Spiel.Zufall.Next(Enum.GetValues<Farbe>().Length);


        return new AnforderungsProfil(minLeistung, maxLeistung, erforderlicherReifenTyp, getriebe, motor, turbolader, lackierungsArt, lackierungsFarbe);
    }

    public bool IstErfüllt(Auto auto)
    {
        return auto.Leistung >= (MinLeistung ?? 0) &&
               auto.Leistung <= (MaxLeistung ?? int.MaxValue) &&
               auto.Reifen.ReifenArt == (ErforderlicheReifenArt ?? auto.Reifen.ReifenArt) &&
               (ErforderlichesGetriebe == null || auto.Getriebe == ErforderlichesGetriebe) &&
               (ErforderlicherMotor == null || auto.Motor == ErforderlicherMotor) &&
               (!ErfordertTurbolader.GetValueOrDefault(false) || auto.HatTurbolader) &&
               (ErforderlicheLackierungsArt == null || auto.Lackierung.Art == ErforderlicheLackierungsArt) &&
               (ErforderlicheLackierungsFarbe == null || auto.Lackierung.Farbe == ErforderlicheLackierungsFarbe);
    }
}
