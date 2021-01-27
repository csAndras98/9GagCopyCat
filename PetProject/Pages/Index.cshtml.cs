using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PetProject.Models;
using PetProject.Services;

namespace PetProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public ApplicationDbService DbService;
        public IEnumerable<Fighter> Fighters { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbService productService)
        {
            _logger = logger;
            DbService = productService;
        }

        public void OnGet()
        {
            
        }

    }
}
