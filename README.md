#Virus
Windows Forms Project by: Martin Ramov, Mila Gjurova and Emilija Stefanovska



##1. Опис на апликацијата
Апликацијата што ја развивме е игра која ја нарековме **Virus** и е наша сопствена идеја. Играта започнува со шест различни карактери и индикација за тоа кој карактер може да се уништи. На секоја нова секунда на сцената се појавува нов карактер, а на секои две секунди се менува индикаторот за тоа кој карактер може да се уништи. Играта се сведува на тоа да корисникот биде доволно брз и да не дозволи прогресивно да се зголемува бројот на карактери (вируси).Со клик врз погрешен карактер се предизвикува појава на нов. Со секое отстранување на карактер од формата (Virus) се добива поен.  Играта ќе заврши кога на сцената ќе се појават 30 карактери, по што имаме можност за зачувување на поените.

##2. Упатство за користење
###2.1 Нова игра
При стартување на апликацијата се отвара почетниот прозор од кој можеме да одбереме почеток на нова игра, преглед на најдобрите поени,вклучување/исклучување на звукот, прикажување на инструкции(правила) за игра или исклучување на апликацијата.

  <img src="Screenshots\Start.png"/>

###2.2 Правила за игра (инструкции)
Со клик на копчето **Instructions** се отвара нова форма каде детално се опишани правилата за игра.

Целта на играта е да се уништуваат што е можно повеќе вируси, се додека нивниот број не достигне 30 - Тогаш е **крај** на играта.
* Играта започнува со 6 различни вируси
* Индикаторот покажува кој вирус може да се уништи во моментот, тој се менува на секои 2 секунди.
* На секоја секунда се појавува по еден нов вирус.
* Со клик на вирус кој е ист како во индикаторот тој се уништува и се добива поен.
* Со клик на вирус кој е различен од тој во индикаторот се предизвикува појавување на нов вирус.
 
  <img src="Screenshots\Instructions.png"/>
 
###2.3 High Scores
Со клик на копчето **HighScores** се отвара нова форма.

Тука се чуваат најдобрите 10 играчи, сортирани по опаѓачки редослед според бројот на поени.

Податоците се зачувуваат локално и не се бришат по затварање на апликацијата.

 <img src="Screenshots\SaveScore.png"/>
  <img src="Screenshots\ScoreBoard.png"/>

###2.4 Вклучување/Исклучување на звукот

На почетниот прозор се наоѓа копче **Sound: ON/ Sound:Off** неговиот изглед зависи од тоа дали е вклучен или исклучен звукот. Со клик на копчето се прави toogle на моменталната состојба на звукот.

##3. Начин на кој е решен проблемот
###3.1 Податочни структури
Секоја променлива и функција содржи **xml _summary_**, со детално објаснување.

Податоците кои се потребни за вирусите(карактерите), нивното движење и исцртување ги чуваме во класа ```public class Character```.

```c#
public class Character
    {
        /// <summary>
        /// URL до слика за карактер
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Координати на исцртување
        /// </summary>
        public float CoordinateX { get; set; }
        public float CoordinateY { get; set; }
        /// <summary>
        /// Агол под кој ќе се придвижува карактерот
        /// </summary>
        private double angle;
        /// <summary>
        /// Придвижување по x и y соодветно
        /// </summary>
        private float velocityX;
        private float velocityY;
        /// <summary>
        /// Брзина на движење
        /// </summary>
        private int Velocity;
        private Random r;
        /// <summary>
        /// Овој конструктор се користи при генерирање на почетните 6 карактери
        /// Овој конструктор се повикува исто така и при генерирање на карактер индикатор
        /// Овој карактер (индикатор) не треба да се движи па затоа во овој случај се пренесува 0 за агол
        /// </summary>
        /// <param name="url"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="angle"></param>
        public Character(string url, float x, float y, double angle)
        {
            Url = url;    
            Velocity = 10;
            this.angle = angle;
            velocityX = (float)(Math.Cos(angle) * Velocity);
            velocityY = (float)(Math.Sin(angle) * Velocity);
            CoordinateX = x;
            CoordinateY = y;
        }

        /// <summary>
        /// Овој конструктор се повикува понатаму за секој нов карактер што ќе се појавува на сцената
        /// </summary>
        /// <param name="url"></param>
        public Character(string url)
        {
            Url = url;
            r = new Random();
            Velocity = 10;
            angle = r.NextDouble() * 2 * Math.PI;
            velocityX = (float)(Math.Cos(angle) * Velocity);
            velocityY = (float)(Math.Sin(angle) * Velocity);
            CoordinateX = r.Next(600);
            CoordinateY = r.Next(300);
        }

        /// <summary>
        /// Во оваа функција се исцртува слика која го претставува карактерот
        /// </summary>
        /// <param name="г"></param>
        public void Draw(Graphics g)
        {
            Stream s = this.GetType().Assembly.GetManifestResourceStream(Url);
            Bitmap bmp = new Bitmap(s);
            s.Close();
            g.DrawImage(bmp, CoordinateX,CoordinateY, 40 , 45);
            bmp.Dispose();
        }

        /// <summary>
        /// Во оваа функција е имплементирано движењето на карактерите
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void Move(float width, float height)
        {
            float nextX = CoordinateX + velocityX;
            float nextY = CoordinateY + velocityY;
            if(nextX<=0 || nextX+40 >= width)
            {
                velocityX = -velocityX;
            }
            if (nextY<=0 || nextY + 45 >= height)
            {
                velocityY = -velocityY;
            }         
            CoordinateX = CoordinateX + velocityX;
            CoordinateY = CoordinateY + velocityY;
        }
    }

```

