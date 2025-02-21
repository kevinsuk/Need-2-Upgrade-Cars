using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Need_2_Upgrade_Cars;

namespace Need_2_Upgrade_Cars.Objekte;

public class Person
{
    public string Vorname { get; set; }
    public string Nachname { get; set; }

    public Person(string vorname, string nachname)
    {
        Vorname = vorname;
        Nachname = nachname;
    }

    public override string ToString()
    {
        return $"{Vorname} {Nachname}";
    }

    public class Fabrik
    {
        public static List<string> Vornamen { get; } = ["Max", "Sarah", "Sara", "Lukas", "Emma", "Daniel", "Andreas", "Julia", "Tom", "Laura", "Dominik"];
        public static List<string> Nachnamen { get; } = ["Müller", "Schmidt", "Schneider", "Fischer", "Weber", "Meyer", "Wagner", "Becker", "Schulz", "Hoffmann"];


        internal static Person ErstelleZufällig() => new (Vornamen.ErhalteZufälligesElement(), Nachnamen.ErhalteZufälligesElement());
    }
}
