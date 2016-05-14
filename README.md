#Virus

##1. Опис на апликацијата
Апликацијата што ја развивме е игра која ја нарековме **Virus** и е наша сопствена идеја. Играта започнува со шест различни карактери и индикација за тоа кој карактер може да се уништи. На секоја нова секунда на сцената се појавува нов карактер, а на секои две секунди се менува индикаторот за тоа кој карактер може да се уништи. Играта се сведува на тоа да корисникот биде доволно брз и да не дозволи прогресивно да се зголемува бројот на карактери (вируси).Со клик врз погрешен карактер се предизвикува појава на нов. Со секое отстранување на карактер од формата (Virus) се добива поен.  Играта ќе заврши кога на сцената ќе се појават 30 карактери, по што имаме можност за зачувување на поените.

##2. Упатство за користење
###2.1 Нова игра
При стартување на апликацијата се отвара почетниот прозор од кој можеме да одбереме почеток на нова игра, преглед на најдобрите поени,вклучување/исклучување на звукот, прикажување на инструкции(правила) за игра или исклучување на апликацијата.

###2.2 Правила за игра (инструкции)
Целта на играта е да се уништуваат што е можно повеќе вируси, се додека нивниот број не достигне 30 - Тогаш е **крај** на играта.
* Индикаторот покажува кој вирус може да се уништи во моментот, тој се менува на секои 2 секунди.
* На секоја секунда се појавува по еден нов вирус.
* Со клик на вирус кој е ист како во индикаторот тој се уништува и се добива поен.
* Со клик на вирус кој е различен од тој во индикаторот се предизвикува појавување на нов вирус.


#33 <strong>Опис на решение на проблемот</strong></br>
Карактерите се имплементирани преку класа Character во која се чуваат сите параметри за карактерите како и начинот на кој се исцртуваат на екран. Во оваа класа е имплементирано и движењето на карактерите. Карактерите се движат хаотично под произволен агол низ екранот и ја менуваат насоката секогаш кога ќе наидат на рабовите од формата. Движењето е изведено преку едноставни тригонометриски функции.
Класата Scene всушност ја претставува сцената за играње. Таа чува листа од карактери и овозможува нивно исцртување и придвишување.
Во класата Form1 се чува објект од класата Scene, два тајмери  еден за движењето на карактерите, вториот за нивно инстанцирање и дополнителни променливи. Во оваа класа е имплементирана целата логика околу надгледување на сцената, инстанцирање и бришење карактери.
На секоја класа, функција и промелива и е доделен xml summary  кој јасно ја опишува нивната функција и примена.</br></br>

<p align="center">
  <img src="Screenshots\Start.png"/>
  <img src="Screenshots\Instructions.png"/>
  <img src="Screenshots\igra.png"/>
  <img src="Screenshots\SaveScore.png"/>
  <img src="Screenshots\ScoreBoard.png"/>
</p>
