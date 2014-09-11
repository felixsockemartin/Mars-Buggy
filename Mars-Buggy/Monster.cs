using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsBuggy
{
    class Monster
    {

        public int position = 0;
        public int art;
        int animation = 0;
        Random monsterzufall = new Random();
        public int anzahl = 0;

        public Monster(int monster, int min, int max)
        {
            position = monsterzufall.Next(min, max);
            art = monster;
        }

        public void Runde()
        {
                position++;
                if (position > Arbeiter.puffer)
                {
                    animation++;
                }
        }

        public void Zeichnen()
        {
            if (art == 0)
            {
                if (animation < 5 && position < Arbeiter.länge)
                {
                    Arbeiter.Feld[position, Arbeiter.breite - 2] = ' ';
                    Arbeiter.Feld[position, Arbeiter.breite - 1] = 'V';
                }

                else if (animation < 15 && position < Arbeiter.länge)
                {
                    Arbeiter.Feld[position, Arbeiter.breite - 2] = 'V';
                    Arbeiter.Feld[position, Arbeiter.breite - 1] = 'O';
                }

                else if (animation < 25 && position < Arbeiter.länge)
                {
                    Arbeiter.Feld[position, Arbeiter.breite - 3] = 'V';
                    Arbeiter.Feld[position, Arbeiter.breite - 2] = 'O';
                    Arbeiter.Feld[position, Arbeiter.breite - 1] = 'O';
                }

                else
                {
                    if (position + 1 < Arbeiter.länge)
                    {
                        Arbeiter.Feld[position + 1, Arbeiter.breite - 3] = '<';
                    }
                    if (position < Arbeiter.länge)
                    {
                        Arbeiter.Feld[position, Arbeiter.breite - 3] = 'O';
                        Arbeiter.Feld[position, Arbeiter.breite - 2] = 'O';
                        Arbeiter.Feld[position, Arbeiter.breite - 1] = 'O';
                    }
                }

            }

            else
            {
                if (animation < 25)
                {
                    Arbeiter.Feld[position, Arbeiter.breite - 1] = 'W';
                }

                else if (animation < 35)
                {
                    Arbeiter.Feld[position, Arbeiter.breite - 2] = 'W';
                    Arbeiter.Feld[position, Arbeiter.breite - 1] = '8';
                }

                else
                {
                    Arbeiter.Feld[position, Arbeiter.breite - 3] = 'W';
                    Arbeiter.Feld[position, Arbeiter.breite - 2] = '8';
                    Arbeiter.Feld[position, Arbeiter.breite - 1] = ' ';
                }
            }

        }
    }
}
