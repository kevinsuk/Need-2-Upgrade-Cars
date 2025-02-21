using Need_2_Upgrade_Cars.Menues;
using Spectre.Console;

namespace Need_2_Upgrade_Cars;

public abstract class Option
{
    public string Titel { get; set; }
    public abstract void Auswerten();

    public override string ToString()
    {
        return Titel;
    }

    protected Option(string titel)
    {
        Titel = titel;
    }
}

public class MenüOption : Option
{
    public MenüOption(string titel, Menü menü) : base(titel)
    {
        Menü = menü;
    }

    public Menü Menü { get; set; }
    public override void Auswerten()
    {
        AnsiConsole.Clear();
        Menü.Anzeigen();
    }
}

public class AktionOption : Option
{
    private Action Aktion { get; }

    public AktionOption(string titel, Action aktion) : base(titel)
    {
        Aktion = aktion;
    }

    public override void Auswerten()
    {
        Aktion.Invoke();
    }
}

public class ProgrammBeendenOption : Option
{
    public ProgrammBeendenOption() : base("Beenden")
    {
    }

    public override void Auswerten()
    {
        Environment.Exit(0);
    }
}