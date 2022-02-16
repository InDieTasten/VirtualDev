#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["VirtualDev.Action/VirtualDev.Action.csproj", "VirtualDev.Action/"]
RUN dotnet restore "VirtualDev.Action/VirtualDev.Action.csproj"
COPY . .
WORKDIR "/src/VirtualDev.Action"
RUN dotnet build "VirtualDev.Action.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VirtualDev.Action.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VirtualDev.Action.dll"]