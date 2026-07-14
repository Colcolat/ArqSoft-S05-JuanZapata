using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Usuarios()
    {
        var usuarios = new List<(string Id, string Email, string Rol)>();
        foreach (var user in _userManager.Users.ToList())
        {
            var roles = await _userManager.GetRolesAsync(user);
            usuarios.Add((user.Id, user.Email!, roles.FirstOrDefault() ?? "Sin rol"));
        }
        ViewBag.Roles = new[] { "Admin", "Medico", "Paciente" };
        return View(usuarios);
    }

    [HttpPost]
    public async Task<IActionResult> AsignarRol(string userId, string nuevoRol)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();

        var rolesActuales = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, rolesActuales);
        await _userManager.AddToRoleAsync(user, nuevoRol);

        return RedirectToAction("Usuarios");
    }
}
