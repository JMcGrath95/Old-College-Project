using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{


    public void OnQuitButtonClicked() => Application.Quit(0);
    public void OnOptionsButtonClicked() 
    { 
    
    
    }
    public void OnPlayButtonClicked() => SceneManager.LoadScene(5);  //5 is Shane's scene, change this later.



}
