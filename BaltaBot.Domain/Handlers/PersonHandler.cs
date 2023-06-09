﻿using BaltaBot.Domain.Commands;
using BaltaBot.Domain.Commands.Interfaces;
using BaltaBot.Domain.Handlers.Interfaces;
using BaltaBot.Domain.Repositories;
using Flunt.Notifications;

namespace BaltaBot.Domain.Handlers
{
    public class PersonHandler : Notifiable<Notification>, 
        IHandler<CreatePersonCommand>,
        IHandler<UpdatePersonCommand>
    {
        private readonly IPersonRepository _personRepository;

        public PersonHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<ICommandResult> Handle(CreatePersonCommand command)
        {
            command.Validate();
            if (!command.IsValid)
                return new GenericCommandResult(false, "Pessoa inválida", command.Notifications);

            var person = await _personRepository.GetByDiscordId(command.DiscordId);

            if (person != null)
                return new GenericCommandResult(true, $"{person.Name} já verificado(a)", person);

            person = new(command.DiscordId, command.Name, command.CreatedAt);
            await _personRepository.Create(person);
            return new GenericCommandResult(true, $"{person.Name} verificado(a)", person);
        }

        public async Task<ICommandResult> Handle(UpdatePersonCommand command)
        {
            command.Validate();
            if (!command.IsValid)
                return new GenericCommandResult(false, "Pessoa inválida", command.Notifications);

            var person = await _personRepository.GetByDiscordId(command.DiscordId);

            if (person == null)
                return new GenericCommandResult(false, $"Não verificado(a)", null);

            person.SetName(command.Name);
            await _personRepository.Update(person);
            return new GenericCommandResult(true, $"{person.Name} verificado(a)", person);
        }
    }
}
