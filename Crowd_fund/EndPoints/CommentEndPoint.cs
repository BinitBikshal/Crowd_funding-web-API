using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Repository;
using System.Security.Claims;

namespace Crowd_fund.EndPoints
{
    public class CommentEndPoint
    {
        public class CommentEndpoint : BaseEndPoint
        {
            private readonly IMapper _mapper;

            public CommentEndpoint(IMapper mapper)
            {
                url = baseUrl + "comments";
                _mapper = mapper;
            }

            public override void MapEndPoints(WebApplication app)
            {
                app.MapGet(url, Getall).Produces(StatusCodes.Status200OK);
                app.MapPost(url, Save).Produces(StatusCodes.Status200OK);
                app.MapGet($"{url}/{{id}}", GetById).Produces(StatusCodes.Status200OK);
            }

            private async Task<IResult> GetById(Guid id, IRepository<Comment> repository)
            {
                var res = await repository.GetSingleAsync(id);
                if (res is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(res);
            }

            [Authorize]
            private async Task<IResult> Save(ClaimsPrincipal user, IRepository<Comment> repository,
                UserManager<ApplicationUser> userManager, CommentRequestDto campaignRequest)
            {
                var loggedInUser = await userManager.GetUserAsync(user);
                var entity = _mapper.Map<Comment>(campaignRequest);
                entity.CreatedAt = DateTime.Now;
                entity.UpdateAt = DateTime.Now;
                entity.UserId = loggedInUser!.Id;

                var res = await repository.CreateAsync(entity);

                return Results.Created($"/comments/{res.Id}", _mapper.Map<Comment, CommentResponseDto>(res));
            }
            [Authorize]
            private async Task<IResult> Getall(IRepository<Comment> repository)
            {
                var res = await repository.GetAllAsync();
                return Results.Ok(res);
            }
        }
    }
}
