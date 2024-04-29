namespace ChemCourses.Utils;

// Question class
public abstract class Question {
    public string title;
    public string description;
    public string Id = Guid.NewGuid().ToString();
    
    public abstract string RenderToHTML();
}

//TextQuestion class
#region TextQuestion
public class TextQuestion : Question {
    
    public string defaultValue = "Vul hier uw antwoord in";
    public int maxLength = 50;

    public TextQuestion SetTitle(string title) {
        this.title = title;
        return this;
    }
    
    public TextQuestion SetId(string Id) {
        this.Id = Id;
        return this;
    }
    
    public TextQuestion SetDescription(string description) {
        this.description = description;
        return this;
    }
    
    public TextQuestion SetPlaceholder(string defaultValue) {
        this.defaultValue = defaultValue;
        return this;
    }
    
    public TextQuestion SetMaxLength(int maxLength) {
        this.maxLength = maxLength;
        return this;
    }
    
    public override string  RenderToHTML() {
        //render the HTML for the text question
        //return
        return $"<div class='forms-text_field' id='{Id}' name='{Id}' data-max_length='{maxLength}' data-default_text='{defaultValue}'>\n" + 
               $"  <div class='forms-text_field_label'>{title}</div>\n" +
               $"  <div class='forms-text_field_desc'>{description}</div>\n" +
               $"  <div contenteditable='true'></div>\n" +
               $"</div>";
    }
}
#endregion

//Section class
#region Section
public class Section : Question {
    public string title;
    public string description;

    public Section SetLabel(string label) {
        base.title = label;
        return this;
    }
    
    public Section SetId(string Id) {
        this.Id = Id;
        return this;
    }
    
    public Section SetTitle(string title) {
        this.title = title;
        return this;
    }
    
    public Section AddDescription(string description) {
        this.description = description;
        return this;
    }
    
    public override string  RenderToHTML() {
        //render the HTML for the section
        return $"<div class='forms-section' id='{Id}'>\n" + 
               $"  <div class='forms-section_title'>{title}</div>\n" +
               $"  <div class='forms-section_desc'>{description}</div>\n" +
               $"</div>";
    }
}
#endregion

//DropdownQuestion class
#region DropdownQuestion
public class DropdownQuestion : Question {
    
    public List<string> options = new();
    public string defaultValue;
    public string value;

    public DropdownQuestion SetLabel(string label) {
        this.title = label;
        return this;
    }
    
    public DropdownQuestion SetId(string Id) {
        this.Id = Id;
        return this;
    }
    
    public DropdownQuestion AddOption(string option) {
        options.Add(option);
        return this;
    }

    public DropdownQuestion SetDefaultValue(string defaultValue) {
        this.defaultValue = defaultValue;
        return this;
    }
    
    public DropdownQuestion SetValue(string value) {
        this.value = value;
        return this;
    }
    
    public override string  RenderToHTML() {
        var html = $"<div class='forms-dropdown_question' id='{Id}'>\n" + 
                   $"  <div class='forms-dropdown_question_title'>{title}</div>\n" +
                   $"  <div class='forms-dropdown_question_desc'>{description}</div>\n" +
                   $"  <div class='forms-dropdown_question_options'>\n";

        foreach (var option in options)
        {
            html += $"      <div class='forms-dropdown_question_option' data-value='{option}'>{option}</div>\n";
        }
        
        html += $"  </div>\n" +
                $"</div>";
        
        return html;
    }
}
#endregion


// SliderQuestion class
#region SliderQuestion
public class SliderQuestion : Question {
    
    public float minValue;
    public float maxValue;
    public float step;
    public float defaultValue;

    public SliderQuestion SetLabel(string label) {
        this.title = label;
        return this;
    }
    
    public SliderQuestion SetId(string Id) {
        this.Id = Id;
        return this;
    }
    
    public SliderQuestion SetSliderRange(float minValue, float maxValue) {
        this.minValue = minValue;
        this.maxValue = maxValue;
        return this;
    }

    public SliderQuestion SetSliderStep(float step) {
        this.step = step;
        return this;
    }
    
    public SliderQuestion SetSliderDefaultValue(float defaultValue) {
        this.defaultValue = defaultValue;
        return this;
    }

