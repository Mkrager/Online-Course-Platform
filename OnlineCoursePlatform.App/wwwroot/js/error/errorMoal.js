document.addEventListener("DOMContentLoaded", () => {
    const popup = document.getElementById('errorPopup');
    const message = document.getElementById('errorMessage').textContent.trim();

    if (message) {
        popup.style.display = 'flex';

        setTimeout(() => {
            popup.style.display = 'none';
        }, 2000);
    }
});