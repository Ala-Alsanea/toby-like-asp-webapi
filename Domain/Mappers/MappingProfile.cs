using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PostApi.Infrastructure.Pagination;
using Topy_like_asp_webapi.Api.Dtos.CollectionDto;
using Topy_like_asp_webapi.Api.Dtos.SpaceDto;
using Topy_like_asp_webapi.Api.Dtos.TabDto;
using Topy_like_asp_webapi.Domain.CQRS.Command;
using Topy_like_asp_webapi.Domain.Entities;

namespace Topy_like_asp_webapi.Domain.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // tab
            CreateMap<TabCreate, Tab>();
            CreateMap<Tab, TabRead>();
            
            // collection
            CreateMap<CollectionCreate, Collection>();
            CreateMap<CreateCollectionCommand, Collection>();
            
            CreateMap<Collection, CollectionRead>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User.Name))
            .ForMember(dest => dest.Space, opt => opt.MapFrom(src => src.Space.Title));

            CreateMap<PagedList<Collection>, CollectionPaged>()
                // .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ForPath(dest => dest.MetaData.Page, opt => opt.MapFrom(src => src.Page))
                .ForPath(dest => dest.MetaData.PageSize, opt => opt.MapFrom(src => src.PageSize))
                .ForPath(dest => dest.MetaData.TotalItems, opt => opt.MapFrom(src => src.TotalItems))
                .ForPath(dest => dest.MetaData.TotalPages, opt => opt.MapFrom(src => src.TotalPages))
                .ForPath(dest => dest.MetaData.HasNext, opt => opt.MapFrom(src => src.HasNext))
                .ForPath(dest => dest.MetaData.HasPrevious, opt => opt.MapFrom(src => src.HasPrevious));




            // space
            CreateMap<SpaceCreate, Space>();
            CreateMap<Space, SpaceRead>();

        }
    }
}