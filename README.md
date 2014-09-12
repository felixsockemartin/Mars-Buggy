Mars-Buggy 1.01
==========

Moon Buggy 2: Jetzt mit Monstern


Game Over

Ein Game Over wird erreicht, wenn
- die Mitte von einem der Räder direkt über einem Loch ist
- das vordere Rad im Maul der Kleiner-Als-Schlange ist, weil man ansonsten auf ihr landet, was man unbeschadet übersteht
- die Mitte von einem der Räder über oder im Maul der W-Schabe

-------------------------------------------------------------------------------------------------------------

Anpassen der Level.txt

Die Datei Level.txt verwaltet die verschiedenen Level mit ihren Schwierigkeitsgrad und den benötigten Punktestand für das nächste Level. Sie ist zum starten des Programmes zwingend notwendig, sollte sie nicht vorhanden sein, startet das Spiel nicht und stürzt ab.

Default:

50;30;100;0\n
100;20;50;0\n
200;20;30;1\n
500;20;30;2\n
0;10;30;2\n

Die Anzahl der Level hängt von der Anzahl der Zeilen in der Datei ab. Leerzeilen sind nicht möglich. In jeder Zeile müssen 4 Ganzzahlen stehen die mit ; abgetrennt sind. Die erste Zeile ist das erste Level und so weiter.

Die Bedeutung der Zahlen:

erforderliche Punktzahl;mindest Abstand der Objekte; maximaler Abstand der Objekte; Objektstufe

Die erforderliche Punktzahl sollte größer sein, als die des Levels davor, ansonsten würde das Level einfach übersprungen werden. Nur die Punktzahl des letzten Levels ist unwichtig.

Objektstufen:
0 - Löcher
1 - Löcher, Kleiner-Als-Schlange
2 - Löcher, Kleiner-Als-Schlange, W-Schabe

------------------------------------------------------------------------------------------------------------

Die Highscores
werden mit der Highscore.txt gelöscht. Sollte die Datei nicht existieren, wird sie angelegt mit 10 Zeilen mit den Namen " " und der Punktzahl 0.
