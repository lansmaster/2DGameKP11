Привет <color=\#ff0000><b>игрок</b></color>, хочешь что-нибудь спросить? #speaker: Дмитрий Владимирович
#speaker: Вы
-> Choises

== Choises ==
+ [Добрый день. Кто вы?] -> Who
+ [Искал кабель] -> Smalltalk
+ [Уйти от разговора] -> QuitDialog

== Who == 
    Здравствуйте, расскажите о себе, пожалуйста.  #speaker: Вы 
    Меня зовут Дмитрий Владимирович, я преподаю 7 дисциплин и все в 403 кабинете. Студенты начальных курсов могут встретиться со мной на математическом моделировании, основах алгоритмизации и программирования и основах проектирования баз данных. #speaker: Дмитрий Владимирович
#speaker: Вы
-> Choises

== Smalltalk == 
    Я где-то здесь оставил сетевой кабель, вы его не видели? #speaker: Вы
        К сожалению, нет. #speaker: Дмитрий Владимирович
#speaker: Вы
-> Choises

== QuitDialog ==
    Извините, я спешу. #speaker: Вы
        Хорошего дня! #speaker: Дмитрий Владимирович 

->END