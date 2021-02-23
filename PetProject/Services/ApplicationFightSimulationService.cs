using PetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public string SimulateFight(List<Opponent> opponents, List<Fighter> fighters) 
        {
            List<Character> characters = new List<Character>();
            characters.AddRange(opponents);
            characters.AddRange(fighters);
            characters.OrderBy(c => c.Initiative);

            StringBuilder combatLog = new StringBuilder();

            while(opponents.Any(o => o.Health > 0) && fighters.Any(f => f.Health > 0))
            {
                foreach(Character character in characters.Where(c => c.Health > 0))
                {
                    if (fighters.Contains(character))
                    {
                        List<Opponent> targets = opponents.Where(o => o.Health > 0).ToList();
                        Attack(character, targets[_random.Next(0, targets.Count)], combatLog);
                    }
                    else
                    {
                        List<Fighter> targets = fighters.Where(f => f.Health > 0).ToList();
                        Attack(character, targets[_random.Next(0, targets.Count)], combatLog);
                    }
                }
            }
            _dbService.HealthUpdate(fighters);
            return combatLog.ToString();
        }

        private void Attack(Character attacker, Character deffender, StringBuilder combatLog)
        {
            if(attacker.Accuracy > _random.Next(0, 100))
            {
                deffender.Health -= attacker.Power;
                combatLog.Append($"{attacker} dealt {attacker.Power} damage to {deffender}.\n");
                if(deffender.Health <= 0)
                {
                    combatLog.Append($"{deffender} Has died.\n");
                }
                else
                {
                    combatLog.Append($"{attacker} has missed {deffender}.\n");
                }
            }
        }
    }
}
