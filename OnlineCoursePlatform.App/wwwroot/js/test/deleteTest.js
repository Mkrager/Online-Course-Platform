function deleteTest(testId) {
    $.ajax({
        url: '/test/delete/' + testId,
        type: 'DELETE',
        dataType: 'json',
        success: function (response) {
            window.location.href = response.redirectUrl;
        },
        error: function (error) {
            console.error('Error:', error);
            alert('Error');
        }
    });
}