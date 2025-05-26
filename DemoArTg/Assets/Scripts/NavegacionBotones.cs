using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NavegacionBotones : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NavegarSignUp(){
        SceneManager.LoadScene("SignUp_view");
    }

    public void NavegarLogIn(){
        SceneManager.LoadScene("LogIn_view");
    }

    public void NavegarMain(){
        SceneManager.LoadScene("Main_view");
    }

    public void NavegarHome(){
        SceneManager.LoadScene("Home_view");
    }

    public void NavegarAngelitas(){
        SceneManager.LoadScene("Vuforia_test");
    }

}
