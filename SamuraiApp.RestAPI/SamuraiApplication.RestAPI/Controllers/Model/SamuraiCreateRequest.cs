namespace SamuraiApplication.RestAPI.Controllers.Model
{
    public class SamuraiCreateRequest
    {

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
    }
}

    

