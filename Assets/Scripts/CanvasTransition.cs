using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTransition : MonoBehaviour
{
    public GorillaSceneManager sceneManager;

    public void PlayFirstLine() {
        sceneManager.PlayFirstLine();
    }

    public void PlayEnding() {
        sceneManager.Play(4);
    }
}
