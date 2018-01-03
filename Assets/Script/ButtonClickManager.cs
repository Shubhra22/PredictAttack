using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonClickManager : MonoBehaviour 
{
    Button submitButton;

	// Use this for initialization
	void Start () 
    {
        submitButton = GetComponent<Button>();
        submitButton.onClick.AddListener(OnClickListener);
	}
	
	void OnClickListener()
    {
        QuestionControllerScript.instance.GetOutPut();
    }
}
