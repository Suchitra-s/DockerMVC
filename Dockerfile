#Layer1
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
 
WORKDIR /src
 
#only csproj file is needed for nuget package restore
COPY ./*.csproj ./
 
RUN dotnet restore
 
#only if dotnet restore is successful , copy remainainig files
COPY ./ ./
 
RUN dotnet build && dotnet publish -o dist
 
#Layer2
FROM mcr.microsoft.com/dotnet/aspnet:7.0
 
WORKDIR /app
 
COPY --from=build /src/dist ./
 
ENV ASPNETCORE_ENVIRONMENT="Production"
ENV ConnectionStrings:SqlConnection="Server=150.43.24.10; Database=proddb; username=saikiran; password=hello"
ENV ASPNETCORE_URLS=http://+:5000

EXPOSE 5000
 
ENTRYPOINT [ "dotnet","DockerMVC.dll" ]
