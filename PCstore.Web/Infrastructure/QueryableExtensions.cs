using AutoMapper.QueryableExtensions;
using PCstore.Web.App_Start;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace PCstore.Web.Infrastructure
{
    public static class QueryableExtensions
    {
        public static IQueryable<TDestination> MapTo<TDestination>(this IQueryable source, params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            return source.ProjectTo(AutoMapperConfig.Configuration, membersToExpand);
        }
    }
}