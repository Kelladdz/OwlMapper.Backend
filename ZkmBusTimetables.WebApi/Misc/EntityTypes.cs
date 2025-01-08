using System.Reflection;
using ZkmBusTimetables.Application.DTOs.Requests;
using ZkmBusTimetables.Application.DTOs.Responses;
using ZkmBusTimetables.Core.Models;

namespace ZkmBusTimetables.WebApi.Misc
{
    public static class EntityTypes
    {
        public static Dictionary<TypeInfo, List<TypeInfo>> ModelTypes => new Dictionary<TypeInfo, List<TypeInfo>>()
         {

            {typeof(Address).GetTypeInfo(), new List<TypeInfo>() {typeof(Guid).GetTypeInfo(), typeof(AddressRequest).GetTypeInfo(), typeof(AddressResponse).GetTypeInfo() } },
            {typeof(BusStop).GetTypeInfo(), new List<TypeInfo>() {typeof(int).GetTypeInfo(), typeof(BusStopRequest).GetTypeInfo(), typeof(BusStopResponse).GetTypeInfo() } },
            {typeof(Departure).GetTypeInfo(), new List<TypeInfo>() {typeof(Guid).GetTypeInfo(), typeof(DepartureRequest).GetTypeInfo(), typeof(DepartureResponse).GetTypeInfo() } },
            {typeof(Line).GetTypeInfo(), new List<TypeInfo>() {typeof(Guid).GetTypeInfo(), typeof(LineRequest).GetTypeInfo(), typeof(LineResponse).GetTypeInfo() } },
            {typeof(RouteLinePoint).GetTypeInfo(), new List<TypeInfo>() {typeof(Guid).GetTypeInfo(), typeof(RouteLinePointRequest).GetTypeInfo(), typeof(RouteLinePointResponse).GetTypeInfo() } },
            {typeof(RouteStop).GetTypeInfo(), new List<TypeInfo>() {typeof(int).GetTypeInfo(), typeof(RouteStopRequest).GetTypeInfo(), typeof(RouteStopResponse).GetTypeInfo() } },
            {typeof(Variant).GetTypeInfo(), new List<TypeInfo>() {typeof(Guid).GetTypeInfo(), typeof(VariantRequest).GetTypeInfo(), typeof(VariantResponse).GetTypeInfo() } }

         };
    }
}
