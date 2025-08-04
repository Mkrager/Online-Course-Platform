function endTest() {
    const userAnswers = [];

    $(".question").each(function () {
        const questionId = $(this).data("question-id");
        const selected = $(this).find("input[type=radio]:checked");

        if (selected.length > 0) {
            userAnswers.push({
                questionId: questionId,
                answerId: selected.val()
            });
        }
    });

    const payload = {
        attempId: $("#attemptId").val(),
        userAnswerDto: userAnswers
    };

    $.ajax({
        url: '/TestAttempt/EndTest',
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(payload),
        success: function (data) {
            if (data.redirectUrl) {
                window.location.href = data.redirectUrl;
            } else {
                alert("Test submitted but no redirect URL received.");
            }
        },
        error: function (xhr, status, error) {
            console.error("AJAX error:", error);
            alert("Something went wrong.");
        }
    });
}
