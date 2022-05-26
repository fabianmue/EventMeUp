## Database manipulation

### Add migration

> `$env:ASPNETCORE_WEBAPI_CONNECTIONSTRING=(Get-Content "webapi.env" | ConvertFrom-StringData).ASPNETCORE_WEBAPI_CONNECTIONSTRING`

> `dotnet ef migrations add Name --project Webapi/Webapi.csproj`
