using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Features;

namespace Server
{
	class InternalRoutingInfoSubscriber : FeatureStartupTask
	{
		readonly RouterConnectionSettings _routerConnectionSettings;

		public InternalRoutingInfoSubscriber(RouterConnectionSettings routerConnectionSettings)
		{
			_routerConnectionSettings = routerConnectionSettings;
		}

		protected override async Task OnStart(IMessageSession session)

		{
			UpdateRoutingTable();
			await Task.CompletedTask;

		}

		void UpdateRoutingTable()
		{
			//	Registerin the publisher works here when it defined in the same libray
			_routerConnectionSettings.RegisterPublisher(Type.GetType("Shared.SQLTransEvent, Shared"), "Samples.Router.MixedTransports.SQLPublisher");
		}

		protected override async Task OnStop(IMessageSession session)
		{
			await Task.CompletedTask;
		}
	}
}

