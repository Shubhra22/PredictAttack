using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ErrorHandler : MonoBehaviour 
{

    public GameObject generalErrorScreen;
    public Text errorText;

    public string internetErrorText;
    public string emptyErrorText;
    public string userExistsText;


    public static ErrorHandler instance;
	// Use this for initialization
	void Start () 
    {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPhpServerErrors()
    {
        generalErrorScreen.SetActive(true);
        errorText.text = internetErrorText;
    }

    public void OnEmptyInput()
    {
        generalErrorScreen.SetActive(true);
        errorText.text = emptyErrorText;
    }

    public void OnUserExits()
    {
        generalErrorScreen.SetActive(true);
        errorText.text = userExistsText;
    }
}
