#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MicroserviceTest.Api.User/MicroserviceTest.Api.User.csproj", "MicroserviceTest.Api.User/"]
COPY ["MicroserviceTest.Common/MicroserviceTest.Common.csproj", "MicroserviceTest.Common/"]
COPY ["MicroserviceTest.Contract/MicroserviceTest.Contract.csproj", "MicroserviceTest.Contract/"]
COPY ["MicroserviceTest.CoreServices/MicroserviceTest.CoreServices.csproj", "MicroserviceTest.CoreServices/"]
COPY ["MicroserviceTest.Services/MicroserviceTest.Services.csproj", "MicroserviceTest.Services/"]
RUN dotnet restore "MicroserviceTest.Api.User/MicroserviceTest.Api.User.csproj"
COPY . .
WORKDIR "/src/MicroserviceTest.Api.User"
RUN dotnet build "MicroserviceTest.Api.User.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroserviceTest.Api.User.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroserviceTest.Api.User.dll"]