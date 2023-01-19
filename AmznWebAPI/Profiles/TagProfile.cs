using AmznMetaLibrary.Models.DTOs;
using AmznMetaLibrary.Models.OpinionModels;
using AutoMapper;
using BenchmarkDotNet.Disassemblers;

namespace AmznWebAPI.Profiles
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<CreateBadOpDTO, Badop>();
            CreateMap<CreateGoodOpDTO, GoodOp>();
        }
    }
}
