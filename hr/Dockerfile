#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["hr/hr.csproj", "hr/"]
COPY ["common/common.csproj", "common/"]
RUN dotnet restore "hr/hr.csproj"
COPY . .
WORKDIR "/src/hr"
RUN dotnet build "hr.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "hr.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "hr.dll"]
