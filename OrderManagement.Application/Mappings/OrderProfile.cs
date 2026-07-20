using AutoMapper;

using OrderManagement.Application.DTOs;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Mappings;

public class OrderProfile : Profile
{

    public OrderProfile()
    {

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(
                dest => dest.Total,
                opt => opt.MapFrom(
                    src => src.Quantity * src.UnitPrice));



        CreateMap<Order, OrderDto>()
            .ForMember(
                dest => dest.Total,
                opt => opt.MapFrom(
                    src => src.Items.Sum(
                        x => x.Quantity * x.UnitPrice)));

    }

}