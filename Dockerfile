FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

COPY . . 
RUN dotnet restore

WORKDIR /src/TalentHub.Presentation.Web
RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./

RUN apt-get update && apt-get install -y openssl && \
    mkdir /app/certificates && \
    openssl req -x509 -nodes -days 365 -newkey rsa:2048 \
    -keyout /app/certificates/aspnetcore.key \
    -out /app/certificates/aspnetcore.crt \
    -subj "/CN=localhost" && \
    openssl pkcs12 -export \
    -inkey /app/certificates/aspnetcore.key \
    -in /app/certificates/aspnetcore.crt \
    -out /app/certificates/aspnetcore.pfx \
    -password pass:S3cur3P@ssw0rd!

EXPOSE 5000
EXPOSE 5001
ENV ASPNETCORE_URLS=http://+:5000;https://+:5001
ENV ASPNETCORE_ENVIRONMENT=Production
ENV Kestrel__Certificates__Default__Path=/app/certificates/aspnetcore.pfx
ENV Kestrel__Certificates__Default__Password=S3cur3P@ssw0rd!

VOLUME [/root/.aspnet/DataProtection-Keys]

ENTRYPOINT ["dotnet", "TalentHub.Presentation.Web.dll"]