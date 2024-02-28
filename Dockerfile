FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY *.sln .
COPY FlowrSpot.WebApi/*.csproj			FlowrSpot.WebApi/
COPY FlowrSpot.Application/*.csproj		FlowrSpot.Application/
COPY FlowrSpot.Domain/*.csproj			FlowrSpot.Domain/
COPY FlowrSpot.Infrastructure/*.csproj	FlowrSpot.Infrastructure/

COPY FlowrSpot.Test/*.csproj			FlowrSpot.Test/

RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release --property:PublishDir=/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final-env
WORKDIR /app
COPY --from=build-env /out .
ENTRYPOINT ["dotnet","FlowrSpot.WebApi.dll"]