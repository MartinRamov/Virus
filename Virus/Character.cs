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
        public string Url { get; set; }
        public float CoordinateX { get; set; }
        public float CoordinateY { get; set; }

        private double angle;
        private float velocityX;
        private float velocityY;
        private int Velocity;
        private Random r;

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

        public void Draw(Graphics g)
        {
            Stream s = this.GetType().Assembly.GetManifestResourceStream(Url);
            Bitmap bmp = new Bitmap(s);
            s.Close();
            g.DrawImage(bmp, CoordinateX,CoordinateY, 40 , 45);
            bmp.Dispose();
        }

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
