FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /eventmeup
COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /eventmeup
COPY --from=build /eventmeup/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "Webapi.dll"]