using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SubjectNerd.Utilities;


[System.Serializable]
public class Question
{
    public string question;
    public enum QuestionType
    {
        YesNo,
        YesNoExtended,
        SingleChoice,
        MultipleChoice,
        Undefined
    }
    public Options[] options;

    public QuestionType questionType;


    public string subQuestion;

    public bool conditionalQuestion;
    public int endIndexConditions;


    public void QuestionCreator()
    {
        if (questionType == QuestionType.YesNo)
        {
            options = new Options[2];

            options[0] = new Options
            {
                name = "Yes",
                value = 1
            };

            options[1] = new Options
            {
                name = "No",
                value = 0
            };

        }
        else if (questionType == QuestionType.YesNoExtended)
        {
            // options = new Options[3];
            options[0].name = "Yes";
            options[0].value = 1;

            options[1].name = "No";
            options[1].value = 0;
        }
    }
}

public class QuestionControllerScript : MonoBehaviour
{
    [Reorderable]
    public List<Question> questions;
    public Transform questionParent;
    List<string> m_DropOptions = new List<string>();
    public GameObject questionTemplate;

    public Dropdown dropDown;
    public Toggle toggler;
    public InputField textBox;
    public Button submitButton;

    public Dictionary<string, int> options = new Dictionary<string, int>();
    public static QuestionControllerScript instance;

    public string resultString;

    string age;
    string gender;

