using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Loadjson : MonoBehaviour {
    [Serializable]
    public class Interactobject
    {
        public int objref;
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


    public RootObject Alldialogs;
    
    // Use this for initialization
    public Loadjson () {
        using (StreamReader r = new StreamReader("Assets/json/data1.json"))
        {
            string json = r.ReadToEnd();
            Debug.Log(json);
            Alldialogs = JsonUtility.FromJson<RootObject>("{\"users\":" + json + "}");
        }


        
    }
}
