using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Virus
{
    public class Scene
    {
        public List<Character> Characters { set; get; }

        public Scene()
        {
            Characters = new List<Character>();
        }

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

        public void MoveCharacters(float width, float height)
        {
            foreach(Character c in Characters)
            {
                c.Move(width,height);
            }
        }
    }
}
