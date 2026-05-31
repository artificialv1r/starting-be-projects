Komande za stvaranje i primenu migracija se realizuju kroz:
- NuGet Package Manager Console (Tools -> NuGet Package Manager -> NuGet Package Manager Console).

Za kreiranje migracije potrebno je uneti komandu u sledećem format:
- add-migration <naziv migracije>

Kreirana migracija se primenjuje izvršavanjem sledeće komande u terminalu:
- update-database

Da izlistate sve migracije koje su do sada primenjene, možete iskoristiti sledeću komandu:
- get-migration

Ukoliko želite da vratite stanje vaše baze na neku od ranijih migracija, možete iskoristiti sledeću komandu:
- update-database -Migration: <naziv ranije migracije>

U slučaju da želite ukloniti poslednju migraciju, to možete učiniti pomoću komande:
- remove-migration

U xUnit tehnologiji, vrste lažnih implementacija realizujemo uz pomoć Moq biblioteke i njene Mock klase.
