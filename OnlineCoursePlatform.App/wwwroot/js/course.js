function deleteCourse(courseId) {
    $.ajax({
        url: '/course/delete/' + courseId,
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