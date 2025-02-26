# ChessTracker



## Opis domene
ChessTracker je inovativna desktop aplikacija dizajnirana za rangiranje šahovskih igrača, namijenjena šahovskim klubovima, organizatorima turnira (šahovskom savezu), sudcima i samim šahistima. Ova aplikacija pojednostavljuje proces praćenja rang-liste i upravljanja rejtingom, omogućavajući korisnicima brz i jednostavan uvid u statistiku i rezultate. Cilj aplikacije je unaprijediti organizaciju šahovskih natjecanja te olakšati vođenje evidencije o igračima, partijama i njihovim rezultatima. Ciljana skupina korisnika su nam šahovski savez koji može dodavati igrače u bazu, generira izvještaj o statistici registriranih korisnika te organizira i upravlja nadolazećim turnira; sudac koji unosi rezultate partija s turnira, provjerava ažurirane rejtinge igrača nakon svakog kola te generira rang-liste i vodi evidenciju o turnirima; klub koji ima mogućnost dodavanja, uređivanja i deaktivacije računa svojih članova, kao i prijavu igrača na turnire gdje igraju kao klub; registrirani korisnik koji prati svoj napredak, pregledava rezultate i rang-liste kako bi se usporedio s drugim igračima te ima mogućnost prijave na turnire gdje igra samostalno.

## Specifikacija projekta

Oznaka | Naziv | Kratki opis | Odgovorni član tima
------ | ----- | ----------- | -------------------
F01 | Autentifikacija korisnika | Za pristup aplikaciji potrebna je izrada korisničkog profila kako bi korisnik moga pristupiti funkcionalnostima aplikacije. Nakon kreiranja korisničkog profila, korisnik unosi svoje korisničko ime i lozinku za prijavu u aplikaciju.  | David Brckan
F02 | Upravljanje igračima | Aplikacija omogućuje klubu dodavanje i uređivanje računa šahovskih igrača. Isto tako, ako igrači više nisu u šahovskom klubu moguće je deaktivirati njihov račun. | 
F03 | Upravljanje turnirima | Aplikacija omogućuje šahovskom savezu dodavanje, ažuriranje te brisanje postavljenih turnira. Navodi se vrijeme, datum, lokacija, tip turnira, broj kola (rundi), vrijeme igranja jedne partije… | 
F04 | Prijava igrača i klubova na turnir | Aplikacija omogućuje klubu da se prijavi za nadolazeći turnir, klub odabire određene sudionike koji će prisustvovati. Isto tako, igračima je omogućeno da se sami prijave na određene turnire gdje nije potrebno igranje u klubu. | David Brckan
F05 | Generiranje parova | Aplikacija omogućuje kreiranje partija po kolima; nasumično se uparuju igrači u svakom kolu s time da igrači ne igraju međusobno više puta tijekom turnira. Nakon evidencije rezultata partija, automatski se generiraju parovi za sljedeća kola. | 
F06 | Evidencija rezultata partija | Sudcu će biti omogućen unos rezultata odigranih partija (pobjeda, poraz, remi). Isto tako ako će zabunom nešto krivo upisati, bit će mu omogućeno ažuriranje postojećih rezultata. | 
F07 | Prikaz rang-liste turnira | Aplikacija omogućuje, na kraju svakog turnira, sudcu generiranje izvješća o igračima te odigranim turnirima. Prikazuje se od najboljeg do najlošijeg igrača/kluba, određuje se pobjednik na temelju rezultata. | 
F08 | Izračun rejtinga | Aplikacija omogućuje automatski preračun rejtinga igrača nakon unosa rezultata partije koristeći odgovarajuće FIDE formule. | 
F09 | Pretraživanje igrača i turnira | Aplikacija omogućuje pretraživanje igrača unosom prezimena, naziva kluba, CRO ID-u te isto tako omogućuje pretraživanje turnira prema njihovom nazivu, datumu. | David Brckan
F10 | Pregled informacija o igračima | Aplikacija omogućuje prikaz osnovnih informacija o igračima; ime, prezime, rejting, nacionalnost, povijest partija. | 
F11 | Generiranje izvještaja o igračima | Administrator generira izvještaje o broju registriranih šahista, statistika igrača (rezultati, rejting, povijest sudjelovanja). | 
F12 | Pregled svih turnira | Aplikacija omogućuje prikaz svih nadolazećih te prošlih turnira. Klikom na odabrani turnir, otvara se više informacija (ako je prošli prikaže se rang lista, a ako je nadolazeći prikažu se svi prijavljeni klubovi i igrači). | 

## Tehnologije i oprema
U ovome projektu koristit ćemo .NET Framework koji inače služi za izradu desktop aplikacija, može poslužiti za izradu mobilnih aplikacija, itd. Razvojno okruženje koje ćemo koristiti je Visual Studio 2022. Koristit ćemo i bazu podataka.
