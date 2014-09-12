using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsBuggy
{
    static public class Level
    {
        //Alle erforderlichen Informationen
        public struct Levelinfo
        {
            public int erforderlichePunktzahl, minAbstandObjekt, maxAbstandObjekt, ObjektStufe;
        }

        public struct bestenliste
        {
            public string name;
            public long score;
        }

        static public Levelinfo[] Speicher;
        //Das derzeitige Level.
        static public int gegenwart;
        static public long score;

        //Lädt die Levelinformationen.
        static public void Laden()
        {
            int i = 0;
            string[] zwischenspeicher = Datei.Laden("Level.txt").Split('\n');
            Speicher = new Levelinfo[zwischenspeicher.Length];
            foreach (string b in zwischenspeicher)
            {
                string[] a = b.Split(';');
                Speicher[i].erforderlichePunktzahl = Convert.ToInt16(a[0]);
                Speicher[i].minAbstandObjekt = Convert.ToInt16(a[1]);
                Speicher[i].maxAbstandObjekt = Convert.ToInt16(a[2]);
                Speicher[i].ObjektStufe = Convert.ToInt16(a[3]);
                i++;
            }
        }

        static public void Informationen()
        {
            string schrift = "Score: " + score + "     Level: " + (gegenwart + 1);
            int i = 0;
            foreach (char k in schrift)
            {
                Arbeiter.Feld[Arbeiter.puffer + i, 0] = k;
                i++;
            }
        }

        //Prüft, ob die Levelbedingungen erfllt sind.
        static public void NextLevel()
        {
            if (Speicher[gegenwart].erforderlichePunktzahl < score && gegenwart < Speicher.Length - 1)
            {
                gegenwart++;
            }
        }

        //Prüft. ob eine neues Highscore erreicht wurde.
        static public void Highscore()
        {
            bestenliste[] liste = new bestenliste[10];
            for (int h = 0; h < 10; h++)
            {
                liste[h].name = " ";
            }
            try
            {
                string[] zwischenspeicher = Datei.Laden("Highscore.txt").Split('\n');
                string[] zwischenspeicher2 = new string[zwischenspeicher.Length * 2];
                int i = 0;
                foreach (string a in zwischenspeicher)
                {
                    if (a.Split(';').Length < 3)
                    {
                        zwischenspeicher2[i] = a.Split(';')[0];
                        i++;
                        zwischenspeicher2[i] = a.Split(';')[1];
                        i++;
                        if (i == 20) { break; }
                    }
                    else
                    {
                        zwischenspeicher2[i] = " ";
                        i++;
                        zwischenspeicher2[i] = "0";
                        i++;
                        if (i == 20) { break; }
                    }
                }
                i = 0;
                for (int x = 0; x < zwischenspeicher2.Length - 1; x++)
                {
                    liste[i].name = zwischenspeicher2[x];
                    x++;
                    liste[i].score = Convert.ToInt64(zwischenspeicher2[x]);
                    i++;
                }
            }
            catch
            {

            }
            liste = Sortieren(liste);
            Console.Clear();
            int w = 1;
            for (int l = liste.Length - 1; l >= 0; l--)
            {
                string speicher = liste[l].name;
                for (int n = 0; n < 20 - liste[l].name.Length; n++)
                {
                    speicher += " ";
                }
                Console.WriteLine(w + " " + speicher + " " + liste[l].score);
                w++;
            }
            Console.WriteLine();
            Console.WriteLine("Ihre Punktzahl: " + score);
            Console.WriteLine("Beliebige Taste zum fortfahren drücken");
            Console.ReadKey(true);
            Console.WriteLine();
            if (liste.Any(f => f.score < Level.score))
            {
                string name = "";
                do
                {
                    Console.WriteLine("Geben Sie bitte ihren Namen ein.");
                    Console.WriteLine("; ist nicht erlaubt. Die Person die es doch tut,");
                    Console.WriteLine("wird beim nächsten Aufruf der Liste rausgekickt!");
                    Console.WriteLine("Bitte Namen eingeben:");
                    name = Console.ReadLine();
                } while (name == "");
                liste[0].name = name;
                liste[0].score = score;
                liste = Sortieren(liste);
                Datei.Bestenlistespeichern(liste);
            }
        }

        static bestenliste[] Sortieren(bestenliste[] liste)
        {
            bool PaarSortiert;
            //solange nicht alle paare bei jedem  Durchlauf     
            //sortiert sind, Alg. wiederholen. 
            //->BubbleSort verfahren
            do
            {
                PaarSortiert = true;
                for (int i = 0; i < liste.Length - 1; i++)
                {
                    if (liste[i].score > liste[i + 1].score)
                    {
                        //zahlen tauschen (nur ein Paar)
                        bestenliste temp = liste[i];
                        liste[i] = liste[i + 1];
                        liste[i + 1] = temp;
                        //nicht sortiert
                        PaarSortiert = false;
                    }
                }
            } while (!PaarSortiert);
            return liste;
        }
    }
}
