# ANSI Repository

## GIT

tu znajdziesz informacje na temat pracy z gitem 

#### Tworzenie branchy do pracy
branche tworzone do pracy powinny wpisywać się w pattern 'feature/*' oraz nie powinny zawierać białych znaków.
Pattern ten jest konieczny, bez niego CI nie będzie w stanie się wykonać.

Bezpośrednia praca na branchu main jest odradzana.

Po stworzeniu pull requesta z brancha feature do main konieczne jest przejście CI (testy & build), dodatkowo dobrą praktyką jest przeglądnięcie kodu przez kogoś, jednak nie jest to wymagane. 

W przypadku dużej ilości commitów zalecam squashowanie ich do mniejszej liczby.

Po skończeniu pracy na danym branchu należy go usunąć.

#### Force politics 
Nie korzystamy z 'git push -f' podczas pracy na branchu Main. Użycie force na pozostałych brachach jest dozwolone.


## DOCKER

tu znajdziesz informacje na temat pracy z gitem 

#### Docker readme
Do plików dockerowych przeznaczony jest osobny folder. Plik readme w tym folderze ma pokazać jak należy skorzystać z docker-compose.
