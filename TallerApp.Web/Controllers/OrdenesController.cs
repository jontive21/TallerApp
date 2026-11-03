using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerApp.Web.Data;
using TallerApp.Web.Models;
using System;
using System.Collections.Generic;

namespace TallerApp.Web.Controllers
{
    [Authorize]
    public class OrdenesController : Controller
    {
        private readonly IRepositorioCliente _repoCliente;
        private readonly IRepositorioVehiculo _repoVehiculo;
        private readonly IRepositorioMecanico _repoMecanico;
        private readonly IRepositorioRepuesto _repoRepuesto;
        private readonly IRepositorioOrdenTrabajo _repoOrden;

        public OrdenesController(
            IRepositorioCliente repoCliente,
            IRepositorioVehiculo repoVehiculo,
            IRepositorioMecanico repoMecanico,
            IRepositorioRepuesto repoRepuesto,
            IRepositorioOrdenTrabajo repoOrden)
        {
            _repoCliente = repoCliente;
            _repoVehiculo = repoVehiculo;
            _repoMecanico = repoMecanico;
            _repoRepuesto = repoRepuesto;
            _repoOrden = repoOrden;
        }

        // GET: Ordenes/Create → Esta es la vista con Vue.js
        
        // GET: Ordenes (Lista de órdenes)
        public IActionResult Index()
        {
            return View();
        }

        // GET: Ordenes/Create → Esta es la vista con Vue.js
        public IActionResult Create()
        {
            return View();
        }

        // ========== Endpoints para Vue.js (GET) ==========

        [HttpGet]
public IActionResult GetClientes(string? busqueda = null)
{
    var clientes = _repoCliente.ObtenerTodos(1, 20, busqueda);
    return Json(clientes);
}

        [HttpGet]
        public IActionResult GetVehiculosPorCliente(int clienteId)
        {
            var vehiculos = _repoVehiculo.ObtenerPorCliente(clienteId);
            return Json(vehiculos);
        }

        [HttpGet]
public IActionResult GetMecanicos(string? busqueda = null)
{
    var mecanicos = _repoMecanico.ObtenerTodos(1, 20, busqueda);
    return Json(mecanicos);
}

        [HttpGet]
        public IActionResult GetRepuestos(string? busqueda = null)
        {
            var repuestos = _repoRepuesto.ObtenerTodos(1, 50, busqueda);
            return Json(repuestos);
        }

        // ========== Endpoint para Guardar la Orden (POST) ==========

        [HttpPost]
        public IActionResult CrearOrden([FromBody] CrearOrdenViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // 1. Mapear ViewModel a Entidad OrdenTrabajo
                var orden = new OrdenTrabajo
                {
                    VehiculoId = model.VehiculoId,
                    MecanicoId = model.MecanicoId,
                    FechaIngreso = DateTime.Parse(model.FechaIngreso),
                    FechaEntregaEstimada = string.IsNullOrEmpty(model.FechaEntregaEstimada) ? (DateTime?)null : DateTime.Parse(model.FechaEntregaEstimada),
                    Estado = "Pendiente",
                    DescripcionProblema = model.DescripcionProblema ?? "",
                    Observaciones = model.Observaciones ?? "",
                    IdUsuarioCreacion = 1 // TODO: Obtener del usuario logueado
                };

                // 2. Mapear lista de repuestos
                var detalles = new List<OrdenRepuesto>();
                if (model.Repuestos != null)
                {
                    foreach (var item in model.Repuestos)
                    {
                        detalles.Add(new OrdenRepuesto
                        {
                            RepuestoId = item.RepuestoId,
                            Cantidad = item.Cantidad,
                            PrecioUnitario = item.PrecioUnitario
                        });
                    }
                }

                // 3. Ejecutar transacción en el repositorio
                int nuevaOrdenId = _repoOrden.CrearOrdenConRepuestos(orden, detalles);

                return Json(new { success = true, idOrden = nuevaOrdenId, mensaje = "Orden creada exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, mensaje = "Error al crear la orden: " + ex.Message });
            }
        }
                // ========== Endpoints para ABM en Vue.js (Listado y Baja) ==========

                [HttpGet]
        public IActionResult GetOrdenes()
        {
            var ordenes = _repoOrden.ObtenerTodos();
            return Json(ordenes);
        }

        [HttpDelete]
        public IActionResult EliminarOrden(int id)
        {
            try
            {
                _repoOrden.Eliminar(id);
                return Json(new { success = true, mensaje = "Orden eliminada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, mensaje = "Error: " + ex.Message });
            }
        }
    }
}