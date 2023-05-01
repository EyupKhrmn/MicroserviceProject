using AutoMapper;
using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Services.Order.Domain.Model;

namespace FreeCourse.Services.Order.Application.Mapping;

public class CustomMap : Profile
{
    public CustomMap()
    {
        CreateMap<Domain.Model.Order, OrderDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        CreateMap<Address, AddressDto>().ReverseMap();
    }
}