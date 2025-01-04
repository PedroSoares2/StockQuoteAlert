O objetivo do sistema é enviar alertas por e-mail sempre que a cotação de um ativo da B3 ultrapassar um limite superior ou cair abaixo de um limite inferior. Esses valores são passados como parâmetros durante a chamada da aplicação.

1. Antes de executar a aplicação, certifique-se de adicionar os valores necessários ao arquivo de configuração appsettings.json. Além disso, verifique-se as propriedades do arquivo para que ele seja sempre copiado como uma versão atualizada.

![image](https://github.com/user-attachments/assets/647693eb-1bee-482b-9cd7-00770b140300)

    a. SmtpSettings
      Contém as configurações para o envio de e-mails via um servidor SMTP.
      
      Host: O endereço do servidor SMTP.
      Exemplo para Gmail: "smtp.gmail.com"
      
      Port: A porta usada para conexão com o servidor SMTP. É recomendado que use a porta 587
      
      Sender: O endereço de e-mail que será usado como remetente.
      Exemplo: "seuemail@gmail.com"
      
      Password:  ⚠️É necessário criar uma senha de aplicativo no e-mail configurado. 
      Essa senha deve ser usada no campo. Isso é importante para serviços como o Gmail, onde senhas normais não funcionam com autenticação de aplicativos.
      Exemplo: "suaSenhaDeAplicativo"
      
      Recipient: O endereço de e-mail para o qual os alertas serão enviados.
      Exemplo: "emailDeDestino@dominio.com"
    
    b. ApiSettings
      Contém as configurações para a integração com a API de cotações. É necessário entrar no site https://brapi.dev/dashboard e criar um novo token para inserir no campo "ApiToken".
      
      ApiToken: O token de autenticação necessário para acessar a API.
      Exemplo: "seuTokenDeApi"
    
     ![image](https://github.com/user-attachments/assets/1b406de4-5774-4675-8c85-3471b8ed2699)

2. Para executar a aplicação via linha de comando, copie o caminho completo do executável localizado na pasta Debug do projeto e adicione-o às variáveis de ambiente do sistema como um novo valor na variável Path.

Exemplo de uso: StockQuote.exe PETR4 22.67 22.59 
