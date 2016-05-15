using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;
using WMPLib;

namespace Virus
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Мапирање на URL локацијата на сликите од карактерите според нивното име
        /// </summary>
        private static Dictionary<int, System.Drawing.Image> characterSet = new Dictionary<int, System.Drawing.Image>
        {
            {0, Properties.Resources.blue },
            {1, Properties.Resources.orange },
            {2, Properties.Resources.bordo },
            {3, Properties.Resources.green },
            {4, Properties.Resources.yellow },
            {5, Properties.Resources.red }
        };
        /// <summary>
        /// Мапирање на бојата на позадината во зависност од бројот на освоени поени
        /// </summary>
        private static Dictionary<int, Color> colorSet = new Dictionary<int, Color>
        {
            {0, Color.Beige },
            {1, Color.LightYellow },
            {2, Color.LightCyan },
            {3, Color.FromArgb(210, 249, 210) },
            {4, Color.BlanchedAlmond },
            {5, Color.FromArgb(255, 153, 153) }
        };
        /// <summary>
        /// Сцена во која шти се наоѓаат сите присутни карактери
        /// </summary>
        private Scene scene;
        /// <summary>
        /// Карактерот кој треба да се отстрани од сцена (Catch)
        /// </summary>
        private Character kill;
        private Timer t1;
        private Timer t2;
        /// <summary>
        /// Димензии на формата
        /// </summary>
        private float width;
        private float height;
        /// <summary>
        /// Променливата start укажува дали е кликнато на копчето пауза или е PlayMode
        /// </summary>
        private bool start;
        private bool playMode;
        /// <summary>
        /// Променливата soundPlay инцицира дали корисникот сака звук
        /// </summary>
        private bool soundPlay;
        /// <summary>
        /// Број на секунди на игра
        /// </summary>
        private int vreme;
        /// <summary>
        /// Број на поени
        /// </summary>
        private int poeni;
        /// <summary>
        /// Плеери за три различни звуци
        /// Почеток на нова игра
        /// Промена на карактер индикатор
        /// Звук при клик (уништување) на соодветниот карактер
        /// Позадинска музика
        /// </summary>
        private SoundPlayer soundNewGame;
        private SoundPlayer soundChange;
        private SoundPlayer soundKill;
        private SoundPlayer backgroundMusic;
        /// <summary>
        /// Лабели за поени и време
        /// </summary>
        private Label lbl1;
        private Label lbl2;
        string fileName;

        public Form1()
        {   
            InitializeComponent();
            fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Virus Achievments.txt");
            using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(fileName, true))
            {
                if (!File.Exists(fileName))
                {
                    File.Create(fileName);
                    file.WriteLine("Player1");
                    file.WriteLine(0);
                }
            }
            height = this.Height - 150;
            width = this.Width - 18;
            DoubleBuffered = true;
            scene = new Scene();
            MaximizeBox = false;
            start = false;
            playMode = false;
            soundPlay = true;
            soundNewGame = new SoundPlayer(Properties.Resources.sound4);
            soundChange = new SoundPlayer(Properties.Resources.sound2);
            soundKill = new SoundPlayer(Properties.Resources.sound1);
            backgroundMusic = new SoundPlayer(Properties.Resources.Acoustic_Background_Music);
            backgroundMusic.PlayLooping();
            poeni = 0;
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            begin();
        }

        /// <summary>
        /// ФУНКЦИЈАТА ГИ ОДРЕДУВА ПОЧЕТНИТЕ УСЛОВИ НА ИГРАТА
        /// </summary>
        private void begin()
        {
            poeni = 0;
            vreme = 0;
            if (soundPlay)
            {
                backgroundMusic.Stop();
                soundNewGame.Play();
            }
            start = true;
            playMode = true;
            Controls.Clear();

            t1 = new Timer();
            t1.Interval = 1000;
            t1.Tick += new EventHandler(t1_Tick);
            t1.Start();

            t2 = new Timer();
            t2.Interval = 100;
            t2.Tick += new EventHandler(t2_Tick);
            t2.Start();

            StartGame();
        }

        /// <summary>
        /// ФУНКЦИЈАТА ГЕНЕРИРА 6 ПОЧЕТНИ КАРАКТЕРИ ОД СЕКОЈ ВИД ПО ЕДЕН
        /// </summary>
        public void StartGame()
        {
            System.Drawing.Image url;
            Random pom = new Random();
            for (int i = 0; i <= 5; ++i)
            {
                url = characterSet[i];
                Character c = new Character(url, pom.Next(300), pom.Next(300), pom.NextDouble() * 2 * Math.PI);
                scene.addCharacter(c);
            }
            Randomizer(2);
        }

        /// <summary>
        /// ФУНКЦИЈА НА ЧИЈ ТАКТ СЕ ГЕНЕРИРААТ НОВИ КАРАКТЕРИ
        /// И НОВ КАРАКТЕР ИНДИКАТОР ВО ЦРВЕНОТО ПОЛЕ (ИНДИКАТОРОТ СЕ МЕНУВА НА СЕКОИ 2 СЕКУНДИ)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void t1_Tick(object sender, EventArgs e)
        {
            ++vreme;
            Randomizer(1);
            if (vreme % 2 == 0)
            {
                Randomizer(2);
                if (soundPlay)
                    soundChange.Play();
            }
            Controls.Clear();
            lbl1.Text = String.Format("Time: {0:00}:{1:00}", vreme / 60, vreme % 60);
            Controls.Add(lbl1);
            Invalidate(true);
        }

        /// <summary>
        /// ФУНКЦИЈА КОЈА НА СЕКОИ 100 ms ГИ ПОМЕСТУВА И ИСЦРТУВА КАРАКТЕРИТЕ 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void t2_Tick(object sender, EventArgs e)
        {
            scene.MoveCharacters(width, height);
            Invalidate(true);
            checkNumber();
        }

        /// <summary>
        /// ФУНКЦИЈА ЗА ИСЦРТУВАЊЕ НА ФОРМАТА
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (start || playMode)
            {
                Color c;
                if (poeni < 50)
                    c = colorSet[poeni / 10];
                else
                    c = colorSet[5];
                e.Graphics.Clear(c);
                Pen p = new Pen(Color.Red, 3);
                e.Graphics.DrawEllipse(p, 300, 370, 60, 60);
                lbl1 = new Label();
                lbl2 = new Label();
                lbl1.BackColor = c;
                lbl2.BackColor = c;
                lbl1.Width = 200;
                lbl2.Width = 200;
                lbl1.Location = new Point(50, 380);
                lbl2.Location = new Point(430, 380);
                lbl1.Font = new Font("Castellar", 18, FontStyle.Bold);
                lbl2.Font = new Font("Castellar", 18, FontStyle.Bold);
                lbl1.Text = String.Format("Time: {0:00}:{1:00}", vreme / 60, vreme % 60);
                lbl2.Text = "Points: " + poeni.ToString();
                Controls.Add(lbl1);
                Controls.Add(lbl2);
                scene.Draw(e.Graphics);
                kill.Draw(e.Graphics);
                p.Dispose();
            }
        }

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
            System.Drawing.Image url = characterSet[r.Next(6)];         

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

        /// <summary>
        /// ФУНКЦИЈАТА СО КОЈА СЕ ПОВИКУВА ПОВТОРНО ИСЦРТУВАЊЕ НА ФОРМАТА ОТКАКО Е МАКСИМИЗИРАНА
        /// </summary>
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
                Invalidate(true);
        }

        /// <summary>
        /// ФУНКЦИЈА КОЈА СЕ АКТИВИРА СО КЛИК НА КОПЧЕТО Exit И СЛУЖИ ЗА ИСКЛУЧУВАЊЕ НА ИГРАТА
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// ФУНКЦИЈА КОЈА ГО ВКЛУЧУВА ИЛИ ИСКЛУЧУВА ЗВУКОТ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSound_Click(object sender, EventArgs e)
        {
            if (soundPlay == true)
            {
                soundPlay = false;
                backgroundMusic.Stop();
                btnSound.Text = "Sound: Off";
                btnSound.ForeColor = Color.FromArgb(192, 0, 0);
            }
            else
            {
                soundPlay = true;
                backgroundMusic.PlayLooping();
                btnSound.Text = "Sound: On";
                btnSound.ForeColor = Color.Black;
            }
        }

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

        /// <summary>
        /// OВАА ФУНКЦИЈА ГО ПРОВЕРУВА БРОЈОТ НА КАРАКТЕРИ НА СЦЕНА
        /// ОТКАКО НА СЦЕНАТА ЌЕ СЕ ПОЈАВАТ 30 КАРАКТЕРИ, ИГРАТА ЗАВРШУВА
        /// ОДРЕДУВА СЛЕДЕН ЧЕКОР ПО ЗАВРШУВАЊЕ НА ИГРАТА
        /// </summary>
        private void checkNumber()
        {
            if (scene.Characters.Count() > 29)
            {
                t1.Stop();
                t2.Stop();
                SaveScore saveScore = new SaveScore();
                DialogResult result = saveScore.ShowDialog();
                scene.Characters.Clear();
                scene = new Scene();
                if (saveScore.Save)
                {
                    string text = saveScore.Name;
                    string point = poeni.ToString();
                    using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(fileName, true))
                    {
                        if (!File.Exists(fileName))
                            File.Create(fileName);
                        file.WriteLine(text);
                        file.WriteLine(point);
                        file.Close();
                    }
                }
                if (result == System.Windows.Forms.DialogResult.Retry)
                {
                    begin();
                }
                else if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    Controls.Clear();
                    start = false;
                    playMode = false;
                    this.KeyDown -= Form1_KeyDown;
                    this.MouseDown -= Form1_MouseDown;
                    InitializeComponent();
                    if (soundPlay == true)
                    {
                        backgroundMusic.PlayLooping();
                        btnSound.Text = "Sound: On";
                        btnSound.ForeColor = Color.Black;
                    }
                    else
                    {
                        backgroundMusic.Stop();
                        btnSound.Text = "Sound: Off";
                        btnSound.ForeColor = Color.FromArgb(192, 0, 0);
                    }
                }
            }
        }

        /// <summary>
        /// ОВАА ФУНКЦИЈА СЕ АКТИВИРА НА КЛИК НА КОПЧЕТО П (Р) ОД ТАСТАТУРА
        /// СЛУЖИ ЗА ПАУЗИРАЊЕ/ПРОДОЛЖУВАЊЕ НА ИГРАТА
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (playMode && e.KeyCode == Keys.P)
            {
                if (start)
                {
                    t1.Stop();
                    t2.Stop();
                    start = false;
                }
                else
                {
                    t1.Start();
                    t2.Start();
                    start = true;
                }
            }
        }

        /// <summary>
        /// фУНКЦИЈА КОЈА ГО ОДРЕДУВА ПРИКАЗОТ НА HIGHSCORES
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHighscore_Click(object sender, EventArgs e)
        {
                System.IO.StreamReader stream = new StreamReader(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Virus Achievments.txt").ToString());
                string line;
                List<String> lines = new List<string>();
                while ((line = stream.ReadLine()) != null)
                {
                    lines.Add(line);
                }
                ScoreBoard scoreBoard = new ScoreBoard();
                string[] names = new string[lines.Count()];
                string[] scores = new string[lines.Count()];

                ////Резултатите во Virus Achievments.txt во Application data се запишуваат во Формат
                ///ИМЕ НА ИГРАЧ
                /// РЕЗУЛТАТ
                /// Парен индекс значи име
                /// Неpарен индекс значи резултат
                int n = 0;
                int s = 0;

                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].Trim() != "")
                    {
                        if (i % 2 == 0)
                        {
                            names[n] = lines[i] + "\n";
                            n++;
                        }
                        else
                        {
                            scores[s] = lines[i] + "\n";
                            s++;
                        }
                    }
                }

                ///Се сортира scores
                ///Паралелно мора да се менува и низата names (Name останува паралелно на Score)
                for (int i = 0; i < scores.Length - 1; i++)
                {
                    for (int j = i; j < scores.Length; j++)
                    {
                        int x = Convert.ToInt32(scores[i]);
                        int y = Convert.ToInt32(scores[j]);
                        if (y > x)
                        {
                            string temp = scores[i];
                            scores[i] = scores[j];
                            scores[j] = temp;
                            string tempName = names[i];
                            names[i] = names[j];
                            names[j] = tempName;
                        }
                    }
                }

                ///10-те најдобри се превземаат во нови стрингови (StringBuilder) кои ке се прикажуваат на екран
                StringBuilder bestScores = new StringBuilder();
                StringBuilder bestNames = new StringBuilder();
                for (int i = 0; i < scores.Length; i++)
                {
                    if (i >= 10)
                    {
                        break;
                    }
                    else
                    {
                        bestScores.Append(scores[i]);
                        bestNames.Append(names[i]);
                    }
                }
                scoreBoard.lbPlayers.Text = bestNames.ToString();
                scoreBoard.lbScores.Text = bestScores.ToString();
                scoreBoard.ShowDialog();
        }

        /// <summary>
        /// ФОРМА КОЈА ГИ ПРИКАЖУВА ИНСТРУКЦИИТЕ НА ИГРАТА
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 i = new Form2();
            i.ShowDialog();    
        }
    }
}

