using Budgeter.IoC;
using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace Budgeter.Functions
{
	public class Program
	{
		public async static Task Main()
		{
			var host = new HostBuilder()
				.ConfigureFunctionsWorkerDefaults()
				.ConfigureServices((context, services) =>
				{
					services.RegisterAuthentication(context.Configuration);
				})
				.Build();

			await host.RunAsync();
		}
	}
}