function cancelCourse(id) {
    $.ajax({
        url: `/coursePublishRequest/cancel/${id}`,
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