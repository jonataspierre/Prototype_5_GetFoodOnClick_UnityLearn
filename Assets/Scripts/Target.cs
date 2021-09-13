using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    public ParticleSystem explosionParticle;

    private float minSpeed = 9.0f;
    private float maxSpeed = 14.7f;

    private float maxTorque = 10.0f;
    private float xRange = 4.0f;
    private float ySpawnPos = -2.0f;

    public int pointValue;

    private AudioSource managerAudio;
    public AudioClip soundPlay;    

    // Start is called before the first frame update
    void Start()
    {
        managerAudio = GetComponent<AudioSource>();

        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();        

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);

        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            gameManager.UpdateScore(pointValue);

            if (gameObject.CompareTag("Bad"))
            {
                Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
                                
                Destroy(gameObject);

                gameManager.GameOver();
            }
            else
            {
                Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
                
                managerAudio.PlayOneShot(soundPlay, 1.0f);

                Destroy(gameObject, 0.1f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);        
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
