using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class JsonDataManager : MonoBehaviour 
{

    JsonData itemData;

    public static JsonDataManager instance;

    private void Start()
    {
        instance = this;
    }
    public JsonData JsonPerser(string jsonString)
    {
        itemData = JsonMapper.ToObject(jsonString);
        //JsonMapper.ToObject<Wrapper<RootObject>>(jsonString);
        //Person thomas = JsonMapper.ToObject<Person>(json);
        return itemData;
    }

    public T[] getJsonAr<T>(string jsonstring)
    {
        Wrapper<T> wrapper = JsonMapper.ToObject<Wrapper<T>>(jsonstring);
       // JsonMapper.ToWrapper()
        //Person thomas = JsonMapper.ToObject<Person>(json);
        return wrapper.array;
    }

    public static T[] getJsonArray<T>(string jsonstring)
    {
        string newjson = "{\"array\":" +jsonstring +"}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newjson);
        return wrapper.array;
    }

    [System.Serializable]

    private class Wrapper<T>
    {
        public T[] array;
    }

}

[System.Serializable]
public class RootObject
{
    public string id { get; set; }
    public string hypertension { get; set; }
    public string hyp_med { get; set; }
    public string diabetes { get; set; }
    public string dia_med { get; set; }
    public string cholesterol { get; set; }
    public string chol_med { get; set; }
    public string smoke { get; set; }
    public string family_history { get; set; }
    public string chest_pain { get; set; }
    public string cp_loaction { get; set; }
    public string cp_mark { get; set; }
    public string cp_type { get; set; }
    public string Neausea { get; set; }
    public string Vomiting { get; set; }
    public string Sweating { get; set; }
    public string Palpitation { get; set; }
    public string Dyspnea { get; set; }
    public string Giddiness { get; set; }
    public string Syncope { get; set; }
    public string cp_duration { get; set; }
    public string LeftArm { get; set; }
    public string RightArm { get; set; }
    public string Back { get; set; }
    public string NoMovement { get; set; }
    public string UpperJaw { get; set; }
    public string AssoticateNeausea { get; set; }
    public string AssoticateVomiting { get; set; }
    public string AssoticateSweating { get; set; }
    public string AssoticatePalpitation { get; set; }
    public string AssoticateDyspnea { get; set; }
    public string doing_while { get; set; }
    public string relieved { get; set; }
    public string persistence { get; set; }
    public string subsided { get; set; }
    public string similar_pain { get; set; }
    public string @class { get; set; }
    public string Time { get; set; }
}
