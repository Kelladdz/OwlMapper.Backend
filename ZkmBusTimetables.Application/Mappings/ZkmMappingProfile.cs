using AutoMapper;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZkmBusTimetables.Application.DTOs.Requests;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.Application.Mappings
{
    public class ZkmMappingProfile : Profile
    {
        public ZkmMappingProfile()
        {

            CreateMap<BusStopRequest, BusStop>();

            CreateMap<AddressRequest, Address>();

            CreateMap<LineRequest, Line>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.LineName))
                .ForMember(dest => dest.Variants, opt => opt.MapFrom(src => new List<Variant>
                {
                    new Variant
                    {
                    ValidFrom = src.ValidFrom,
                    Route = src.Route,
                    IsDefault = true,
                    RouteStops = src.RouteStops.Select(routeStop => new RouteStop
                    {
                        BusStopId = routeStop.BusStopId,
                        TimeToTravelInMinutes = routeStop.TimeToTravelInMinutes,
                        Order = routeStop.Order
                    }).ToList(),
                    Departures = src.Departures.Select(departure => new Departure
                    {
                        ScheduleDay = departure.ScheduleDay,
                        Time = departure.Time,
                        IsOnlyInSchoolDays = departure.IsOnlyInSchoolDays,
                        IsOnlyInDaysWithoutSchool = departure.IsOnlyInDaysWithoutSchool
                    }).ToList(),
                    RouteLinePoints = src.RouteLinePoints.Select(routeLinePoint => new RouteLinePoint
                    {
                        Coordinate = routeLinePoint.Coordinate,
                        Order = routeLinePoint.Order,
                        IsManuallyAdded = routeLinePoint.IsManuallyAdded
                    }).ToList()
                    }
                }));

            CreateMap<VariantRequest, Variant>()
                .ForMember(dest => dest.ValidFrom, opt => opt.MapFrom(src => src.ValidFrom))
                .ForMember(dest => dest.Line, opt => opt.Ignore())
                .ForMember(dest => dest.RouteStops, opt => opt.MapFrom(src => src.RouteStops))
                .ForMember(dest => dest.Departures, opt => opt.MapFrom(src => src.Departures))
                .ForMember(dest => dest.RouteLinePoints, opt => opt.MapFrom(src => src.RouteLinePoints))
                .ForMember(dest => dest.Route, opt => opt.MapFrom(src => src.Route));

            CreateMap<RouteStopRequest, RouteStop>()
                .ForMember(dest => dest.BusStopId, opt => opt.MapFrom(src => src.BusStopId))
                .ForMember(dest => dest.TimeToTravelInMinutes, opt => opt.MapFrom(src => src.TimeToTravelInMinutes))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order))
                .ForMember(dest => dest.Variant, opt => opt.Ignore());

            CreateMap<DepartureRequest, Departure>()
                .ForMember(dest => dest.ScheduleDay, opt => opt.MapFrom(src => src.ScheduleDay))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time))
                .ForMember(dest => dest.IsOnlyInSchoolDays, opt => opt.MapFrom(src => src.IsOnlyInSchoolDays))
                .ForMember(dest => dest.IsOnlyInDaysWithoutSchool, opt => opt.MapFrom(src => src.IsOnlyInDaysWithoutSchool))
                .ForMember(dest => dest.Variant, opt => opt.Ignore());
            CreateMap<RouteLinePointRequest, RouteLinePoint>()
                .ForMember(dest => dest.Coordinate, opt => opt.MapFrom(src => src.Coordinate))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order))
                .ForMember(dest => dest.IsManuallyAdded, opt => opt.MapFrom(src => src.IsManuallyAdded))
                .ForMember(dest => dest.Variant, opt => opt.Ignore());

            CreateMap<RegisterRequest, ApplicationUser>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));
        }
    }
}
