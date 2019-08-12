using NServiceBus;

namespace Shared
{
	public class SQLTransEvent :
		IEvent
	{
		public string Id { get; set; }
	}
}