<h1 align="center">Welcome to FiapDVP2-Microservices-Architecture 👋</h1>
<p>
  <img alt="Version" src="https://img.shields.io/badge/version-1.0.0-blue.svg?cacheSeconds=2592000" />
  <img src="https://img.shields.io/badge/npm-%3E%3D5.5.0-blue.svg" />
  <img src="https://img.shields.io/badge/node-%3E%3D9.3.0-blue.svg" />
  
</p>

> Trabalho de conclusão da matéria Microservices architecture da turma Fiap DevOps Engeneering 2

### 🏠 [Homepage](https://github.com/LuizCarlosSoares/FiapDVP2-MicroservicesArchitecture)

## Pré-requisitos
[.Net Core SDK 3.1.100](https://dotnet.microsoft.com/download/dotnet-core/3.1)

Teste a versão com :

```bash
  dotnet --version
  $ 3.1.100
```

Instalar o Tye

```bash
  dotnet tool install -g Microsoft.Tye --version "0.1.0-alpha.20168.8" --add-source https://dotnetfeed.blob.core.windows.net/dotnet-core/index.json
```

Instalar o [Docker](www.docker.io)

Instalar o [Docker-Compose](https://docs.docker.com/compose/)

Subir componentes

```bash
   docker-compose up -d --build
```

Restaurar dependencias do projeto

```bash
   dotnet restore
```

Compilar app

```bash
   dotnet build
```

Navegar até o diretório onde a aplicação foi clonada e executar:

```bash
   tye run
```

<http://localhost:8000>

Para acessar o open telemetry:

<http://localhost:9411/zipkin>


## Considerações:

   * Não usamos 100% do gerenciamento de configuração mas a ferramenta Tye possibilita

## O que foi implementado:

   * Oauth usando o [IdentityServer] do .NET (https://github.com/IdentityServer/IdentityServer4); 
   * API Gateway usando [Ocelot]: (https://github.com/ThreeMammals/Ocelot);
   * Service Discovery e resiliência [dotnet tye]: (https://github.com/dotnet/tye);

## Authores

👤 **Luiz Carlos Soares (RM-336362)**
👤 **Vinicius Briotto (RM-336362)**
👤 **Renan (RM-336362)**
👤 **Edgar (RM-336362)**

