using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        return;
    }

    // Update is called once per frame
    private void Update()
    {
        return;
    }

    private void OnTriggerEnter(Collider col)
    {
        Destroy(gameObject);
    }
}
