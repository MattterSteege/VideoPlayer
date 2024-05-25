//TEXT FIELD
var TextFields = Array.from(document.getElementsByClassName("forms-text_field"));
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
var DropdownQuestions = Array.from(document.getElementsByClassName("forms-dropdown_question"));
DropdownQuestions.forEach((DropdownQuestion) => {
    const title = DropdownQuestion.querySelector('.forms-dropdown_question_title');
    const optionsContainer = DropdownQuestion.querySelector('.forms-dropdown_question_options');
    const selectedContainer = DropdownQuestion.querySelector('.forms-dropdown_question_selected');
    const options = Array.from(optionsContainer.children[0].children);

    // Hide options initially
    optionsContainer.style.display = 'none';

    // Toggle dropdown menu when title is clicked
    title.addEventListener('click', () => {
        const isExpanded = optionsContainer.style.display === 'block';
        optionsContainer.style.display = isExpanded ? 'none' : 'block';
        selectedContainer.style.display = (isExpanded && selectedContainer.textContent != "") ? 'block' : 'none';
        title.textContent = isExpanded ? '▼' + title.textContent.substring(1) : '▲' + title.textContent.substring(1);
    });

    // Handle option selection
    options.forEach((option) => {
        option.addEventListener('click', () => {
            const selectedValue = option.getAttribute('data-value');
            const selectedOption = DropdownQuestion.querySelector(`[data-selected="true"]`);
            if (selectedOption) selectedOption.setAttribute('data-selected', 'false');
            option.setAttribute('data-selected', 'true');
            selectedContainer.textContent = selectedValue;
            selectedContainer.style.display = 'block';
            optionsContainer.style.display = 'none';
            title.textContent = '▼' + title.textContent.substring(1);
        });
    });
});


//SLIDER
var slider = Array.from(document.getElementsByClassName("forms-slider_question"));
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
var MultipleChoiceQuestions = Array.from(document.getElementsByClassName("forms-multiple_choice_question"));
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
var CheckboxQuestions = Array.from(document.getElementsByClassName("forms-checkbox_question"));
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

//TRUE/FALSE
var TrueFalseQuestions = Array.from(document.getElementsByClassName("forms-true_false_question"));
TrueFalseQuestions.forEach((TrueFalseQuestion) => {
    const options = Array.from(TrueFalseQuestion.querySelectorAll('.forms-true_false_question_option'));

    // Add event listener for options
    options.forEach((option) => {
        option.addEventListener('click', function () {
            options.forEach(opt => {
                opt.classList.remove("selected");
            });
            option.classList.add("selected");
        });
    });
});

//Fill in the blank
//no code needed

//Matching
var MatchingQuestions = Array.from(document.getElementsByClassName("forms-matching_question"));
MatchingQuestions.forEach((MatchingQuestion) => {
    function Sortable(parentElement) {
        var draggingElement = null;

        // Function to handle the drag start event
        function handleDragStart(event) {
            draggingElement = event.target;
            event.dataTransfer.effectAllowed = 'move';
            event.dataTransfer.setData('text/html', draggingElement.innerHTML);
            draggingElement.classList.add('dragging');
        }

        // Function to handle the drag over event
        function handleDragOver(event) {
            if (event.preventDefault) {
                event.preventDefault();
            }
            event.dataTransfer.dropEffect = 'move';
            return false;
        }

        // Function to handle the drag enter event
        function handleDragEnter(event) {
            event.target.classList.add('over');
        }

        // Function to handle the drag leave event
        function handleDragLeave(event) {
            event.target.classList.remove('over');
        }

        // Function to handle the drop event
        function handleDrop(event) {
            if (event.stopPropagation) {
                event.stopPropagation();
            }
            if (draggingElement !== event.target) {
                draggingElement.innerHTML = event.target.innerHTML;
                event.target.innerHTML = event.dataTransfer.getData('text/html');
            }
            return false;
        }

        // Function to handle the drag end event
        function handleDragEnd(event) {
            draggingElement.classList.remove('dragging');
            var elements = parentElement.querySelectorAll('.over');
            elements.forEach(function(element) {
                element.classList.remove('over');
            });
        }

        // Initialize the sortable
        function init() {
            var elements = parentElement.children;
            for (var i = 0; i < elements.length; i++) {
                elements[i].setAttribute('draggable', 'true');
                elements[i].addEventListener('dragstart', handleDragStart, false);
                elements[i].addEventListener('dragenter', handleDragEnter, false);
                elements[i].addEventListener('dragover', handleDragOver, false);
                elements[i].addEventListener('dragleave', handleDragLeave, false);
                elements[i].addEventListener('drop', handleDrop, false);
                elements[i].addEventListener('dragend', handleDragEnd, false);
            }
        }

        // Call the init function
        init();
    }

    Sortable(MatchingQuestion.querySelector('.sortable-list'));
});

//All the form question types that still need to be implemented
// - Date
// - Time
// - File Upload
// - Rating
// - Likert Scale
// - Matrix



//BUTTON CONTROLS
var nextButton = document.querySelector(".button.next");
nextButton.style.opacity = 1;
var previousButton = document.querySelector(".button.back");
previousButton.style.opacity = 0;

var questionContainer = Array.from(document.querySelector(".question-container").children);
questionContainer.forEach((element, index) => {
    element.style.zIndex = questionContainer.length - index;
});

//.question-legend > children (p)
var questionLegendChildren = Array.from(document.querySelector(".question-legend").children);
questionLegendChildren.forEach((element) => {
    element.style.fontWeight = "";
});
questionLegendChildren[0].style.fontWeight = "bold";

var currentPanel = 0;

nextButton.addEventListener("click", function () {
    if (nextButton.style.opacity === '0') return;
    
    nextPanel();
    previousButton.style.opacity = 1;
    if (currentPanel === questionContainer.length - 1) {
        nextButton.style.opacity = 0;
    }
});

previousButton.addEventListener("click", function () {
    if (previousButton.style.opacity === '0') return;
    
    PreviousPanel();
    nextButton.style.opacity = 1;
    if (currentPanel === 0) {
        previousButton.style.opacity = 0;
    }
});

///switchPanel(1) will move the panel one to the right
///switchPanel(-1) will move the panel one to the left
function nextPanel() {
    //random -1 or 1
    var random = Math.floor(Math.random() * 2) * 2 - 1;
    questionContainer[currentPanel].style.transform = `translateX(${random * 110}%)`;
    currentPanel++;
    
    questionLegendChildren.forEach((element) => {
        element.style.fontWeight = "";
    });    
    questionLegendChildren[currentPanel].style.fontWeight = "bold";
}

function PreviousPanel() {
    currentPanel--;
    questionContainer[currentPanel].style.transform = `translateX(0%)`;
    
    questionLegendChildren.forEach((element) => {
        element.style.fontWeight = "";
    });
    questionLegendChildren[currentPanel].style.fontWeight = "bold";
}