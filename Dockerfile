FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

# copy csproj(s) and restore as distinct layers
WORKDIR /src
COPY Auth.Service.csproj ./Auth.Service.csproj
RUN dotnet restore

# copy everything else and build
COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://*:80

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Auth.Service.dll"]
