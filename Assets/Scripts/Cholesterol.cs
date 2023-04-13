using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cholesterol : MonoBehaviour
{
    SphereCollider col;
    MeshRenderer ren;

    public AudioSource generateSource;
    public AudioClip createSound;
    public AudioClip destroySound;
    public AudioSource impactSoundSource;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<SphereCollider>();
        ren = GetComponent<MeshRenderer>();
        generateSource.clip = createSound;
        generateSource.Play();
    }

    public void DestroyCholesterol()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        generateSource.clip = destroySound;
        generateSource.Play();

        Invoke("Delete", destroySound.length);
    }

    private void Delete()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.layer == 6)
        {
            impactSoundSource.Play();
            Rigidbody rb = go.GetComponent<Rigidbody>();
            Vector3 vel = rb.velocity;
            rb.AddForce(-vel * 10000);
        }
    }
}