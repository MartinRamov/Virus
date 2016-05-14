using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Virus
{
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
}
