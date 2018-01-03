using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InputFieldManager : MonoBehaviour {

    InputField inpField;
    // Use this for initialization
    void Start()
    {
        inpField = GetComponent<InputField>();
        inpField.onValueChanged.AddListener(delegate {
            OnInputValueChanged(inpField);
        });
    }

    void OnInputValueChanged(InputField inputField)
    {
        InputValues.instance.key = inputField.text;
    }
    // Update 
}
