FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base

# update system
RUN apt-get update -y && apt-get upgrade -y

# Install missing software for production
RUN apt-get install bash apt-utils libnss3-tools -y

# For the creation of the thumbnails it is also necessary to include some graphics libraries
# See (https://github.com/dotnet/dotnet-docker/issues/618#issuecomment-416543274) for documentation

# Soft link (instead of downloading a new one...)
RUN ln -s /lib/x86_64-linux-gnu/libdl-2.24.so /lib/x86_64-linux-gnu/libdl.so

# Install the missing libraries
RUN apt-get install -y --allow-unauthenticated libgdiplus

# Remove the unused lists of apt repositories     
RUN rm -rf /var/lib/apt/lists/*

WORKDIR /app


#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["TestEmptyPNGGDI/TestEmptyPNGGDI.csproj", "TestEmptyPNGGDI/"]
RUN dotnet restore "TestEmptyPNGGDI/TestEmptyPNGGDI.csproj"
COPY . .
WORKDIR "/src/TestEmptyPNGGDI"
RUN dotnet build "TestEmptyPNGGDI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestEmptyPNGGDI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestEmptyPNGGDI.dll"]