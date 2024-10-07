using AutoMapper;
using Core.Contracts;
using Core.Models;

namespace Core.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Event, CreateEventRequest>();
        CreateMap<CreateEventRequest, Event>();
        CreateMap<Participant, CreateParticipantRequest>();
        CreateMap<CreateParticipantRequest, Participant>();
        
    }
}