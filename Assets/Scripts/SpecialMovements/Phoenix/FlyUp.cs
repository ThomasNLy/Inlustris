using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyUp : MonoBehaviour
{
    public float delayInSeconds;
    public float speed;
    public float rotateSpeed;
    // Use this for initialization
    void Start()
    {
        StartCoroutine("Appear", GetComponentInChildren<Renderer>());
        StartCoroutine("FlyingUp", delayInSeconds);
    }

    IEnumerator FlyingUp(float delay)
    {
        while (true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + rotateSpeed);
            yield return new WaitForSeconds(delayInSeconds);
        }
    }

    IEnumerator Appear(Renderer phoenixRenderer)
    {
        Color temp = phoenixRenderer.material.color;
        temp.a = 0;
        phoenixRenderer.material.color = temp;
        while (temp.a <= 1)
        {
            temp.a += 0.01f;
            phoenixRenderer.material.color = temp;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
