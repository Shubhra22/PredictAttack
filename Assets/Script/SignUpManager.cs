using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using UnityEngine.Networking;

public class SignUpManager : MonoBehaviour
{
    public GameObject signupObject;
    public GameObject signinObject;
    public GameObject questionObject;


    public string registerUrl;
    public string loginUrl;
    public string ageGenderUrl;
    public string heartAttackDataUrl;

    string uid;

    public static SignUpManager instance;
    // Use this for initialization
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnClickSignUp()
    {
        string fname = signupObject.transform.Find("First Name").GetComponent<InputField>().text;
        string lname = signupObject.transform.Find("Last Name").GetComponent<InputField>().text;
        string age = signupObject.transform.Find("Age").GetComponent<InputField>().text;
        Dropdown d = signupObject.transform.Find("Gender").GetComponent<Dropdown>();
        string gender = d.options[d.value].text;
        string profession = signupObject.transform.Find("Profession").GetComponent<InputField>().text;
        string cellNum = signupObject.transform.Find("Mobile").GetComponent<InputField>().text;
        string email = signupObject.transform.Find("Email").GetComponent<InputField>().text;

        AddNewUser(fname, lname, age, gender, profession, cellNum, email);

        //

        //PlayerPrefs.SetString("Age",age);
        //PlayerPrefs.SetString("Gender",gender);
        //signupObject.SetActive(false);
        //questionObject.SetActive(true);

    }

    public void OnclickSignIn()
    {
        string fname = signinObject.transform.Find("First Name").GetComponent<InputField>().text;
        string mobile = signinObject.transform.Find("Mobile").GetComponent<InputField>().text;

        if (fname == "" || mobile == "")
        {
            ErrorHandler.instance.OnEmptyInput();
            return;
        }

        StartCoroutine(SignIn(fname, mobile));

    }

    public void OnCickTest()
    {
        StartCoroutine(GetUserAgeAndGender("66"));
    }

    public void AddNewUser(string fname, string lname, string age, string gender, string profession, string cell, string email)
    {
        WWWForm form = new WWWForm();

        form.AddField("FirstName", fname);
        form.AddField("LastName", lname);
        form.AddField("Age", age);
        form.AddField("Gender", gender);
        form.AddField("Profession", profession);
        form.AddField("Mobile", cell);
        form.AddField("Email", email);

        WWW www = new WWW(registerUrl, form);
        if (www.error == "Error")
        {
            ErrorHandler.instance.OnPhpServerErrors();
        }

        else
        {
            SuccessHandler.instance.SuccessManager();
        }


    }

    IEnumerator SignIn(string fname, string mobile)
    {
        
        WWWForm form = new WWWForm();
        form.AddField("FirstName", fname);
        form.AddField("Mobile", mobile);

        WWW www = new WWW(loginUrl, form);
        yield return www;

        Debug.Log(www.error);
        if(www.error== "Error")
        {
            ErrorHandler.instance.OnPhpServerErrors();
        }

        else
        {
            JsonData test = JsonDataManager.instance.JsonPerser(www.text);

            string code = test[0]["code"].ToString();
            uid = test[0]["UserId"].ToString();
            if (code == "Success")
            {
                signinObject.SetActive(false);
                questionObject.SetActive(true);
                StartCoroutine(GetUserAgeAndGender(uid));

            }
        }
       

    }

    IEnumerator GetUserAgeAndGender(string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);

        WWW www = new WWW(ageGenderUrl, form);
        yield return www;

        JsonData test = JsonDataManager.instance.JsonPerser(www.text);

        string age = test[0]["Age"].ToString();
        string gender = test[0]["Gender"].ToString();

        QuestionControllerScript.instance.AgeAndGender(age, gender);

        //print(test[0]["Age"]);
        // print(test["Age"] + " " + www.text);

    }

    public void AddToDataBase(List<string> entry) 
    {
        StartCoroutine(HeartAttackDb(entry));

        //print(uid + " "+ www.text);

    }

    IEnumerator HeartAttackDb(List<string> entry)
    {
        WWWForm form = new WWWForm();
        form.AddField("UserId", uid);
        form.AddField("Hypertension", entry[0]);
        form.AddField("HpYear", entry[1]);
        form.AddField("Diabeties", entry[2]);
        form.AddField("DbYear", entry[3]);
        form.AddField("Cholestrol", entry[4]);
        form.AddField("CholYear", entry[5]);
        form.AddField("Smoke", entry[6]);
        form.AddField("FamilyHpten", entry[7]);
        form.AddField("Neausea", entry[11]);
        form.AddField("Sweating", entry[12]);
        form.AddField("Palpitation", 0);
        form.AddField("Dyspnea", entry[9]);
        form.AddField("Syncope", entry[13]);
        form.AddField("ChestPainLocation", entry[15]);

        form.AddField("MarkFigure", entry[16]);
        form.AddField("PainType", entry[17]);
        form.AddField("LastPain", entry[18]);
        form.AddField("LeftArm", entry[19]);
        form.AddField("RightArm", entry[20]);
        form.AddField("Back", entry[21]);
        form.AddField("UpperJaw", entry[22]);

        form.AddField("NoMovement", entry[23]);
        form.AddField("AssoticateNeausea", entry[26]);
        form.AddField("AssoticateVomiting", entry[30]);
        form.AddField("AssoticatePalpitation", entry[27]);
        form.AddField("AssoticateDyspnea", entry[24]);
        form.AddField("AssoticateGiddiness", entry[25]);
        form.AddField("AssoticateSyncope", entry[29]);

        form.AddField("PainTime", entry[31]);
        form.AddField("PainRel", entry[32]);
        form.AddField("PainLong", entry[34]);
        form.AddField("PianSub", entry[33]);
        form.AddField("SmilirPain", entry[35]);

        yield return new WaitUntil(() => OutputController.instance.requestComplete == true);

        form.AddField("Class", OutputController.instance.resultText.text);
        WWW www = new WWW(heartAttackDataUrl, form);

    }

    /*
$UserId =$_POST["UserId"];



$PainTime=$_POST["PainTime"]; 31
$PainRel=$_POST["PainRel"]; 32
$PainLong =$_POST["PainLong"]; 34
$PianSub=$_POST["PianSub"]; 33
$SmilirPain=$_POST["SmilirPain"]; 35
*/

}

