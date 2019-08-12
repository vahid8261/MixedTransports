using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared;

namespace Server
{
	class SQLTransEventHandler :
		IHandleMessages<SQLTransEvent>
	{
		static ILog log = LogManager.GetLogger<SQLTransEventHandler>();

		public async Task Handle(SQLTransEvent message, IMessageHandlerContext context)
		{
			#region RequestHandler

			log.Info($"SQLTransEvent handler {message.Id}");
			await Task.CompletedTask;


			//await context.Reply(new MyReply
			//{
			//	Id = message.Id
			//}).ConfigureAwait(false);


			#endregion
		}
	}
}