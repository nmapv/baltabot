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
        private readonly IPersonRepository _personRepository;

        public PremiumHandler(IPremiumRepository premiumRepository, IPremiumApiRepository premiumApiRepository, IPersonRepository personRepository)
        {
            _premiumRepository = premiumRepository;
            _premiumApiRepository = premiumApiRepository;
            _personRepository = personRepository;
        }

        public async Task<ICommandResult> Handle(CreatePremiumCommand command)
        {
            command.Validate();
            if (!command.IsValid)
                return new GenericCommandResult(false, "Premium inválido", command.Notifications);

            var person = await _personRepository.GetByDiscordId(command.DiscordId);
            if (person == null)
                return new GenericCommandResult(false, "Pessoa não cadastrada", null);

            var premium = await _premiumRepository.Get(command.GetGuid());
            if (premium != null)
                return new GenericCommandResult(true, $"Premium {premium.Id} já cadastrado", premium);

            premium = await _premiumApiRepository.Create(command.GetGuid(), person);

            if (premium == null)
                return new GenericCommandResult(false, "Premium informado não existe", null);

            await _premiumRepository.Create(premium);
            return new GenericCommandResult(true, $"Premium {premium.Id} cadastrado", premium);
        }
    }
}
