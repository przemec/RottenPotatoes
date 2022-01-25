
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using rottenpotatoes.Models;

namespace rottenpotatoes.Controllers
{
  [Authorize]
  public class AccountController : Controller
  {
    private UserManager<IdentityUser> _userManager;
    private SignInManager<IdentityUser> _signInManager;
    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
      _userManager = userManager;
      _signInManager = signInManager;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
      return View(new LoginModel
      {
        ReturnUrl = returnUrl
      });
    }

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
      if (ModelState.IsValid)
      {
        IdentityUser user = await _userManager.FindByNameAsync(loginModel.Nick);
        if (user != null)
        {
          await _signInManager.SignOutAsync();
          if ((await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
          {
            return Redirect(loginModel?.ReturnUrl ?? "/");
          }
        }
      }
      ModelState.AddModelError("", "Nieprawidłowy nick lub hasło");
      return View(loginModel);
    }

    public async Task<RedirectResult> LogoutAsync(string returnUrl = "/")
    {
      await _signInManager.SignOutAsync();
      return Redirect(returnUrl);
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Register(string returnUrl)
    {
      return View(new RegisterModel
      {
        ReturnUrl = returnUrl
      });
    }

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterModel registerModel)
    {
      if (ModelState.IsValid)
      {
        IdentityUser user = await _userManager.FindByNameAsync(registerModel.Nick);
        if (user == null)
        {
          user = new IdentityUser(registerModel.Nick);

          var result = await _userManager.CreateAsync(user, registerModel.Password);
          if (!result.Succeeded)
          {
            ModelState.AddModelError("", "Password must have at least 1 lower and upper case letter, 1 number and 1 special character");
            return View(registerModel);
          }
          await _signInManager.SignOutAsync();
          if ((await _signInManager.PasswordSignInAsync(user, registerModel.Password, false, false)).Succeeded)
          {
            return Redirect(registerModel?.ReturnUrl ?? "/");
          }
        }
      }
      ModelState.AddModelError("", "This nickname exists");
      return View(registerModel);
    }
  }
}