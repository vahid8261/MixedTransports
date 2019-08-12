
namespace Shared
{
	using NServiceBus;

	public class SqlTransCommand : ICommand
	{
		public string Id { get; set; }
	}
}
