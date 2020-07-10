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
    public float delayLead = 2000f;
    public float delayClue = 3000f;
    public static int curPlevel = 1, curMlevel = 1;
    public static int score=0;
    public static Quaternion fpsangle;
    public bool selected = false;
    public bool[] visited = new bool[50];
    public bool playeractive = false;
    public float basetime = 0;
    public bool click_close = false;
    public int phase = 0, milestone = 0;
    // Use this for initialization
    [System.Obsolete]

    public void Start()
    {
        for (int i = 0; i < 50; i++)
            visited[i] = false;

        temp.enabled = false;
        Cursor.visible = false;
    
        obj = new Loadjson();
        Alldialogs = obj.Alldialogs;
        ui.GetComponent<Canvas>().enabled = false;
      // StartCoroutine( Start_routine());  
    }
    public void act_click() {
        score+=10;
        Debug.Log(score);
    }

    public void button_click() {
        selected = true;
        ui.GetComponent<Canvas>().enabled = false;
    }
    public void close_click()
    {
        click_close = true;
        ui.GetComponent<Canvas>().enabled = false;
    }

    
    [Obsolete]
    void Update () 
    {
        
        if ((Time.time - basetime) > 10) {
            //set lead dialogs
            if (phase == 0)
            {
                if (milestone == 0)
                {
                    StartCoroutine(ShowTextnooptions("I should check kitchen drawer"));
                }
                else
                {
                    StartCoroutine(ShowTextnooptions(Alldialogs.users[1].leading));
                    
                }
            }
            else if (phase == 1) { }

        }


        Ray ray = new Ray(fpsCam.transform.position, fpsCam.transform.forward);
        RaycastHit hit;
       
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag.Equals("Interact"))
            {

                Debug.Log(hit.collider.gameObject.name);
                
                if (!playeractive && Input.GetMouseButtonDown(0))
                {
                    int curhit = Convert.ToInt32(hit.collider.gameObject.name);
                    if(visited[curhit])
                    {

                        StartCoroutine(ShowTextnooptions(Alldialogs.users[curhit].text));
                    }
                    if (phase == 0) 
                         {

                            //kitchen_drawer. show msg, no need for visited, set basetime to 0
                            if (milestone == 0 && curhit == 0)
                            {
                                StartCoroutine(ShowTextnooptions(Alldialogs.users[curhit].text));
                                visited[curhit] = true;
                                milestone++;
                            }
                            if (milestone == 1 && curhit == 1)
                            {
                                StartCoroutine(ShowText(curhit));
                                visited[curhit] = true;
                                phase++;
                                milestone = 0;
                            }
                            basetime = Time.time;
                        }
                    


                }
            }

        }
    }
    [Obsolete]
    IEnumerator Start_routine()
    {
        StartCoroutine( ShowTextnooptions("I will lead you to kitchen drawer"));
        Debug.Log("1");
        
        while (true)
        {
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit,3))
            {
                Debug.Log("if ke andar");
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.tag.Equals("Interact"))
                {
                    Debug.Log("abhich click kar");
                    
                    int curhit = Convert.ToInt32(hit.collider.gameObject.name);
                    Debug.Log(curhit);
                    if (curhit == 0 && (Input.GetKey("e")|| Input.GetKey("mouse 0")) )
                    {
                        StartCoroutine(ShowTextnooptions(Alldialogs.users[0].text)); //i am kitchen drawer
                        break;
                    }
                }
            }
            yield return null;
        }
        
        visited[0] = true;
        basetime = Time.time;
        while (Time.time - basetime < 10) { yield return 0; }
        Debug.Log("4");
        ShowTextnooptions(Alldialogs.users[1].text); //explain about game and stuff
        Debug.Log("5");
        playeractive = true;
        basetime = Time.time;

        while (Time.time - basetime < 10 && !visited[1]) {
            
        }
        if (!visited[1]) {
            ShowTextnooptions(Alldialogs.users[1].leading);
        }
        Debug.Log("6");
        basetime = Time.time;

        while (Time.time - basetime < 10 && !visited[1])
        {

        }
        if (!visited[1])
        {
            ShowTextnooptions(Alldialogs.users[1].clue);
        }
        Debug.Log("7");
    }
    
    [Obsolete]

    IEnumerator ShowText(int curhit)
    {
        playeractive = false;
        Cursor.visible = true;
       // Cursor.lockState;
        GameObject.Find("act text").GetComponent<TextMeshProUGUI>().text = Alldialogs.users[curhit ].option[0];
        GameObject.Find("ignore text").GetComponent<TextMeshProUGUI>().text = Alldialogs.users[curhit ].option[1];
        GameObject.Find("option text").GetComponent<TextMeshProUGUI>().text = Alldialogs.users[curhit ].option[2];
        ui.GetComponent<Canvas>().enabled = true;
        if (visited[curhit])
        {
            // ui.GetComponent<Button>().enabled = false;
            GameObject.Find("choice_canvas").GetComponent<Canvas>().enabled = false;
        }
        Vector3 pos = fpsCam.transform.position;
        Quaternion rot = fpsCam.transform.rotation;
        temp.transform.position = pos;
        temp.transform.rotation = rot;
        temp.enabled = true;
        fpsCam.enabled = false ;

        string curstring = Alldialogs.users[curhit].text;
        string lead = Alldialogs.users[curhit].leading;
        string clue = Alldialogs.users[curhit].clue;
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
        temp.enabled = false;


        fpsCam.enabled = true;
        fpsCam.transform.position = pos;
        fpsCam.transform.rotation = rot;

        visited[curhit] = true;
        //fpsCam.transform.rotation = temp.transform.rotation;
       
        ui.GetComponent<Canvas>().enabled = false;
        playeractive = true;
        selected = false;
        Cursor.visible = false;
    }

    

    [Obsolete]
    IEnumerator ShowTextnooptions(string curstring)
    {
        playeractive = false;
        Cursor.visible = true;
        
        ui.GetComponent<Canvas>().enabled = true;
        GameObject.Find("choice_canvas").GetComponent<Canvas>().enabled = false;
        
        Vector3 pos = fpsCam.transform.position;
        Quaternion rot = fpsCam.transform.rotation;
        temp.transform.position = pos;
        temp.transform.rotation = rot;
        temp.enabled = true;
        fpsCam.enabled = false;
        StartCoroutine(dialog(curstring));
        
        while (!click_close)
        {
            yield return null;
        }
        
        temp.enabled = false;
        fpsCam.enabled = true;
        fpsCam.transform.position = pos;
        fpsCam.transform.rotation = rot;
        ui.GetComponent<Canvas>().enabled = false;
        click_close = false;
        Cursor.visible = false;
        basetime = Time.time;
    }
    IEnumerator dialog(string curstring)
    {
        for (int i = 0; i < curstring.Length; i++)
        {
            string currentText = curstring.Substring(0, i + 1);
            GameObject dt = GameObject.Find("dt");
            dt.GetComponent<TextMeshProUGUI>().text = currentText;

            yield return new WaitForSeconds(delay);
        }
    }
}
   

