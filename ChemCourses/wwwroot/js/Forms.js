//TEXT FIELD
const TextFields = Array.from(document.getElementsByClassName("forms-text_field"));
TextFields.forEach((TextField) => {

    const maxLength = Number(TextField.dataset.max_length);
    const defaultText = TextField.dataset.default_text;
    const editableElement = TextField.querySelector('div[contenteditable=true]');
    
    // Set default text
    editableElement.innerText = defaultText;
    editableElement.style.color = 'gray';

    // Add event listener for focus
    editableElement.addEventListener('focus', function () {
        if (editableElement.innerText === defaultText) {
            editableElement.innerText = '';
            editableElement.style.color = 'black';
        }
    });

    // Add event listener for blur
    editableElement.addEventListener('blur', function () {
        if (editableElement.innerText === '') {
            editableElement.innerText = defaultText;
            editableElement.style.color = 'gray';
        }
    });

    // Add event listener for input
    editableElement.addEventListener('input', function () {
        if (editableElement.innerText.length > maxLength) {
            editableElement.innerText = editableElement.innerText.substring(0, maxLength);
        }
    });
});

//DROPDOWN
const DropdownQuestions = Array.from(document.getElementsByClassName("forms-dropdown_question"));
DropdownQuestions.forEach((DropdownQuestion) => {

    const title = DropdownQuestion.querySelector('.forms-dropdown_question_title');
    const options = Array.from(DropdownQuestion.querySelectorAll('.forms-dropdown_question_option'));
    const selectedOption = DropdownQuestion.querySelector('.forms-dropdown_question_option[data-selected=true]');

    // Add event listener for click
    title.addEventListener('click', function () {
        DropdownQuestion.classList.toggle('active');
    });

    // Add event listener for options
    options.forEach((option) => {
        option.addEventListener('click', function () {
            title.innerText = option.dataset.value;
            options.forEach((option) => {
                option.dataset.selected = false;
            });
            option.dataset.selected = true;
            DropdownQuestion.classList.remove('active');
        });
    });
});

//SLIDER
// JavaScript code for custom slider functionality
const slider = Array.from(document.getElementsByClassName("forms-slider_question"));
slider.forEach((Slider) => {
    const sliderTrack = Slider.querySelector('.slider-track');
    const sliderThumb = Slider.querySelector('.slider-thumb');
    const sliderValue = Slider.querySelector('.slider-value');
    
    console.log(Slider.dataset)
    
    const minValue = Number(Slider.dataset.min);
    const maxValue = Number(Slider.dataset.max);
    const step = Number(Slider.dataset.step);
    const defaultValue = Number(Slider.dataset.default_value);
    
    sliderValue.textContent = defaultValue;
    
    // Calculate initial thumb position
    const percentage = ((defaultValue - minValue) / (maxValue - minValue)) * 100;
    sliderThumb.style.left = `${percentage}%`;
    
    let isDragging = false;

    // Get initial position of slider thumb
    let initialX = 0;

    sliderThumb.addEventListener('mousedown', function (e) {
        isDragging = true;
        initialX = e.clientX - sliderThumb.getBoundingClientRect().left;
    });

    document.addEventListener('mousemove', function (e) {
        if (isDragging) {
            console.log(maxValue - minValue);
            setSliderValue(e.clientX, maxValue - minValue);
        }
    });

    document.addEventListener('mouseup', function () {
        isDragging = false;
    });

    function setSliderValue(number, maxSteps) {
        const stepSize = sliderTrack.offsetWidth / maxSteps;

        let newX = number - initialX - sliderTrack.getBoundingClientRect().left;
        const maxPosition = sliderTrack.offsetWidth - sliderThumb.offsetWidth;

        // Keep thumb within track bounds
        newX = Math.min(Math.max(newX, 0), maxPosition);

        // Calculate the closest step
        const closestStep = Math.round(newX / stepSize) * stepSize;

        // Update thumb position
        sliderThumb.style.left = `${closestStep}px`;

        // Calculate slider value based on thumb position
        const percentage = (closestStep / maxPosition) * 100;
        sliderValue.textContent = Math.round((percentage / 100) * (maxValue - minValue) + minValue);
    }
});

//MULTIPLE CHOICE
const MultipleChoiceQuestions = Array.from(document.getElementsByClassName("forms-multiple_choice_question"));
MultipleChoiceQuestions.forEach((MultipleChoiceQuestion) => {
    const options = Array.from(MultipleChoiceQuestion.querySelectorAll('.forms-multiple_choice_question_option'));

    // Add event listener for options
    options.forEach((option) => {
        option.addEventListener('click', function () {
 
            if (MultipleChoiceQuestion.dataset.allow_multiple_selection === 'False') {
                options.forEach(opt => {
                    opt.classList.remove("selected");
                });
            }
            
            console.log(MultipleChoiceQuestion.dataset, option.classList.contains("selected"));

            if (option.classList.contains("selected") && MultipleChoiceQuestion.dataset.allow_multiple_selection === 'True') {
                option.classList.remove("selected");
            } else {
                option.classList.add("selected");
            }
        });
    });
});

//CHECKBOX
const CheckboxQuestions = Array.from(document.getElementsByClassName("forms-checkbox_question"));
CheckboxQuestions.forEach((CheckboxQuestion) => {
    const options = Array.from(CheckboxQuestion.querySelectorAll('.forms-checkbox_question_option'));

    // Add event listener for options
    options.forEach((option) => {
        option.addEventListener('click', function () {
            if (option.classList.contains("selected")) {
                option.classList.remove("selected");
            } else {
                option.classList.add("selected");
            }
        });
    });
});

//All the form question types that still need to be implemented
// - Date
// - Time
// - File Upload
// - Rating
// - Likert Scale
// - Matrix