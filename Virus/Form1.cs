using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;
using WMPLib;

namespace Virus
{
    public partial class Form1 : Form
    {
        private enum TYPE
        {
            blue,
            orange,
            bordo,
            green,
            yellow,
            red
        }
        private TYPE Current { set; get; }
        private static Dictionary<string, string> characterSet = new Dictionary<string, string>
        {
            {"blue", "Virus.blue.png" },
            {"orange", "Virus.orange.png" },
            {"bordo", "Virus.bordo.png" },
            {"green", "Virus.green.png" },
            {"yellow", "Virus.yellow.png" },
            {"red", "Virus.red.png" }
        };
        private static Dictionary<int, Color> colorSet = new Dictionary<int, Color>
        {
            {0, Color.Beige },
            {1, Color.LightYellow },
            {2, Color.LightCyan },
            {3, Color.FromArgb(210, 249, 210) },
            {4, Color.BlanchedAlmond },
            {5, Color.FromArgb(255, 153, 153) }
        };
        private Scene scene;
        private Character kill;
        private Timer t1;
        private Timer t2;
        private float width;
        private float height;
        private bool start;
        private bool playMode;
        private bool soundPlay;
        private int vreme;
        private int poeni;
        private SoundPlayer soundNewGame;
        private SoundPlayer soundChange;
        private SoundPlayer soundKill;
        private WMPLib.WindowsMediaPlayer wplayer; 
        private Label lbl1;
        private Label lbl2;

        public Form1()
        {   
            InitializeComponent();
            height = this.Height - 150;
            width = this.Width - 18;
            DoubleBuffered = true;
            scene = new Scene();
            MaximizeBox = false;
            start = false;
            playMode = false;
            soundPlay = true;
            soundNewGame = new SoundPlayer("sound4.wav");
            soundChange = new SoundPlayer("sound2.wav");
            soundKill = new SoundPlayer("sound1.wav");
            wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = "Acoustic Background Music.mp3";
            poeni = 0;    
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            begin();
        }

        private void begin()
        {
            poeni = 0;
            vreme = 0;
            if (soundPlay)
            {
                soundNewGame.Play();
                wplayer.URL = "Annoying Song.mp3";
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

        private void t1_Tick(object sender, EventArgs e)
        {
            if (wplayer.playState != WMPPlayState.wmppsPlaying && soundPlay)
                wplayer.controls.play();
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

        private void t2_Tick(object sender, EventArgs e)
        {
            scene.MoveCharacters(width, height);
            Invalidate(true);
            checkNumber();
        }

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

        private void Randomizer(int a)
        {
            Random r = new Random();
            Current = (TYPE)r.Next(6);
            String url = characterSet[Current.ToString()];
            Character c;
            if (a == 1)
            {
                c = new Character(url);
                scene.addCharacter(c);
            }
            if (a == 2)
            {
                c = new Character(url, 310, 380, 0);
                kill = c;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            height = this.Height - 150;
            width = this.Width - 18;
            if (WindowState == FormWindowState.Maximized)
                Invalidate(true);
        }

        public void StartGame()
        {
            String url;
            Random pom = new Random();
            for (int i = 0; i <= 5; ++i)
            {
                Current = (TYPE)i;
                url = characterSet[Current.ToString()];
                Character c = new Character(url, pom.Next(300), pom.Next(300), pom.NextDouble() * 2 * Math.PI);
                scene.addCharacter(c);
            }
            Randomizer(2);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSound_Click(object sender, EventArgs e)
        {
            if (soundPlay == true)
            {
                soundPlay = false;
                wplayer.controls.stop();
                btnSound.Text = "Sound: Off";
                btnSound.ForeColor = Color.FromArgb(192, 0, 0);
            }
            else
            {
                soundPlay = true;
                wplayer.controls.play();
                btnSound.Text = "Sound: On";
                btnSound.ForeColor = Color.Black;
            }
        }

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

        private void checkNumber()
        {
            if (scene.Characters.Count() > 29)
            {
                t1.Stop();
                t2.Stop();
                wplayer.controls.stop();
                SaveScore saveScore = new SaveScore();
                scene.Characters.Clear();
                scene = new Scene();
                DialogResult result = saveScore.ShowDialog();             
                if (saveScore.Save)
                {
                    string text = saveScore.Name;
                    string point = poeni.ToString();
                    using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(@"Achievments.txt", true))
                    {
                        file.WriteLine(text);
                        file.WriteLine(point);
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
                        wplayer.URL = "Acoustic Background Music.mp3";
                        btnSound.Text = "Sound: On";
                        btnSound.ForeColor = Color.Black;
                    }
                    else
                    {
                        wplayer.controls.stop();
                        btnSound.Text = "Sound: Off";
                        btnSound.ForeColor = Color.FromArgb(192, 0, 0);
                    }
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (playMode && e.KeyCode == Keys.P)
            {
                if (start)
                {
                    t1.Stop();
                    t2.Stop();
                    start = false;
                    wplayer.controls.pause();
                }
                else
                {
                    t1.Start();
                    t2.Start();
                    start = true;
                    if (soundPlay)
                        wplayer.controls.play();
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
            ////Провери дали во .txt постојат претходни резултати
            if (System.IO.File.ReadAllLines(@"Achievments.txt") != null)
            {
                string[] lines = System.IO.File.ReadAllLines(@"Achievments.txt");
                string txt = "";
                ScoreBoard scoreBoard = new ScoreBoard();
                string[] names = new string[lines.Length];
                string[] scores = new string[lines.Length];

                ////Резултатите во .txt во Bin се запишуваат во Формат
                ///ИМЕ НА ИГРАЧ
                /// РЕЗУЛТАТ
                /// Парен индекс значи име
                /// Неpарен индекс значи резултат
                int n = 0;
                int s = 0;

                for (int i = 0; i < lines.Length; i++)
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
                scoreBoard.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 i = new Form2();
            i.ShowDialog();    
        }
    }
}

