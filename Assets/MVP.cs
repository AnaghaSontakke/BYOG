using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}

public class MVP : MonoBehaviour {


    [Serializable]
    public class Interactobject {

        public int phase;
        public int milestone;
        public string text;
        public string[] option;
        public string leading;
        public string clue;

    }
    [Serializable]
    public class RootObject
    {
        public Interactobject[] users;
    }

    public Camera fpsCam;
    public GameObject prefab;
    public float delay = 0.1f;
    public string fullText;//can be fetched everytime from a reserve of dialogues
    private string currentText = "";
    public List<Interactobject> items;
    public static int curPlevel = 1, curMlevel = 1;
    public static int score=0;
    public RootObject myObject;
    // Use this for initialization
    [System.Obsolete]

    void Start()
    {
        LoadJson();
    }

    public void LoadJson()
    {
        using (StreamReader r = new StreamReader("Assets/json/data1.json"))
        {
            string json = r.ReadToEnd();
            Debug.Log(json);
            myObject = JsonUtility.FromJson<RootObject>("{\"users\":" + json + "}");
            Debug.Log(myObject.users[2].leading);
            
            //items = JsonConvert.DeserializeObject<List<Interactobject>>(json);
        }
    }

    void Update () 
    {

        Ray ray = new Ray(fpsCam.transform.position, fpsCam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag.Equals("Interact"))
            {
                Debug.Log(hit.collider.gameObject.name);
                Debug.Log("new ray colliding");
                if (Input.GetMouseButtonDown(0))
                {
                    int curhit = Convert.ToInt32(hit.collider.gameObject.name);
                    Debug.Log(curhit);
                    if (curPlevel == 1)
                    {
                        if (curhit <= 2 && curhit > 0)
                        {
                            StartCoroutine(ShowText(myObject.users[curhit - 1].text));
                        }
                    }
                    else if(curPlevel == 2)
                    {



                    }


                   // GameObject prefabgo = Instantiate(prefab, fpsCam.transform.position, Quaternion.identity);
                    //it will call IEnumerator Show text which can use time and delays
                   // StartCoroutine(ShowText());
                }
            }

        }
    }
    IEnumerator ShowText(string curstring)
    {
        GameObject prefabgo = Instantiate(prefab, fpsCam.transform.position, Quaternion.identity);
        fpsCam.enabled = false ;
        for (int i = 0; i<curstring.Length; i++)
        {
            currentText = curstring.Substring(0, i+1);
            GameObject dt = GameObject.Find("dt");
            dt.GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        Debug.Log("yay");
        while (!Input.GetButton("ok"))
        {
            yield return null;
        }
        fpsCam.enabled = true;
        Destroy(prefabgo);
        
    }
}


   

