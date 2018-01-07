using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using UnityEngine.Networking;

public class ShowAllData : MonoBehaviour 
{


	// Use this for initialization
	void Start () 
    {
        StartCoroutine(GetRequestedData("http://sohure.com/heart/showAll.php"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator GetRequestedData(string url)
    {
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

                    print(jsonData[0]);

                }
            }
        }
    }

}
