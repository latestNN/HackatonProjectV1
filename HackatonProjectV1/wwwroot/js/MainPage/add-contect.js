document.addEventListener('DOMContentLoaded', () => {
    
    // --- ELEMENTLER ---
    const addContentForm = document.getElementById('addContentForm');

    // --- FORM GÖNDERME ---
    if (addContentForm) {
        addContentForm.addEventListener('submit', (e) => {
            e.preventDefault(); // Sayfa yenilenmesini engelle

            // Verileri al
            const title = document.getElementById('postTitle').value;
            const category = document.getElementById('postCategory').value;
            const unit = document.getElementById('relatedUnit').value;
            const content = document.getElementById('postContent').value;

            // Basit doğrulama
            if (title && category && unit && content) {
                // Burada normalde verileri Backend'e gönderirsin.
                // Biz şimdilik başarılı sayfasına yönlendiriyoruz.
                window.location.href = 'success.html';
            } else {
                alert("Lütfen tüm alanları doldurunuz.");
            }
        });
    }
});