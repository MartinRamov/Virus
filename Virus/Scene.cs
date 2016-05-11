using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Virus
{
    public class Scene
    {
        /// <summary>
        /// Листа од карактери присутни на сцената
        /// </summary>
        public List<Character> Characters { set; get; }

        public Scene()
        {
            Characters = new List<Character>();
        }
        /// <summary>
        /// МЕТОД ЗА ДОДАВАЊЕ КАРАКТЕРИ
        /// </summary>
        /// <param name="c"></param>
        public void addCharacter(Character c)
        {
            Characters.Add(c);
        }

        public void Draw(Graphics g)
        {
            foreach(Character c in Characters)
            {
                c.Draw(g);
            }
        }
        /// <summary>
        /// МЕТОД КОИ ПРЕДИЗВИКУВА КАРАКТЕРИТЕ ДА СЕ ДВИЖАТ
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void MoveCharacters(float width, float height)
        {
            foreach(Character c in Characters)
            {
                c.Move(width,height);
            }
        }
    }
}
