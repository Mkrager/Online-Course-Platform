function createApplication(bio, experience) {
    $.ajax({
        url: '/teacherApplication/create',
        type: 'Post',
        contentType: 'application/json',
        data: JSON.stringify({
            bio: bio,
            experience: experience
        }),
        success: function (response) {
            if (response.redirectToUrl) {
                window.location.href = response.redirectToUrl;
            }
            else {
                for (var key in response.errors) {
                    var errorMessage = response.errors[key];
                    $('#' + key).next('.error-text').text(errorMessage);
                }
            }
        },
        error: function (xhr) {
            console.log(xhr.responseText);
        }
    });
}

document.addEventListener("DOMContentLoaded", function () {
    function openApplicationModal(requestId) {
        currentRejectId = requestId;
        document.getElementById("createApplicationModal").style.display = "flex";
    }

    function closeApplicationModal() {
        document.getElementById("createApplicationModal").style.display = "none";
        document.getElementById("bio").value = "";
        document.getElementById("experience").value = "";
    }

    document.getElementById("createApplciation").addEventListener("click", function () {
        const bio = document.getElementById("bio").value;
        const experience = document.getElementById("experience").value;
        if (!bio.trim()) {
            alert("Please enter a bio.");
            return;
        }
        if (!experience.trim()) {
            alert("Please enter a experience.");
            return;
        }
        createApplication(bio, experience);
        closeApplicationModal();
    });

    window.openApplicationModal = openApplicationModal;
    window.closeApplicationModal = closeApplicationModal;
});