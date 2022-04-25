FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ScrumBoard.WebAPI/ScrumBoard.WebAPI.csproj", "ScrumBoard.WebAPI/"]
RUN dotnet restore "ScrumBoard.WebAPI/ScrumBoard.WebAPI.csproj"
COPY . .
WORKDIR "/src/ScrumBoard.WebAPI"
RUN dotnet build "ScrumBoard.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ScrumBoard.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ScrumBoard.WebAPI.dll"]
