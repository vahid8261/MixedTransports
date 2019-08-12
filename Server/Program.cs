using System;
using System.Threading.Tasks;
using NServiceBus;
using RoutingLibrary;
using Server;


class Program
{
    static async Task Main()
	{
		Console.Title = "Samples.Router.MixedTransports.Server";
		var endpointConfiguration = new EndpointConfiguration(
			"Samples.Router.MixedTransports.Server");
		endpointConfiguration.PurgeOnStartup(true);
		Configure(endpointConfiguration);


		//endpointConfiguration.EnableFeature<InternalAutomaticRoutingFeature>();
		endpointConfiguration.EnableFeature<AutomaticRoutingFeature>();


		var endpointInstance = await Endpoint.Start(endpointConfiguration)
			.ConfigureAwait(false);

		Console.WriteLine("Press <enter> to exit.");
		Console.ReadLine();

		await endpointInstance.Stop()
			.ConfigureAwait(false);
	}

	static void Configure(EndpointConfiguration endpointConfiguration)
	{
		var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
		transport.Routing().ConnectToRouter("Samples.Router.MixedTransports.Router");
	
		transport.ConnectionString("host=localhost");
		transport.UseConventionalRoutingTopology();

		endpointConfiguration.UsePersistence<InMemoryPersistence>();

		var recoverability = endpointConfiguration.Recoverability();
		recoverability.Immediate(immediate => immediate.NumberOfRetries(0));
		recoverability.Delayed(delayed => delayed.NumberOfRetries(0));

		endpointConfiguration.SendFailedMessagesTo("error");
		endpointConfiguration.AuditProcessedMessagesTo("audit");
		endpointConfiguration.EnableInstallers();
	}

}