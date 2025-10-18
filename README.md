# BancaApiTest - Solución de Prueba Técnica .NET 8

## Requisitos Previos

Antes de ejecutar la API, asegúrate de tener instalados los siguientes programas en tu sistema:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Git](https://git-scm.com/)

---

## Instrucciones para Ejecutar la API

Sigue los pasos a continuación para clonar y ejecutar la API en tu máquina local:

### 1. Abrir la Terminal

Abre la terminal o consola de tu preferencia y navega hasta la carpeta donde quieras clonar el proyecto.

### 2. Inicializar un Repositorio (Opcional)

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

### 8. Ejecutar los Test
```bash
dotnet test
```
o puedes verlos ejecutando con Visual Studio



## 🌟 Resumen del Proyecto

Esta API de banca fue desarrollada como solución a la prueba técnica propuesta. La aplicación está construida sobre **ASP.NET Core 8 Web API** e implementa las funcionalidades de gestión de clientes, cuentas bancarias y registro de transacciones (depósitos y retiros), utilizando una arquitectura basada en **Servicios** e **Inyección de Dependencias**.

### Tecnologías Clave

* **Framework:** .NET 8
* **Tipo de Proyecto:** ASP.NET Core Web API
* **Persistencia:** Entity Framework Core (para mejor productividad en el desarrollo) con **SQLite** (Base de datos en memoria para el desarrollo/prueba, cumpliendo con el requisito de persistencia).
* **Pruebas Unitarias:**  xUnit y Moq

---

## ⚙️ Arquitectura y Diseño Implementado

Se ha priorizado un diseño limpio y extensible, enfocándose en los siguientes principios:

### 1. Estructura de la Solución

La solución se divide en proyectos lógicos para asegurar la separación de responsabilidades:
* `BancaApi.Api`: Punto de entrada (Endpoints, Controllers).
* `BancaApi.Application`: Contiene la lógica de negocio (Servicios) e inyección de dependencias.
* `BancaApi.Domain`: Contiene los modelos de datos (Entidades) y las reglas de negocio base.
* `BancaApi.Infrastructure`: Implementación de persistencia (EF Core, Repositorios).
* `BancaApi.Tests`: Proyectos de Pruebas Unitarias.

### 2. Documentación y Endpoints

Acceda a la documentación interactiva de la API (Swagger) para probar todas las funcionalidades:

### 3. Pruebas Unitarias

Se implementaron pruebas unitarias exhaustivas con **xUnit** y el *mocking framework* **Moq** para validar la lógica de negocio, cubriendo los siguientes escenarios:
* **La creación de una cuenta bancaria. (testUnitario 1)**
    - **Nombre del Archivo:** `CreateBankAccountCommandHandlerTests.cs`

    - **Prueba que realiza:** 
     **Handle_ValidCommand_CreatesAccountAndReturnsSuccess**

    - La prueba verifica el flujo exitoso de la creación de una cuenta bancaria.

    - Asegura que, cuando se proporciona un ClientId válido y un InitialBalance, el handler:

    - Verifica que el cliente existe.
    Genera un número de cuenta.
    Llama al repositorio para guardar la nueva entidad (AddAccountAsync).
    Retorna un resultado de éxito (IsSuccess es true) con el DTO de respuesta correcto.

* **Las operaciones de depósito y retiro. (testUnitario 2)**

    - **Nombre del Archivo:** `TransactionServiceTests.cs`

    - **Pruebas que realiza:**

1. **ApplyTransaction_Deposit_IncreasesBalance**  
   Verifica que, al realizar un depósito, el saldo de la cuenta aumenta correctamente.  
   - Crea una cuenta con un saldo inicial.  
   - Aplica una transacción de tipo depósito.  
   - Comprueba que el nuevo saldo es la suma del saldo inicial más el monto depositado.

2. **ApplyTransaction_SuccessfulWithdrawal_DecreasesBalance**  
   Comprueba que un retiro válido disminuye el saldo correctamente.  
   - Crea una cuenta con un saldo suficiente.  
   - Aplica una transacción de tipo retiro.  
   - Verifica que el nuevo saldo es el saldo inicial menos el monto retirado.

3. **ApplyTransaction_InsufficientBalanceWithdrawal_ThrowsInvalidOperationException**  
   Valida que no se permita retirar más dinero del que hay disponible.  
   - Crea una cuenta con un saldo menor al monto del retiro.  
   - Intenta realizar la transacción de retiro.  
   - Verifica que se lance una excepción `InvalidOperationException` con el mensaje **“Insufficient balance for withdrawal.”**  
   - Comprueba que el saldo no cambia tras el intento fallido.

4. **ApplyTransaction_InvalidType_ThrowsArgumentOutOfRangeException**  
   Asegura que el servicio maneje correctamente tipos de transacción no válidos.  
   - Crea una cuenta con saldo.  
   - Intenta aplicar una transacción con un tipo no reconocido.  
   - Verifica que se lance una excepción `ArgumentOutOfRangeException`.

* **La aplicación de intereses al saldo de la cuenta. (testUnitario 3)**


**Nombre del Archivo:** `InterestServiceTests.cs`

**Pruebas que realiza:**

1. **ApplyInterest_ShouldIncreaseBalance**  
   Verifica que el servicio aplique correctamente los intereses a una cuenta bancaria.  
   - Crea una cuenta con un saldo inicial.  
   - Aplica una tasa de interés anual del 5%.  
   - Comprueba que el nuevo saldo aumente correctamente en función de la tasa aplicada (por ejemplo, de 1000 a 1050).  
   - Verifica que tanto el valor retornado como el saldo actualizado de la cuenta sean correctos.

2. **ApplyInterest_NegativeRate_ShouldThrow**  
   Garantiza que el sistema maneje adecuadamente tasas de interés inválidas.  
   - Crea una cuenta con un saldo inicial.  
   - Intenta aplicar una tasa de interés negativa.  
   - Verifica que se lance una excepción `ArgumentOutOfRangeException` con el mensaje **“Annual interest rate cannot be negative.”**

* **La consulta de saldo y el historial de transacciones. (testUnitario 4 y 5)**

    - **Nombre del Archivo:** `GetBankAccountByNumberQueryHandlerTests.cs`

    - **Pruebas que realiza:**

1. **Handle_AccountFound_ReturnsSuccessWithDto**  
   Verifica el flujo exitoso cuando una cuenta bancaria existe en el sistema.  
   - Simula que el repositorio (`IBankAccountRepository`) encuentra una cuenta con un número específico.  
   - Usa AutoMapper para convertir la entidad `BankAccountEntity` en un DTO (`BankAccountResDto`).  
   - Comprueba que el resultado (`Result`) sea exitoso (`IsSuccess = true` y `Status = Ok`).  
   - Valida que los datos del DTO coincidan con los esperados.  
   - Confirma que los métodos del repositorio y del mapper se llamen exactamente una vez.

2. **Handle_AccountNotFound_ReturnsNotFoundResult**  
   Evalúa el comportamiento del handler cuando el número de cuenta no existe.  
   - Simula que el repositorio no encuentra ninguna cuenta (retorna `null`).  
   - Verifica que el resultado (`Result`) sea un estado `NotFound`.  
   - Asegura que el mapper **no sea invocado**, ya que no hay entidad que mapear.  
   - Comprueba que el repositorio se haya consultado exactamente una vez.

### Consulta del historial de transacciones de una cuenta bancaria

**Nombre del Archivo:** `GetBankAccountHistoryQueryHandlerTests.cs`

**Pruebas que realiza:**

1. **Handle_AccountFoundWithTransactions_ReturnsSuccessWithHistory**  
   Verifica el comportamiento exitoso cuando la cuenta **existe** y tiene un historial de transacciones.  
   - Simula que el repositorio (`IBankAccountRepository`) devuelve una cuenta con transacciones.  
   - Usa AutoMapper para mapear las entidades `TransactionsEntity` a `TransactionResDto`.  
   - Comprueba que el resultado (`Result`) sea exitoso (`IsSuccess = true`).  
   - Valida que:  
     - El saldo final (`Balance`) coincida con el esperado.  
     - El nombre del cliente y el número de cuenta sean correctos.  
     - Exista la cantidad adecuada de transacciones en el resultado.  
   - Verifica que el método `GetHistoryAsync` del repositorio se llame exactamente una vez.

2. **Handle_AccountFoundNoTransactions_ReturnsSuccessWithEmptyHistory**  
   Prueba el caso donde la cuenta existe pero **no tiene transacciones registradas**.  
   - Simula que el repositorio devuelve una cuenta sin historial.  
   - Verifica que el resultado sea exitoso (`IsSuccess = true`) y que la lista de transacciones esté vacía.  
   - Comprueba que el repositorio se consulte una vez.  
   - Asegura que el mapper **no se invoque**, ya que no hay transacciones que mapear.

3. **Handle_AccountNotFound_ReturnsNotFoundResult**  
   Evalúa el comportamiento cuando el número de cuenta **no existe** en la base de datos.  
   - Simula que el repositorio retorna `null`.  
   - Verifica que el resultado tenga estado `NotFound` y contenga el mensaje `"Bank account not found."`.  
   - Asegura que el mapper no se use y que el repositorio se consulte una sola vez.


---




