// See https://aka.ms/new-console-template for more information
using Azure.Core;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Demo.CosmosDB_Final;
using Demo.CosmosDB_Final.Models.Accounts;
using Demo.CosmosDB_Final.Models.Accounts.Users.Coaches;
using Demo.CosmosDB_Final.Models.Accounts.Users.Players;
using Demo.CosmosDB_Final.Models.Teams;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var services = new ServiceCollection();
services.RegisterDependencies();

var provider = services.BuildServiceProvider();

var config = provider.GetService<IConfiguration>();
var cosmosClient = provider.GetService<CosmosClient>();

var accountContainer = cosmosClient.GetContainer(config.GetValue<string>("CosmosDb:DefaultDatabaseName"), "Accounts");
var teamContainer = cosmosClient.GetContainer(config.GetValue<string>("CosmosDb:DefaultDatabaseName"), "Teams");

// create the account
var account = new Account()
{
    Name = "San Pasqual Eagles"
};

var response = await accountContainer.CreateItemAsync(account);


List<Coach> coaches = [
    new Coach(){ FirstName = "Heather", LastName = "Calvin", DisplayName = "Coach Calvin", Email = "example@testing.com", YearsExperience = 19, PhoneNumber = "5558880000"},
    new Coach(){ FirstName = "Jane", LastName = "Doe", DisplayName = "Coach Doe", Email = "example2@testing.com", YearsExperience = 5, PhoneNumber = "5558880000"},
    new Coach(){ FirstName = "Jeff", LastName = "Smith", DisplayName = "Coach Smith", Email = "example3@testing.com", YearsExperience = 5, PhoneNumber = "5558880000"},
];

foreach(var coach in coaches)
{
    coach.AccountId = account.Id;
    await accountContainer.CreateItemAsync(coach);
}

List<Player> players = [
    new Player() { FirstName = "Brian", LastName = "Urlacher",  Email = "example@testing.com", PhoneNumber = "5558880000", Position = "Goalie"},
    new Player() { FirstName = "Walter", LastName = "Payton",  Email = "example@testing.com", PhoneNumber = "5558880000", Position = "Forward"},
    new Player() { FirstName = "Devin", LastName = "Hester",  Email = "example@testing.com", PhoneNumber = "5558880000", Position = "Wing"},
    new Player() { FirstName = "Lance", LastName = "Briggs",  Email = "example@testing.com", PhoneNumber = "5558880000", Position = "Hole Set"},
    new Player() { FirstName = "Jay", LastName = "Cutler",  Email = "example@testing.com", PhoneNumber = "5558880000", Position = "Driver"},
];

var tasks = new List<Task>();
foreach (var player in players)
{
    player.AccountId = account.Id;
    tasks.Add(accountContainer.CreateItemAsync(player));
}

await Task.WhenAll(tasks);
tasks.Clear();


List<Team> teams = [
    new Team(){ Name = "2025 Womens Varsity", TeamLevel = TeamLevel.Varsity, Season = Season.Womens},
    new Team(){ Name = "2025 Womens JV", TeamLevel = TeamLevel.JV, Season = Season.Womens},
    new Team(){ Name = "2025 Mens Varsity", TeamLevel = TeamLevel.Varsity, Season = Season.Mens},
    new Team(){ Name = "2025 Mens JV", TeamLevel = TeamLevel.JV, Season = Season.Mens},
    new Team(){ Name = "2025 Mens Freshman", TeamLevel = TeamLevel.Novice, Season = Season.Mens}
];

/// Example of embedding data 
var mensVarsity = teams.First(x => x.Season == Season.Mens && x.TeamLevel == TeamLevel.Varsity);
for(var i = 0; i < players.Count; i++)
{
    var player = players[i];
    var assignment = new PlayerAssignment()
    {
        PlayerNumber = (i + 1).ToString(),
        TeamOverview = new TeamOverview() { Name = mensVarsity.Name, Id = mensVarsity.Id }
    };

    player.TeamAssignments.Add(assignment);

    // update the player with the new team assignment
    tasks.Add(accountContainer.UpsertItemAsync(player));
}

await Task.WhenAll(tasks);




// Example of Referencing
tasks.Clear();
foreach(var team in teams)
{
    for(var i = 0; i < coaches.Count; i++)
    {
        var coach = coaches[i];
        var assignment = new CoachAssignment()
        {
            CoachId = coach.Id, // references the coach from the Accounts collection
            Level = (CoachLevel)i,
            TeamOverview = new TeamOverview() { Name = team.Name, Id = team.Id }, 
            TeamLevel = team.TeamLevel
        };

        tasks.Add(teamContainer.CreateItemAsync(assignment));
    }
}

await Task.WhenAll(tasks);
