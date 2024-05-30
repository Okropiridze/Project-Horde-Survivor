using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    private Transform player;
    private Vector3 mousePos;

    private Vector3 camShakeoffset;
    private Vector3 shakeTarget;

    private float shakeDuration = 0f;
    private float shakeIntencity = 0f;
    private float decreaseFactor = 0.9f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void ShakeCamera(float intencity, float duration)
    {
        shakeDuration = duration;
        shakeIntencity = intencity;
        NewShakeOffset();
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            shakeDuration -= Time.deltaTime * decreaseFactor;
            camShakeoffset = Vector3.Lerp(camShakeoffset, shakeTarget, 0.05f);
        }
    }

    void NewShakeOffset()
    {
        shakeTarget = Random.insideUnitSphere * shakeIntencity;
        shakeIntencity -= Time.deltaTime * decreaseFactor;

        if (shakeDuration > 0) Invoke("NewShakeOffset", 0.05f);
        else
        {
            shakeDuration = 0f;
            camShakeoffset = Vector3.zero;
        }
    }

    private void LateUpdate()
    {
        Vector3 screenPoint = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(screenPoint - new Vector3(0, 0, -10f));
        float posX = (player.position.x - mousePos.x) / 20;
        
        posX = Mathf.Clamp(posX, -1f, 1f);

        float posY = (player.position.y - mousePos.y) / 20;
        posY = Mathf.Clamp(posY, -1f, 1f);
        transform.position = new Vector3(player.position.x - posX, player.position.y - posY, -10f) + camShakeoffset;
    }
}
