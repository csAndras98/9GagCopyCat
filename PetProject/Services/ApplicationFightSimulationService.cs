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
                        Opponent target = opponents[_random.Next(0, opponents.Count)];
                        Attack(character, target, combatLog);
                        if(target.Health <= 0)
                        {
                            opponents.Remove(target);
                        }
                    }
                    else if(opponents.Contains(character))
                    {
                        Fighter target = fighters[_random.Next(0, fighters.Count)];
                        Attack(character, target, combatLog);
                        if(target.Health <= 0)
                        {
                            fighters.Remove(target);
                            _dbService.DeleteFighter(target);
                        }
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
             }
            else
            {
                combatLog.Append($"{attacker} has missed {deffender}.\n");
            }
        }
    }
}
