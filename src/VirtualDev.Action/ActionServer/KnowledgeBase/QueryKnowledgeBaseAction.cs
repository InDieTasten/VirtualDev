using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualDev.Action.ActionServer.Models;
using VirtualDev.Database.Models;

namespace VirtualDev.Action.ActionServer.KnowledgeBase
{
    [RasaAction("action_query_knowledge_base")]
    public class QueryKnowledgeBaseAction : IAsyncActionHandler
    {
        private readonly IMongoDatabase mongoDatabase;

        public QueryKnowledgeBaseAction(IMongoDatabase mongoDatabase)
        {
            this.mongoDatabase = mongoDatabase;
        }

        public async Task<RasaActionOutput> HandleAsync(RasaActionInput actionRequest)
        {
            RasaEvent lastUserMessage = actionRequest.Tracker.Events.LastOrDefault(e => e.Type == "user");
            string entityType = lastUserMessage.ParseData.Entities
                .OrderByDescending(entity => entity.Confidence)
                .FirstOrDefault(entity => entity.Name == "EntityType")?.Value;

            IEnumerable<string> results = new List<string>();

            if (entityType == "projects")
            {
                var collection = mongoDatabase.GetCollection<Project>(entityType);
                var cursor = await collection.FindAsync(project => true);
                var projects = await cursor.ToListAsync();
                results = projects.Select(project => project.Name);
            }

            return new RasaActionOutput
            {
                Responses = results.Select((result, index) =>
                    new TextResponse
                    {
                        Text = $"[{index+1}] {entityType}: {result}"
                    })
            };
        }
    }
}
