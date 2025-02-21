using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Need_2_Upgrade_Cars
{
    public static class ExtentionMethoden
    {
        public static T ErhalteZufälligesElement<T>(this List<T> elemente)
        {
            return elemente[Spiel.Zufall.Next(elemente.Count)];
        }
    }
}
