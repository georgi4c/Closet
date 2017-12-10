using AutoMapper;

namespace Closet.Common.Mapping
{
    public interface IHaveCustomMapping
    {
        void ConfigureMapping(Profile mapper);
    }
}
