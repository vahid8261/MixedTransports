using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Features;

namespace RoutingLibrary
{
	class RoutingInfoSubscriber : FeatureStartupTask
	{
		readonly RouterConnectionSettings _routerConnectionSettings;

		public RoutingInfoSubscriber(RouterConnectionSettings routerConnectionSettings)
		{
			_routerConnectionSettings = routerConnectionSettings;
		}

		protected override async Task OnStart(IMessageSession session)

		{
			await UpdateRoutingTable();

		}

		async Task UpdateRoutingTable()
		{
			_routerConnectionSettings.RegisterPublisher(Type.GetType("Shared.SQLTransEvent, Shared"), "Samples.Router.MixedTransports.SQLPublisher");
			await Task.CompletedTask;
		}

		protected override async Task OnStop(IMessageSession session)
		{
			await Task.CompletedTask;
		}
	}

}
