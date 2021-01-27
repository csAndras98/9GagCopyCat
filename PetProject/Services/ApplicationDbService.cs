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
                _context.Add(fighter);
                _context.SaveChanges();
            }
        }
        public AppUser GetUser(string id)
        {
            return _context.Users.Find(id);
        }
    }
}
