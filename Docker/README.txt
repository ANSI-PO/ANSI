Jak to odpalić:
    - zainstaluj dockera na twój system operacyjny
    - uruchom, będąc w tym folderze, komendę "docker-compose --env-file compose.env up" w powershellu i nie zamykaj jego okna (tak długo jak będzie ono działać, będą działać osobne 'małe dwa komputerki')
    - uruchom adminera wchodząc pod link 'http://localhost:8080/' Adminer to nie baza danych a GUI
    - wprowadź dane logowania:
        user       - root
        password   - dostepne w pliku compose.env
        database   - ANSI
        server     - mysql_ansi_db
        system     - MySql
    - sama baza danych działa na localhost:3306 - port jest exposed 
    - baza danych automatycznie zaciaga komendy dostepne w pliku CREATE_TABLES.sql i wykonuje je na bazie.

    UWAGA:
    pod obraz nie jest podpięty wolumen. Po usunięciu obrazu z dockerfile baza danych jest ponownie inicjalizowana defaultowymi danymi. 