    // Use this for initialization
    void Start()
    {
        MakeQuestion();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OptionManager(Question question)
    {
        m_DropOptions.Clear();
        for (int j = 0; j < question.options.Length; j++)
        {
            m_DropOptions.Add(question.options[j].name);
            if (!options.ContainsKey(question.options[j].name))
                options.Add(question.options[j].name, question.options[j].value);
        }

    }


    public void MakeQuestion()
    {
        for (int i = 0; i < questions.Count; i++) // loop through the question array and create objects for each question
        {
            questions[i].QuestionCreator();

            GameObject questionClone = Instantiate(questionTemplate, questionParent); // create a new questiomn
            questionClone.transform.name = i.ToString();// rename the object to idenify easily

            Transform targetObj = questionParent.GetChild(i); // get the current question object
            targetObj.GetComponentInChildren<Text>().text = questions[i].question;// replace the question text (i.e."Do u hvae chest pain??"/ Do u smoke etc)
            OptionManager(questions[i]); // Add options

            if (questions[i].questionType == Question.QuestionType.YesNo || questions[i].questionType == Question.QuestionType.YesNoExtended || questions[i].questionType == Question.QuestionType.SingleChoice)
            {
                GameObject clone = Instantiate(dropDown.gameObject, targetObj);
                Dropdown m_Dropdown = clone.GetComponent<Dropdown>();

                m_Dropdown.ClearOptions();
                m_Dropdown.AddOptions(m_DropOptions);

                m_Dropdown.options.Add(new Dropdown.OptionData() { text = "" });
                // Select it
                m_Dropdown.GetComponent<Dropdown>().value = m_Dropdown.GetComponent<Dropdown>().options.Count - 1;
                // Remove it
                m_Dropdown.GetComponent<Dropdown>().options.RemoveAt(m_Dropdown.GetComponent<Dropdown>().options.Count - 1);
                // Done!
                clone.AddComponent<DropDownManager>();
                clone.AddComponent<InputValues>();
                if (questions[i].subQuestion != "")
                {
                    /* Create a new textBox Objext for subquestion */
                    GameObject textBoxClone = Instantiate(textBox.gameObject, targetObj);
                    InputField textBoxField = textBoxClone.GetComponent<InputField>();
                    /* End clone */

                    textBoxField.placeholder.GetComponent<Text>().text = questions[i].subQuestion;// assign subquestion from the question array
                    textBoxClone.SetActive(false);
                    textBoxClone.AddComponent<InputValues>();
                    textBoxClone.AddComponent<InputFieldManager>();
                }
            }

            else if (questions[i].questionType == Question.QuestionType.MultipleChoice)
            {
                foreach (var item in m_DropOptions)
                {
                    /* Create a new Toogle Objext for subquestion */
                    GameObject clone = Instantiate(toggler.gameObject, targetObj);
                    Toggle m_Toggle = clone.GetComponent<Toggle>();
                    /* End Toggle */


                    m_Toggle.GetComponentInChildren<Text>().text = item;
                    m_Toggle.isOn = false;
                    clone.AddComponent<InputValues>();
                    clone.AddComponent<ToggleManager>();
                }
            }

        }
        Instantiate(submitButton.gameObject, questionParent);

        ProcessQuestion();

    }

    public void ShowHideSubQuestion(int i, int index)
    {
        if (questions[i].subQuestion != "")
        {
            if (index == 0) // index 0 means Yes
                questionParent.GetChild(i).GetChild(2).gameObject.SetActive(true);
            else if (index == 1)
                questionParent.GetChild(i).GetChild(2).gameObject.SetActive(false);
        }

    }

    public void ShowHideSubQuestion(int i, string key)
    {

        if (questions[i].conditionalQuestion)
        {
            switch (options[key])
            {
                case 2:
                    for (int j = i + 2; j <= questions[i].endIndexConditions + i; j++)
                    {
                        questionParent.GetChild(j).gameObject.SetActive(true);
                    }
                    questionParent.GetChild(i + 1).gameObject.SetActive(false);

                    if (questions[i].question == "Do you have chest pain?")
                    {
                        for (int j = i + 2; j < questions.Count; j++)
                        {
                            questionParent.GetChild(j).gameObject.SetActive(true); // active rest of the questions if question is DYHCP
                        }
                    }

                    break;
                case 3:
                    questionParent.GetChild(i + 1).gameObject.SetActive(true);
                    for (int j = i + 2; j <= questions[i].endIndexConditions + i; j++)
                    {
                        questionParent.GetChild(j).gameObject.SetActive(false);
                    }

                    break;
                case 4: // can not explain option
                    for (int j = i + 1; j <= questions[i].endIndexConditions + i; j++)
                    {
                        questionParent.GetChild(j).gameObject.SetActive(false);
                    }

                    break;
                default:
                    for (int j = i + 1; j <= questions[i].endIndexConditions + i; j++)
                    {
                        questionParent.GetChild(j).gameObject.SetActive(true);
                    }

                    break;
            }
        }
        //questions[i].
    }
    public void ProcessQuestion()
    {
        questionParent.GetChild(6).gameObject.SetActive(false);

        for (int i = 8; i < questionParent.childCount - 1; i++)
        {
            questionParent.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void GetOutPut()
    {
        string s = age + "," + gender + ",";
        //string dbEntry ="";
        List<string> dbEntry = new List<string>();
        int k;
        for (int i = 0; i < questionParent.childCount - 1; i++)
        {
            for (int j = 1; j < questionParent.GetChild(i).childCount; j++)
            {
                Transform targetObj = questionParent.GetChild(i).GetChild(j);
               // print(targetObj.gameObject.activeSelf);

                if (targetObj.gameObject.activeSelf)
                {                    
                    if (targetObj.parent.gameObject.activeSelf)
                    {
                        print(targetObj.parent.name + " : " + targetObj.GetComponent<InputValues>().key);
                        if (targetObj.GetComponent<InputValues>().key == "")
                        {
                            print(targetObj.parent.name);
                            ErrorHandler.instance.OnEmptyInput();
                            return;
                        }
                    }
                    else if (!targetObj.parent.gameObject.activeSelf)
                    {
                        targetObj.GetComponent<InputValues>().key = "No";
                    }

                    dbEntry.Add(targetObj.GetComponent<InputValues>().key);
                    if (Int32.TryParse(targetObj.GetComponent<InputValues>().key, out k))
                    {
                        s += targetObj.GetComponent<InputValues>().key + ",";
                    }
                    else
                    {
                        s += options[targetObj.GetComponent<InputValues>().key] + ",";

                    }
                }
                else
                {
                    s += options["No"] + ",";
                    dbEntry.Add("No");


                   // print("NO");
                }

            }
        }
        OutputController.instance.ProcessInput(s);
        SignUpManager.instance.AddToDataBase(dbEntry);
        print(s);
        //return s;

    }


    public void AgeAndGender(string a, string g)
    {
        age = a;
        if (g == "Male")
        {
            gender = "2";
        }
        else
        {
            gender = "3";
        }
    }
}

