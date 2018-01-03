using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessHandler : MonoBehaviour 
{

    public GameObject successScreen;

    public static SuccessHandler instance;
	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SuccessManager()
    {
        successScreen.SetActive(true);
    }
}
