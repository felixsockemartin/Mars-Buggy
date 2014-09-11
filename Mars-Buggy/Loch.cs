using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsBuggy
{
    public class Loch
    {
        Random lochzufall = new Random();
        public int position;


        public Loch(int min, int max)
        {
            position = lochzufall.Next(min, max);
        }

        public void Runde()
        {
            position++;
        }

        public void Zeichnen()
        {
                if (position < Arbeiter.länge && position > Arbeiter.puffer)
                {
                    Arbeiter.Feld[position, Arbeiter.breite - 2] = ' ';
                    Arbeiter.Feld[position, Arbeiter.breite - 1] = 'ö';
                    Arbeiter.Feld[position - 1, Arbeiter.breite - 2] = '|';
                }
                if (position + 1 < Arbeiter.länge && position > Arbeiter.puffer)
                {
                    Arbeiter.Feld[position + 1, Arbeiter.breite - 2] = '|';
                }
        }
    }
}
