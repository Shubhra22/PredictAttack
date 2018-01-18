using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.UI;

public class UserDataManager : MonoBehaviour 
{
    public GameObject gridPrefab;
    public Transform dataContainer;

    public static UserDataManager instance;
	// Use this for initialization
	void Start () 
    {
        instance = this;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MakeGridWithData(string jsonText)
    {
        JsonData jsonData = JsonDataManager.instance.JsonPerser(jsonText);
        for (int i = 0; i < jsonData.Count; i++)
        {
            GameObject clone = Instantiate(gridPrefab, dataContainer);
            clone.transform.Find("Date").GetComponent<Text>().text = jsonData[i]["Time"].ToString();
            clone.transform.Find("Result").GetComponent<Text>().text = jsonData[i]["class"].ToString();
            //item["class"]
        }
    }
}
