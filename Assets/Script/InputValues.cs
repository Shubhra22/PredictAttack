using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputValues : MonoBehaviour
{

    public string key = "No";

    public static InputValues instance;
	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	
}
