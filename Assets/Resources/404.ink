Здравствуй, <color=\#ff0000><b>игрок</b></color>, ты что-то хотел узнать?  #speaker: Елена Сергеевна
#speaker: Вы
-> Choises

== Choises ==
+ [Добрый день. Кто вы?] -> Who
+ [Истории] -> Smalltalk
+ [Уйти от разговора] -> QuitDialog


== Who == 
#speaker: Елена Сергеевна
Я Елена Сергеевна, преподаватель основ алгоритмизации и программирования, инновационных технологий,  инструментальных средств разработки программного обеспечения и других дисциплин. Чаще всего мои пары  проходят в 404 кабинете
#speaker: Вы
-> Choises

== Smalltalk == 
    #speaker: Вы
    Вы можете рассказать что-то интересное?
    #speaker: Елена Сергеевна
    Я сама училась в этом колледже, до сих пор помню как у нас появились урны с сортировкой, это ведь был проект какого-то студента
    #speaker: Вы
    Ого, спасибо. Это интересный момент!
#speaker: Вы
-> Choises

== QuitDialog ==
#speaker: Вы
Извините, я спешу 
#speaker: Елена Сергеевна
Давай, беги по своим делам

-> END
