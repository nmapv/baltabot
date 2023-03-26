using BaltaBot.Domain.Commands;
using BaltaBot.Domain.Commands.Interfaces;
using BaltaBot.Domain.Handlers.Interfaces;
using BaltaBot.Domain.Repositories;
using Flunt.Notifications;

namespace BaltaBot.Domain.Handlers
{
    public class PremiumHandler : Notifiable<Notification>,
        IHandler<CreatePremiumCommand>
    {
        private readonly IPremiumRepository _premiumRepository;
        private readonly IPremiumApiRepository _premiumApiRepository;

        public PremiumHandler(IPremiumRepository premiumRepository, IPremiumApiRepository premiumApiRepository)
        {
            _premiumRepository = premiumRepository;
            _premiumApiRepository = premiumApiRepository;
        }

        public async Task<ICommandResult> Handle(CreatePremiumCommand command)
        {
            command.Validate();
            if (!command.IsValid)
                return new GenericCommandResult(false, "Premium inválido", command.Notifications);

            var premium = await _premiumApiRepository.GetById(command.GetGuid());            
            AddNotifications(premium.Notifications);

            if (!IsValid)
                return new GenericCommandResult(false, "Falha ao cadastrar premium", Notifications);

            await _premiumRepository.Create(premium);
            return new GenericCommandResult(true, $"Premium {premium.Id} cadastrada", premium);
        }
    }
}
