READ-ME

Olá, aqui irei colocar algumas instruções de como executar o projeto.
Mas, antes de tudo, essa é a primeira oportunidade que eu recebi, gostei da ideia de criar um projeto para demonstrar até aonde coseguimos ir.
Foram dois dias BEM produtivos, consegui extrair muito bem meu conhecimento para concluir tudo isso em dois dias. E ficou bem legal!! Espero que gostem também.

--- DATABASE ----

Utilizei Mysql server 8.0
(Eu gerei o banco manualmente, sem a ajuda do Entity code fist)
Recomendo fazer o restore da database seguindo este preset:
eu particularment uso o MySQL workbench

![image](https://github.com/WillianLorysTec/herois-backendAPI/assets/62141114/d5c9553b-eca6-4389-bce3-8668f9459af8)


-- CONNECTION STRING --

Provavelmente será necessário mudar senha ou a porta da connection string;

.NET -> Tudo esta dentro da classe AcessoDados.cs   path: Data -> Repositorio -> AcessoDados.cs

![image](https://github.com/WillianLorysTec/herois-backendAPI/assets/62141114/436fab38-af24-4155-921e-2f38e9d3e539)



Angular -> Aqui eu acredito que se tiver alguma mudança, seria relacionado a porta do IIS (DETALHE:: eu usei o tempo todo o servidor padrão IIS disponibilizado pelo Visual Studio,
executar o projeto diferente disso, pode talvez ocacionar algum erro de cors... fora que mudaria a porta previamente configurada no launchsettings.json que faz o bat com o environment
do endpoint no projto ANGULAR). Caso precise mudar a porta no projeto ANGULAR o arquivo Environment.prod.ts   path: Environment -> Environment.prod.ts

![image](https://github.com/WillianLorysTec/herois-backendAPI/assets/62141114/31ba6afb-3f9a-4430-9479-d37456d76f51)






Execução do docker compose
