# BancaApiTest

## Requisitos Previos

Antes de ejecutar la API, asegúrate de tener instalados los siguientes programas en tu sistema:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Git](https://git-scm.com/)

---

## Instrucciones para Ejecutar la API

Sigue los pasos a continuación para clonar y ejecutar la API en tu máquina local:

### 1. Abrir la Terminal

Abre la terminal o consola de tu preferencia y navega hasta la carpeta donde quieras clonar el proyecto.

### 2. Inicializar un Repositorio

Si deseas iniciar un repositorio Git en tu carpeta local, ejecuta:

```bash
git init
```

### 3. Clonar el Repositorio
Clona el repositorio oficial de la API usando:

```bash
git clone https://github.com/jorgeTreminio07/BancaApiTest.git
```

### 4. Acceder a la Carpeta del Proyecto
Una vez clonado, entra a la carpeta creada:

```bash
cd BancaApiTest
```
### 5. Restaurar Paquetes NuGet
Ejecuta:

```bash
dotnet restore
```
Esto descargará e instalará todas las dependencias necesarias del proyecto definidas en los archivos de configuración (.csproj y global.json).

Nota: Es un paso importante para asegurar que el proyecto tenga todas las librerías necesarias antes de compilar.

### 6. Compilar el Proyecto
Ejecuta:

```bash
dotnet build
```
Esto compila el proyecto, verifica que no existan errores de código y genera los archivos binarios necesarios para ejecutar la aplicación.

### 7. Ejecutar la API
Finalmente, para iniciar la API, ejecuta:

```bash
dotnet run --project BancaApi.Api
```

