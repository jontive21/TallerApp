using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TallerApp.Web.Models;
using TallerApp.Web.Data;

namespace TallerApp.Web.Controllers;

public class HomeController : Controller
{
    private readonly IRepositorioUsuario _repoUsuario;

    public HomeController(IRepositorioUsuario repoUsuario)
    {
        _repoUsuario = repoUsuario;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}