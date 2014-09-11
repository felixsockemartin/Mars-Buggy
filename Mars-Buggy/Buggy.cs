using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsBuggy
{
    static class Buggy
    {
        static public int x = Arbeiter.länge - 10;
        static public int y = Arbeiter.breite - 4;
        static int animation = 0;
        static int sprungzähler = 0;

        //Die verschiedenen Bilder des Buggys.
        public struct Bilder
        {
            public char[,] bild;
        }

        static Bilder[] bilder;

        //Fügt das richtige Bild in das Array zum zeichnen ein.
        static public void zeichnen()
        {
            
            for (int g = 0; g < 2; g++)
            {
                for (int a = 0; a < 8; a++)
                {
                    Arbeiter.Feld[x + a, y + g] = bilder[animation].bild[a, g];
                }
            }
        }

        //Gibt die Regeln was in einer Runde mit dem Buggy passiert.
        static public void Runde()
        {
            if (animation == 2)
            {
                animation = 0;
            }
            else
            {
                animation++;
            }

            if (sprungzähler != 0)
            {
                y--;
                sprungzähler--;
            }

            else if (y != Arbeiter.breite - 4)
            {
                y++;
            }
        }

        //Setzt den Sprungzähler, wenn der Buggy auf den Boden ist.
        static public void sprung()
        {
            if (y == Arbeiter.breite - 4)
            {
                sprungzähler = 8;
            }
        }

        //Die Regeln für eine Kollision.
        static public void Kollision()
        {
            if (((Arbeiter.löcher.Any(z => z.position == x + 1 || z.position == x + 5) || Arbeiter.monster.Any(z => z.position + 1 == x + 1 && z.art == 0)) && y == Arbeiter.breite - 4) || ((Arbeiter.monster.Any(z => (z.position == x + 1 || z.position == x + 5)&& z.art == 1)) && (y == Arbeiter.breite - 4 || y == Arbeiter.breite - 5)))
            {
                Arbeiter.Tot();
            }
        }

        //Ladet die Bilder des Buggys.
        static public void Laden()
        {
            bilder = new Bilder[3];
            for (int g = 1; g <= 3; g++)
            {
                bilder[g - 1].bild = new char[8, 2];
                string[] a = Datei.Laden(@".\Buggy\"+g+".txt").Split('\n');
                for (int t = 0; t < 2; t++)
                {
                    int i = 0;
                    foreach (char k in a[t])
                    {
                        bilder[g - 1].bild[i, t] = k;
                        i++;
                    }
                }
            }
        }
    }
}
