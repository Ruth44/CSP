using AutoMapper;
using CSP.ViewModels;
using CSP.Models;

namespace CSP.Profiles
{
    public class CSPProfile : Profile
    {
        public CSPProfile()
        {
//                     CreateMap<Song, MusicCreate>();
//                     CreateMap<MusicCreate, Song>();
CreateMap<Organization, ReadOrganizations>();
 CreateMap<ReadOrganizations, Organization>();
 CreateMap<Service, ReadServices>();
 CreateMap<ReadServices, Service>();
//                      CreateMap<Song, ReadSongs>();
//                     CreateMap<ReadSongs, Song>();
//                      CreateMap<Song, ReadAlbums>();
//                     CreateMap<ReadAlbums, Song>();
//                      CreateMap<Song, ReadArtists>();
//                     CreateMap<ReadArtists, Song>();
//                     // CreateMap<CommandUpdateDto, Command>();
//                     // CreateMap<Command, CommandUpdateDto>();
        }
    }
}