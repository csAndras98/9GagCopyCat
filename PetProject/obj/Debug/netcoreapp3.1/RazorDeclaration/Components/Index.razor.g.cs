#pragma checksum "C:\Users\tompu\OneDrive\Desktop\c#\PetProject\PetProject\Components\Index.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4abbc517f1725de89fa5c7ea8b2d3aa2fe7306d0"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace PetProject.Components
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\tompu\OneDrive\Desktop\c#\PetProject\PetProject\Components\Index.razor"
using PetProject.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\tompu\OneDrive\Desktop\c#\PetProject\PetProject\Components\Index.razor"
using PetProject.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\tompu\OneDrive\Desktop\c#\PetProject\PetProject\Components\Index.razor"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\tompu\OneDrive\Desktop\c#\PetProject\PetProject\Components\Index.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\tompu\OneDrive\Desktop\c#\PetProject\PetProject\Components\Index.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
    public partial class Index : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 101 "C:\Users\tompu\OneDrive\Desktop\c#\PetProject\PetProject\Components\Index.razor"
       
    protected AppUser User { get; set; }
    protected List<Fighter> Fighters { get; set; }
    protected List<Fighter> Party { get; set; }
    protected List<Fighter> NewFighters { get; set; }
    protected List<Opponent> Opponents { get; set; }
    protected List<string> Fightlog { get; set; }
    protected int Loot { get; set; }

    protected void Refresh()
    {
        Party = DbService.GetPartyMembers(User.Id);
    }

    protected void ChangeParty(Fighter fighter)
    {
        if (Party.Count() < 4 && !Party.Contains(fighter) && User.DungeonRank == 0)
        {
            DbService.ChangePartyMember(fighter.Id);
        }
        else if (Party.Contains(fighter) && User.DungeonRank == 0)
        {
            DbService.ChangePartyMember(fighter.Id);
        }
        Refresh();
    }

    protected void Reroll()
    {
        NewFighters = DbService.RandomFighters();
    }

    protected void Fight()
    {
        foreach(Fighter fighter in Fighters.Where(f => !f.InParty))
        {
            fighter.Health = fighter.MaxHealth;
        }
        DbService.ChangeDungeonRank(User.Id, User.DungeonRank + 1);
        if (User.DungeonRank > User.Best)
        {
            DbService.ChangeDungeonBest(User.Id, User.DungeonRank);
        }
        Opponents = DbService.RandomOpponents(User.DungeonRank);
        if(User.DungeonRank == 1)
        {
            Loot = Opponents.Select(o => o.Loot).Sum();
        }
        else
        {
            Loot += Opponents.Select(o => o.Loot).Sum();
        }
        Fightlog = FightSimulationService.SetUpFight(new List<Opponent>(Opponents), Party);
    }

    protected void Retreat()
    {
        DbService.ChangeDungeonRank(User.Id, 0);
        Opponents.Clear();
        Fightlog.Clear();
        if(Party.Count > 0)
        {
            DbService.Loot(Loot, User.Id);
        }
    }

    protected void Buy(Fighter fighter)
    {
        if (fighter.Price <= User.Funds)
        {
            DbService.BuyFighter(fighter, User);
            NewFighters.Remove(fighter);
            Fighters = DbService.GetMyFighters(User.Id);
        }
    }

    protected override async Task OnInitializedAsync()
    {
        Fightlog = new List<string>();
        Opponents = new List<Opponent>();
        NewFighters = DbService.RandomFighters();
        User = DbService.GetUserByName(SignInManager.Context.User.Identity.Name);
        Fighters = DbService.GetMyFighters(User.Id);
        Party = DbService.GetPartyMembers(User.Id);
        DbService.ChangeDungeonRank(User.Id, 0);
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private SignInManager<AppUser> SignInManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ApplicationFightSimulationService FightSimulationService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ApplicationDbService DbService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private UserManager<AppUser> UserManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    }
}
#pragma warning restore 1591
