using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HurtEffect : MonoBehaviour
{
    public float speed;

    public Image image;

    private Color c;

    void OnEnable()
    {
        StartCoroutine(EffectHandler());
    }

    IEnumerator EffectHandler()
    {
        c = new Color(1, 1, 1, 0);
        image.color = c;

        while (c.a < 1)
        {
            c.a += Time.deltaTime * speed;
            image.color = c;
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(1);
        while (c.a > 0)
        {
            c.a -= Time.deltaTime * speed;
            image.color = c;
            yield return new WaitForFixedUpdate();
        }
        gameObject.SetActive(false);
    }
}