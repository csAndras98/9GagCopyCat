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
        public List<Fighter> GetAllFighters()
        {
            return _context.Fighters.ToList();
        }
        public Fighter GetFighter(int id)
        {
            return _context.Fighters.Find(id);
        }
        public List<Fighter> GetMyFighters(string userId)
        {
            return _context.Fighters.Where(f => f.UserId.Equals(userId)).ToList();
        }

        public void BuyFighter(Fighter fighter, AppUser user)
        {
            _context.Add(new Fighter() 
            {
                Image = fighter.Image,
                Name = fighter.Name,
                Price = fighter.Price,
                Level = fighter.Level,
                Health = fighter.Health,
                MaxHealth = fighter.MaxHealth,
                Accuracy = fighter.Accuracy,
                Power = fighter.Power,
                Initiative = fighter.Initiative,
                InParty = fighter.InParty,
                UserId = user.Id
            }
            );
            _context.Users.Find(user.Id).Funds -= fighter.Price;
            _context.SaveChanges();
        }
        public void Loot(int amount, string userId)
        {
            _context.Users.Find(userId).Funds += amount;
            _context.SaveChanges();
        }

        public List<Fighter> GetPartyMembers(string userId)
        {
            return _context.Fighters.Where(f => f.UserId.Equals(userId) && f.InParty == true).ToList();
        }

        public void ChangePartyMember(int id)
        {
            _context.Fighters.Find(id).InParty = !_context.Fighters.Find(id).InParty;
            _context.SaveChanges();
        }

        public List<Fighter> RandomFighters()
        {
            Random Random = new Random();

            List<Fighter> fighters = new List<Fighter>();
            for (int i = 0; i < 6; i++)
            {
                int HP = Random.Next(60, 101);
                fighters.Add(new Fighter()
                {
                    MaxHealth = HP,
                    Health = HP,
                    Accuracy = Random.Next(40, 91),
                    Initiative = Random.Next(1, 4),
                    Power = Random.Next(10, 21),
                    Level = Random.Next(1, 4),
                    Price = Random.Next(0, 5),
                    Name = _context.Names.Find(Random.Next(1, 4)).CharName,
                    Image = _context.Portraits.Find(Random.Next(15, 17)).Image,
                    InParty = false
                });
            }
            return fighters;
        }

        public List<Opponent> RandomOpponents(int level)
        {
            Random Random = new Random();

            Opponent[] possibleOpponents = _context.Opponents.Where(o => o.Level <= level).ToArray();
            List<Opponent> opponents = new List<Opponent>();
            for (int i = 0; i < Random.Next(1, 4); i++)
            {
                Opponent opponent = possibleOpponents[Random.Next(0, possibleOpponents.Length)];
                Opponent opponent1 = new Opponent()
                {
                    Id = opponent.Id,
                    Health = opponent.Health,
                    MaxHealth = opponent.MaxHealth,
                    Accuracy = opponent.Accuracy,
                    Image = opponent.Image,
                    Level = opponent.Level,
                    Initiative = opponent.Initiative,
                    Name = opponent.Name,
                    Power = opponent.Power,
                    Loot = opponent.Loot
                };
                opponents.Add(opponent1);
            }
            return opponents;
        }

        public void DeleteFighter(Fighter fighter)
        {
            _context.Fighters.Remove(fighter);
            _context.SaveChanges();
        }

        public AppUser GetUser(string id)
        {
            return _context.Users.Find(id);
        }

        public AppUser GetUserByName(string name)
        {
            return _context.Users.First(u => u.UserName.Equals(name));
        }

        public void ChangeDungeonRank(string id,int rank)
        {
            _context.Users.Find(id).DungeonRank = rank;
            _context.SaveChanges();
        }
        public void ChangeDungeonBest(string id, int rank)
        {
            _context.Users.Find(id).Best = rank;
            _context.SaveChanges();
        }
    }
}
