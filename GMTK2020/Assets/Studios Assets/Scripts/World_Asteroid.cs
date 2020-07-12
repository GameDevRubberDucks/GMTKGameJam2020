using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_Asteroid : MonoBehaviour
{
    //Public
    public List<GameObject> asteroids = new List<GameObject>();

    public float minRotSpeed, maxRotSpeed;

    private float rotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < asteroids.Count; i++)
            asteroids[i].SetActive(false);

        asteroids[Random.Range(0, 3)].SetActive(true);

        transform.Rotate(Vector3.forward, Random.Range(0.0f, 360.0f));

        rotSpeed = Random.Range(minRotSpeed, maxRotSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotSpeed * Time.deltaTime);
    }
}
