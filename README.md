# comandos
1. !verify -> cadastra o usuário na base (necessário, pois o discord net só permite consulta de 100 usuários do canal)
2. !premium -> consulta o premium numa api* e após validado é cadastrado na base. (*deixei uma api fake no premiumApiRepository)
3. !cleaning -> faz uma limpeza dos premiums vencidos no canal.

# requisitos
1. variável de ambiente TOKEN_DISCORD com o token do [bot](https://discord.com/developers/applications).
2. todos os Privileged Gateway Intents ativos.
3. permissão administrador para o bot (img 1).

![image](https://user-images.githubusercontent.com/59609545/227974443-126bf43a-c026-4d57-b5df-471259b3dd46.png)

4. cargo do bot precisa estar acima dos quais ele irá gerir.

![image](https://user-images.githubusercontent.com/59609545/227972619-786d4178-fa9e-4586-b055-ae59c25cc8ba.png)

# observações
1. Testado com sqlite em memória e sql server.
2. Utiliza o fluent para migrations.
3. Utiliza repositório genérico com dapper contrib.
