function approveCourse(id) {
    $.ajax({
        url: `/coursePublishRequest/approve/${id}`,
        type: 'PUT',
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
function rejectCourse(id, rejectReason) {
    $.ajax({
        url: '/coursePublishRequest/reject/',
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify({
            id: id,
            rejectReason: rejectReason
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
    let currentRejectId = null;

    function openRejectModal(requestId) {
        currentRejectId = requestId;
        document.getElementById("rejectModal").style.display = "flex";
    }

    function closeRejectModal() {
        document.getElementById("rejectModal").style.display = "none";
        document.getElementById("rejectReason").value = "";
    }

    document.getElementById("confirmReject").addEventListener("click", function () {
        const reason = document.getElementById("rejectReason").value;
        if (!reason.trim()) {
            alert("Please enter a reason for rejection.");
            return;
        }
        rejectCourse(currentRejectId, reason);
        closeRejectModal();
    });

    window.openRejectModal = openRejectModal;
    window.closeRejectModal = closeRejectModal;
});
