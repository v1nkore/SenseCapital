FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TicTacToe.API/TicTacToe.API.csproj", "TicTacToe.API/"]
COPY ["TicTacToe.Application/TicTacToe.Application.csproj", "TicTacToe.Application/"]
COPY ["TicTacToe.Abstractions/TicTacToe.Abstractions.csproj", "TicTacToe.Abstractions/"]
COPY ["TicTacToe.Contracts/TicTacToe.Contracts.csproj", "TicTacToe.Contracts/"]
COPY ["TicTacToe.Domain/TicTacToe.Domain.csproj", "TicTacToe.Domain/"]
COPY ["TicTacToe.Infrastructure/TicTacToe.Infrastructure.csproj", "TicTacToe.Infrastructure/"]
RUN dotnet restore "TicTacToe.API/TicTacToe.API.csproj"
COPY . .
WORKDIR "/src/TicTacToe.API"
RUN dotnet build "TicTacToe.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TicTacToe.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicTacToe.API.dll"]