Во класата ```public class Scene``` чуваме листа од карактери кои моментално се прикажуваат и преку неа го вршиме исцртувањето и движењето на објектите од класата Character во даден момент.

Во главната класа ```public class Form1``` чуваме објект од класата Scene и во оваа класа се сите поважни функции кои се справуваат со настаните.

###3.2 Функции
####3.2.1 Тајмер
Има два тајмери од кои едниот го тригерира настанот за додавање на нов карактер, за промена за индикаторот и ја ажурира вредноста на лабелата за изминато време. Другиот се користи за придвижувањето и исцртувањето на карактерите.
####3.2.2 Клик настан
Оваа функција најпрво проверува дали е кликнато на вирус, а потоа ако е кликнато на вирус се проверува дали е истиот како во индикаторот, доколку е истиот се уништува, се добива поен и се ажурира лабелата за поени, а во спротивно се повикува функцијата Randomizer() со параметар 1.
```c#
        /// <summary>
        /// ФУНКЦИЈАТА СЕ АКТИВИРА НА КЛИК НА СЦЕНА
        /// ПРОВЕРУВА ДАЛИ Е КЛИКНАТО НА КАРАКТЕРОТ КОЈ ТРЕБА СЕ СЕ ОТСТРАНИ ОД СЦЕНАТА (УНИШТИ)
        /// ДОКОЛКУ СЕ КЛИКНЕ НА ПОГРЕШЕН КАРАКТЕР (РАЗЛИЧЕН ОД ИНДИКАТОРОТ), СЕ ПОЈАВУВА НОВ КАРАКТЕР
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (start)
            {
                foreach (Character c in scene.Characters)
                {
                    if ((e.X >= c.CoordinateX && e.X <= (c.CoordinateX + 40)) && (e.Y >= c.CoordinateY && e.Y <= (c.CoordinateY + 45)))
                    {
                        if (c.Url != kill.Url)
                        {
                            Randomizer(1);
                        }
                        else
                        {
                            scene.Characters.Remove(c);
                            poeni++;
                            if (soundPlay)
                                soundKill.Play();
                        }
                        break;
                    }
                }
                Controls.Clear();
                lbl2.Text = "Points: " + poeni.ToString();
                Controls.Add(lbl2);
                checkNumber();
            }
        }
```
####3.2.3 Randomizer()
Оваа функција прима два параметри **1** или **2**. Функцијата користејки рандом генерирање на вредност генерира број од 0 до 5, според мапирањето се одредува кој карактер ќе се појави. Во зависност од параметарот или се променува индикаторот или се генерира нов карактер кој се движи.
```c#
        /// <summary>
        /// ФУНКЦИЈАТА ГЕНЕРИРА РАНДОМ КАРАКТЕРИ
        /// </summary>
        /// <param name="a"></param>
        private void Randomizer(int a)
        {
            Character c;
            Random r = new Random();

            //Се генерира рандом вредност од 0 до 5
            //Според мапирањето во characterSet, се одредува кој карактер (URL) ќе се појави
            String url = characterSet[r.Next(6)];         

            ///Ако параметарот а е еден значи се генерира карактер кој се движи
            if (a == 1)
            {
                c = new Character(url);
                scene.addCharacter(c);
            }
            ///Ако параметарот а е 2 значи дека се генерира карактер индикатор
            ///(карактер во црвеното поле кој укажува кој е карактерот кој треба да се уништи (Virus)
            if (a == 2)
            {
                c = new Character(url, 310, 380, 0);
                kill = c;
            }
        }

```
  <img src="Screenshots\igra.png"/>



