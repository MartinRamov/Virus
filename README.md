#Virus
<ol>
<li style="color:gray;"><strong>Опис на апликацијата</strong></li>
Апликацијата што ја развивме ја нарековме “Catch the Virus” и е наша сопствена идеја. Играта започнува со шест различни карактери и индикација за тоа кој карактер треба да се отстрани од екранот. На секоја нова секунда на сцената се појавува нов карактер, а на секои две секунди се менува индикаторот за тоа кој карактер да се отстрани од сцената. Играта се сведува на тоа да корисникот биде доволно брз и да не дозволи прогресивно да се зголемува бројот на карактери ( вируси ).Исто така клик врз погрешен карактер предизвикува појава на нов. Со секое отстранување на карактер од формата (Catching virus) се добива еден поен.  Играта ќе заврши кога на сцената ќе се појават триесет карактери, односно , просторот станува премногу хаотичен за да се продолжи со игра.

<li><strong>Упатство за користење</strong></li>
На почетниот екран се наоѓаат пет копчиња. На клик на копчето [Start a game]      корисникот има можност да започне нова игра, на клик на копчето [High Scores] корисникот добива преглед на десетте најдобри играчи. Апликацијата подржува и Silent mode кој се одбира со клик на копчето [Sound] кое истовремено индицира дали звукот е вклучен или исклучен. На клик на копчето [Instructions] се отвара дијалог прозорец преку кој на корисникот детално му се објаснува начинот на игра. Копчето [Еxit] служи за излез од апликацијата. На клик на копчето P од тастатурата се паузира играта. За повторно отпочнување по клик на копчето за пауза повторно се клика на истото копче (P).
По завршување на секоја игра се отвара прозорец со три копчиња. На копчето [Save my score] единствено се зачувува постигнатиот резултат, а потоа се одбира [Retry] за нова игра или [Cancel] за враќање назад кон почетното мени.

<li><strong>Опис на решение на проблемот</strong></li>
Карактерите се имплементирани преку класа Character во која се чуваат сите параметри за карактерите како и начинот на кој се исцртуваат на екран. Во оваа класа е имплементирано и движењето на карактерите. Карактерите се движат хаотично под произволен агол низ екранот и ја менуваат насоката секогаш кога ќе наидат на рабовите од формата. Движењето е изведено преку едноставни тригонометриски функции.
Класата Scene всушност ја претставува сцената за играње. Таа чува листа од карактери и овозможува нивно исцртување и придвишување.
Во класата Form1 се чува објект од класата Scene, два тајмери  еден за движењето на карактерите, вториот за нивно инстанцирање и дополнителни променливи. Во оваа класа е имплементирана целата логика околу надгледување на сцената, инстанцирање и бришење карактери.
На секоја класа, функција и промелива и е доделен xml summary  кој јасно ја опишува нивната функција и примена.
</ol>
<p align="center">
  <img src="Screenshots\Pocetno meni.png"/>
  <img src="Screenshots\colorchange.png"/>
  <img src="Screenshots\ending dialog.png"/>
  <img src="Screenshots\igra.png"/>
  <img src="Screenshots\score board.png"/>
</p>
