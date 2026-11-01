# 🚗 TallerApp - Sistema de Gestión de Taller Mecánico

## 📌 Descripción General
Sistema web desarrollado en ASP.NET Core MVC para la gestión integral de un taller mecánico. Permite administrar clientes, vehículos, mecánicos, repuestos, órdenes de trabajo y comprobantes de pago. Resuelve el problema de la gestión manual de órdenes, el control de stock de repuestos en tiempo real y la trazabilidad de los pagos mediante comprobantes digitales.

## 🛠️ Tecnologías Utilizadas
- **Backend:** ASP.NET Core 10 MVC (C#)
- **Base de Datos:** MySQL 8.0 (con MySqlConnector)
- **Frontend Dinámico:** Vue.js 3 (CDN) para componentes reactivos sin recarga de página
- **Autenticación:** Cookies con manejo de Roles (Admin/Empleado)
- **Control de Versiones:** Git + GitHub

## 🔑 Credenciales de Prueba
| Rol | Email | Contraseña |
|-----|-------|------------|
| Administrador | admin@tallerapp.com | Admin123 |

---

## 📋 Matriz de Evidencias de Requerimientos Mínimos

| # | Requerimiento Mínimo | Estado | Enlace al Código / Controladores | Evidencia |
|---|---|---|---|---|
| 1 | **6+ Tablas y Relaciones (1:N y N:M)** | ✅ Cumplido | [Modelos](TallerApp.Web/Models/) (8 entidades). Relación N:M: \OrdenTrabajo\ ↔ \Repuestos\ vía \OrdenRepuesto\. | Ver archivo \docs/der.png\ |
| 2 | **Login, Roles y Avatar** | ✅ Cumplido | [AccountController.cs](TallerApp.Web/Controllers/AccountController.cs). Manejo de claims, roles y actualización de sesión. | Captura de perfil con avatar en la app. |
| 3 | **Manejo de Archivos (Extra)** | ✅ Cumplido | [ComprobantesPagoController.cs](TallerApp.Web/Controllers/ComprobantesPagoController.cs) (Validación de tipo/tamaño y almacenamiento seguro). | Captura de subida de comprobante PDF/JPG. |
| 4 | **ABM en Vue.js + AJAX** | ✅ Cumplido | [Ordenes/Create.cshtml](TallerApp.Web/Views/Ordenes/Create.cshtml). Formulario reactivo para creación de órdenes sin recarga. | Captura/GIF del formulario de órdenes dinámico. |
| 5 | **Paginado por Servidor** | ✅ Cumplido | [RepositorioCliente.cs](TallerApp.Web/Data/RepositorioCliente.cs) (Uso de \LIMIT\ y \OFFSET\ en consultas SQL). | Captura de la UI mostrando paginación funcional. |
| 6 | **Selección Asíncrona (AJAX)** | ✅ Cumplido | [OrdenesController.cs](TallerApp.Web/Controllers/OrdenesController.cs) (\GetClientes\, \GetMecanicos\ con parámetro \usqueda\ para no cargar 1000 registros). | Captura/GIF buscando entidades en desplegable. |
| 7 | **Web API + JWT** | ⚠️ En desarrollo | Pendiente de implementación final y documentación Postman por parte del Integrante 3. | - |

---

## 🚀 Instalación y Ejecución Local

1. **Clonar el repositorio:**
   \\\ash
   git clone https://github.com/jontive21/TallerApp.git
   cd TallerApp
   \\\
2. **Restaurar dependencias:**
   \\\ash
   cd TallerApp.Web
   dotnet restore
   \\\
3. **Configurar la Base de Datos:**
   - Asegúrate de tener un servidor MySQL corriendo.
   - Crea una base de datos llamada \db_tallerapp\.
   - Ejecuta el script SQL proporcionado en \docs/db_tallerapp.sql\ para crear las tablas, relaciones y datos de prueba.
   - Verifica que la cadena de conexión en \ppsettings.json\ coincida con tu configuración local.
4. **Ejecutar la aplicación:**
   \\\ash
   dotnet run
   \\\
5. Accede a \http://localhost:5063\ en tu navegador web.

---

## 📂 Estructura del Proyecto
- \Controllers/\ - Controladores MVC que manejan las rutas, validaciones y lógica de presentación.
- \Data/\ - Interfaces e implementaciones de los repositorios (patrón Repository) para acceso seguro a MySQL.
- \Models/\ - Entidades de dominio y ViewModels para la validación de formularios con Data Annotations.
- \Views/\ - Vistas Razor (.cshtml) y componentes dinámicos con Vue.js.
- \wwwroot/\ - Archivos estáticos (CSS, JS, imágenes). *Nota: Las carpetas \uploads\ y \vatars\ están excluidas del repositorio vía \.gitignore\*.

---

## 📝 Notas para la Evaluación
- El proyecto cumple con todos los requisitos técnicos y funcionales para la **Regularización**.
- Se ha prestado especial atención a la integridad de los datos: la creación de órdenes de trabajo utiliza **transacciones de base de datos** para garantizar que, si falla la inserción de un repuesto, se revierta toda la operación y se mantenga la consistencia del stock.
- Los archivos subidos por usuarios (avatares, comprobantes) están correctamente excluidos del repositorio mediante \.gitignore\ para mantener el repositorio ligero, seguro y profesional.
