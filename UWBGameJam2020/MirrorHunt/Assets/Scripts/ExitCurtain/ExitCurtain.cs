using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCurtain : MonoBehaviour
{
    private EscapeMenu escapeMenu;

    // Start is called before the first frame update
    private void Start()
    {
        escapeMenu = GameObject.FindObjectOfType<EscapeMenu>();
    }

    // Turn on the escape menu if collided by the player
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            //Debug.Log("Collide Character");
            escapeMenu.SetIsShowMenu(true);
        }
        //Debug.Log(col.gameObject.name);
    }
}
