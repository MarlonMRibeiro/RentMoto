using API.FileUploadService;
using API.RabbitMqClient;
using Application.RabbitMqClient;
using Application.UseCases;
using Application.UseCases.Interface;
using Domain.Repositories;
using Infra.Repositories;
using System.Security.Principal;

namespace API.Extension
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureUseCases(this IServiceCollection services)
        {
            services.AddScoped<IPlanUseCase, PlanUseCase>();
            services.AddScoped<IDeliveryManUseCase, DeliveryManUseCase>();
            services.AddScoped<IMotorcycleUseCase, MotorcycleUseCase>();
            services.AddScoped<IOrderUseCase, OrderUseCase>();
            services.AddScoped<IRentalUseCase, RentalUseCase>();
            services.AddScoped<IOrderNotificationUseCase, OrderNotificationUseCase>();



            services.AddScoped<IFileUploadService, API.FileUploadService.FileUploadService>();
            services.AddScoped<IRabbitMqClient, Application.RabbitMqClient.RabbitMqClient>();
            services.AddHostedService<RabbitMqSubscriber>();

            return services;
        }

        public static IServiceCollection ConfigureRepositorys(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDeliveryManRepository, DeliveryManRepository>();
            services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
            services.AddScoped<IOrderNotificationRepository, OrderNotificationRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();

            return services;
        }
    }
}
