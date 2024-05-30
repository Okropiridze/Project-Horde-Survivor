using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSplash : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 0.3f);
    }
}
