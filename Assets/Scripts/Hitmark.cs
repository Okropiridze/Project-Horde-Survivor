using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitmark : MonoBehaviour
{
    [SerializeField] List<Sprite> hitmarks = new List<Sprite>();

    private void Start()
    {
        int rand = Random.Range(0, hitmarks.Count);
        Sprite randHitmark = hitmarks[rand];
        GetComponent<SpriteRenderer>().sprite = randHitmark;

        float randRotation = Random.Range(0, 360f);
        transform.Rotate(0,0,randRotation);

        Destroy(gameObject, 0.1f);
    }
}
