using AutoMapper;
using CSP.ViewModels;
using CSP.Models;

namespace CSP.Profiles
{
    public class CSPProfile : Profile
    {
        public CSPProfile()
        {
                    CreateMap<Request, GetRequest>();
                    CreateMap<GetRequest, Request>();
            CreateMap<Organization, ReadOrganizations>();
            CreateMap<ReadOrganizations, Organization>();
            CreateMap<Service, ReadServices>();
            CreateMap<ReadServices, Service>();
            CreateMap<Service, CreateServices>();
            CreateMap<CreateServices, Service>();
                     CreateMap<Request, CreateRequest>();
                    CreateMap<CreateRequest, Request>();
                     CreateMap<Ticket, CreateTicket>();
                    CreateMap<CreateTicket, Ticket>();
                    CreateMap<GetTicket, Ticket>();
                    CreateMap<Ticket, GetTicket>();
                    CreateMap<GetServices, Service>();
                    CreateMap<Service, GetServices>();
                          CreateMap<CreateUserAccount, User>();
                    CreateMap<User, CreateUserAccount>();
        }
    }
}