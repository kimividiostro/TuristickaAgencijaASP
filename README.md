# TuristickaAgencijaASP

ASP .NET MVC aplikacija koja implementira ASP .NET Identity.

Korisnik se registruje na sajt unosenjem korisnickog imena, email adrese i lozinke. Ukoliko korisnicko ime i email adresa ne postoje u bazi, korisniku se salje verifikacioni link na unetu email adresu i korisnik se moze ulogovati tek nakon sto je posetio link.
Takodje, pri svakom logovanju, korisniku se salje verifikacioni kod na email adresu, za slucaj da je lozinka kompromitovana.

U slucaju da se unese neispravna lozinka vise od 5 puta, nalog se zakljucava na 5 min(demonstracije radi).

Nakon uspesnog logovanja, korisnik se preusmerava na stranicu koja izlistava sve aranzmane koje agencija nudi(jquery datatables omogucava pretragu i sortiranje). Korisnik klikce na aranzman i preusmerava se na stranicu koja prikazuje detalje tog aranzmana i dugme kojim korisnik rezervisi taj aranzman(ukoliko nije rezervisao isti pre toga). Korisnik rezervisane aranzmane moze videti na stranici svog profila.

Admin moze dodavati nove aranzmane.
