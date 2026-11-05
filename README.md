TallerApp - Sistema de Gestión de Taller Mecánico
Descripción
Sistema web MVC en ASP.NET Core para la gestión integral de un taller mecánico: órdenes de trabajo, clientes, vehículos, mecánicos, repuestos y comprobantes de pago, con control de roles y seguridad en el backend.
Tecnologías
Backend: ASP.NET Core MVC (.NET 10)
Base de datos: MySQL (XAMPP)
Frontend: HTML, CSS, Bootstrap, Vue.js
API: Web API REST con JWT
Control de versiones: Git + GitHub
Credenciales de Prueba
Rol
Email
Contraseña
Administrador
admin@tallerapp.com
Admin123
Empleado
empleado@tallerapp.com
Admin123
Matriz de Evidencias de Requerimientos
#
Requerimiento
Estado
Código
Evidencia
1
6+ Tablas y Relaciones (1:N, N:M)
✅
Data/
docs/der.png
2
Login/Logout funcional
✅
AccountController.cs
docs/evidencia_login_admin.png
3
Seguridad y manejo de roles
✅
Program.cs
docs/evidencia_usuarios_roles.png
4
Funcionalidad restringida por rol
✅
ComprobantesPagoController.cs
docs/evidencia_comprobantes_admin.png y docs/evidencia_acceso_denegado.png
5
Avatar personalizado
✅
AccountController.cs
docs/evidencia_perfil_avatar.png
6
Manejo de Archivos
✅
ComprobantesPagoController.cs
docs/evidencia_comprobante.gif
7
ABM en Vue.js + AJAX
✅
Views/
docs/evidencia_vue_abm.gif
8
Paginado por Servidor
✅
Models/Paginador.cs
docs/evidencia_paginado.png
9
Búsqueda Asíncrona AJAX
✅
OrdenesController.cs
docs/evidencia_ajax_busqueda.gif
10
Web API REST con JWT
✅
ApiOrdenesController.cs
docs/evidencia_api_bearer.png
Estructura del Proyecto
TallerApp.Web/Controllers/ - Controladores MVC y API
TallerApp.Web/Data/ - Repositorios e interfaces
TallerApp.Web/Models/ - Entidades y ViewModels
TallerApp.Web/Views/ - Vistas Razor y Vue.js
TallerApp.Web/wwwroot/ - Archivos estáticos
docs/ - DER, SQL, evidencias y Postman
Instalación y Ejecución
Clonar el repositorio.
Iniciar XAMPP (Apache + MySQL en puerto 3306).
Importar docs/db_tallerapp.sql en phpMyAdmin.
Ejecutar: cd TallerApp.Web y luego dotnet run
Acceder a http://localhost:5063 e iniciar sesión.
Colección Postman
Disponible en docs/TallerApp_API.postman_collection.json para probar la Web API con JWT.
Grupo 36 - Laboratorio de Programación II - Proyecto Integrador Final 2026