    public override string RenderToHTML()
    {
        //render the HTML for the slider question
        return $"<div class='forms-slider_question' id='{Id}' data-min='{minValue}' data-max='{maxValue}' data-step='{step}' data-default_value='{defaultValue}'>\n" +
               $"  <div class='forms-slider_question_title'>{title}</div>\n" +
               $"  <div class='forms-slider_question_desc'>{description}</div>\n" +
               $"  <div class='slider-container'>\n" +
               $"   <div class='slider-track'>\n" +
               $"   <div class='slider-thumb''></div>\n" +
               $"  </div>\n" +
               $"  <div class='slider-value'>5</div>\n" +
               $"</div>\n" +
               $"</div>";
    }
}
#endregion

// MultipleChoiceQuestion class
#region MultipleChoiceQuestion
public class MultipleChoiceQuestion : Question {
    
    public List<string> options = new();
    public string defaultValue;
    public bool allowMultipleSelection = false;

    public MultipleChoiceQuestion SetLabel(string label) {
        this.title = label;
        return this;
    }
    
    public MultipleChoiceQuestion SetId(string Id) {
        this.Id = Id;
        return this;
    }
    
    public MultipleChoiceQuestion AddOption(string option) {
        options.Add(option);
        return this;
    }

    public MultipleChoiceQuestion SetDefaultValue(string defaultValue) {
        this.defaultValue = defaultValue;
        return this;
    }

    public MultipleChoiceQuestion AllowMultipleSelections() {
        allowMultipleSelection = true;
        return this;
    }
    
    public override string  RenderToHTML() {
        //render the HTML for the multiple choice question
        var html = $"<div class='forms-multiple_choice_question' id='{Id}' data-allow_multiple_selection='{allowMultipleSelection}'>\n" + 
                   $"  <div class='forms-multiple_choice_question_title'>{title}</div>\n" +
                   $"  <div class='forms-multiple_choice_question_desc'>{description}</div>\n" +
                   $"  <div class='forms-multiple_choice_question_options'>\n";

        if (allowMultipleSelection)
        {
            foreach (var option in options)
            {
                html += $"      <div class='forms-multiple_choice_question_option {(option == defaultValue ? "selected" : "")}' data-value='{option}'>\n" +
                        $"        <div class='forms-multiple_choice_question_option_checkbox'></div>\n" +
                        $"        <div class='forms-multiple_choice_question_option_label'>{option}</div>\n" +
                        $"      </div>\n";
            }
        }
        else
        {
            foreach (var option in options)
            {
                html += $"      <div class='forms-multiple_choice_question_option' data-value='{option}'>\n" +
                        $"        <div class='forms-multiple_choice_question_option_radio'></div>\n" +
                        $"        <div class='forms-multiple_choice_question_option_label'>{option}</div>\n" +
                        $"      </div>\n";
            }
        }
        
        html += $"  </div>\n" +
                $"</div>";
        
        return html;
    }
}
#endregion

//CheckboxQuestion class
#region CheckboxQuestion
public class CheckboxQuestion : Question {

    public string option;
    public bool defaultValue;
    
    public CheckboxQuestion SetLabel(string label) {
        this.title = label;
        return this;
    }
    
    public CheckboxQuestion SetId(string Id) {
        this.Id = Id;
        return this;
    }
    
    public CheckboxQuestion SetOption(string option) {
        this.option = option;
        return this;
    }
    
    public CheckboxQuestion SetDefaultValue(bool defaultValue) {
        this.defaultValue = defaultValue;
        return this;
    }
    
    public override string  RenderToHTML() {
        //render the HTML for the checkbox question
        return $"<div class='forms-checkbox_question' id='{Id}'>\n" + 
               $"  <div class='forms-checkbox_question_title'>{title}</div>\n" +
               $"  <div class='forms-checkbox_question_desc'>{description}</div>\n" +
               $"  <div class='forms-checkbox_question_option'>\n" +
               $"    <div class='forms-checkbox_question_option_checkbox {(defaultValue ? "selected" : "")}'></div>\n" +
               $"    <div class='forms-checkbox_question_option_label'>{option}</div>\n" +
               $"  </div>\n" +
               $"</div>";
    }

}
#endregion
