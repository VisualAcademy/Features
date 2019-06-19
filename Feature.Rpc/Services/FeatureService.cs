using System.Linq;
using System.Threading.Tasks;
using Feature.BlazorServer.Data;
using FeatureApp;
using Grpc.Core;

namespace Feature.Rpc
{
    public class FeatureService : FeatureType.FeatureTypeBase
    {
        public override Task<FeatureReply> GetAll(FeatureRequest request, ServerCallContext context)
        {
            //[1] 인-메모리 
            //FeatureReply reply = new FeatureReply();
            //reply.Features.Add(new FeatureEntity { Id = 1, Title = "ASP.NET", Created = "1234" });
            //reply.Features.Add(new FeatureEntity { Id = 2, Title = "Blazor", Created = "1234" });
            //reply.Features.Add(new FeatureEntity { Id = 3, Title = "C#", Created = "1234" });

            //[2] 실제 데이터베이스 
            FeatureReply reply = new FeatureReply();
            using (var ctx = new ApplicationDbContext())
            {
                var features = ctx.Features.OrderBy(f => f.Id).ToList();
                foreach (var f in features)
                {
                    reply.Features.Add(new FeatureEntity { Id = f.Id, Title = f.Title, Created = f.Created.ToString() });
                }
            }

            return Task.FromResult(reply);
        }
    }
}
