using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Need_2_Upgrade_Cars.Objekte;

public class SelectionOption
{
    public string Text { get; set; }
    public Action Aktion { get; }

    public SelectionOption(string text, Action aktion)
    {
        Text = text;
        Aktion = aktion;
    }

    public override string ToString()
    {
        return Text;
    }
}
