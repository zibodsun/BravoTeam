using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroceryStoreSceneManager : MonoBehaviour
{
    public List<GameObject> narrationLines = new();
    public GameObject plane;

    // Start is called before the first frame update
    void Start()
    {
        // store all children
        foreach (Transform child in transform)
        {
            narrationLines.Add(child.gameObject);
        }
    }
    public void StartAvocadoAnimation()
    {
        plane.SetActive(true);
    }

    // Plays a narration line after pausing currently playing ones
    public void Play(int index)
    {
        for (int i = 0; i < narrationLines.Count; i++)
        {
            if (index == i)
            {
                narrationLines[index].SetActive(true);
            }
            else
            {
                narrationLines[i].SetActive(false);
            }
        }
    }
}
