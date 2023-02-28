FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
EXPOSE 80
EXPOSE 443
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#USER root:root
WORKDIR /app
COPY hello-app.csproj ./
#RUN dotnet restore hello-app.csproj
COPY . .
WORKDIR "/app/hello-app"
RUN dotnet build "hello-app.csproj" -c Release -o /app/build FROM build AS publish
RUN dotnet publish "hello-app.csproj" -c Release -o /app/publish /p:UseAppHost=false FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "hello-app.dll"]
