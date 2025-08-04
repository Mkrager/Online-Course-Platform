function updateTest() {
    const form = document.getElementById('updateTestForm');
    const formData = new FormData(form);

    $.ajax({
        url: '/test/update',
        type: 'Put',
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            if (response.redirectToUrl) {
                window.location.href = response.redirectToUrl;
            } else {
                $('.error-text').text('');

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
