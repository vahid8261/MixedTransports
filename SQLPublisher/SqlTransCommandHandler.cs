using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared;

class SqlTransCommandHandler :
	IHandleMessages<SqlTransCommand>
{
	static ILog log = LogManager.GetLogger<SqlTransCommandHandler>();

	public async Task Handle(SqlTransCommand message, IMessageHandlerContext context)
	{
		#region RequestHandler

		log.Info($"SqlTransCommand handler {message.Id}");
		await context.Publish(new SQLTransEvent()
		{
			Id = message.Id
		}).ConfigureAwait(false);

		//await context.Reply(new MyReply
		//{
		//	Id = message.Id
		//}).ConfigureAwait(false);

		//await context.Send(new ClientMessage( )).ConfigureAwait(false); 

		#endregion
	}
}

