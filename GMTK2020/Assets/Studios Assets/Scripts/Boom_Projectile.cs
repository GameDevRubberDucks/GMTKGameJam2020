using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom_Projectile : MonoBehaviour
{

    public float projectileSpeed = 1.0f;
    public float timer = 0.0f;
    public GameObject explosionParticles;

    // Update is called once per frame
    private void Start()
    {
        Invoke("BlowUp", timer);
    }

    void Update()
    {
        this.transform.position += this.transform.right * projectileSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "breakable")
        {
            GameObject breakableExplosion = Instantiate(explosionParticles, collision.transform.position, Quaternion.identity, null);
            Destroy(breakableExplosion, 1.5f);

            Destroy(collision.gameObject);
            GameObject.FindObjectOfType<Camera_Shake>().Shake(1.5f, 0.5f);
        }

        BlowUp();
    }

    public void BlowUp()
    {
        GameObject projectileExplosion = Instantiate(explosionParticles, this.transform.position, Quaternion.identity, null);
        Destroy(projectileExplosion, 1.5f);
        Destroy(this.gameObject);
    }
}
