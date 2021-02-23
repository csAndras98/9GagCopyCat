using PetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetProject.Services
{
    public class ApplicationFightSimulationService
    {
        private readonly ApplicationDbService _dbService;
        private Random _random;
        public ApplicationFightSimulationService(ApplicationDbService dbService)
        {
            _dbService = dbService;
            _random = new Random();
        }

        public void SimulateFight(List<Opponent> opponents, List<Fighter> fighters) 
        {
            List<Character> characters = new List<Character>();
            characters.AddRange(opponents);
            characters.AddRange(fighters);
            characters.OrderBy(c => c.Initiative);

            while(opponents.Any(o => o.Health > 0) && fighters.Any(f => f.Health > 0))
            {
                foreach(Character character in characters.Where(c => c.Health > 0))
                {
                    if (fighters.Contains(character))
                    {
                        List<Opponent> targets = opponents.Where(o => o.Health > 0).ToList();
                        Attack(character, targets[_random.Next(0, targets.Count)]);
                    }
                    else
                    {
                        List<Fighter> targets = fighters.Where(f => f.Health > 0).ToList();
                        Attack(character, targets[_random.Next(0, targets.Count)]);
                    }
                }
                _dbService.HealthUpdate(fighters);
            }
        }

        private void Attack(Character attacker, Character deffender)
        {
            if(attacker.Accuracy > _random.Next(0, 100))
            {
                deffender.Health -= attacker.Power;
            }
        }
    }
}
