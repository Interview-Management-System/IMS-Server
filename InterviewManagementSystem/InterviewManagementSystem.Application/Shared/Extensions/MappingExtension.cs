using InterviewManagementSystem.Domain.Shared.Paginations;

namespace InterviewManagementSystem.Application.Shared.Extensions;

internal static class MappingExtension
{
    internal static IMappingExpression<PaginatedSearchRequest, PaginationParameter<T>> MapPagination<T>(this IMappingExpression<PaginatedSearchRequest, PaginationParameter<T>> mapping) where T : class
    {
        return mapping
            .ForPath(dest => dest.PageSize, opt => opt.MapFrom(src => src.PaginationRequest.PageSize))
            .ForPath(dest => dest.PageIndex, opt => opt.MapFrom(src => src.PaginationRequest.PageIndex));
    }
}
