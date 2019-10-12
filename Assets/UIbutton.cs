using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIbutton : MonoBehaviour {
    public GameObject menuCanvas;
    public GameObject controlsCanvas;
    public GameObject creditsCanvas;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void PauseButton()
    {
        menuCanvas.SetActive(true);
    }
    public void PlayButton()
    {
        menuCanvas.SetActive(false);
    }
    public void ControlsButtonMenu()
    {
        controlsCanvas.SetActive(true);
    }
    public void ControlsBackButton()
    {
        controlsCanvas.SetActive(false);
    }
    public void CreditsButtonMenu()
    {
        creditsCanvas.SetActive(true);
    }
    public void CreditsBackButton()
    {
        creditsCanvas.SetActive(false);
    }
	void Update () {

    }
}
