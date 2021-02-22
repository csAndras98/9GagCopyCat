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
        public ApplicationFightSimulationService(ApplicationDbService dbService)
        {
            _dbService = dbService;
        }

        public void SimulateFight(AppUser user) 
        {
            List<Fighter> fighters = _dbService.GetPartyMembers(user.Id);
            List<Opponent> opponents = _dbService.RandomOpponents(user.DungeonRank);
        }

    }
}
