# Propuesta de Proyecto Integrador Final - TallerApp

## 1. Datos del Grupo

| Apellido | Nombre | Github |
|----------|--------|-------|
| Roldán | Saúl | GitHub: saul28roldan-pixel |
| Ontiveros | José | GitHub: jontive21 |
|  Orozco| Miguel | GitHub: BLACK1895 |

*(Reemplazar con los datos reales de los integrantes del grupo)*

## 2. Título del Proyecto

**TallerApp - Sistema de Gestión Integral para Talleres Mecánicos**

## 3. Temática Elegida

Sistema web MVC para la gestión de órdenes de trabajo, clientes, vehículos, mecánicos y repuestos en un taller mecánico.

## 4. Descripción de la Problemática

Los talleres mecánicos pequeños y medianos suelen gestionar sus órdenes de trabajo, turnos, repuestos y facturación de manera manual o mediante planillas de cálculo dispersas. Esto genera:
- Pérdida de trazabilidad sobre el estado de cada vehículo en reparación.
- Dificultad para auditar qué mecánico realizó cada trabajo y qué repuestos utilizó.
- Errores en el stock de repuestos por falta de control en tiempo real.
- Imposibilidad de generar reportes de productividad por mecánico o de rentabilidad por tipo de trabajo.

## 5. Propuesta de Solución

Se propone desarrollar **TallerApp**, un sistema web MVC que permita:
- Gestionar el alta, baja y modificación de clientes, vehículos, mecánicos y repuestos.
- Crear **Órdenes de Trabajo** que vinculen un vehículo, un mecánico y los repuestos utilizados, con seguimiento de estado (Pendiente → En Proceso → Finalizada → Entregada).
- Controlar el stock de repuestos de forma automática al asignarlos a una orden.
- Registrar comprobantes de pago asociados a cada orden (manejo de archivos).
- Implementar un **módulo SPA con Vue.js** para la gestión ágil de órdenes de trabajo sin recargas de página.
- Exponer una **Web API REST con JWT** para consulta de órdenes y reportes.
- Control de roles: **Administrador** (acceso total), **Mecánico** (solo ve sus órdenes asignadas), **Recepcionista** (gestiona clientes y turnos).

## 6. Modelo de Datos

### Entidades principales (8 tablas)

| Entidad | Descripción |
|---------|-------------|
| **Usuario** | IdUsuario, NombreCompleto, Email, PasswordHash, Rol, Avatar |
| **Cliente** | IdCliente, NombreCompleto, DNI, Telefono, Email, Direccion |
| **Vehiculo** | IdVehiculo, Patente, Marca, Modelo, Anio, Color, ClienteId (FK) |
| **Mecanico** | IdMecanico, NombreCompleto, Matricula, Especialidad, Email, Telefono |
| **Repuesto** | IdRepuesto, Nombre, Codigo, Precio, Stock, Foto (archivo) |
| **OrdenTrabajo** | IdOrden, VehiculoId (FK), MecanicoId (FK), FechaIngreso, FechaEntrega, Estado, Descripcion, IdUsuarioCreacion |
| **OrdenRepuesto** | IdOrdenRepuesto, OrdenId (FK), RepuestoId (FK), Cantidad, PrecioUnitario (relación N:M) |
| **ComprobantePago** | IdComprobante, OrdenId (FK), Monto, FechaPago, ArchivoComprobante, Estado, IdUsuarioCreacion |

### Descripción de Relaciones
- **Cliente → Vehículo (1:N):** Un cliente puede tener múltiples vehículos registrados.
- **OrdenTrabajo → Vehículo + Mecánico:** Cada orden pertenece a un vehículo y está asignada a un mecánico.
- **OrdenTrabajo ↔ Repuesto (N:M):** Una orden usa múltiples repuestos, y un repuesto se usa en muchas órdenes (vía tabla intermedia `OrdenRepuesto`).
- **OrdenTrabajo → ComprobantePago (1:N):** Una orden puede tener múltiples comprobantes de pago.

## 7. Diagrama Entidad-Relación (DER)

![Diagrama Entidad-Relación](docs/DER_TallerApp.drawio.png)