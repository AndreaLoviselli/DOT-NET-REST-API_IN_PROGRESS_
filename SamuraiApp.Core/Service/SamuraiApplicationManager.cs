using SamuraiApp.Core.Model;
using SamuraiApp.Core.Model.Errors;
using SamuraiApp.Core.Service.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiApp.Core.Service
{
    public class SamuraiApplicationManager
    {
        private ISamuraiStorageService _storageService;

        public SamuraiApplicationManager(ISamuraiStorageService storageService)
        {
            _storageService = storageService;
        }
        public ICollection<Samurai> GetSamurais() => _storageService.GetSamurais();

        public Samurai? GetById(int samuraiId) =>
            _storageService.GetById(samuraiId);

        public Samurai CreateSamurai(string name, int lifePoints)
        {
            if (lifePoints <= 0)
            {
                throw new NegativeLifePointsException();
            }
                return _storageService.SaveSamurai(name, lifePoints);
        }
        public Samurai? SaveSamurai(int id, string name, int lifePoints) => _storageService.SaveSamurai(id, name, lifePoints);
    
        public void DeleteSamurai(int id) => _storageService.DeleteSamurai(id);

        public bool Exists(int id) => _storageService.Exists(id);
    }
}
