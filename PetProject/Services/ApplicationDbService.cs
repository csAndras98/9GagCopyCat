using Microsoft.AspNetCore.Identity;
using PetProject.Data;
using PetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetProject.Services
{
    public class ApplicationDbService
    {
        private readonly ApplicationDbContext _context;
        public ApplicationDbService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Fighter> GetAllFighters()
        {
            return _context.Fighters;
        }
        public Fighter GetFighter(int id)
        {
            return _context.Fighters.Find(id);
        }
        public IEnumerable<Fighter> GetMyFighters(string userId)
        {
            return _context.Fighters.Where(f => f.UserId.Equals(userId));
        }

        public void BuyFighter(Fighter fighter, AppUser user)
        {
            if(user.Funds >= fighter.Price)
            {
                fighter.User = user;
                fighter.UserId = user.Id;
                _context.Add(fighter);
                _context.SaveChanges();
            }
        }

        public Fighter[] RandomFighters()
        {
            Random Random = new Random();

            Fighter[] fighters = new Fighter[6];
            for (int i = 0; i < fighters.Length; i++)
            {
                fighters[i] = new Fighter()
                {
                    Health = Random.Next(60, 101),
                    Morale = 100,
                    Accuracy = Random.Next(40, 91),
                    Initiative = Random.Next(1, 4),
                    Power = Random.Next(10, 21),
                    Level = Random.Next(1, 4),
                    Name = _context.Names.Find(Random.Next(1, 4)).CharName,
                    Image = _context.Portraits.Find(Random.Next(1, 4)).Image
                };
            }
            return fighters;
        }

        public AppUser GetUser(string id)
        {
            return _context.Users.Find(id);
        }
    }
}
