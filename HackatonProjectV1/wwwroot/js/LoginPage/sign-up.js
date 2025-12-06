document.addEventListener('DOMContentLoaded', () => {
    /* --- FORM ELEMANLARI --- */
    const signupForm = document.getElementById('signup-form');
    
    // Şifre Alanları ve İkonlar
    const passwordInput = document.getElementById('password');
    const togglePasswordIcon = document.getElementById('togglePassword');
    
    const passwordConfirmInput = document.getElementById('password-confirm');
    const toggleConfirmIcon = document.getElementById('togglePasswordConfirm');

    // 1. Şifre Göster/Gizle (Ana Şifre)
    togglePasswordIcon.addEventListener('click', () => {
        const type = passwordInput.getAttribute('type') === 'password' ? 'text' : 'password';
        passwordInput.setAttribute('type', type);
        togglePasswordIcon.classList.toggle('fa-eye');
        togglePasswordIcon.classList.toggle('fa-eye-slash');
    });

    // 2. Şifre Göster/Gizle (Şifre Onayla)
    toggleConfirmIcon.addEventListener('click', () => {
        const type = passwordConfirmInput.getAttribute('type') === 'password' ? 'text' : 'password';
        passwordConfirmInput.setAttribute('type', type);
        toggleConfirmIcon.classList.toggle('fa-eye');
        toggleConfirmIcon.classList.toggle('fa-eye-slash');
    });

    // Dosya Yükleme İsim Gösterme
    const fileInput = document.getElementById('student-doc');
    const fileNameDisplay = document.getElementById('file-name');

    fileInput.addEventListener('change', function() {
        if (this.files && this.files.length > 0) {
            fileNameDisplay.textContent = this.files[0].name;
            fileNameDisplay.style.color = "#ffffff"; 
        } else {
            fileNameDisplay.textContent = "Dosya seçilmedi";
            fileNameDisplay.style.color = "#8b949e";
        }
    });

    // Kayıt Formu Submit
    signupForm.addEventListener('submit', (e) => {
        e.preventDefault();
        
        const password = document.getElementById('password').value;
        const passwordConfirm = document.getElementById('password-confirm').value;
        const tcno = document.getElementById('tcno').value;

        if (password !== passwordConfirm) {
            alert("Şifreler eşleşmiyor!");
            return;
        }

        if (tcno.length !== 11) {
            alert("TC Kimlik Numarası 11 haneli olmalıdır.");
            return;
        }

        // DEĞİŞİKLİK BURADA:
        // verify.html sayfasına yönlendiriyoruz
        window.location.href = "verify.html";
    });

    /* --- SLIDER (ŞELALE EFEKTİ) --- */
    const slides = document.querySelectorAll('.text-slide');
    let currentSlide = 0;
    const slideInterval = 3000;

    function nextSlide() {
        const activeSlide = slides[currentSlide];
        activeSlide.classList.remove('active');
        activeSlide.classList.add('exit');

        currentSlide = (currentSlide + 1) % slides.length;
        const nextSlideEl = slides[currentSlide];

        nextSlideEl.classList.remove('exit');
        void nextSlideEl.offsetWidth; 
        nextSlideEl.classList.add('active');

        setTimeout(() => {
            activeSlide.classList.remove('exit');
        }, 800); 
    }

    setInterval(nextSlide, slideInterval);
});