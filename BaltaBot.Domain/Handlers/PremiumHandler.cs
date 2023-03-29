using BaltaBot.Domain.Commands;
using BaltaBot.Domain.Commands.Interfaces;
using BaltaBot.Domain.ExternalServices;
using BaltaBot.Domain.Handlers.Interfaces;
using BaltaBot.Domain.Repositories;
using Flunt.Notifications;

namespace BaltaBot.Domain.Handlers
{
    public class PremiumHandler : Notifiable<Notification>,
        IHandler<CreatePremiumCommand>,
        IHandler<GetPremiumInactiveCommand>,
        IHandler<DeletePremiumCommand>
    {
        private readonly IPremiumRepository _premiumRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IPremiumService _premiumService;

        public PremiumHandler(IPremiumRepository premiumRepository, IPersonRepository personRepository, IPremiumService premiumService)
        {
            _premiumRepository = premiumRepository;
            _personRepository = personRepository;
            _premiumService = premiumService;
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

            premium = await _premiumService.Get(command.GetGuid(), person);

            if (premium == null)
                return new GenericCommandResult(false, "Premium informado não existe", null);

            await _premiumRepository.Create(premium);
            return new GenericCommandResult(true, $"Premium {premium.Id} cadastrado", premium);
        }

        public async Task<ICommandResult> Handle(GetPremiumInactiveCommand command)
        {
            var premiums = await _premiumRepository.GetInactives();

            if (premiums == null || premiums.Count() == 0)
                return new GenericCommandResult(false, "Nenhum inativo encontrado", null);

            var result = premiums.Select(x => x.Person.DiscordId).ToList();
            return new GenericCommandResult(true, $"{result.Count()} inativo(s)", result);
        }

        public async Task<ICommandResult> Handle(DeletePremiumCommand command)
        {
            command.Validate();
            if (!command.IsValid)
                return new GenericCommandResult(false, "Premium inválido", command.Notifications);

            await _premiumRepository.DeleteByDiscorId(command.DiscordId);
            return new GenericCommandResult(true, "Premium deletado", null);
        }
    }
}
