using AutoMapper;
using form.Core;
using form.Dto;

namespace form
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            CreateMap<EmployerForm, EmployerFormDto>()
                     .ForMember(dest => dest.PersonalInformation, opt => opt.MapFrom(src => src.PersonalInformation))
                     .ForMember(dest => dest.CustomQuestions, opt => opt.MapFrom(src => src.CustomQuestions))
                     .ForMember(dest => dest.AdditionalQuestions, opt => opt.MapFrom(src => src.AdditionalQuestions));

            CreateMap<PersonalInformation, PersonalInformationDto>();
            CreateMap<CustomQuestion, CustomQuestionDtos>();

            CreateMap<ClientForm, ClientFormDto>()
                    .ForMember(dest => dest.majorquestions, opt => opt.MapFrom(src => src.majorquestions))
                    .ForMember(dest => dest.AdditionalQuestion, opt => opt.MapFrom(src => src.AdditionalQuestion));
                    
        }
    }
}
