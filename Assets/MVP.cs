using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;


public class MVP : MonoBehaviour {

    public Camera fpsCam;
    public GameObject ui;
    public Camera temp;
    public float delay = 0.001f;
    public Loadjson obj;
    public Loadjson.RootObject Alldialogs;
    public static int curPlevel = 1, curMlevel = 1;
    public static int score=0;
    public static Quaternion fpsangle;
    public bool selected = false;
    public bool[] visited = new bool[50];
    // Use this for initialization
    [System.Obsolete]

    public void Start()
    {
        for (int i = 0; i < 50; i++)
            visited[i] = false;
        Debug.Log("started");
        temp.enabled = false;
        Cursor.visible = false;
        
        ui.GetComponent<Canvas>().enabled = false;
        obj = new Loadjson();
        Alldialogs = obj.Alldialogs;
    }
    public void act_click() {
        score+=10;
        Debug.Log(score);
    }

    public void button_click() {
        selected = true;
        ui.GetComponent<Canvas>().enabled = false;
    }
    
    public bool displaying = false;

    [Obsolete]
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
                
                if (!displaying && Input.GetMouseButtonDown(0))
                {
                    
                       
                    int curhit = Convert.ToInt32(hit.collider.gameObject.name);
                    Debug.Log(curhit);
                    if (curPlevel == 1)
                    {
                        if (curhit <= 2 && curhit > 0)
                        {
                            
                            StartCoroutine(ShowText(curhit, Alldialogs.users[curhit - 1].text));
                            
                        }
                    }
                    else if(curPlevel == 2)
                    {



                    }
                }
            }

        }
    }
    

    [Obsolete]

    IEnumerator ShowText(int curhit, string curstring)
    {
        displaying = true;
        Cursor.visible = true;
        
        ui.GetComponent<Canvas>().enabled = true;
        if (visited[curhit])
        {
            // ui.GetComponent<Button>().enabled = false;
            GameObject.Find("act").GetComponent<Image>().enabled = false ;
            GameObject.Find("act text").GetComponent<TextMeshPro>().enabled = false;
        }
        temp.transform.position = fpsCam.transform.position;
        temp.transform.rotation = fpsCam.transform.rotation;
        temp.enabled = true;
        fpsCam.enabled = false ;

        for (int i = 0; i<curstring.Length; i++)
        {
            string currentText = curstring.Substring(0, i+1);
            GameObject dt = GameObject.Find("dt");
            dt.GetComponent<TextMeshProUGUI>().text = currentText;
           
;            yield return new WaitForSeconds(delay);
        }
        while (!selected)
        {
            yield return null;
        }
        visited[curhit] = true;
        temp.enabled = false;
        fpsCam.transform.rotation = temp.transform.rotation;
        fpsCam.enabled = true;
        ui.GetComponent<Canvas>().enabled = false;
        displaying = false;
        selected = false;
        Cursor.visible = false;
    }
}


   

