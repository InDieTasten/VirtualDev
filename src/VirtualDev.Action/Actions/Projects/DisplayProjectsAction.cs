using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;
using VirtualDev.Action.ActionServer;
using VirtualDev.Action.ActionServer.Models;
using VirtualDev.Database.Models;

namespace VirtualDev.Action.Actions.Projects
{
    [RasaAction("action_display_projects")]
    public class DisplayProjectsAction : IAsyncActionHandler
    {
        private readonly IMongoDatabase mongoDatabase;

        public DisplayProjectsAction(IMongoDatabase mongoDatabase)
        {
            this.mongoDatabase = mongoDatabase;
        }

        public async Task<RasaActionOutput> HandleAsync(RasaActionInput actionRequest)
        {
            var projectsCollection = mongoDatabase.GetCollection<Project>("projects");
            var cursor = await projectsCollection.FindAsync(project => true);
            var projects = await cursor.ToListAsync();

            return new RasaActionOutput
            {
                Responses = projects.Select(project => new TextResponse
                {
                    Text = $"- {project.Name}"
                })
            };
        }
    }
}
