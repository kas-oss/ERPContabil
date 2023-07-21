
using DIR.Settings;
using Consul;


namespace DIR.RegistersExtensions
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection AddConsulSettings(this IServiceCollection services, ServiceSettings serviceSettings)
        {
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
                {
                    consulConfig.Address = new Uri(serviceSettings.ConsulAddress);
                })
            );
            return services;
        }

        public static IApplicationBuilder UseConsul(this IApplicationBuilder app, ServiceSettings serviceSettings)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>().CreateLogger("AppExtensions");
            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            var registration = new AgentServiceRegistration()
            {
                Name    = serviceSettings.ServiceName,
                Address = serviceSettings.ServiceAddress, 
                Port    = serviceSettings.ServicePort 
            };

            logger.LogInformation("Registering with Consul");
            consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            consulClient.Agent.ServiceRegister(registration).ConfigureAwait(true);

            lifetime.ApplicationStopping.Register(() =>
            {
                logger.LogInformation("Unregistering from Consul");
            });

            return app;
        }
    }
}