using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GroceryStoreTransitionManager : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // called at the end of the fade-to-white animation
    public void LoadPalmOilScene() {
        SceneManager.LoadScene("BUILD_Rainforest");
    }

    public void SceneTransition() {
        anim.Play("Grocery-FadeToWhite");
    }
}
