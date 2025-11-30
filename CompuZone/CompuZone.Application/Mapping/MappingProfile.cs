using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CompuZone.Application.Features.Commands;
using CompuZone.Application.Features.Commands.CategoryCommands;
using CompuZone.Application.Features.Commands.CustomerCommands;
using CompuZone.Application.Features.Commands.ProductCommands;
using CompuZone.Application.Features.Dtos.Responses.CategoryResponses;
using CompuZone.Application.Features.Dtos.Responses.CustomerResponses;
using CompuZone.Application.Features.Dtos.Responses.ProductResponses;
using CompuZone.Application.Features.Dtos.Responses.StockResponses;
using CompuZone.Domain.Entities;
using CompUZone.Models;

namespace CompuZone.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductCategory, CategoryReadReponseDto>().ReverseMap();
            CreateMap<ProductCategory, CategoryAddCommand>().ReverseMap();
            CreateMap<ProductCategory, CategoryUpdateCommand>().ReverseMap();

            CreateMap<ProductCatalog, ProductReadReponseDto>().ReverseMap();
            CreateMap<ProductCatalog, ProductAddCommand>().ReverseMap();
            CreateMap<ProductCatalog, ProductUpdateCommand>().ReverseMap();


            CreateMap<Customer, CustomerReadReponseDto>().ReverseMap();
            CreateMap<Customer, CustomerAddCommand>().ReverseMap();
            CreateMap<Customer, CustomerUpdateCommand>().ReverseMap();
        }
    }
}
