FROM mcr.microsoft.com/dotnet/aspnet:5.0 as build
Workdir apiapp

EXPOSE 80
EXPOSE 443
EXPOSE 5024

COPY SPatrickBack/SPatrickBack.csproj SPatrickBack/
RUN dotnet restore "SPatrickBack/SPatrickBack.csproj"

COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 
WORKDIR /apiapp
COPY --from=build /apiapp/out .
ENTRYPOINT ["dotnet", "SPatrickBack.dll"]

##See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#EXPOSE 5024
#
#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#WORKDIR /src
#COPY ["SPatrickBack/SPatrickBack.csproj", "SPatrickBack/"]
#RUN dotnet restore "SPatrickBack/SPatrickBack.csproj"
#COPY . .
#WORKDIR "/src/SPatrickBack"
#RUN dotnet build "SPatrickBack.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "SPatrickBack.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "SPatrickBack.dll"]