#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PRN231.TicketBooking.API/PRN231.TicketBooking.API.csproj", "PRN231.TicketBooking.API/"]
COPY ["PRN231.TicketBooking.Common/PRN231.TicketBooking.Common.csproj", "PRN231.TicketBooking.Common/"]
COPY ["PRN231.TicketBooking.BusinessObject/PRN231.TicketBooking.BusinessObject.csproj", "PRN231.TicketBooking.BusinessObject/"]
COPY ["PRN231.TicketBooking.Service/PRN231.TicketBooking.Service.csproj", "PRN231.TicketBooking.Service/"]
COPY ["PRN231.TicketBooking.Repository/PRN231.TicketBooking.Repository.csproj", "PRN231.TicketBooking.Repository/"]
COPY ["PRN231.TicketBooking.DAL/PRN231.TicketBooking.DAO.csproj", "PRN231.TicketBooking.DAL/"]
RUN dotnet restore "./PRN231.TicketBooking.API/PRN231.TicketBooking.API.csproj"
COPY . .
WORKDIR "/src/PRN231.TicketBooking.API"
RUN dotnet build "./PRN231.TicketBooking.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PRN231.TicketBooking.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PRN231.TicketBooking.API.dll"]