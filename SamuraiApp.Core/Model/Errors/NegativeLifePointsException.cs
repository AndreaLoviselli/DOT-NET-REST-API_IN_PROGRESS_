using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiApp.Core.Model.Errors
{
    public class NegativeLifePointsException : Exception
    {
        public override string Message => "Errore, i punti vita non possono essere <= 0!";
    }

}
