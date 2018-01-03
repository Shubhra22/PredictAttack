using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleManager : MonoBehaviour 
{
    Toggle tog;
	// Use this for initialization
	void Start () 
    {
        tog = GetComponent<Toggle>();
        tog.onValueChanged.AddListener(delegate {
            OnToggleValueChanged(tog);
        });

        GetComponent<InputValues>().key = "No";
    }

    void OnToggleValueChanged(Toggle toggle)
    {
        if (toggle.isOn)
            GetComponent<InputValues>().key = "Yes";//tog.GetComponentInChildren<Text>().text;
        else
            GetComponent<InputValues>().key = "No";
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
