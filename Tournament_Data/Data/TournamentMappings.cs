using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Tournament_Core.Dto;
using Tournament_Core.Entities;

namespace Tournament_Data.Data
{
    public class TournamentMappings : Profile
    {
        public TournamentMappings()
        {
            CreateMap<Tournament, TournamentDto>();
            CreateMap<Game, GameDto>();
        }
    }
}
