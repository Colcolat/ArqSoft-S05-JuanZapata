# Implementación de Patrones GoF - Rama `GOF`

Esta rama contiene la implementación de tres patrones de diseño del Gang of Four (GoF) aplicados a la entidad `Paciente` dentro del proyecto `CitasApp`.

## Patrones Implementados

### 1. Factory Pattern
Se creó la clase `PacienteFactory` para centralizar y encapsular la creación de instancias de pacientes. Esto permite extender fácilmente la creación de diferentes tipos de pacientes en el futuro sin modificar la lógica del controlador.

### 2. Decorator Pattern
Se implementó para añadir responsabilidades y estados dinámicamente a un paciente sin alterar su clase base.
- **`IPaciente`**: Interfaz base.
- **`Paciente`**: Componente concreto.
- **`PacienteDecorator`**: Clase base para los decoradores.
- **`PacienteVIP` / `PacienteUrgente`**: Decoradores concretos que añaden etiquetas a la descripción del paciente.

### 3. Observer Pattern
Se implementó para notificar a otros sistemas o servicios cuando un nuevo paciente es registrado.
- **`PacienteManager`**: Actúa como el *Subject* que mantiene la lista de observadores y notifica cuando se registra un paciente en la base de datos (JSON).
- **`IPacienteObserver`**: Interfaz para los observadores.
- **`EmailNotificador` / `LogNotificador`**: Observadores concretos que simulan el envío de un correo electrónico y el registro de un log en la terminal.

## Endpoints API

Se creó el controlador `PacientesApiController` para exponer la API REST:
- **`GET /api/pacientes`**: Retorna la lista de todos los pacientes.
- **`GET /api/pacientes/{id}`**: Retorna el detalle de un paciente específico.
- **`POST /api/pacientes`**: Registra un nuevo paciente utilizando la Factory, aplica Decorators si se especifica un tipo, e invoca al Observer para enviar notificaciones en consola.

## Ejecución y Pruebas
1. Ejecuta el servidor: `dotnet run` (dentro de la carpeta `CitasApp`).
2. **GET**: Abre en el navegador `http://localhost:<puerto>/api/pacientes`.
3. **POST**: Ejecuta en PowerShell:
   ```powershell
   $body = @{ Nombre="Ana"; Apellido="Torres"; Email="ana@mail.com"; Telefono="987654"; Tipo="VIP" } | ConvertTo-Json
   Invoke-RestMethod -Method POST -Uri "http://localhost:<puerto>/api/pacientes" -Body $body -ContentType "application/json"
   ```
