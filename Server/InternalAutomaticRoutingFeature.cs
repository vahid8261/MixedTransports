using NServiceBus;
using NServiceBus.Features;

namespace Server
{
	class InternalAutomaticRoutingFeature : Feature
	{
		protected override void Setup(FeatureConfigurationContext context)
		{
			var settings = context.Settings;
			settings.TryGet<RouterConnectionSettings>(out var _routerConnectionSettings);

			//Register the routing info subscriber
			context.RegisterStartupTask(builder => new InternalRoutingInfoSubscriber(_routerConnectionSettings));
		}
	}
}

