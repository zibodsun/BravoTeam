using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasTransition : MonoBehaviour
{
    public GorillaSceneManager sceneManager;

    public void PlayFirstLine() {
        sceneManager.PlayFirstLine();
    }

    public void PlayEnding() {
        sceneManager.Play(4);
    }

    public void SceneTransition() {
        SceneManager.LoadScene("BUILD_GroceryStore");
    }
}
