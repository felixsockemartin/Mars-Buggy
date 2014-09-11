using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsBuggy
{
    class Schuss
    {
        public int position = Buggy.x;

        public bool Treffer()
        {
            return Arbeiter.monster.Any(y => (y.position == position || y.position == position + 1 || y.position == position - 1));
        }

        public void Runde()
        {
            position--;
        }

        public void Zeichnen()
        {
            if (position < Arbeiter.länge && position > Arbeiter.puffer)
            {
                Arbeiter.Feld[position, Arbeiter.breite - 3] = '*';
            }
        }
    }
}
