FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ForumProject.csproj", "./"]
RUN dotnet restore "./ForumProject.csproj"
COPY . .
RUN dotnet build "ForumProject.csproj" -c Release -o /app
FROM build AS publish
RUN dotnet publish "ForumProject.csproj" -c Release -o /app
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ForumProject.dll"]