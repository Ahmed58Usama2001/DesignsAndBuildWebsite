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
            .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom<ImageUrlResolver>());

        #endregion

    }


}