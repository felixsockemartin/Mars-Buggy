using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MarsBuggy
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Level.Laden();
                System.ConsoleKeyInfo key;
                Console.Clear();
                Console.Write(Datei.Laden("Titelbildschirm.txt"));
                key = Console.ReadKey(true);

                if (Convert.ToString(key.Key) == "Spacebar")
                {
                    Arbeiter.Start();
                    Thread arbeiterAktualisiert = new Thread(Arbeiter.aktualisieren);
                    arbeiterAktualisiert.Priority = ThreadPriority.Highest;
                    arbeiterAktualisiert.Start();
                    //Wartet auf eine Eingabe während das Spiel ausgeführt wird
                    do
                    {
                        key = Console.ReadKey(true);
                        switch (Convert.ToString(key.Key))
                        {
                            case "Q":
                                Arbeiter.Tot();
                                Thread.Sleep(10);
                                break;

                            case "Spacebar":
                                Buggy.sprung();
                                break;

                            case "A":
                                Arbeiter.schuss = true;
                                break;
                        }
                    }
                    while (Arbeiter.run);
                    Level.Highscore();
                }
                else if (Convert.ToString(key.Key) == "Q")
                { break; }
            } while (true);
        }
    }
}
