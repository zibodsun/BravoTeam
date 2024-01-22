using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneAnimationEvents : MonoBehaviour
{
    public GroceryStoreSceneManager sceneManager;
    public GameObject particles;
    public GameObject audio;

    private void Start()
    {
        sceneManager = FindAnyObjectByType<GroceryStoreSceneManager>();
    }

    public void PlayFirstAvocadoLine()
    {
        sceneManager.Play(0);
    }

    public void PlaySecondAvocadoLine()
    {
        sceneManager.Play(1);
    }

    public void EnableEffects() {
        particles.SetActive(true);
        audio.SetActive(true);
    }

    public void End()
    {
        particles.SetActive(false);
        audio.SetActive(false);
        gameObject.SetActive(false);
    }
}
