using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void GoToScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }
}
