# DeviceControl

Repositório oficial do desafio “Desafio Fullstack” para integração de dispositivos IoT com .NET 7 e Angular 19, utilizando Telnet via ncat e mock de API CIoTD.

## Descrição do Projeto

Este projeto implementa uma solução fullstack para consulta e controle de dispositivos IoT usando:

- **Backend** em C# (.NET 8)  
- **Frontend** em Angular 19 com PrimeNG e Tailwind CSS  
- **Autenticação** OAuth2 “fake” com JWT  
- **Comunicação Telnet** via ncat para simular dispositivos  
- **Mock de API Devices** no Postman seguindo especificação OpenAPI  

A interface web permite ao usuário:

1. Fazer **login** e armazenar token JWT.  
2. Listar dispositivos cadastrados.  
3. Ver detalhes (fabricante, descrição, comandos).  
4. Executar comandos via Telnet e exibir resposta formatada.



## 🛠️ Tecnologias

- **Backend**: .NET 8 (C# 10)
- **Frontend**: Angular 19 + PrimeNG + Tailwind
- **Autenticação**: JWT com OAuth2 simulado
- **Testes**: xUnit + Moq
- **Ferramentas**: Postman (API Mock), Ncat (Telnet Server)



## Funcionalidades Implementadas

- **OAuth2 Fake**: fluxo de login por HTTP POST e armazenamento de JWT no `localStorage`.  
- **Listagem de Dispositivos**: consumo do endpoint `GET /api/devices`.  
- **Detalhes do Dispositivo**: exibição de manufacturer, description e lista de comandos.  
- **Execução de Comandos**: formulário dinâmico de parâmetros + chamada Telnet via backend.  
- **Mock Devices**: collections do Postman adaptadas com OpenAPI e exemplos.  
- **TelnetService**: envio de comandos a `localhost:2323`, retorno de JSON via ncat.  
- **Testes Unitários (xUnit + Moq)**: cobertura de `DeviceService` e `CommandService`.

## Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)  
- [Node.js ≥ 16.x](https://nodejs.org/)  
- [Angular CLI](https://angular.io/cli) (`npm install -g @angular/cli`)  
- [ncat (nmap)](https://nmap.org/ncat/)  
- [Postman](https://www.postman.com/) (para mock Devices) 

## Configuração

1. **Clone o repositório** 
```bash
   git clone https://github.com/silvio-swat/device-control.git
   cd device-control
```

2. **Configurar Mock CIoTD**

- Importe a collection Postman em tests/Postman-Mocks/CIoTD.postman_collection.json

- Execute o Mock Server no Postman

3. **Iniciar Telnet Mock**
```bash
ncat --listen --keep-open --verbose --exec C:\ncat-mock\run-mock.bat -p 2323
```
4. **Backend (.NET)**
```bash
cd src/IoTControl.API
dotnet restore
dotnet run
```
5. **Frontend (Angular)**
```bash
cd src/ClientApp
npm install
ng serve --open
```

3. ** **
```bash

```
