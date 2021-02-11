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
    }
}
