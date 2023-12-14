using SamuraiApp.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiApp.Core.Service.Storage
{
    public class InMemorySamuraiStorageService : ISamuraiStorageService
    {
        private ICollection<Samurai> _samuraiList;
        public InMemorySamuraiStorageService()
        {
            _samuraiList = new List<Samurai>();

            _samuraiList.Add(new Samurai(CalculateNextSamuraiId(), "Tony", 10));
            _samuraiList.Add(new Samurai(CalculateNextSamuraiId(), "Mario", 20));
        }

        public ICollection<Samurai> GetSamurais() => _samuraiList;
        public Samurai? GetById(int samuraiId) => FindSamurai(samuraiId);
        public bool IsEmpty() => _samuraiList.Count == 0;
        public bool Exists(int samuraiId) => FindSamurai(samuraiId) != null;    
                    
        public Samurai SaveSamurai(string name,int lifePoints)
        {
            var samuraiDaSalvare = new Samurai(CalculateNextSamuraiId(), name, lifePoints);
            _samuraiList.Add(samuraiDaSalvare);
            return samuraiDaSalvare; 
        }

        public Samurai? SaveSamurai(int samuraiId, string newName, int newLifePoints)
        {
            var samuraiToUpdate = FindSamurai(samuraiId);
            if(samuraiToUpdate != null)           
               {
                samuraiToUpdate.Name = newName;
                samuraiToUpdate.LifePoints = newLifePoints;
                 return samuraiToUpdate;
               }           
        return null;
        }

        public void DeleteSamurai(int idToDelete)
        {
            var samuraiToDelete = FindSamurai(idToDelete);
            if(samuraiToDelete != null) _samuraiList.Remove(samuraiToDelete);           
        }

        private int CalculateNextSamuraiId()
        {
            if (IsEmpty()) return 1;
            else return _samuraiList.Max(s => s.Id) + 1;                            
        }

        private Samurai? FindSamurai(int samuraiId) =>
            _samuraiList.FirstOrDefault(s => s.Id == samuraiId);

    }
}
