Я сейчас немного занят, <color=\#ff0000><b>игрок</b></color>, что ты хотел? #speaker: Григорий Валерьевич
#speaker: Вы
-> Choises

== Choises ==
+ [Добрый день. Кто вы?] -> Who
+ [Искал 307 кабинет] -> Smalltalk
+ [Уйти от разговора] -> QuitDialog


== Who == 
Расскажите немного о себе. #speaker: Вы
Оу, хорошо, я Григорий Валерьевич, здесь, в 303 кабинете, проводятся пары по операционным системам, инновационным технологиям и другим дисциплинам, связанных с администрированием и управлением систем. Скучно точно не будет! #speaker: Григорий Валерьевич
#speaker: Вы
-> Choises

== Smalltalk == 
Я искал 310, не подскажете куда мне? #speaker: Вы
    А, это тебе в другой конец коридора.  #speaker: Григорий Валерьевич
#speaker: Вы
-> Choises

== QuitDialog ==
Спасибо, больше не буду вас отвлекать. #speaker: Вы
Удачи, заглядывай ко мне позже. #speaker: Григорий Валерьевич
-> END