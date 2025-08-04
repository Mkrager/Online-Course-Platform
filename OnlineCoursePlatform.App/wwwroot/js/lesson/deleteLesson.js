function deleteLesson(lessonId) {
    $.ajax({
        url: '/lesson/delete/' + lessonId,
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