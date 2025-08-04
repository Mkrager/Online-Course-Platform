let questionCount = 0;
function addQuestion() {
    const index = questionCount++;
    const questionBlock = document.createElement('div');
    questionBlock.classList.add('question');
    questionBlock.setAttribute('data-index', index);

    questionBlock.innerHTML = `
            <div class="question-header">
                <input type="text" name="Questions[${index}].Text" placeholder="Question Text" />
                <span onclick="toggleQuestion(${index})"><ion-icon name="caret-down-outline"></ion-icon></span>
            </div>
            <div class="answers">
                <div class="answer">
                    <input type="checkbox" name="Questions[${index}].Answers[0].IsCorrect" value="true" />
                    <input type="text" name="Questions[${index}].Answers[0].Text" placeholder="Answer Option" />
                </div>
                <button type="button" class="btn" onclick="addAnswer(${index}, this)">+ Add Answer</button>
                <button type="button" class="btn btn-danger" onclick="removeQuestion(this)">Delete Question</button>
            </div>
        `;

    document.getElementById('questions').appendChild(questionBlock);
}

function addAnswer(questionIndex, btn) {
    const answersDiv = btn.closest('.answers');
    const index = answersDiv.querySelectorAll('.answer').length;
    const answerDiv = document.createElement('div');
    answerDiv.classList.add('answer');

    answerDiv.innerHTML = `
        <input type="checkbox" name="Questions[${questionIndex}].Answers[${index}].IsCorrect" value="true" />
        <input type="text" name="Questions[${questionIndex}].Answers[${index}].Text" placeholder="Answer Option" />
    `;

    answersDiv.insertBefore(answerDiv, btn);
}

function toggleQuestion(index) {
    const block = document.querySelector(`[data-index="${index}"]`);
    block.classList.toggle('collapsed');

    const icon = block.querySelector('.question-header ion-icon');
    const isCollapsed = block.classList.contains('collapsed');

    icon.setAttribute('name', isCollapsed ? 'caret-back-outline' : 'caret-down-outline');
}

function removeQuestion(button) {
    const block = button.closest('.question');
    block.remove();
}
