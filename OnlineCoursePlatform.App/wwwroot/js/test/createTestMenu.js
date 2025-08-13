let questionCount = document.querySelectorAll('.question').length;

document.getElementById('addQuestionBtn').addEventListener('click', () => addQuestion());

function addQuestion() {
    const questionTemplate = document.getElementById('question-template').content.cloneNode(true);
    const questionIndex = questionCount++;

    const questionBlock = questionTemplate.querySelector('.question');
    questionBlock.dataset.index = questionIndex;

    const questionInput = questionBlock.querySelector('input[type="text"]');
    questionInput.name = `Questions[${questionIndex}].Text`;

    const answersDiv = questionBlock.querySelector('.answers');
    const firstAnswer = createAnswer(questionIndex, 0);
    answersDiv.insertBefore(firstAnswer, answersDiv.querySelector('.add-answer'));

    questionBlock.querySelector('.toggle').onclick = () => toggleQuestion(questionIndex);
    questionBlock.querySelector('.add-answer').onclick = (e) => addAnswer(questionIndex, e.target);
    questionBlock.querySelector('.delete-question').onclick = (e) => removeQuestion(e.target);

    document.getElementById('questions').appendChild(questionBlock);
}

function createAnswer(questionIndex, answerIndex) {
    const answerTemplate = document.getElementById('answer-template').content.cloneNode(true);
    const answerDiv = answerTemplate.querySelector('.answer');

    const checkbox = answerDiv.querySelector('input[type="checkbox"]');
    checkbox.name = `Questions[${questionIndex}].Answers[${answerIndex}].IsCorrect`;

    const textInput = answerDiv.querySelector('input[type="text"]');
    textInput.name = `Questions[${questionIndex}].Answers[${answerIndex}].Text`;

    answerDiv.querySelector('.delete-answer').onclick = (e) => removeAnswer(e.target);

    return answerDiv;
}

function addAnswer(questionIndex, btn) {
    const answersDiv = btn.closest('.answers');
    const index = answersDiv.querySelectorAll('.answer').length;
    const answerDiv = createAnswer(questionIndex, index);
    answersDiv.insertBefore(answerDiv, btn);
}

function toggleQuestion(index) {
    const block = document.querySelector(`[data-index="${index}"]`);
    block.classList.toggle('collapsed');
    const icon = block.querySelector('.question-header ion-icon');
    icon.setAttribute('name', block.classList.contains('collapsed') ? 'caret-back-outline' : 'caret-down-outline');
}

function removeQuestion(button) {
    button.closest('.question').remove();
}

function removeAnswer(button) {
    button.closest('.answer').remove();
}
