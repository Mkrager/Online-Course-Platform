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
        url: '/coursePublishRequest/reject',
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

const CoursePublishStatusText = {
    0: "Pending",
    1: "Approved",
    2: "Rejected"
};

function getFilteredCoursePublishRequests(status) {
    $.ajax({
        url: `/coursePublishRequest/listFiltered?status=${status}`,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            const container = document.getElementById('courseRequestContainer');
            container.innerHTML = '';

            if (response.data && response.data.length > 0) {
                response.data.forEach(request => {
                    const card = document.createElement('div');
                    card.className = 'course-card';

                    const statusText = CoursePublishStatusText[request.status] || request.status;

                    let statusHtml = '';
                    let actionsHtml = '';

                    if (statusText === "Approved") {
                        statusHtml = `<p class="author">Approved By: ${request.processedName} (${new Date(request.processedAt).toLocaleDateString()})</p>`;
                    } else if (statusText === "Rejected") {
                        statusHtml = `<p class="author">Rejected By: ${request.processedName} (${new Date(request.processedAt).toLocaleDateString()})</p>
                                      <p class="author">Rejected reason: ${request.rejectReason}</p>`;
                    } else {
                        statusHtml = `<p class="author">Status: ${statusText}</p>`;
                        actionsHtml = `
                            <div class="course-request-actions">
                                <button class="btn-lessons approve-btn" data-id="${request.id}">Approve</button>
                                <button class="btn-lessons btn-danger reject-btn" data-id="${request.id}">Reject</button>
                            </div>
                        `;
                    }

                    card.innerHTML = `
                        <h3 class="title">Course:<a href="/course/details/${request.courseId}" class="btn-submit btn-view-details">View details</a>
                        </h3>
                        <div class="category-section">
                            <span class="category">Requested By: ${request.requestedName || request.requestedBy}</span>
                            <span class="category">Date: ${new Date(request.requestedDate).toLocaleDateString()}</span>
                        </div>
                        ${statusHtml}
                        ${actionsHtml}
                    `;

                    container.appendChild(card);
                });

                document.querySelectorAll('.approve-btn').forEach(btn => {
                    btn.addEventListener('click', function () {
                        const id = this.dataset.id;
                        approveCourse(id);
                    });
                });

                document.querySelectorAll('.reject-btn').forEach(btn => {
                    btn.addEventListener('click', function () {
                        const id = this.dataset.id;
                        openRejectModal(id);
                    });
                });
            } else {
                container.innerHTML = '<p>No requests found.</p>';
            }
        },
        error: function (xhr) {
            console.log(xhr.responseText);
        }
    });
}

document.querySelectorAll('button[data-status]').forEach(button => {
    button.addEventListener('click', function () {
        const status = this.dataset.status;
        getFilteredCoursePublishRequests(status);
    });
});


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