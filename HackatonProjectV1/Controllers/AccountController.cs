using HackatonProjectV1.Entities;
using HackatonProjectV1.Services;
using HackatonProjectV1.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HackatonProjectV1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly EDevletService _eDevletService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> singInManager, EDevletService eDevletService)
        {
            _userManager = userManager;
            _signInManager = singInManager;
            _eDevletService = eDevletService;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // 1. Model kurallara uyuyor mu? (Email formatı doğru mu, boş mu vs.)
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email,

                    // Yeni alanları buraya ekliyoruz:
                    NameAndLastname = model.NameAndLastname,
                    TcNo = model.TcNo,
                    StudentBarcode = model.StudentBarcode,
                    IsApproved = false // Yeni kullanıcılar başlangıçta onaylı değil
                };

                // 2. Kullanıcıyı oluştur (Şifreyi otomatik hash'ler)
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Başarılıysa kullanıcıyı içeri al (Login yap) ve Anasayfaya gönder
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    //TempData["barcode"] = model.StudentBarcode;                    
                    return RedirectToAction("Verify");
                }

                // Hata varsa (örn: Bu email zaten kayıtlı), hataları modele ekle
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            // Hata varsa formu tekrar göster
            return View(model);
        }

        public IActionResult Verify()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Önce kullanıcı var mı diye e-mail ile kontrol edelim (Opsiyonel ama iyi pratiktir)
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    // PasswordSignInAsync parametreleri:
                    // 1. Kullanıcı Adı (Biz email kullanıyoruz ama Identity username bekler, o yüzden user.UserName veriyoruz)
                    // 2. Şifre
                    // 3. Beni Hatırla (True ise cookie süresi uzar)
                    // 4. Lockout (Yanlış girişte hesabı kilitleyelim mi? Şimdilik false)

                    var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        // Başarılıysa Anasayfaya gönder
                        return RedirectToAction("Index", "Home");
                    }
                }

                // Kullanıcı yoksa veya şifre yanlışsa:
                ModelState.AddModelError("", "Email veya şifre hatalı.");
            }

            return View(model);
            
        }

        //public async Task<IActionResult> Dogrula()
        //{
        //    if (TempData["barcode"] == null)
        //        return RedirectToAction("Index");
        //    var barcode = TempData["barcode"].ToString();            
        //    var sonuc = await _eDevletService.DogrulaAsyn(barcode);
        //    var model = JsonSerializer.Deserialize<BelgeSonuc>(sonuc);
        //    var user = await _userManager.GetUserAsync(User);
        //    user.IsApproved = model.durum;

        //   return RedirectToAction("Index", "Home");
        //}

    }
}   

