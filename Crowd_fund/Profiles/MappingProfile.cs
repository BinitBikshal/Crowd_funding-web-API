using AutoMapper;
using Domain.DTOs;
using Domain.Entities;

namespace Crowd_fund.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CampaignRequestDto, Campaign>();
            CreateMap<Campaign, CampaignResponsetDto>();

            CreateMap<DonationRequestDto, Donation>();
            CreateMap<Donation, DonationResponseDto>();

            CreateMap<CommentRequestDto, Comment>();
            CreateMap<Comment, CommentResponseDto>();

        }
    }
}
