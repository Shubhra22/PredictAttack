using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using LitJson;

public class OutputController : MonoBehaviour 
{
    public static OutputController instance;
    public GameObject resultScreen;
    public Text resultText;
    public Text resultDescription;

    public string heartAttckText;
    public string noheartAttackText;
    public string maybeHeartAttackText;

    public string websiteUrl = "http://ssarker.pythonanywhere.com/apps/";
	// Use this for initialization
	void Start () 
    {
        instance = this;
       // StartCoroutine(GetRequestedData(websiteUrl));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ProcessInput(string s)
    {
        string url = websiteUrl + s.Substring(0, s.Length - 1);

        StartCoroutine(GetRequestedData(url));
        print(url);
    }

    IEnumerator GetRequestedData(string url)
    {

        resultScreen.SetActive(true);
        resultText.text = "Predicting...";
        UnityWebRequest req = UnityWebRequest.Get(url);
        {
            yield return req.Send();

            if (req.isError)
            {
                Debug.Log("error" + req.error);
                ErrorHandler.instance.OnPhpServerErrors();
                //StartCoroutine(GetRequestedData(websiteUrl));
            }
            else
            {
                if (req.isDone)
                {
                    string jsonResult = System.Text.Encoding.UTF8.GetString(req.downloadHandler.data);
                    JsonData jsonData = JsonDataManager.instance.JsonPerser(jsonResult);
                    print(jsonData["result"]);

                    resultScreen.SetActive(true);
                    resultText.text = jsonData["result"].ToString();


                    switch (jsonData["result"].ToString())
                    {

                        case "Heart Attack":
                            resultDescription.text = heartAttckText;
                            break;

                        case "No heart attack":
                            resultDescription.text = noheartAttackText;
                            break;

                        case "Maybe heart attack":
                            resultDescription.text = maybeHeartAttackText;
                            break;

                    }
                    //if(jsonResult.ToString() == "Heart attack" || jsonResult.ToString() == "No heart attack" || jsonResult.ToString() == "May be heart attack")
                    //{
                    //    //Debug.Log(jsonResult.ToString());
                    //    JsonData jsonData = JsonDataManager.instance.JsonPerser(jsonResult);
                    //    print(jsonData["result"]);

                    //    resultScreen.SetActive(true);
                    //    resultText.text = jsonResult.ToString();
                    //}
                    //else
                    //{
                    //    //StartCoroutine(GetRequestedData(url));
                    //    JsonData jsonData = JsonDataManager.instance.JsonPerser(jsonResult);
                    //    print(jsonData["result"]);

                    //}

                        //StartCoroutine(GetRequestedData(websiteUrl));

                }
            }
        }

    }

}
