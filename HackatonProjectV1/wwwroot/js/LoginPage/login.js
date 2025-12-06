document.addEventListener('DOMContentLoaded', () => {
    /* --- FORM İŞLEMLERİ --- */
    const loginForm = document.getElementById('login-form');
    const passwordInput = document.getElementById('password');
    const togglePasswordIcon = document.getElementById('togglePassword');

    // Şifre Göster/Gizle
    togglePasswordIcon.addEventListener('click', () => {
        const type = passwordInput.getAttribute('type') === 'password' ? 'text' : 'password';
        passwordInput.setAttribute('type', type);
        togglePasswordIcon.classList.toggle('fa-eye');
        togglePasswordIcon.classList.toggle('fa-eye-slash');
    });

    // Form Submit
    loginForm.addEventListener('submit', (e) => {
        e.preventDefault();
        alert("Giriş işlemi simüle edildi.");
    });

    /* --- GELİŞMİŞ SLIDER (ŞELALE EFEKTİ) --- */
    const slides = document.querySelectorAll('.text-slide');
    let currentSlide = 0;
    const slideInterval = 3000; // 3 Saniye bekleme süresi

    function nextSlide() {
        // 1. Mevcut slaytı al
        const activeSlide = slides[currentSlide];
        
        // 2. Mevcut slayttan 'active'i silip 'exit' ekle (Aşağı kayıp yok olsun)
        activeSlide.classList.remove('active');
        activeSlide.classList.add('exit');

        // 3. Bir sonraki slaytı belirle
        currentSlide = (currentSlide + 1) % slides.length;
        const nextSlideEl = slides[currentSlide];

        // 4. Yeni slayta 'active' ver (Yukarıdan gelip yerleşsin)
        // Önce temizlik: Eğer üzerinde 'exit' sınıfı kalmışsa sil (başa dönenler için)
        nextSlideEl.classList.remove('exit');
        void nextSlideEl.offsetWidth; // CSS'i tetiklemek için (Magic trick)
        nextSlideEl.classList.add('active');

        // 5. Temizlik: Eski slayt tamamen kaybolduktan sonra (0.8sn) 'exit' sınıfını silip başa (yukarı) alalım
        setTimeout(() => {
            activeSlide.classList.remove('exit');
        }, 800); // CSS transition süresiyle (0.8s) aynı olmalı
    }

    // Döngüyü başlat
    setInterval(nextSlide, slideInterval);
});