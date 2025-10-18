document.addEventListener("DOMContentLoaded", function () {
    const viewBioBtns = document.querySelectorAll(".btn-view-details");

    viewBioBtns.forEach(btn => {
        btn.addEventListener("click", function () {
            const card = btn.closest(".course-card");

            const modal = card.querySelector(".bioModal");

            modal.style.display = "flex";
        });
    });
});

function closeBioModal(button) {
    const modal = button.closest(".bioModal");
    if (!modal) return;
    modal.style.display = "none";
}

window.addEventListener("click", function (event) {
    if (event.target.classList.contains("bioModal")) {
        event.target.style.display = "none";
    }
});