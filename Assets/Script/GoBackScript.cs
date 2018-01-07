using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackScript : MonoBehaviour 
{

    public GameObject objectToActive;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(gameObject.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                objectToActive.SetActive(true);
                gameObject.SetActive(false);
            }
        }
	}
}
