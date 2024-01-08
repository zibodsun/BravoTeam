using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaSceneManager : MonoBehaviour
{
    public List<Gorilla> gorillas = new List<Gorilla>();
    public Human human;

    public float waitBeforeAttackTime;
    private float _timer;

    public bool humanStartedCutting;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // check if all gorillas have bananas
        foreach (Gorilla g in gorillas) if(!g.HasBanana()){
            return;
        }

        // this code won't run untill all gorillas have bananas
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
}