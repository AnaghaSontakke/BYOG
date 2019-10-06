using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVP : MonoBehaviour {

    public Camera fpsCam;
   // public GameObject player;
    public GameObject prefab;
    public float delay = 0.1f;
    public string fullText;//can be fetched everytime from a reserve of dialogues
    private string currentText = "";
    // Use this for initialization
    
	void Update () {

        Ray ray = new Ray(fpsCam.transform.position, fpsCam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("new ray"+ hit);
            if (hit.collider.tag.Equals("Interact"))
            {
                Debug.Log("new ray colliding");
                if (Input.GetMouseButtonDown(0))
                {
                    Instantiate(prefab, fpsCam.transform.position, Quaternion.identity);
                    StartCoroutine(ShowText());//it will call IEnumerator Show text which can use time and delays
                }
            }

        }
    }
    IEnumerator ShowText()
    {
        for (int i = 0; i<fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            prefab.GetComponentInChildren<TextMesh>().text = currentText;
            yield return new WaitForSeconds(delay);
}
    }
}


   

