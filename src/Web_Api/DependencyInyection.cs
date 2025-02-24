using Web_Api.Middleware;

namespace Web_Api;

public static class DependencyInyection {
    public static IServiceCollection AddPresentation(this IServiceCollection services) {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddControllers();
        services.AddTransient<GlobalExceptionHandlerMiddleware>();

        return services;
    }

}