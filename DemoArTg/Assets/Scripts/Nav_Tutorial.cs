using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Nav_Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TutorialStep_n0(){
        SceneManager.LoadScene("TS_0");
    }

    public void TutorialStep_n1(){
        SceneManager.LoadScene("TS_1");
    }

    public void TutorialStep_n2(){
        SceneManager.LoadScene("TS_2");
    }

    public void TutorialStep_n3(){
        SceneManager.LoadScene("TS_3");
    }
}
