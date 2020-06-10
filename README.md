# FiapDVP2-MicroservicesArchitecture

## Instalação

### Instalar o .Net Framework Core

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

Após subir a aplicação acessar:

<http://localhost:8000>
