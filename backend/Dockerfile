FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ContactManager.API/ContactManager.API.csproj", "ContactManager.API/"]
RUN dotnet restore "ContactManager.API/ContactManager.API.csproj"
COPY . .
WORKDIR "/src/ContactManager.API"
RUN dotnet build "ContactManager.API.csproj" -c Release -o /app/build

#publish
FROM build AS publish 
RUN dotnet publish "ContactManager.API.csproj" -c Release -o /app/publish

#final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactManager.API.dll"]