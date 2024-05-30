using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField] TMP_Text damageText;
    private float destroyAfter = 0.25f;
    private float moveUpSpeed = 3f;
    public void SetDamageText(float text)
    {
        damageText.text = text.ToString();
    }

    private void Start()
    {
        Invoke("HideText", destroyAfter);
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + moveUpSpeed * Time.deltaTime, transform.position.z);
    }

    private void HideText()
    {
        Destroy(gameObject);
    }
}
