using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break_Floor : MonoBehaviour
{
    public ParticleSystem collisionPartcileSystem;
    public SpriteRenderer sr;
    public bool once = true;
    public AudioSource crack;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && once)
        {
            var em = collisionPartcileSystem.emission;
            var dur = collisionPartcileSystem.main.duration;

            em.enabled = true;
            collisionPartcileSystem.Play();

            once = false;
            Destroy(sr);
            Invoke(nameof(DestroyObj), dur);

            crack.Play();
        }
    }

    void DestroyObj()
    {
        Destroy(gameObject);
    }
}
