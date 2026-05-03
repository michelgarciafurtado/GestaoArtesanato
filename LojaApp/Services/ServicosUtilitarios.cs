namespace LojaApp.Services;

public static class ServicosUtilitarios
{
    public static IServiceCollection AddServicosUtilitarios(this IServiceCollection services)
    {
        services.AddScoped<GenImagensService>();
        return services;
    }
}
