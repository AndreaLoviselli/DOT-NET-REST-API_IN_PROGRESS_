using SamuraiApp.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiApp.Core.Service.Storage
{
    public interface ISamuraiStorageService
    {
        bool IsEmpty();
        ICollection<Samurai> GetSamurais();
        public Samurai? GetById(int samuraiId);
        bool Exists(int samuraiId);

        Samurai SaveSamurai(string name, int lifePoints);
        Samurai? SaveSamurai(int samuraiId, string newName, int newLifePoints);
        void DeleteSamurai(int idToDelete);
    }
}
