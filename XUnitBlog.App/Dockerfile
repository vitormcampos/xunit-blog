FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY XUnitBlog.Domain/*.csproj XUnitBlog.Domain/
COPY XUnitBlog.Data/*.csproj XUnitBlog.Data/
COPY XUnitBlog.App/*.csproj XUnitBlog.App/

RUN dotnet restore XUnitBlog.App/XUnitBlog.App.csproj

COPY . .

RUN dotnet publish XUnitBlog.App/XUnitBlog.App.csproj -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "XUnitBlog.App.dll"]
