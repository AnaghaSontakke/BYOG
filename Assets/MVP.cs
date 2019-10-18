using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;


public class MVP : MonoBehaviour
{

    public Camera fpsCam;
    public GameObject ui;
    public GameObject b1;
    public GameObject b2;
    public GameObject b3;
    
    public Camera temp;
    public float delay = 0.001f;
  //  public Loadjson obj;
  //  public Loadjson.RootObject Alldialogs;
    public static int score = 0;
    public bool selected = false;
    public bool[] visited = new bool[50];
    public bool playeractive = false;
    public float basetime = 0;
    public bool click_close = false;
    public int milestone = 0;

    public string[] dial = { "This is where it started, and this is where it is going to end. I thought I will be staying here for my entire life. Well, life does take uncertain outcomes. Everything here takes me back in the time!",
         "She always says that I need to first know the \"WHY\" and asked me to check out everything here to get closer to the truth! So how will the empty drawers and cabinets talk to me? These experts are mostly useless",
        "Oh, so ghosts don't live here yet! I will probably have to spend some time here, even if it means nothing much. Move ahead, shall I?",
        "Let me take a tour and see what these guys might have changed in here, sofa might be a good start",
        "So, I will need to follow my instincts and search the house for clues as she instructed remembering my past, although I despise instructions.",
        "They haven't changed the place much though! Now that I have taken the tour of the house, I feel I can do some searching.",
        "Where should I begin? Maybe things closest to the entrance.",
        "I was always scared of my father, but there was one thing that he told me and it stuck to me. I don't really recall it correctly, but remember writing it somewhere",
        "Here it is! \"To know what is right and to choose to ignore it is the act of a coward\" it helped me choose whenever there were many ways I always acted on my instincts, what will I do now I wonder?",
        "Childhood was not so good. They used to say I got bored very easily. Video games were my escape to all the adults and their cockiness",
        "They always tried to put me down, called me naughty and unwanted! I used to enjoy my sweet revenge all the time. Amazing times when I got bored and went outside",
        "This game was my favourite and I remember how I used to throw pebbles on those uncle's houses who used to pester me. I later felt",
        "Childhood reminds me of that disgusting toy! I wonder where the rest would have been. The old lady always kept it in a cupboard. I would be pissed if they have thrown them",
        "My foster parents pretended to be good to me and brought a disgusting bear! I couldn't look at it, so I decided to do something about it",
        "Amazing, I know the lady more than I think I know. I burnt that bear in the backyard. No one came to know, but they asked about it later. I easily lied that I loved it but",
        "I got wiser when I was in my high school. I was way smarter than most of them, girls always would want to talk to me. I guess they never had the courage to come and talk so I used mails to contact them",
        "Mails were a comfortable medium for them I thought. My first date was with Linda. I might still have her photo on the computer.",
        "I almost forgot her face. She looked charming then, but nobody understood my talents. We did decide to go to a nice place, but I didn't go because",
        "This house has so many memories! Some memories also leave marks. I had a cousin and I didn't like him much, one day when he was crying. I just locked him up in the bathroom.",
        "Everyone was laughing later when we all cousins met, but I remember how adults reacted then",
        "I left him here for 3 hours, he was sobbing, struggling and shouting! I didn't open the cabinet till mom came. I didn't say sorry to him, of course, because",
        "My doctor told me to maintain a journal. I did get married and it was the biggest mistake of my life",
        "I never understood what love is! But I thoroughly loved the way I used to play around with her",
        "This diary is something where I wrote all my secrets. All the little tricks I used on the people, and why shouldn't I. I was never respected for what I am. One of which I remember was",
        "Sometimes I thought about the weird urges I used to get, and sometimes I acted on it. One day my kids got a dog! I hated it, but I had to pretend to be a good father",
        "I had no time for a dog! Cleaning the shit and barks. It even attacked me.",
        "This picture just reminds me of how it was helpless when I made it go to sleep forever. I did it and I felt",
        "After that incident, nothing remained the same. I felt powerful and all those weird ideas started feeling normal and usual. Just grabbing the knife would give me a different sense of thrill that I never knew about earlier",
        "I wanted to do it again. So I did! One day I grabbed that knife when the little Emily had come to ask for chocolates on Halloween",
        "That was it! She was nothing to me. It doesn't mean that I wanted to hurt her or do something. There was a question in my mind; If I ignore this will I become a coward. I followed that quote and",
        "Just the idea of going ahead was fascinating! I wondered if I could do it. It took me seconds to come up with a plan. Obviously I could dispose of it by chopping the body in the bathroom",
        "With this knife I did it. I can't say that I felt very good or so! I just went ahead with it",
        "Next steps were simple! It became a ritual. I had easy access to people. Not only people I hated but anyone would do. I should do",
        "I still remember that day, when Emily's parents had come to search for her at our doorstep.",
        "I felt no remorse while facing them. I greeted them and they were all blah blah blah",
        "They were standing right here! I felt no remorse, It never occurred to me that I never thought about getting caught or why I didn't even think of why I didn't feel sorry for them because",
        "My doctor at the prison had asked me to collect memories and try and analyse them, she says it is my brain's structure responsible and there are some signs such as manipulation, lying, narcissism, lack of remorse, lack of empathy and irresponsibility. She says I can help other sick people like me by telling what had happened. She also says I can feel normal someday!",
        "My doctor at the prison had asked me to collect memories and try and analyse them, she says it is my brain's structure responsible and there are some signs such as manipulation, lying, narcissism, lack of remorse, lack of empathy and irresponsibility. I dont believe when she says that all this information will help sick people like me. Whereas this society itself is sick.",
        "Checking out everything is boring but will have to be done",
        "I can't pass time by just roaming around or standing here. Let's just try and find something which she supposedly thinks will help me",
        "Well, she had challenged me to visit this house. Here I am, doing nothing!",
        "I had read somewhere the memory related to objects is pretty strong! I might have to search for more"
    };
    
