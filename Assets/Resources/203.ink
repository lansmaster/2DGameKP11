О, привет <color=\#ff0000><b>игрок</b></color>, ты что-то ищешь? #speaker: Вадим Алексеевич
#speaker: Вы
-> Choises

== Choises ==
* [Добрый день. Кто вы?] -> Who
* [Искал документы] -> Smalltalk
+ [Уйти от разговора] -> QuitDialog


== Who == 
Здравствуйте. Расскажите, кто вы? #speaker: Вы
#speaker: Вадим Алексеевич 
Ну, хорошо, я Вадим Алексеевич, я веду инновационные технологии, архитектуру аппаратных средств, прикладную электронику и основы вычислительной техники. Также я являюсь преподавателем учебной и производственной практики. Мой кабинет 303.
#speaker: Вы
-> Choises

== Smalltalk == 
Я искал документы, они где-то здесь... #speaker: Вы
    О, недавно видел их в 206 кабинете #speaker: Вадим Алексеевич
Спасибо большое! #speaker: Вы
#speaker: Вы
-> Choises

== QuitDialog ==
Спасибо, я пойду #speaker: Вы
#speaker: Григорий Валерьевич
Удачи
-> END
