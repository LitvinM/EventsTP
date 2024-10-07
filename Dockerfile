FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR ./

COPY *.sln ./
COPY EventsTP/*.csproj ./EventsTP/
COPY Model/*.csproj ./Model/
COPY Tests/*.csproj ./Tests/
RUN dotnet restore

COPY . ./
WORKDIR /EventsTP
RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR ./
COPY --from=build /out .

EXPOSE 80
ENTRYPOINT ["dotnet", "EventsTP.dll"]