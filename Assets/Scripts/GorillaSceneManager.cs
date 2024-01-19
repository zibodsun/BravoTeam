using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaSceneManager : MonoBehaviour
{
    public List<Gorilla> gorillas = new();
    public Human human;
    public List<GameObject> narrationLines = new();
    public Animator fadeSceneController;

    public float waitBeforeAttackTime;      // time between the appearence of the human and the gorillas starting to walk
    private float _timer;

    public bool humanStartedCutting;

    // Start is called before the first frame update
    void Start()
    {
        // store all children
        foreach (Transform child in transform)
        {
            narrationLines.Add(child.gameObject);
        }
        Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        // check if all gorillas have bananas
        foreach (Gorilla g in gorillas) if(!g.HasBanana()){
            return;
        }

        // this code won't run until all gorillas have bananas
        human.gameObject.SetActive(true);

        // wait before walking to the attack position
        if (_timer < waitBeforeAttackTime)
        {
            _timer += Time.deltaTime;
        }
        else {
            foreach (Gorilla g in gorillas)
            {
                g.AttackHuman();
            }
        }
    }
    public void PlayFirstLine()
    {
        Play(1);
        StartCoroutine(PlaySecondLine());   // waits then plays the second line
    }
    IEnumerator PlaySecondLine() {
        yield return new WaitForSeconds(20f);
        Play(2);
    }

    // Plays a narration line after pausing currently playing ones
    public void Play(int index) {
        for (int i = 0; i < narrationLines.Count; i++) {
            if (index == i)
            {
                narrationLines[index].SetActive(true);
            }
            else {
                narrationLines[i].SetActive(false);
            }
        }
    }
}