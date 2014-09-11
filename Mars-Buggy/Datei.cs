using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsBuggy
{
    static class Datei
    {
        /// <summary>
        /// Lädt eine Datei.
        /// </summary>
        /// <param name="datei">Der relative Pfad der Datei.</param>
        /// <returns>Gibt den Dateiinhalt als String zurück.</returns>
        static public string Laden(string datei)
        {
            StreamReader text = new StreamReader(datei, System.Text.Encoding.UTF8);
            string Text = text.ReadToEnd();
            text.Close();
            return Text;
        }

        /// <summary>
        /// Dient dazu die Bestenliste zu speichern.
        /// </summary>
        /// <param name="liste">Die Liste die gespeichert werden soll.</param>
        static public void Bestenlistespeichern(Level.bestenliste[] liste)
        {
            StreamWriter text = new StreamWriter("Highscore.txt");
            text.Write("");
            foreach (Level.bestenliste x in liste)
            {
                string speicher = "";
                speicher += x.name + ";" + x.score;
                text.WriteLine(speicher);
            }
            text.Close();
        }
    }
}
