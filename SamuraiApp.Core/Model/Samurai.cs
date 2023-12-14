using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiApp.Core.Model
{
    public class Samurai
    {   
        public int Id { get; }

        public string Name { get; set; }

        private int _lifePoints;
        public int LifePoints 
        {
            get { return _lifePoints; }
            set 
            {
                if (value <= 0) throw new Exception("I punti vita non possono essere minori o uguali a zero!");
                else _lifePoints = value;
            }
        }

        public Samurai(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public Samurai(int id, string name, int lifePoints) : this(id, name)
        {
            LifePoints = lifePoints;
        }
        
    }
}
