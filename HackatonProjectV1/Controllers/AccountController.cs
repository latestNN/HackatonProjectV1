using HackatonProjectV1.Entities;
using HackatonProjectV1.Services;
using HackatonProjectV1.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    TcNo = model.TcNo,
                    StudentDocumentPath = model.StudentDocumentPath
                };

                // 2. Kullanıcıyı oluştur (Şifreyi otomatik hash'ler)
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Başarılıysa kullanıcıyı içeri al (Login yap) ve Anasayfaya gönder
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
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

        public async Task<IActionResult> Dogrula(string barcode)
        {
            var sonuc = await _eDevletService.DogrulaAsyn("BARKOD_NUMARASI_HERE");

            return Content(sonuc, "application/json");
        }

    }
}   

