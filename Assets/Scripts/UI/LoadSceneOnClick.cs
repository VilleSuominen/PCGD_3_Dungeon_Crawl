using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    public void LoadByIndex(int sceneIndex)     // Loads a new scene according to its index number in the build menu
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
