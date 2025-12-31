# Etapa 1: Imagen base para ejecución (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base  # Imagen liviana solo para ejecutar
WORKDIR /app                                       # Carpeta de trabajo dentro del contenedor
EXPOSE 8080                                        # Expone puerto HTTP 8080
EXPOSE 8081                                        # Expone puerto HTTPS 8081

# Etapa 2: Imagen para compilar (build)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build     # Imagen con herramientas de desarrollo
WORKDIR /src                                       # Carpeta de trabajo para código fuente
COPY ["WebAPINET8.csproj", "."]                    # Copia archivo de proyecto primero
RUN dotnet restore "WebAPINET8.csproj"             # Restaura paquetes NuGet
COPY . .                                           # Copia todo el código fuente
RUN dotnet build "WebAPINET8.csproj" -c Release -o /app/build  # Compila proyecto

# Etapa 3: Publicar aplicación
FROM build AS publish                              # Usa etapa de compilación como base
RUN dotnet publish "WebAPINET8.csproj" -c Release -o /app/publish  # Publica para producción

# Etapa 4: Imagen final liviana
FROM base AS final                                 # Vuelve a la imagen base liviana
WORKDIR /app                                       # Establece carpeta de trabajo
COPY --from=publish /app/publish .                 # Copia solo lo publicado
ENTRYPOINT ["dotnet", "WebAPINET8.dll"]           # Comando para iniciar la aplicación