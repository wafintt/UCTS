FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development 
EXPOSE 8080
EXPOSE 8081 

FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RetrieveJWTToken/RetrieveJWTToken.csproj", "RetrieveJWTToken/"]
RUN dotnet restore "RetrieveJWTToken/RetrieveJWTToken.csproj"
COPY . .
WORKDIR "/src/RetrieveJWTToken"
RUN dotnet build "RetrieveJWTToken.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RetrieveJWTToken.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RetrieveJWTToken.dll"]