using Microsoft.AspNetCore.Identity;

namespace HackatonProjectV1.Entities
{
    public class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            
            var userManager = service.GetService<UserManager<AppUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole<int>>>();

            
            await roleManager.CreateAsync(new IdentityRole<int>("Admin"));
            await roleManager.CreateAsync(new IdentityRole<int>("Member"));

            
            var adminUser = await userManager.FindByEmailAsync("admin@gmail.com");

            if (adminUser == null)
            {
                
                var newAdmin = new AppUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    NameAndLastname = "SistemYoneticisi",
                    TcNo = "11111111111", // Zorunlu alanların
                    StudentBarcode = "Yok", // Boş geçmemeli
                    EmailConfirmed = true,
                    IsApproved = true // Admin olduğu için direkt onaylı
                };

                // Kullanıcıyı oluştur ve şifresini belirle
                // Not: Şifre kurallarına uygun olmalı (Büyük, küçük harf, rakam, sembol)
                var result = await userManager.CreateAsync(newAdmin, "Admin123!");

                if (result.Succeeded)
                {
                    // Kullanıcı oluştuysa ona "Admin" rolünü ver
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                }
            }
        }
    }
}
