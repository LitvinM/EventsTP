using Application.Contracts;
using AutoMapper;
using Core.Models;
using DataAccess.Entities;

namespace Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Event, CreateEventRequest>();
        CreateMap<CreateEventRequest, Event>();
        CreateMap<Participant, CreateParticipantRequest>();
        CreateMap<CreateParticipantRequest, Participant>();
        
        CreateMap<ParticipantEntity, Participant>();
        CreateMap<Participant, ParticipantEntity>();
        
        CreateMap<Event, EventEntity>()
            .ForMember(dest => dest.Participants, opt => opt.MapFrom(src => src.Participants));
        CreateMap<EventEntity, Event>()
            .ForMember(dest => dest.Participants, opt => opt.MapFrom(src => src.Participants));
    }
}