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
    const sliderContainer = Slider.querySelector('.slider-container');
    const sliderTrack = Slider.querySelector('.slider-track');
    const sliderThumb = Slider.querySelector('.slider-thumb');
    const sliderValue = Slider.querySelector('.slider-value');

    const min = Number(Slider.getAttribute('data-min').replace(",", "."));
    const max = Number(Slider.getAttribute('data-max').replace(",", "."));
    const step = Number(Slider.getAttribute('data-step').replace(",", "."));
    let value = Number(Slider.getAttribute('data-default_value').replace(",", "."));

    const trackWidth = sliderTrack.offsetWidth;
    const thumbWidth = sliderThumb.offsetWidth;

    const setValue = (newValue) => {
        value = Math.min(Math.max(min, newValue), max);
        const numbersAfterComma = step.toString().split(".")[1] ? step.toString().split(".")[1].length : 0;
        value = Number(value.toFixed(numbersAfterComma));
        const position = ((value - min) / (max - min)) * (trackWidth - thumbWidth)
        sliderThumb.style.left = `${position}px`;
        sliderValue.textContent = value;
    };

    setValue(value);

    const getPosition = (event) => {
        const rect = sliderTrack.getBoundingClientRect();
        const x = event.clientX - rect.left;
        return Math.min(Math.max(0, x), trackWidth - thumbWidth);
    };

    const moveThumb = (event) => {
        const position = getPosition(event);
        const percentage = position / (trackWidth - thumbWidth);
        const newValue = Math.round(percentage * (max - min) / step) * step + min;
        setValue(newValue);
    };

    sliderThumb.addEventListener('mousedown', (event) => {
        moveThumb(event);
        const moveHandler = (moveEvent) => {
            moveThumb(moveEvent);
        };
        const upHandler = () => {
            document.removeEventListener('mousemove', moveHandler);
            document.removeEventListener('mouseup', upHandler);
        };
        document.addEventListener('mousemove', moveHandler);
        document.addEventListener('mouseup', upHandler);
    });
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