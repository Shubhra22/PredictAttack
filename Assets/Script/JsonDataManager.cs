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
public class Currency
{
    public string code { get; set; }
    public string name { get; set; }
    public string symbol { get; set; }
}

[System.Serializable]
public class Language
{
    public string iso639_1 { get; set; }
    public string iso639_2 { get; set; }
    public string name { get; set; }
    public string nativeName { get; set; }
}

[System.Serializable]
public class Translations
{
    public string de { get; set; }
    public string es { get; set; }
    public string fr { get; set; }
    public string ja { get; set; }
    public string it { get; set; }
    public string br { get; set; }
    public string pt { get; set; }
    public string nl { get; set; }
    public string hr { get; set; }
    public string fa { get; set; }
}

[System.Serializable]
public class RootObject
{
    public string name { get; set; }
    public List<string> topLevelDomain { get; set; }
    public string alpha2Code { get; set; }
    public string alpha3Code { get; set; }
    public List<string> callingCodes { get; set; }
    public string capital { get; set; }
    public List<object> altSpellings { get; set; }
    public string region { get; set; }
    public string subregion { get; set; }
    public int population { get; set; }
    public List<object> latlng { get; set; }
    public string demonym { get; set; }
    public double? area { get; set; }
    public double? gini { get; set; }
    public List<string> timezones { get; set; }
    public List<object> borders { get; set; }
    public string nativeName { get; set; }
    public string numericCode { get; set; }
    public List<Currency> currencies { get; set; }
    public List<Language> languages { get; set; }
    public Translations translations { get; set; }
    public string flag { get; set; }
    public List<object> regionalBlocs { get; set; }
    public string cioc { get; set; }
}
