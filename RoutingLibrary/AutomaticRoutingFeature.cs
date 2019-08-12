using System;
using NServiceBus;
using NServiceBus.Features;

namespace RoutingLibrary
{
	public class AutomaticRoutingFeature : Feature
	{
		protected override void Setup(FeatureConfigurationContext context)
		{
			var settings = context.Settings;
			settings.TryGet<RouterConnectionSettings>(out var _routerConnectionSettings);

			//Register the routing info subscriber
			context.RegisterStartupTask(builder => new RoutingInfoSubscriber(_routerConnectionSettings));

			//Registerin the publisher works here when it defined in the feature setup
			//_routerConnectionSettings.RegisterPublisher(Type.GetType("Shared.SQLTransEvent, Shared"), "Samples.Router.MixedTransports.SQLPublisher");

		}
	}
}

