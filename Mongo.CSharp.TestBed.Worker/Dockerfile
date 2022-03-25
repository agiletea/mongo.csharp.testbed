#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Mongo.CSharp.TestBed.Worker/Mongo.CSharp.TestBed.Worker.csproj", "Mongo.CSharp.TestBed.Worker/"]
RUN dotnet restore "Mongo.CSharp.TestBed.Worker/Mongo.CSharp.TestBed.Worker.csproj"
COPY . .
WORKDIR "/src/Mongo.CSharp.TestBed.Worker"
RUN dotnet build "Mongo.CSharp.TestBed.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mongo.CSharp.TestBed.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mongo.CSharp.TestBed.Worker.dll"]