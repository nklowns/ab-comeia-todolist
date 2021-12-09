FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7067
EXPOSE 5034

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ab-comeia-todolist.csproj", "./"]
RUN dotnet restore "ab-comeia-todolist.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "ab-comeia-todolist.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ab-comeia-todolist.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ab-comeia-todolist.dll"]
