﻿// using System.Text.Json;
// using System.Text.Json.Serialization;
// using System.Text.RegularExpressions;
//
// namespace ChemCourses.Utils;
//
// // Question class
// [JsonDerivedType(typeof(TextQuestion))]
// [JsonDerivedType(typeof(Section))]
// [JsonDerivedType(typeof(DropdownQuestion))]
// [JsonDerivedType(typeof(SliderQuestion))]
// [JsonDerivedType(typeof(MultipleChoiceQuestion))]
// [JsonDerivedType(typeof(CheckboxQuestion))]
// [JsonDerivedType(typeof(TrueFalseQuestion))]
// [JsonDerivedType(typeof(FillInTheBlankQuestion))]
// [JsonDerivedType(typeof(MatchingQuestion))]
// public abstract class Question
// {
//     public string Title { get; set; }
//     public string Description { get; set; }
//     public string Summary { get; set; }
//     public int Id { get; set; }
//     public string Type => this.GetType().Name;
//
//     public abstract string RenderToHTML(int id = 0);
// }
//
// #region TextQuestion
//
// public class TextQuestion : Question
// {
//     public string DefaultValue { get; set; } = "Vul hier uw antwoord in";
//     public int MaxLength { get; set; } = 50;
//
//
//     public TextQuestion SetTitle(string Title)
//     {
//         this.Title = Title;
//         return this;
//     }
//
//     public TextQuestion SetDescription(string Description)
//     {
//         this.Description = Description;
//         return this;
//     }
//
//     public TextQuestion SetSummary(string Summary)
//     {
//         this.Summary = Summary;
//         return this;
//     }
//
//     public TextQuestion SetPlaceholder(string DefaultValue)
//     {
//         this.DefaultValue = DefaultValue;
//         return this;
//     }
//
//     public TextQuestion SetMaxLength(int maxLength)
//     {
//         this.MaxLength = maxLength;
//         return this;
//     }
//
//     public override string RenderToHTML(int Id = 0)
//     {
//         this.Id = Id;
//         //render the HTML for the text question
//         //return
//         return
//             $"<div class='forms-text_field' id='{Id}' name='{Id}' data-max_length='{MaxLength}' data-default_text='{DefaultValue}'>\n" +
//             $"  <div class='forms-text_field_label'>{Title}</div>\n" +
//             $"  <p class='forms-text_field_desc'>{Description}</p>\n" +
//             $"  <div contenteditable='true'></div>\n" +
//             $"</div>";
//     }
// }
//
// #endregion
//
// #region Section
//
// public class Section : Question
// {
//     public Section SetTitle(string label)
//     {
//         Title = label;
//         return this;
//     }
//
//     public Section SetDescription(string Description)
//     {
//         this.Description = Description;
//         return this;
//     }
//
//     public override string RenderToHTML(int Id = 0)
//     {
//         this.Id = Id;
//
//         //render the HTML for the section
//         return $"<div class='forms-section' id='{Id}'>\n" +
//                $"  <div class='forms-section_Title'>{Title}</div>\n" +
//                $"  <p class='forms-section_desc'>{Description}</p>\n" +
//                $"</div>";
//     }
// }
//
// #endregion
//
// #region DropdownQuestion
//
// public class DropdownQuestion : Question
// {
//     public List<string> Options { get; set; } = new List<string>();
//     public string DefaultValue { get; set; }
//     public string Value { get; set; }
//
//     public DropdownQuestion SetTitle(string label)
//     {
//         this.Title = label;
//         return this;
//     }
//
//     public DropdownQuestion SetDescription(string Description)
//     {
//         this.Description = Description;
//         return this;
//     }
//
//     public DropdownQuestion SetSummary(string Summary)
//     {
//         this.Summary = Summary;
//         return this;
//     }
//
//     public DropdownQuestion AddOption(string option)
//     {
//         Options.Add(option);
//         return this;
//     }
//
//     public DropdownQuestion SetOptions(List<string> options)
//     {
//         this.Options = options;
//         return this;
//     }
//
//     public DropdownQuestion SetDefaultValue(string DefaultValue)
//     {
//         this.DefaultValue = DefaultValue;
//         return this;
//     }
//
//     public DropdownQuestion SetValue(string value)
//     {
//         this.Value = value;
//         return this;
//     }
//
//     public override string RenderToHTML(int Id = 0)
//     {
//         this.Id = Id;
//
//         var html = $"<div class='forms-dropdown_question' id='{Id}'>\n" +
//                    $"  <div class='forms-dropdown_question_title'>▼ {Title}</div>\n" +
//                    $"  <p class='forms-dropdown_question_desc'>{Description}</p>\n" +
//                    $"  <div class='forms-dropdown_question_selected' style='display: none;'></div>\n" +
//                    $"  <div class='forms-dropdown_question_options'>\n" +
//                    $"      <div class='forms-dropdown_question_option_container'>\n";
//
//         foreach (var option in Options)
//         {
//             html += $"          <div class='forms-dropdown_question_option' data-value='{option}'>{option}</div>\n";
//         }
//
//         html += $"      </div>\n" +
//                 $"  </div>\n" +
//                 $"</div>";
//
//         return html;
//     }
// }
//
// #endregion
//
// #region SliderQuestion
//
// public class SliderQuestion : Question
// {
//     public float MinValue { get; set; }
//     public float MaxValue { get; set; }
//     public float Step { get; set; }
//     public float DefaultValue { get; set; }
//
//     public SliderQuestion SetTitle(string label)
//     {
//         this.Title = label;
//         return this;
//     }
//
//     public SliderQuestion SetDescription(string Description)
//     {
//         this.Description = Description;
//         return this;
//     }
//
//     public SliderQuestion SetSummary(string Summary)
//     {
//         this.Summary = Summary;
//         return this;
//     }
//
//     public SliderQuestion SetSliderRange(float minValue, float maxValue)
//     {
//         this.MinValue = minValue;
//         this.MaxValue = maxValue;
//         return this;
//     }
//
//     public SliderQuestion SetStep(float step)
//     {
//         this.Step = step;
//         return this;
//     }
//
//     public SliderQuestion SetDefaultValue(float DefaultValue)
//     {
//         this.DefaultValue = DefaultValue;
//         return this;
//     }
//
//     public override string RenderToHTML(int Id = 0)
//     {
//         this.Id = Id;
//
//         //render the HTML for the slider question
//         return
//             $"<div class='forms-slider_question' id='{Id}' data-min='{MinValue}' data-max='{MaxValue}' data-step='{Step}' data-default_value='{DefaultValue}'>\n" +
//             $"  <div class='forms-slider_question_Title'>{Title}</div>\n" +
//             $"  <p class='forms-slider_question_desc'>{Description}</p>\n" +
//             $"  <div class='slider-container'>\n" +
//             $"   <div class='slider-track'>\n" +
//             $"   <div class='slider-thumb''></div>\n" +
//             $"  </div>\n" +
//             $"  <div class='slider-value'>5</div>\n" +
//             $"</div>\n" +
//             $"</div>";
//     }
// }
//
// #endregion
//
// #region MultipleChoiceQuestion
//
// public class MultipleChoiceQuestion : Question
// {
//     public List<string> Options { get; set; } = new List<string>();
//     public string DefaultValue { get; set; }
//     public bool AllowMultiple { get; set; }
//
//     public MultipleChoiceQuestion SetTitle(string label)
//     {
//         this.Title = label;
//         return this;
//     }
//
//     public MultipleChoiceQuestion SetDescription(string Description)
//     {
//         this.Description = Description;
//         return this;
//     }
//
//     public MultipleChoiceQuestion SetSummary(string Summary)
//     {
//         this.Summary = Summary;
//         return this;
//     }
//
//     public MultipleChoiceQuestion AddOption(string option)
//     {
//         Options.Add(option);
//         return this;
//     }
//
//     public MultipleChoiceQuestion SetOptions(List<string> options)
//     {
//         this.Options = options;
//         return this;
//     }
//
//     public MultipleChoiceQuestion SetDefaultValue(string DefaultValue)
//     {
//         this.DefaultValue = DefaultValue;
//         return this;
//     }
//
//     public MultipleChoiceQuestion AllowMultipleSelections()
//     {
//         AllowMultiple = true;
//         return this;
//     }
//
//     public override string RenderToHTML(int Id = 0)
//     {
//         this.Id = Id;
//
//         //render the HTML for the multiple choice question
//         var html =
//             $"<div class='forms-multiple_choice_question' id='{Id}' data-allow_multiple_selection='{AllowMultiple}'>\n" +
//             $"  <div class='forms-multiple_choice_question_Title'>{Title}</div>\n" +
//             $"  <p class='forms-multiple_choice_question_desc'>{Description}</p>\n" +
//             $"  <div class='forms-multiple_choice_question_options'>\n";
//
//         if (AllowMultiple)
//         {
//             foreach (var option in Options)
//             {
//                 html +=
//                     $"      <div class='forms-multiple_choice_question_option {(option == DefaultValue ? "selected" : "")}' data-value='{option}'>\n" +
//                     $"        <div class='forms-multiple_choice_question_option_checkbox'></div>\n" +
//                     $"        <div class='forms-multiple_choice_question_option_label'>{option}</div>\n" +
//                     $"      </div>\n";
//             }
//         }
//         else
//         {
//             foreach (var option in Options)
//             {
//                 html += $"      <div class='forms-multiple_choice_question_option' data-value='{option}'>\n" +
//                         $"        <div class='forms-multiple_choice_question_option_radio'></div>\n" +
//                         $"        <div class='forms-multiple_choice_question_option_label'>{option}</div>\n" +
//                         $"      </div>\n";
//             }
//         }
//
//         html += $"  </div>\n" +
//                 $"</div>";
//
//         return html;
//     }
// }
//
// #endregion
//
// #region CheckboxQuestion
//
// public class CheckboxQuestion : Question
// {
//     public string Option { get; set; }
//     public bool DefaultValue { get; set; }
//
//     public CheckboxQuestion SetTitle(string label)
//     {
//         this.Title = label;
//         return this;
//     }
//
//     public CheckboxQuestion SetDescription(string Description)
//     {
//         this.Description = Description;
//         return this;
//     }
//
//     public CheckboxQuestion SetSummary(string Summary)
//     {
//         this.Summary = Summary;
//         return this;
//     }
//
//     public CheckboxQuestion SetOption(string option)
//     {
//         this.Option = option;
//         return this;
//     }
//
//     public CheckboxQuestion SetDefaultValue(bool DefaultValue)
//     {
//         this.DefaultValue = DefaultValue;
//         return this;
//     }
//
//     public override string RenderToHTML(int Id = 0)
//     {
//         this.Id = Id;
//
//         //render the HTML for the checkbox question
//         return $"<div class='forms-checkbox_question' id='{Id}'>\n" +
//                $"  <div class='forms-checkbox_question_Title'>{Title}</div>\n" +
//                $"  <p class='forms-checkbox_question_desc'>{Description}</p>\n" +
//                $"  <div class='forms-checkbox_question_option'>\n" +
//                $"    <div class='forms-checkbox_question_option_checkbox {(DefaultValue ? "selected" : "")}'></div>\n" +
//                $"    <div class='forms-checkbox_question_option_label'>{Option}</div>\n" +
//                $"  </div>\n" +
//                $"</div>";
//     }
// }
//
// #endregion
//
// #region TrueFalseQuestion
//
// public class TrueFalseQuestion : Question
// {
//     public bool DefaultValue { get; set; }
//
//     public TrueFalseQuestion SetTitle(string label)
//     {
//         this.Title = label;
//         return this;
//     }
//
//     public TrueFalseQuestion SetDescription(string Description)
//     {
//         this.Description = Description;
//         return this;
//     }
//
//     public TrueFalseQuestion SetSummary(string Summary)
//     {
//         this.Summary = Summary;
//         return this;
//     }
//
//     public TrueFalseQuestion SetDefaultValue(bool DefaultValue)
//     {
//         this.DefaultValue = DefaultValue;
//         return this;
//     }
//
//     public override string RenderToHTML(int Id = 0)
//     {
//         this.Id = Id;
//
//         //render the HTML for the true/false question
//         return $"<div class='forms-true_false_question' id='{Id}'>\n" +
//                $"  <div class='forms-true_false_question_Title'>{Title}</div>\n" +
//                $"  <p class='forms-true_false_question_desc'>{Description}</p>\n" +
//                $"  <div class='forms-true_false_question_options'>\n" +
//                $"    <div class='forms-true_false_question_option {(DefaultValue ? "selected" : "")}' data-value='true'>True</div>\n" +
//                $"    <div class='forms-true_false_question_option {(DefaultValue ? "" : "selected")}' data-value='false'>False</div>\n" +
//                $"  </div>\n" +
//                $"</div>";
//     }
// }
//
// #endregion
//
// #region FillInTheBlankQuestion
//
// public class FillInTheBlankQuestion : Question
// {
//     public List<string> Parts { get; set; }
//     public List<string> Answers { get; set; } = new List<string>();
//
//     public FillInTheBlankQuestion SetTitle(string label)
//     {
//         this.Title = label;
//         return this;
//     }
//
//     public FillInTheBlankQuestion SetDescription(string Description)
//     {
//         this.Description = Description;
//         return this;
//     }
//
//     public FillInTheBlankQuestion SetSummary(string Summary)
//     {
//         this.Summary = Summary;
//         return this;
//     }
//
//     /// <summary>
//     /// Input a string with the blanks in the text. The blanks should be surrounded by {{ and }} while containing the anwser.
//     /// </summary>
//     /// <example>
//     /// SetText("The capital of the Netherlands is {{Amsterdam}}. It is a very beautiful city. It's most famous for its {{canals}}.");
//     /// </example>
//     public FillInTheBlankQuestion SetText(string text)
//     {
//         Parts = Regex.Split(text, "{{.*?}}").ToList();
//         Answers = Regex.Matches(text, "{{(.*?)}}").Select(m => m.Groups[1].Value).ToList();
//         return this;
//     }
//
//     public override string RenderToHTML(int Id = 0)
//     {
//         this.Id = Id;
//
//         //render the HTML for the fill in the blank question
//         var html = $"<div class='forms-fill_in_the_blank_question' id='{Id}'>\n" +
//                    $"  <div class='forms-fill_in_the_blank_question_Title'>{Title}</div>\n" +
//                    $"  <p class='forms-fill_in_the_blank_question_desc'>{Description}</p>\n" +
//                    $"  <div class='forms-fill_in_the_blank_question_text'>\n";
//
//         for (int i = 0; i < Parts.Count; i++)
//         {
//             html += $"{Parts[i]}\n";
//
//             if (i < Answers.Count)
//             {
//                 html +=
//                     $"<div contenteditable='true' type='text' class='forms-fill_in_the_blank_question_text_input' data-answer='{Answers[i]}'></div>\n";
//             }
//         }
//
//         html += $"</div>\n" +
//                 $"</div>";
//
//         return html;
//     }
// }
//
// #endregion
//
// #region MatchingQuestion
//
// public class MatchingQuestion : Question
// {
//     public List<string> Options { get; set; } = new List<string>();
//     public List<string> Answers { get; set; } = new List<string>();
//
//     public MatchingQuestion SetTitle(string label)
//     {
//         this.Title = label;
//         return this;
//     }
//
//     public MatchingQuestion SetDescription(string Description)
//     {
//         this.Description = Description;
//         return this;
//     }
//
//     public MatchingQuestion SetSummary(string Summary)
//     {
//         this.Summary = Summary;
//         return this;
//     }
//
//     public MatchingQuestion AddQuestionAndAnswer(string question, string answer)
//     {
//         Options.Add(question);
//         Answers.Add(answer);
//         return this;
//     }
//
//     public MatchingQuestion SetQuestionAndAnswer(List<string> questions, List<string> answers)
//     {
//         Options = questions;
//         Answers = answers;
//         return this;
//     }
//
//     //the rendering should be a grid (when possible) that is randomly shuffled, then the user can click and the answer and question should be matched
//     public override string RenderToHTML(int Id = 0)
//     {
//         this.Id = Id;
//
//         //render the HTML for the matching question
//         var html = $"<div class='forms-matching_question' id='{Id}'>\n" +
//                    $"  <div class='forms-matching_question_Title'>{Title}</div>\n" +
//                    $"  <p class='forms-matching_question_desc'>{Description}</p>\n" +
//                    $"  <div class='forms-matching_question_grid'>\n" +
//                    $"    <div class='forms-matching_question_grid_inner'>\n";
//
//         foreach (var answer in Answers)
//         {
//             html += $"      <div class='forms-matching_question_option' data-answer='{answer}'>{answer}</div>\n";
//         }
//
//         html += $"    </div>\n" +
//                 $"    <div class='forms-matching_question_grid_inner sortable-list'>\n";
//
//         List<string> shuffledOptions = new List<string>();
//         for (int i = 0; i < Options.Count; i++)
//         {
//             shuffledOptions.Add(
//                 $"      <div class='forms-matching_question_option draggable' draggable='true' data-answer='{Answers[i]}'>{Options[i]}</div>");
//         }
//
//         shuffledOptions.Shuffle();
//
//         html += string.Join("\n", shuffledOptions);
//
//
//         html += $"    </div>\n" +
//                 $"  </div>\n" +
//                 $"</div>";
//
//         return html;
//     }
// }
//
// #endregion
//
// #region Deserialization
//
// public class QuestionConverter : JsonConverter<Question>
// {
//     public override bool CanConvert(Type typeToConvert)
//     {
//         return typeToConvert.IsAssignableFrom(typeof(Question));
//     }
//
//     public override Question? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions? options)
//     {
//         if (reader.TokenType != JsonTokenType.StartObject)
//             throw new JsonException("Invalid JSON format. Expected an object.");
//
//         using var jsonDocument = JsonDocument.ParseValue(ref reader);
//         var jsonObject = jsonDocument.RootElement;
//
//         bool has_typeProperty =
//             jsonObject.TryGetProperty("Type", out var typeProperty); //Beware of the capital T version
//         bool hasTypeProperty =
//             jsonObject.TryGetProperty("type", out var TypeProperty); //Beware of the lowercase t version
//
//         if (!has_typeProperty && !hasTypeProperty)
//             throw new JsonException("Could not determine the type of the Question object.");
//
//         string type = typeProperty.GetString() ?? TypeProperty.GetString() ??
//             throw new JsonException("Could not determine the type of the Question object.");
//         Question? question = type switch
//         {
//             "TextQuestion" => jsonObject.Deserialize<TextQuestion>(options),
//             "MatchingQuestion" => jsonObject.Deserialize<MatchingQuestion>(options),
//             "Section" => jsonObject.Deserialize<Section>(options),
//             "DropdownQuestion" => jsonObject.Deserialize<DropdownQuestion>(options),
//             "SliderQuestion" => jsonObject.Deserialize<SliderQuestion>(options),
//             "MultipleChoiceQuestion" => jsonObject.Deserialize<MultipleChoiceQuestion>(options),
//             "CheckboxQuestion" => jsonObject.Deserialize<CheckboxQuestion>(options),
//             "TrueFalseQuestion" => jsonObject.Deserialize<TrueFalseQuestion>(options),
//             "FillInTheBlankQuestion" => jsonObject.Deserialize<FillInTheBlankQuestion>(options),
//
//             _ => throw new JsonException($"Unsupported question type: {type}")
//         };
//
//         return question;
//     }
//
//     public override void Write(Utf8JsonWriter writer, Question value, JsonSerializerOptions options)
//     {
//         JsonSerializer.Serialize(writer, value, value.GetType(), options);
//     }
// }
//
// #endregion