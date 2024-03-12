using System.Collections;
using UnityEngine;

public class RockSnowEffect : MonoBehaviour
{
    public ParticleSystem snowParticleSystem;
    public float snowDuration = 5f; // Duration of snow effect in seconds

    private bool isSnowActive = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Snow"))
        {
            if (!isSnowActive)
            {
                isSnowActive = true;
                snowParticleSystem.Play();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Snow"))
        {
            if (isSnowActive)
            {
                isSnowActive = false;
                StartCoroutine(DisableSnowAfterDelay());
            }
        }
    }
    
    private IEnumerator DisableSnowAfterDelay()
    {
        yield return new WaitForSeconds(snowDuration);
        snowParticleSystem.Stop();
    }
}

