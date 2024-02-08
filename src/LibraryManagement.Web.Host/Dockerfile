FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/LibraryManagement.Web.Host/LibraryManagement.Web.Host.csproj", "src/LibraryManagement.Web.Host/"]
COPY ["src/LibraryManagement.Web.Core/LibraryManagement.Web.Core.csproj", "src/LibraryManagement.Web.Core/"]
COPY ["src/LibraryManagement.Application/LibraryManagement.Application.csproj", "src/LibraryManagement.Application/"]
COPY ["src/LibraryManagement.Core/LibraryManagement.Core.csproj", "src/LibraryManagement.Core/"]
COPY ["src/LibraryManagement.EntityFrameworkCore/LibraryManagement.EntityFrameworkCore.csproj", "src/LibraryManagement.EntityFrameworkCore/"]
WORKDIR "/src/src/LibraryManagement.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/LibraryManagement.Web.Host", "src/LibraryManagement.Web.Host"]
COPY ["src/LibraryManagement.Web.Core", "src/LibraryManagement.Web.Core"]
COPY ["src/LibraryManagement.Application", "src/LibraryManagement.Application"]
COPY ["src/LibraryManagement.Core", "src/LibraryManagement.Core"]
COPY ["src/LibraryManagement.EntityFrameworkCore", "src/LibraryManagement.EntityFrameworkCore"]
WORKDIR "/src/src/LibraryManagement.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "LibraryManagement.Web.Host.dll"]
