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

        public List<string> SetUpFight(List<Opponent> opponents, List<Fighter> fighters)
        {
            List<Character> characters = new List<Character>();
            characters.AddRange(opponents);
            characters.AddRange(fighters);
            characters.OrderBy(c => c.Initiative);
            return SimulateFight(characters, opponents, fighters, new List<string>());
        }

        public List<string> SimulateFight(List<Character> characters, List<Opponent> opponents, List<Fighter> fighters, List<string> combatLog) 
        {
            foreach(Character  character in characters)
            {
                if (fighters.Contains(character))
                {
                    Opponent target = opponents[_random.Next(0, opponents.Count)];
                    Attack(character, target, combatLog);
                    if(target.Health <= 0)
                    {
                        opponents.Remove(target);
                        if(opponents.Count == 0)
                        {
                            return combatLog;
                        }
                    }
                }
                if (opponents.Contains(character))
                {
                    Fighter target = fighters[_random.Next(0, fighters.Count)];
                    Attack(character, target, combatLog);
                    if (target.Health <= 0)
                    {
                        fighters.Remove(target);
                        _dbService.DeleteFighter(target);
                        if (fighters.Count == 0)
                        {
                            return combatLog;
                        }
                    }
                }
            }
            return SimulateFight(characters, opponents, fighters, combatLog);
        }

        private void Attack(Character attacker, Character deffender, List<string> combatLog)
        {
            if(attacker.Accuracy > _random.Next(0, 100))
            {
                deffender.Health -= attacker.Power;
                combatLog.Add($"{attacker.Name} dealt {attacker.Power} damage to {deffender.Name}.");
                if(deffender.Health <= 0)
                {
                    combatLog.Add($"{deffender.Name} has died.");
                }
             }
            else
            {
                combatLog.Add($"{attacker.Name} has missed {deffender.Name}.");
            }
        }
    }
}
