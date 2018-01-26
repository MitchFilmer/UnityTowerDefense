using UnityEngine;
using System.Collections;

public class DelayedDestroy : MonoBehaviour
{
    public float delay = 1.0f;

    private void Awake()
    {
        StartCoroutine("DestroyAfterDelay");
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}