using Crowd_fund.EndPoints;

namespace Crowd_fund.Extension
{
    public class MapEndPointRouting
    {
        public static void MapEndPoints(WebApplication app)
        {
            using(var scope= app.Services.CreateScope())
            {
                var services = scope.ServiceProvider.GetServices<BaseEndPoint>();
                foreach(var service in services)
                {
                    service.MapEndPoints(app);
                }
            }
        }
    }
}
