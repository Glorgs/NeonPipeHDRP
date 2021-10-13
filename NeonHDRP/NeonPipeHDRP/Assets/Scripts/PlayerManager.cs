using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int numberHP = 3;
    public float invulnerabilityTime = 1f;
    public int blinkFrame = 3;

    private Renderer playerRenderer;
    private Rigidbody playerBody;

    // Start is called before the first frame update
    void Start()
    {
        playerRenderer = GetComponentInChildren<Renderer>();
        playerBody = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            Damage();
        }
    }

    void Damage()
    {
        numberHP--;
        //UpdateUI();

        if (numberHP == 0)
        {
            //Death();
        }
        else
        {
            StartCoroutine(Invulnerability());
        }
    }

    IEnumerator Invulnerability()
    {
        float t = 0;
        int blink = 0;

        playerBody.isKinematic = true;
        playerRenderer.enabled = false;

        while(t < invulnerabilityTime)
        {
            blink++;
            if(blink > blinkFrame)
            {
                blink = 0;
                playerRenderer.enabled = !playerRenderer.enabled;
            }
            t += Time.deltaTime;
            yield return null;
        }

        playerBody.isKinematic = false;
        playerRenderer.enabled = true;
    }


}