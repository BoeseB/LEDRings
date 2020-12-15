Notizen zur Übung:

- Wie immer wo ist die richtige Schnittstelle um mit dem Testen zu beginnen?
- Wieviele Tests sind ausreichend?
	- Ich habe das gefühl mein Test für den ersten Ring hat schon relativ viele Testfälle.
		- Kann mir hier Property based testing weiterhelfen?
			- Antwort: Ich denke nicht, da es die Logik zur generieren der Messages eigentlich duplizieren würde als Regel.
- Habe ich früh genug angefangen den code auch in die Production code base einzubauen?
	- Gefühlt kam dies jetzt relativ spät.
	- Wie hätte sich das Code design dadurch geändert?
- Erkenntnis nach Diskussion mit Peter zu Zwischenstand: 
	- Die Anforderungen auf Anhieb richtig zu bekommen ist tricky.
		- Kleinigkeiten die ich übersehen habe:
			- Wert soll bei ändern des Reglers auf den LEDs aktualisiert werden und nicht erst beim drücken von 'Save'
			- Wann die Regel wann welche LEDs leuchten sollen war mir aus den Anforderungen auch nicht ganz klar ob die hälfte der LEDs schon beim Wert 50 oder erst bei Wert 51 leuchten soll
				- Peter war der Auffassung erst bei Wert 51 ich habe aber bei 50 implementiert.
				- Hier Hilft wahrscheinlich konkrete Testfälle mit dem Produktowner durchzugehen, vorallem an den "Randbereichen"
- Erkenntnis Testframework
	- Collection Asserts im MS Test Framework haben einen nicht sehr hilfreiche Ausgabe im Fehlerfall
		- Deswegen Wechsel zu XUnit.
