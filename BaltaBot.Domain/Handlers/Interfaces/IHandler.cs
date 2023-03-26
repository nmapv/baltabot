using BaltaBot.Domain.Commands.Interfaces;

namespace BaltaBot.Domain.Handlers.Interfaces
{
    public interface IHandler<T> where T : ICommand
    {
        Task<ICommandResult> Handle(T command);
    }
}