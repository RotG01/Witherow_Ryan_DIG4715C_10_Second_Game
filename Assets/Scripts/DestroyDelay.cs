using UnityEngine;
using System.Collections;

public class DestroyDelay : MonoBehaviour
{
    public float delay;

    void Start()
    {
        Destroy(gameObject, delay);
    }
}
