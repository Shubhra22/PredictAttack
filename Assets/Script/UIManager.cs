using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour 
{
    public Transform rootParent;

    Transform activeObject;
	// Use this for initialization
	void Start () 
    {
        //for (int i = 0; i < rootParent.childCount; i++)
        //{
        //    if(rootParent.GetChild(i).gameObject.activeSelf)
        //    {
        //        activeObject = rootParent.GetChild(i);
        //    }
        //}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Reload()
    {
        SceneManager.LoadScene("Scene");
    }

}
