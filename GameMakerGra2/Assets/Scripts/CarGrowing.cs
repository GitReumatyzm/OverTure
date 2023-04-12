using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGrowing : MonoBehaviour
{
    public GameObject Player;
    public float maxScale = 2f;
    public float minScale = 0.5f;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Rozmiar: " + Player.transform.localScale);
            Player.transform.localScale *= 1.5f;
            Player.transform.localScale = ClampScale(Player.transform.localScale);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Rozmiar: " + Player.transform.localScale);
            Player.transform.localScale /= 1.5f;
            Player.transform.localScale = ClampScale(Player.transform.localScale);
        }
    }

    private Vector3 ClampScale(Vector3 scale)
{
    scale.x = Mathf.Clamp(scale.x, minScale, maxScale);
    scale.y = Mathf.Clamp(scale.y, minScale, maxScale);
    scale.z = Mathf.Clamp(scale.z, minScale, maxScale);

    if (scale.x < minScale)
    {
        scale.x = minScale;
    }

    if (scale.y < minScale)
    {
        scale.y = minScale;
    }

    if (scale.z < minScale)
    {
        scale.z = minScale;
    }

    return scale;
}
}