using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public GameObject image;
    public Animator animator;


    public void TurnImageOff()
    {
        image.SetActive(false);
    }

    public void NextLevel()
    {
        image.SetActive(true);
        animator.SetTrigger("Play");
    }
}
