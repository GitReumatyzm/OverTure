using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGrowing : MonoBehaviour
{
    public GameObject Player;

    public float maxScale = 2f;
    public float minScale = 0.5f;


    void Start()
    {

    Player = GameObject.FindGameObjectWithTag("Player");

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
    {
        Debug.Log("Rozmiar: " + Player.transform.localScale);
        Player.transform.localScale *= 2f;
        Player.transform.localScale = ClampScale(Player.transform.localScale);
    }
    else if (Input.GetKeyDown(KeyCode.Alpha2))
    {
         Debug.Log("Rozmiar: " + Player.transform.localScale);
        Player.transform.localScale = ClampScale(Player.transform.localScale / 2f);
        Debug.Log("Before clamp: " + Player.transform.localScale);
        Player.transform.localScale = ClampScale(Player.transform.localScale);
        Debug.Log("After clamp: " + Player.transform.localScale);
    }

    }

    private Vector3 ClampScale(Vector3 scale)
     {
    scale.x = Mathf.Clamp(scale.x, minScale, maxScale);
    scale.y = Mathf.Clamp(scale.y, minScale, maxScale);
    scale.z = Mathf.Clamp(scale.z, minScale, maxScale);
    return scale;
    }
}
