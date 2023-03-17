#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
# COPY . .
EXPOSE 80
EXPOSE 443

# если собраено снаружи

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Votp/Votp.csproj", "Votp/"]
COPY ["TimeToken/Votp.DS.TToken.csproj", "TimeToken/"]
COPY ["Votp.DS.Entities/Votp.DS.Entities.csproj", "Votp.DS.Entities/"]
COPY ["Votp.Contracts/Votp.Contracts.csproj", "Votp.Contracts/"]
COPY ["Votp.DS.Database/Votp.DS.Database.csproj", "Votp.DS.Database/"]
COPY ["Votp.Utils/Votp.Utils.csproj", "Votp.Utils/"]
COPY ["Votp.Web.TToken/Votp.Web.TToken.csproj", "Votp.Web.TToken/"]
COPY ["Votp.Web.TokenBase/Votp.Web.TokenBase.csproj", "Votp.Web.TokenBase/"]
RUN dotnet restore "Votp/Votp.csproj"
COPY . .
WORKDIR "/src/Votp"
RUN dotnet build "Votp.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "Votp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Votp.dll"]