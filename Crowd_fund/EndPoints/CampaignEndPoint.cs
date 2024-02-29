
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using Repository;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace Crowd_fund.EndPoints
{
    public class CampaignEndPoint : BaseEndPoint
    {
        private readonly IMapper _mapper;
        
        public CampaignEndPoint(IMapper mapper)
        {
            url = baseUrl + "campaigns";
            _mapper = mapper;
        }

        public override void MapEndPoints(WebApplication app)
        {
            app.MapGet(url, Getall).Produces(StatusCodes.Status200OK);
            app.MapPost(url, Save).Produces(StatusCodes.Status200OK);
            app.MapGet($"{url}/{{id}}", GetById).Produces(StatusCodes.Status200OK);
        }

        private async Task<IResult> GetById(Guid id,IRepository<Campaign> repository)
        {
            var res = await repository.GetSingleAsync(id);
            if (res is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(res);
        }

        [Authorize]
        private async Task<IResult> Save(ClaimsPrincipal user, IRepository<Campaign> repository,
            UserManager<ApplicationUser> userManager, CampaignRequestDto campaignRequest)
        {
            var loggedInUser = await userManager.GetUserAsync(user);
            var entity = _mapper.Map<Campaign>(campaignRequest);
            entity.CreatedAt = DateTime.Now;
            entity.UpdateAt = DateTime.Now;
            entity.UserId = loggedInUser!.Id;

            var res = await repository.CreateAsync(entity);

            return Results.Created($"/campaigns/{res.Id}", _mapper.Map<Campaign, CampaignResponsetDto>(res));
        }

        [Authorize]
        private async Task<IResult> Getall(IRepository<Campaign> repository)
        {
            var res = await repository.GetAllAsync();
            return Results.Ok(res);
        }

    }
}
