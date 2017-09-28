using AutoMapper;

namespace PCstore.Web.Infrastructure
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
