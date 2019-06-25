using System.Linq;
using System.Threading.Tasks;
using Feature.Models;
using Grpc.Core;

namespace Feature.RpcService
{
    public class FeatureServer : FeatureService.FeatureServiceBase
    {
        private readonly FeatureContext dbContext;

        public FeatureServer(FeatureContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public override Task<FeatureReply> GetAll(FeatureRequest request, ServerCallContext context)
        {
            FeatureReply reply = new FeatureReply();
            var features = dbContext.Features.OrderBy(f => f.Id).ToList();
            foreach (var f in features)
            {
                reply.Features.Add(new FeatureEntity { Id = f.Id, Title = f.Title, Created = f.Created.ToString() });
            }

            return Task.FromResult(reply);
        }
    }
}
