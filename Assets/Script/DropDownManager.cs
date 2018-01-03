using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DropDownManager : MonoBehaviour 
{
    Dropdown dropDown;
	// Use this for initialization
	void Start () 
    {
        dropDown = GetComponent<Dropdown>();
        dropDown.onValueChanged.AddListener(delegate {
            OnDropDownValueChanged(dropDown);
        });
	}
	
    void OnDropDownValueChanged(Dropdown d)
    {
        GetComponent<InputValues>().key = d.options[d.value].text;
        QuestionControllerScript.instance.ShowHideSubQuestion(System.Convert.ToInt32(transform.parent.name),d.value);
        QuestionControllerScript.instance.ShowHideSubQuestion(System.Convert.ToInt32(transform.parent.name), d.options[d.value].text);

    }
	// Update is called once per frame
	void Update () {
		
	}
}
