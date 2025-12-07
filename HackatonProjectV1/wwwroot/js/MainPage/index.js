document.addEventListener('DOMContentLoaded', () => {
    
    // --- ELEMENT SEÇİMLERİ ---
    const allContents = document.querySelectorAll('.content-section');
    const allTabs = document.querySelectorAll('.tab'); 
    const dropdownContainers = document.querySelectorAll('.dropdown-container');
    const dropdownItems = document.querySelectorAll('.dropdown-item');
    const departmentMenu = document.getElementById('department-menu');
    const departmentItems = departmentMenu ? departmentMenu.querySelectorAll('.dropdown-item') : [];

    // --- 1. DROPDOWN AÇMA / KAPAMA MANTIĞI ---
    dropdownContainers.forEach(container => {
        const btn = container.querySelector('.dropdown-btn');
        const menu = container.querySelector('.dropdown-menu');

        btn.addEventListener('click', (e) => {
            e.stopPropagation(); 
            // Diğer açık olan dropdownları kapat
            dropdownContainers.forEach(other => {
                if (other !== container) {
                    other.querySelector('.dropdown-menu').classList.remove('show');
                    other.classList.remove('active');
                }
            });
            // Tıklananı aç/kapat
            menu.classList.toggle('show');
            container.classList.toggle('active');
        });
    });

    // Sayfa boşluğuna tıklayınca menüyü kapat
    document.addEventListener('click', () => {
        dropdownContainers.forEach(container => {
            container.querySelector('.dropdown-menu').classList.remove('show');
            container.classList.remove('active');
        });
    });

    // --- 2. ÜNİVERSİTE BUTONU (RESETLEME) ---
    const universityBtn = document.getElementById('btn-university');
    if (universityBtn) {
        universityBtn.addEventListener('click', () => {
            allTabs.forEach(t => t.classList.remove('active'));
            document.querySelectorAll('.dropdown-btn').forEach(btn => btn.classList.remove('active'));
            universityBtn.classList.add('active');
            
            showContent('content-universite');
            filterDepartments('all');
        });
    }

    // --- 3. DROPDOWN SEÇİMİ VE İÇERİK FİLTRELEME ---
    dropdownItems.forEach(item => {
        item.addEventListener('click', (e) => {
            e.stopPropagation(); 
            const parentContainer = item.closest('.dropdown-container');
            const parentBtn = parentContainer.querySelector('.dropdown-btn');

            // Tab görselliğini ayarla
            allTabs.forEach(t => t.classList.remove('active'));
            parentBtn.classList.add('active');

            // Hedef içeriği belirle
            const targetId = item.getAttribute('data-target');
            const filterCategory = item.getAttribute('data-filter');

            // Bölüm listesini filtrele (Eğer fakülte seçildiyse)
            if (filterCategory) {
                filterDepartments(filterCategory);
            } 
            
            // İçeriği Göster
            showContent(targetId, filterCategory);

            // Menüyü kapat
            parentContainer.querySelector('.dropdown-menu').classList.remove('show');
            parentContainer.classList.remove('active');
        });
    });

    // --- YARDIMCI FONKSİYONLAR ---
    function filterDepartments(category) {
        departmentItems.forEach(deptItem => {
            const deptCategory = deptItem.getAttribute('data-category');
            if (category === 'all') {
                deptItem.style.display = 'block';
            } else {
                if (deptCategory === category) deptItem.style.display = 'block';
                else deptItem.style.display = 'none';
            }
        });
    }

    function showContent(targetId, facultyFilter = null) {
        allContents.forEach(content => { content.classList.remove('active'); });

        if (targetId === 'content-universite') {
            allContents.forEach(c => c.classList.add('active'));
        } 
        else if (facultyFilter) {
            // Fakülte seçildiyse hem fakülteyi hem ona bağlı bölümleri göster
            const mainContent = document.getElementById(targetId);
            if (mainContent) mainContent.classList.add('active');

            departmentItems.forEach(deptItem => {
                if (deptItem.getAttribute('data-category') === facultyFilter) {
                    const deptContentId = deptItem.getAttribute('data-target');
                    const deptContentDiv = document.getElementById(deptContentId);
                    if (deptContentDiv) deptContentDiv.classList.add('active');
                }
            });
        } 
        else {
            // Tekil seçim
            const targetContent = document.getElementById(targetId);
            if (targetContent) targetContent.classList.add('active');
        }
    }

    // Başlangıç Ayarı
    showContent('content-universite'); 
    filterDepartments('all'); 

    // --- 4. SAĞ PANEL FORM ---
    const filterForm = document.getElementById('filterForm');
    if(filterForm) {
        filterForm.addEventListener('submit', (e) => {
            e.preventDefault(); 
            const selectedTags = [];
            filterForm.querySelectorAll('input[type="checkbox"]:checked').forEach(box => selectedTags.push(box.value));
            console.log("Seçilen Etiketler:", selectedTags);
            alert("Filtreleme uygulandı: " + selectedTags.join(", "));
        });
    }

    // --- 5. BEĞENİ BUTONLARI (MUTUAL EXCLUSIVE) ---
    // Bu kod dinamik olarak eklenen kartlarda da çalışması için event delegation veya 
    // statik kartlar için doğrudan döngü kullanabilir. Şimdilik statik kartlar için:
    const postCards = document.querySelectorAll('.post-card');

    postCards.forEach(card => {
        const likeBtn = card.querySelector('.btn-like');
        const dislikeBtn = card.querySelector('.btn-dislike');

        if(likeBtn && dislikeBtn) {
            likeBtn.addEventListener('click', (e) => {
                e.stopPropagation(); // Karta tıklamayı engelle (Detay sayfasına gitmesin)
                if (likeBtn.classList.contains('active')) {
                    likeBtn.classList.remove('active');
                    likeBtn.querySelector('i').classList.replace('fa-solid', 'fa-regular');
                } else {
                    likeBtn.classList.add('active');
                    likeBtn.querySelector('i').classList.replace('fa-regular', 'fa-solid');
                    
                    dislikeBtn.classList.remove('active');
                    dislikeBtn.querySelector('i').classList.replace('fa-solid', 'fa-regular');
                }
            });

            dislikeBtn.addEventListener('click', (e) => {
                e.stopPropagation(); // Karta tıklamayı engelle
                if (dislikeBtn.classList.contains('active')) {
                    dislikeBtn.classList.remove('active');
                    dislikeBtn.querySelector('i').classList.replace('fa-solid', 'fa-regular');
                } else {
                    dislikeBtn.classList.add('active');
                    dislikeBtn.querySelector('i').classList.replace('fa-regular', 'fa-solid');
                    
                    likeBtn.classList.remove('active');
                    likeBtn.querySelector('i').classList.replace('fa-solid', 'fa-regular');
                }
            });
        }
        
        // Yorum butonuna tıklanınca da detay sayfasına gitmesin ama işlem yapsın
        const commentBtn = card.querySelector('.btn-comment');
        if(commentBtn) {
            commentBtn.addEventListener('click', (e) => {
                e.stopPropagation();
                // Yorum sayfasına gitmesi için kartın tıklama olayını tetikletebiliriz 
                // veya özel bir işlem yapabiliriz. Şimdilik pasif.
            });
        }
    });

    // --- 6. KARTLARA TIKLAYINCA DETAY SAYFASINA GİT (YENİ) ---
    //postCards.forEach(card => {
    //    card.addEventListener('click', (e) => {
    //        // Eğer tıklanan yer bir butonsa (yukarıda stopPropagation yaptık ama garanti olsun)
    //        //if (e.target.closest('.action-btn')) return;

    //        //// Karttaki verileri topla
    //        //const title = card.querySelector('.post-title').innerText;
    //        //const content = card.querySelector('.post-content').innerText;
    //        //const username = card.querySelector('.username').innerText;
    //        //const time = card.querySelector('.time').innerText;
    //        //const badge = card.querySelector('.badge').innerText;
    //        //const badgeClass = card.querySelector('.badge').className; 
    //        //const avatarText = card.querySelector('.avatar').innerText;
    //        //const avatarClass = card.querySelector('.avatar').className; 

    //        //// Verileri URL Parametresi yap
    //        //const params = new URLSearchParams();
    //        //params.append('title', title);
    //        //params.append('content', content);
    //        //params.append('user', username);
    //        //params.append('time', time);
    //        //params.append('badge', badge);
    //        //params.append('badgeClass', badgeClass);
    //        //params.append('avText', avatarText);
    //        //params.append('avClass', avatarClass);

    //        // Yönlendir
    //        window.location.href = 'https://www.youtube.com/';
    //    });
    //});
});