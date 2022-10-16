Jak to odpalić:
- zainstaluj dockera na twój system operacyjny
- uruchom będąc w tym folderze komendę 'docker compose up' w powershellu i nie zamykaj jego okna (tak długo jak będzie ono działać, będą działać osobne 'małe dwa komputerki')
- uruchom adminera wchodząc pod link 'http://localhost:8080/?server=database' Adminer to nie baza danych a GUI
- wprowadź dane logowania które zawarte są w pliku *.env (uwaga, zmiana tych danych spowoduje zmianę danych logowania. FYI Root password jest automatycznie, randomowo generowane)
- sama baza danych działa na porcie 3306
- pod bazę danych został podpięty wolumen (taki dysk którym zarządza docker), z tego względu dane które wprowadzicie do bazy danych zostaną zapamiętane(przy ponownym uruchomieniu docker compose up nie musicie ich wprowadzać)