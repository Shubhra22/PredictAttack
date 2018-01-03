
using UnityEngine;

[System.Serializable]
public class Options
{
    public string name;
    public int value;
}

[System.Serializable]
public class Questions
{
    public string question;
    public Options[] options;

}

[System.Serializable]
public class QuestionGenerator 
{
    public Question question;
    public bool hasSubQuestion;
    public Question subQuestion;
}

public class QuestionManager : MonoBehaviour
{
    
    public QuestionGenerator[] questionGenerator;
    //public Question subQuestion;


}
