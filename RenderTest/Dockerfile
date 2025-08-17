#
## Stage 1: Build the app
#FROM mcr.microsoft.com/dotnet/sdk:9.0-nanoserver-ltsc2022 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /RenderTest
#COPY *.csproj .
#RUN dotnet restore
#COPY . .
#RUN dotnet publish -c Release -o /app/publish
#
## Stage 2: Run the app
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./RenderTest.csproj" -c Release -o /app/publish /p:UseAppHost=false
#
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "RenderTest.dll"]

# Stage 1: Build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0-nanoserver-ltsc2022 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /
COPY RenderTest.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish

# Stage 2: Run the app
FROM mcr.microsoft.com/dotnet/aspnet:9.0-nanoserver-ltsc2022 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "RenderTest.dll"]