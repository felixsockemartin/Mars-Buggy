using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MarsBuggy
{
    //führt die eigt. arbeiten aus
    static class Arbeiter
    {
        //Einstellungen
        public const int
            puffer = 100,
            bildrate = 40,
            breite = 20,
            länge = 100 + puffer,

            wiederholung = 1000 / bildrate;
        static public bool run;
        static public bool schuss;

        static public char[,] Feld = new char[länge, breite];
        static public Loch[] löcher;
        static public Monster[] monster;
        static public Schuss[] schüsse;

        //Der Thread des eigt. Spiels
        static public void aktualisieren()
        {
            Buggy.Laden();
            while (run)
            {
                zeichnen();
                Buggy.Kollision();
                runde();
                Schießen();
                Level.NextLevel();

                Thread.Sleep(wiederholung);
            }
        }

        //Schreibt das Array in die Konsole
        static public void zeichnen()
        {
            string bild = "";
            grundfeld();
            Buggy.zeichnen();
            foreach (Loch l in löcher)
            {
                l.Zeichnen();
            }
            foreach (Monster m in monster)
            {
                m.Zeichnen();
            }
            foreach (Schuss k in schüsse)
            {
                k.Zeichnen();
            }
            Level.Informationen();
            bild = "";
            //Erschafft aus dem 2-Dimensionalen Array einen string
            for (int y = 0; y < breite; y++)
            {
                for (int x = puffer; x < länge; x++)
                {
                    bild += Feld[x, y];
                }
                bild += "\n";
            }
            Console.Clear();
            Console.WriteLine(bild);
        }

        //konstruiert das Spielfeld
        static void grundfeld()
        {
            for (int y = 0; y < breite; y++)
            {
                for (int x = 0; x < länge; x++)
                {
                    Feld[x, y] = ' ';
                    if (y == breite - 2)
                    {
                        Feld[x, y] = 'T';
                    }
                    if (y == breite - 1)
                    {
                        Feld[x, y] = '|';
                    }
                }
            }
            //return feld;
        }

        //Berrechnet alles was in einer Runde passiert und stellt das Regelwerk fürs Erschaffen dar
        static void runde()
        {
            Buggy.Runde();

            for (int i = 0; i < löcher.Length; i++)
            {
                löcher[i].Runde();
                if (löcher[i].position == länge)
                {
                    Töten("Loch");
                }
            }

            for (int x = 0; x < monster.Length; x++)
            {
                monster[x].Runde();
                if (monster[x].position == länge)
                {
                    Töten("Monster");
                }
            }

            for (int x = 0; x < schüsse.Length; x++)
            {
                schüsse[x].Runde();
                if (schüsse[x].position == puffer)
                {
                    Töten("Schuss");
                }
            }

            if(schüsse.Any(y => y.Treffer()))
            {
                Töten("Schuss");
                Töten("Monster");
            }

            if (Level.Speicher[Level.gegenwart].ObjektStufe == 0 && löcher.Length == 0)
            {
                Erschaffen("Loch", 0, 30, 0);
            }

            else if (löcher.Length == 0 && monster.Length == 0)
            {
                Würfel();
            }

            else if (Level.Speicher[Level.gegenwart].ObjektStufe > 0)
            {
                if (löcher.Any(x => x.position == puffer) || monster.Any(y => y.position == puffer))
                { 
                    Würfel(); 
                }
            }

            else
            {
                if (löcher.Any(x => x.position == puffer))
                {
                    Erschaffen("Loch", puffer - Level.Speicher[Level.gegenwart].maxAbstandObjekt, puffer - Level.Speicher[Level.gegenwart].minAbstandObjekt, 0);
                }
            }
        }

        //Setzt die Startwerte, damit auch ein neues Spiel möglich ist ohne das Programm beenden zu müssen
        static public void Start()
        {
            run = true;
            löcher = new Loch[0];
            monster = new Monster[0];
            schüsse = new Schuss[0];
            schuss = false;
            Level.gegenwart = 0;
            Level.score = 0;
            Console.WindowHeight = breite + 2;
            Console.WindowWidth = länge - puffer + 1;
        }

        /// <summary>
        /// Erschafft das Objekt das man will und passt das dazugehörige Array an.
        /// </summary>
        /// <param name="opt">Was will man erschaffen?("Monster" "Loch" "Schuss")</param>
        /// <param name="min">Der minimale Abstand zum Vorgänger.</param>
        /// <param name="max">Der maximale Abstand zum Vorgänger.</param>
        /// <param name="mon">Die Monsterart (0 oder 1) muss auch im Falle eines anderen Objektes übergeben werden, ist dann aber unwichtig.</param>
        static void Erschaffen(string opt, int min, int max, int mon)
        {
            switch (opt)
            {
                case "Loch":
                    System.Array.Resize<Loch>(ref löcher, löcher.Length + 1);
                    löcher[löcher.Length - 1] = new Loch(min, max);
                    if (löcher.Length > 1) { LöcherSortieren(); }
                    break;

                case "Monster":
                    System.Array.Resize<Monster>(ref monster, monster.Length + 1);
                    monster[monster.Length - 1] = new Monster(mon, min, max);
                    if (monster.Length > 1) { MonsterSortieren(); }
                    break;

                case "Schuss":
                    System.Array.Resize<Schuss>(ref schüsse, schüsse.Length + 1);
                    schüsse[schüsse.Length - 1] = new Schuss();
                    if (schüsse.Length > 1) { SchüsseSortieren(); }
                    if (Level.score > 0)
                    {
                        Level.score -= 1;
                    }
                    break;
            }
        }

        /// <summary>
        /// Entfernt ein Objekt.
        /// </summary>
        /// <param name="opt">Was will man entfernen?("Monster" "Loch" "Schuss")</param>
        static void Töten(string opt)
        {
            switch (opt)
            {
                case "Loch":
                    System.Array.Clear(löcher, löcher.Length - 1, 1);
                    System.Array.Resize<Loch>(ref löcher, löcher.Length - 1);
                    Level.score += 5;
                    break;

                case "Monster":
                    System.Array.Clear(monster, monster.Length - 1, 1);
                    System.Array.Resize<Monster>(ref monster, monster.Length - 1);
                    Level.score += 10;
                    break;

                case "Schuss":
                    System.Array.Clear(schüsse, schüsse.Length - 1, 1);
                    System.Array.Resize<Schuss>(ref schüsse, schüsse.Length - 1);
                    break;
            }
        }

        //damit das letzte Loch im Array das erste Loch ist das auf den Buggy trifft
        static void LöcherSortieren()
        {
            bool PaarSortiert;
            //solange nicht alle paare bei jedem  Durchlauf     
            //sortiert sind, Alg. wiederholen. 
            //->BubbleSort verfahren
            do
            {
                PaarSortiert = true;
                for (int i = 0; i < löcher.Length - 1; i++)
                {
                    if (löcher[i].position > löcher[i + 1].position)
                    {
                        //zahlen tauschen (nur ein Paar)
                        Loch temp = löcher[i];
                        löcher[i] = löcher[i + 1];
                        löcher[i + 1] = temp;
                        //nicht sortiert
                        PaarSortiert = false;
                    }
                }
            } while (!PaarSortiert);
        }

        //damit das letzte Monster im Array das erste Monster ist das auf den Buggy trifft
        static void MonsterSortieren()
        {
            bool PaarSortiert;
            //solange nicht alle paare bei jedem  Durchlauf     
            //sortiert sind, Alg. wiederholen. 
            //->BubbleSort verfahren
            do
            {
                PaarSortiert = true;
                for (int i = 0; i < monster.Length - 1; i++)
                {
                    if (monster[i].position > monster[i + 1].position)
                    {
                        //zahlen tauschen (nur ein Paar)
                        Monster temp = monster[i];
                        monster[i] = monster[i + 1];
                        monster[i + 1] = temp;
                        //nicht sortiert
                        PaarSortiert = false;
                    }
                }
            } while (!PaarSortiert);
        }

        //damit das letzte Monster im Array das erste Monster ist das auf den Buggy trifft
        static void SchüsseSortieren()
        {
            bool PaarSortiert;
            //solange nicht alle paare bei jedem  Durchlauf     
            //sortiert sind, Alg. wiederholen. 
            //->BubbleSort verfahren
            do
            {
                PaarSortiert = true;
                for (int i = 0; i < schüsse.Length - 1; i++)
                {
                    if (schüsse[i].position < schüsse[i + 1].position)
                    {
                        //zahlen tauschen (nur ein Paar)
                        Schuss temp = schüsse[i];
                        schüsse[i] = schüsse[i + 1];
                        schüsse[i + 1] = temp;
                        //nicht sortiert
                        PaarSortiert = false;
                    }
                }
            } while (!PaarSortiert);
        }
        
        //Entscheidet der Zufall was gespawnt wird
        static void Würfel()
        {
            Random random = new Random();
            //Die Wahrscheinlichkeit eines Loches ist größer.
            if (random.Next(0, 100) > 70)
            {
                //Die Wahrscheinlichkeit eines Monsters Stufe 1 ist größer.
                if (Level.Speicher[Level.gegenwart].ObjektStufe > 1 && random.Next(0, 100) > 80)
                {
                    Erschaffen("Monster", puffer - Level.Speicher[Level.gegenwart].maxAbstandObjekt, puffer - Level.Speicher[Level.gegenwart].minAbstandObjekt, 1);
                }
                else
                {
                    Erschaffen("Monster", puffer - Level.Speicher[Level.gegenwart].maxAbstandObjekt, puffer - Level.Speicher[Level.gegenwart].minAbstandObjekt, 0);
                }
            }
            else
            {
                Erschaffen("Loch", puffer - Level.Speicher[Level.gegenwart].maxAbstandObjekt, puffer - Level.Speicher[Level.gegenwart].minAbstandObjekt, 0);
            }
        }

        //Wird in Falle eines Game Over ausgeführt
        static public void Tot()
        {
            run = false;
        }

        //Erschafft einen Schuss an der richtigen position.
        static public void Schießen()
        {
            if (Buggy.y == breite - 4 && schuss)
            {
                Erschaffen("Schuss", 0, 0, 0);
            }
            schuss = false;
        }
    }
}