using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelAsync : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1045,500,false);
        Invoke("Loading",2);
    }
    void Loading(){
        SceneManager.LoadSceneAsync(1);  
    }
}
