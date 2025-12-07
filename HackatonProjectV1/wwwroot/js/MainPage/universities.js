document.addEventListener('DOMContentLoaded', () => {
    
    const searchInput = document.getElementById('uniSearchInput');
    const universityRows = document.querySelectorAll('.uni-row'); // Tüm satırları seç
    const noResultMsg = document.getElementById('noResult');

    // --- ARAMA İŞLEVİ ---
    searchInput.addEventListener('input', (e) => {
        
        // Türkçe karakter uyumlu küçük harfe çevirme
        const searchTerm = e.target.value.toLocaleLowerCase('tr').trim();
        let hasResult = false;

        universityRows.forEach(row => {
            // HTML'deki data-name ve data-city özelliklerini al
            const name = row.getAttribute('data-name').toLocaleLowerCase('tr');
            const city = row.getAttribute('data-city').toLocaleLowerCase('tr');

            // İsim veya Şehir aranan kelimeyi içeriyor mu?
            if (name.includes(searchTerm) || city.includes(searchTerm)) {
                // Eşleşeni göster (CSS'teki display:flex yapısını korumak için flex veriyoruz)
                row.style.display = 'flex'; 
                hasResult = true;
            } else {
                // Eşleşmeyeni gizle
                row.style.display = 'none'; 
            }
        });

        // Hiç sonuç yoksa mesajı göster, varsa gizle
        if (hasResult) {
            noResultMsg.style.display = 'none';
        } else {
            noResultMsg.style.display = 'block';
        }
    });

});