    // Use this for initialization
    [System.Obsolete]

    public void Start()
    {


        //This enables Main Camera


        temp.enabled = false;
      
        Cursor.visible = false;

        //  obj = new Loadjson();
        //  Alldialogs = obj.Alldialogs;
       ui.gameObject.SetActive(false);
        // ************************************************************** Read this*********************************************
        //Initiating the first dialog box, ShowTextnooptions shows canvas with buttons disabled
        //Using two coroutines _--- ShowText() and ShowTextnooptions()
        //showtext has itself a call to showtextnooption with string passed as the leading dialog,
        //**************************************************
       StartCoroutine(ShowTextnooptions(dial[0]));

    }
    public void act_click()
    {
        //triggered when option selected, attached to option buttons, updates score
        if(milestone>=3 && milestone<=5) score += 10;
        if (milestone == 6) score += 20;
        if (milestone >= 7) score += 30;
        Debug.Log(score);
    }

    public void button_click()
    {
        //attached to options and close button
        selected = true;
        ui.gameObject.SetActive(false);
    }
    public void close_click()
    {
        //not really using this function
        click_close = true;
     //   ui.gameObject.SetActive(false);
    }
    public void exit_game()
    {
        Application.Quit();
    }

    [Obsolete]
    void Update()
    {

        /* if ((Time.time - basetime) > 10)
         {
             basetime = Time.time;
             //set lead dialogs
             if (phase == 0)
             {
                 if (milestone == 0)
                 {
                 }
                 else
                 {
                   //  StartCoroutine(ShowTextnooptions(Alldialogs.users[1].leading));

                 }
             }
             else if (phase == 1) { }

         }*/

        
        Ray ray = new Ray(fpsCam.transform.position, fpsCam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit,2))
        {
            if (hit.collider.tag.Equals("Interact"))
            {
                //interact object detected
                Debug.Log(hit.collider.gameObject.name);

                if ( Input.GetMouseButtonDown(0))
                {
                    int curhit = Convert.ToInt32(hit.collider.gameObject.name);
                    Debug.Log(milestone);


                    //matching the milestone with current hit object
                        if (milestone == 0 && curhit == 0)
                        {
                           //kitchen drawer
                           
                            {

                            //ShowText( leader dialog numer(to show next without options, --------------------)
                                StartCoroutine(ShowText(3,curhit, dial[2], "juice", "ignore", "chips"));
                            
                                

                            }
                       
                    }
                        if (milestone == 1 && curhit == 1)
                        {//sofa wala
                            StartCoroutine(ShowTextnooptions(dial[5] + dial[6]));
                            milestone++;
                        }
                        if (milestone == 2 && curhit == 2)
                        {
                            //fridge sticky note
                            
                            {
                                StartCoroutine(ShowText(9,curhit, dial[8], "Act on what is right", "ignore", "Ignore it all!"));
                              //  StartCoroutine(ShowTextnooptions(dial[9]));
                                
                            }
                      
                    }
                        if (milestone == 3 && curhit == 3)
                        {
                            /*if (visited[curhit])
                            {
                                StartCoroutine(ShowTextnooptions(dial[11]));
                            }
                            else
                            {*/
                                StartCoroutine(ShowText(12,curhit, dial[11],"Amazing that I pissed them off", "I can never got caught", "staying home is better"));
                              //  StartCoroutine(ShowTextnooptions(dial[12]));
                                
                            //}
                        
                    }
                        if (milestone == 4 && curhit == 4)
                        {
                            /*if (visited[curhit])
                            {
                                StartCoroutine(ShowTextnooptions(dial[14]));
                            }
                            else*/
                            {
                                StartCoroutine(ShowText(15,curhit, dial[14], "The teacher took it away", "Sam took it", "Had to tell the truth"));
                               // StartCoroutine(ShowTextnooptions(dial[15]));
                                
                            }
                       
                        }
                        if (milestone == 5 && curhit == 5)
                        {
                            StartCoroutine(ShowText(18, curhit, dial[17], "I was just too good for her", "She was a jerk anyway", "I looked fat that day"));
                        }
                    if (milestone == 6 && curhit == 6)
                    {
                        StartCoroutine(ShowText(21, curhit, dial[20], "He deserved it", "He wasn't hurt", "I was ashamed"));
                    }
                    if (milestone == 7 && curhit == 7)
                    {
                        StartCoroutine(ShowText(24, curhit, dial[23], "Fooled them that I was working as an Engineer", "Carried hobbies which they had no idea about", "prepared a surprise party"));
                    }
                    if (milestone == 8 && curhit == 8)
                    {
                        StartCoroutine(ShowText(27, curhit, dial[26], "Pure relief", "One less trouble", "Guilty and arranged a funeral later"));
                    }
                    if (milestone == 9 && curhit == 9)
                    {
                        StartCoroutine(ShowText(30, curhit, dial[29], "Acted on impulses", "Acted out everything in my mind", "Ignored the urges"));
                    }
                    if (milestone == 10 && curhit == 10)
                    {
                        StartCoroutine(ShowText(33, curhit, dial[32], "it again and again", "not risk getting caught", "consult someone"));
                    }
                    if (milestone == 11 && curhit == 11)
                    {
                        StartCoroutine(ShowText(42, curhit, dial[35], "This how it was always", "Why and how should I have cared", "The world is insane or I am"));
                    }
                }
            }

        }
        if (milestone > 11)
        {
            if(score<=50) StartCoroutine(ShowTextnooptions(dial[36]));
            else StartCoroutine(ShowTextnooptions(dial[37]));
        }
    }   
  
    [Obsolete]

    IEnumerator ShowText(int leader, int curhit, string curstring,string option1, string option2, string option3)
    {

        //This shows canvas with options
        milestone++;
        playeractive = false;
        Cursor.visible = true;
        // Cursor.lockState;
        ui.gameObject.SetActive(true);

        b1.gameObject.SetActive(true);
        b2.gameObject.SetActive(true);
        b3.gameObject.SetActive(true);

        b1.gameObject.GetComponentInChildren<Text>().text = option1;
        b2.gameObject.GetComponentInChildren<Text>().text = option2;
        b3.gameObject.GetComponentInChildren<Text>().text = option3;
      
        Vector3 pos = fpsCam.transform.position;
        Quaternion rot = fpsCam.transform.rotation;
        temp.transform.position = pos;
        temp.transform.rotation = rot;
        temp.enabled = true;
        fpsCam.enabled = false;

     //   string curstring = Alldialogs.users[curhit].text;
      //  string lead = Alldialogs.users[curhit].leading;
     //   string clue = Alldialogs.users[curhit].clue;


        //displays the text
        for (int i = 0; i < curstring.Length; i++)
        {
            string currentText = curstring.Substring(0, i + 1);
            GameObject dt = GameObject.Find("Narrative_box");
            dt.GetComponentInChildren<Text>().text = currentText;

            yield return new WaitForSeconds(delay);
        }

        //waiting for option to be clicked, when clicked, selected gets true
        while (!selected)
        {
            yield return null;
        }
        
        fpsCam.enabled = true;
        temp.enabled = false;


        
        fpsCam.transform.position = pos;
        fpsCam.transform.rotation = rot;

        visited[curhit] = true;
        //fpsCam.transform.rotation = temp.transform.rotation;

        playeractive = true;

        //selected set to false for further use
        selected = false;
        Cursor.visible = false;
        ui.gameObject.SetActive(false);
        //activationg lead dialog
        StartCoroutine(ShowTextnooptions(dial[leader]));
        visited[curhit] = true;

    }



    [Obsolete]
    IEnumerator ShowTextnooptions(string curstring)
    {
        playeractive = false;
        Cursor.visible = true;

        ui.gameObject.SetActive(true);

        b1.gameObject.SetActive(false);
        b2.gameObject.SetActive(false);
        b3.gameObject.SetActive(false);
        //trying to save parameters to pas to temporary camera

        Vector3 pos = fpsCam.transform.position;
        Quaternion rot = fpsCam.transform.rotation;
       
        temp.enabled = true;
        temp.transform.position = pos;
        temp.transform.rotation = rot;
        fpsCam.enabled = false;

       // GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
        // StartCoroutine(dialog(curstring));
        StartCoroutine(dialog(curstring));


        //waiting for a close button to be clicked
        while (!selected)
        {
            yield return null;
        }
        Debug.Log("fsd");
       // fpsCam.enabled = true;
        temp.enabled = false;
         fpsCam.enabled = true;
       // GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
        fpsCam.transform.position = pos;
        fpsCam.transform.rotation = rot;

        b1.gameObject.SetActive(true);
        b2.gameObject.SetActive(true);
        b3.gameObject.SetActive(true);

        ui.gameObject.SetActive(false);

        selected = false;
        Cursor.visible = false;
        basetime = Time.time;


      
    }
    IEnumerator dialog(string curstring)
    {
        for (int i = 0; i < curstring.Length; i++)
        {
            string currentText = curstring.Substring(0, i + 1);
            GameObject dt = GameObject.Find("Narrative_box");
            dt.GetComponentInChildren<Text>().text = currentText;

            yield return new WaitForSeconds(delay);
        }
    }
}


