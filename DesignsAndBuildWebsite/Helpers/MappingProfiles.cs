namespace DesignsAndBuild.APIs.Helpers;

public class MappingProfiles : Profile
{
        public MappingProfiles()
        {
            
            CreateMap<UserMessage, UserMessageDto>().ReverseMap();


        #region OurProject
        CreateMap<OurProjectCreateDto, OurProject>()
            .ForMember(dest => dest.VideoUrl, opt => opt.Ignore())
            .ForMember(dest => dest.Images, opt => opt.Ignore());

        CreateMap<OurProject, OurProjectReturnDto>()
            .ForMember(dest => dest.VideoUrl, opt => opt.MapFrom<VideoUrlResolver>())
            .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom<ImageUrlResolver>())
            .ForMember(dest => dest.DurationInDays, opt => opt.MapFrom<DurationInDaysResolver>());

        #endregion

    }


}

public class DurationInDaysResolver : IValueResolver<OurProject, OurProjectReturnDto, int?>
{
    public int? Resolve(OurProject source, OurProjectReturnDto destination, int? destMember, ResolutionContext context)
    {
        if (source.StartDate.HasValue && source.EndDate.HasValue)
        {
            TimeSpan difference = source.EndDate.Value.Date - source.StartDate.Value.Date;
            return difference.Days;
        }
        else
        {
            return 0; // or 0 if you prefer
        }
    }
}