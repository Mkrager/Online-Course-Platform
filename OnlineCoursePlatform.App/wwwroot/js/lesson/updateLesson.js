function updateLesson() {
    const form = document.getElementById('updateLessonForm');
    const formData = new FormData(form);

    $.ajax({
        url: '@Url.Action("Update", "Lesson")',
        type: 'PUT',
        data: formData,
        processData: false,
        contentType: false,
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