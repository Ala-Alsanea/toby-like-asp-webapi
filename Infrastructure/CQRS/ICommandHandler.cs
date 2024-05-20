namespace PostApi.Infrastructure.ErrorHandling
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Result Handle(TCommand command);
    }

    // public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand<TResponse>
    // {
    //     Result<TResponse> Handle(TCommand command);
    // }
}