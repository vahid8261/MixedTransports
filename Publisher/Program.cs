using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Client;
using NServiceBus;
using NServiceBus.Transport.SQLServer;

class Program
{
	static async Task Main()
	{
		Console.Title = "Samples.Router.MixedTransports.Client";
		const string letters = "ABCDEFGHIJKLMNOPQRSTUVXYZ";
		var random = new Random();
		var endpointConfiguration = new EndpointConfiguration("Samples.Router.MixedTransports.Client");
		endpointConfiguration.PurgeOnStartup(true);

		var transport = endpointConfiguration.UseTransport<SqlServerTransport>();
		transport.ConnectionString("Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;Initial Catalog=NserviceBusStorage;Application Name=NServiceBus;");
		transport.DefaultSchema("dbo");
		transport.Transactions(TransportTransactionMode.SendsAtomicWithReceive);

		transport.WithPeekDelay(TimeSpan.FromMilliseconds(500));


		endpointConfiguration.UsePersistence<InMemoryPersistence>();

		var recoverability = endpointConfiguration.Recoverability();
		recoverability.Immediate(immediate => immediate.NumberOfRetries(0));
		recoverability.Delayed(delayed => delayed.NumberOfRetries(0));

		endpointConfiguration.SendFailedMessagesTo("error");
		endpointConfiguration.AuditProcessedMessagesTo("audit");
		endpointConfiguration.EnableInstallers();
		endpointConfiguration.EnableFeature<AutomaticRoutingFeature>();

		#region ConnectorConfig

		var bridge = transport.Routing().ConnectToRouter("Samples.Router.MixedTransports.Router");
		bridge.RouteToEndpoint(typeof(MyMessage), "Samples.Router.MixedTransports.Server");
		//bridge.RegisterPublisher(typeof(MyEvent), "Samples.Router.MixedTransports.Server");

		#endregion

		var endpointInstance = await Endpoint.Start(endpointConfiguration)
			.ConfigureAwait(false);
		Console.WriteLine("Press <enter> to send a message");
		while (true)
		{
			Console.ReadLine();
			var id = new string(Enumerable.Range(0, 4).Select(x => letters[random.Next(letters.Length)]).ToArray());
			var message = new MyMessage
			{
				Id = id
			};
			await endpointInstance.Send(message)
				.ConfigureAwait(false);
		}
	}
}