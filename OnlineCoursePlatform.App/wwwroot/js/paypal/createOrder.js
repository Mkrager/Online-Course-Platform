function createOrder(courseId) {
    $.ajax({
        url: '/payPal/createOrder?courseId=' + courseId,
        type: 'POST',
        success: function (result) {
            if (result.isSuccess) {
                window.open(
                    result.data,
                    '_blank',
                    'width=1000,height=800,top=100,left=200,toolbar=no,menubar=no,scrollbars=yes,resizable=yes'
                );
            } else {
                alert('Error: ' + result.errorText);
            }
        },
        error: function (xhr, status, error) {
            console.error('AJAX Error:', error);
            alert('Error occured');
        }
    });
}