using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShredder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("triggered!");
        Destroy(collision.gameObject);
    }
}
