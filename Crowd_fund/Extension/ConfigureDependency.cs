using Crowd_fund.EndPoints;
using Repository;
using System.Runtime.CompilerServices;
using static Crowd_fund.EndPoints.CommentEndPoint;

namespace Crowd_fund.Extension
{
    public static class ConfigureDependency
    {
        public static void ConfigureDependencies(this IServiceCollection services)
        {
            services.AddScoped<BaseEndPoint, CampaignEndPoint>();
            services.AddScoped<BaseEndPoint, UserEndPoint>();
            services.AddScoped<BaseEndPoint, DonationEndPoint>();
            services.AddScoped<BaseEndPoint, CommentEndpoint>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
