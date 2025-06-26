PersonaAPI - API REST
Una API REST para la gestión de personas, implementando operaciones CRUD completas con validaciones de negocio y arquitectura limpia.

🚀 Características
- CRUD completo: Crear, leer, actualizar y eliminar personas
- Filtrado avanzado: Búsqueda por nombre, apellido o email
- Validaciones de negocio: Email único, edad mínima, fechas válidas
- Manejo robusto de excepciones: Respuestas consistentes y control de errores
- Arquitectura limpia: Separación clara de responsabilidades
- Autenticación: Sistema de token bearer para seguridad
- Base de datos PostgreSQL: Integración completa con Entity Framework Core

🛠️ Tecnologías utilizadas
- .NET 7
- Entity Framework Core
- PostgreSQL
- ASP.NET Core Web API
- C#

📋 Prerrequisitos
- .NET 7 SDK o superior
- PostgreSQL 12 o superior
- Visual Studio 2022 / Visual Studio Code (opcional)
- Git

⚙️ Configuración e instalación

1. Clonar el repositorio
bash
git clone https://github.com/reyesEspinozaE/PersonaAPI.git
cd PersonaAPI

2. Configurar la base de datos
Crear una base de datos PostgreSQL llamada "prueba_tecnica_datasys"
Ejecutar el script SQL incluido en /Database/script.sql

Actualizar la cadena de conexión en appsettings.json:
json

"ConnectionStrings": {
  "ConnectionDB": "Host=localhost;Database=prueba_tecnica_datasys;Username=postgres;Password="su_contraseña";Port=5432"
},

3. Ejecutar la aplicación
   
La API estará disponible en: https://localhost:7163

📊 Estructura de la base de datos

Tabla: personas

Campo	              Tipo	          Descripción
id_persona	        int	            Clave primaria (auto-incremental)
nombre	            varchar(100)	  Nombre de la persona
apellido	          varchar(100)	  Apellido de la persona
fecha_nacimiento	  date	          Fecha de nacimiento
email	              varchar(255)	  Email único
telefono	          varchar(20)	    Teléfono (opcional)
direccion	          varchar(500)	  Dirección (opcional)
fecha_registro	    timestamp	      Fecha de registro automática

🔗 Endpoints disponibles

Autenticación
Todas las peticiones deben incluir el header:

Authorization: Bearer KvoVsjSoPGH9ojSB3x3QE4BVWl4m6unW6VTwpPoXZI

Es decir si deseas realizar pruebas a los endpoints en Swagger, primero debes ingresar el token en la sección de "Authorization", o de lo contrario recibiras un error 401 'Unauthorized'.
Esto se realiza de esta manera por temas de seguridad.

Personas
Método	                Endpoint	            Descripción
GET	                    /api/personas	        Obtener todas las personas
GET	                    /api/personas/{id}	  Obtener persona por ID
POST	                  /api/personas	        Crear nueva persona
PUT	                    /api/personas/{id}	  Actualizar persona
DELETE	                /api/personas/{id}	  Eliminar persona
GET	                    /api/personas/buscar	Filtrar personas

🔧 Validaciones implementadas

Email único: No se permiten emails duplicados
Edad mínima: La persona debe ser mayor de 18 años
Fechas válidas: La fecha de nacimiento no puede ser futura
Campos requeridos: Nombre, apellido, fecha de nacimiento y email
Formato de email: Validación en el servicio

🚨 Manejo de errores
La API maneja los siguientes códigos de estado:

200 OK: Operación exitosa
201 Created: Recurso creado exitosamente
400 Bad Request: Datos inválidos o faltantes
404 Not Found: Recurso no encontrado
500 Internal Server Error: Error interno del servidor

🧪 Pruebas
Para probar la API, puedes usar (Recuerda ingresar el token de autorización):

Swagger UI: Disponible en https://localhost:7163/swagger 

📝 Licencia
Este proyecto es para fines de evaluación técnica.

👨‍💻 Autor
Everth Reyes Espinoza

GitHub: @reyesEspinozaE
Desarrollado como parte de una prueba técnica - 2025

