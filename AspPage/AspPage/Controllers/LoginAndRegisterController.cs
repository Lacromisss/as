using AspPage.Models;
using AspPage.Vm;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspPage.Controllers
{
    public class LoginAndRegisterController : Controller
    {
        private readonly UserManager<AppUser> _user;
        private readonly SignInManager<AppUser> _sign;

        public LoginAndRegisterController(UserManager<AppUser> user,SignInManager<AppUser> sign)
        {
            _user = user;
            _sign = sign;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Index(RegisterVm registerVm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser appUser = new AppUser {
                UserName = registerVm.UserName,
                FirstName = registerVm.FirstName,
                LastName = registerVm.LastName,
                Email = registerVm.Email,
                

            
            
            };
            IdentityResult result = await _user.CreateAsync(appUser, registerVm.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("",item.Description);

                }

            }
            await _sign.SignInAsync(appUser,true);

            return RedirectToAction("Home" ,"Index");
        }
    }
}
