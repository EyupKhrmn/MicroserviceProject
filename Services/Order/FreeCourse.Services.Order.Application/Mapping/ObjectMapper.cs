using AutoMapper;

namespace FreeCourse.Services.Order.Application.Mapping;

public class ObjectMapper
{
    private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CustomMap>();
        });

        return config.CreateMapper();
    });

    public static IMapper Mapper => Lazy.Value;
}