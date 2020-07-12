using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom_Projectile : MonoBehaviour
{

    public float projectileSpeed = 1.0f;
    public float timer = 0.0f;
    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.transform.right * projectileSpeed;

        Destroy(this.gameObject,0.50f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "breakable")
        {
            Destroy(collision.gameObject);
            Destroy(this);
            GameObject.FindObjectOfType<Camera_Shake>().Shake(1.5f, 0.5f);
        }
        Destroy(this.gameObject);
    }
}
