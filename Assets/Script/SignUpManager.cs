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

    string uid;
    // Use this for initialization
    void Start()
    {

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


}

