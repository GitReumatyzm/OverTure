using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Countdown : MonoBehaviour 
{
    public float timeStart = 3;
    public GameObject Player;
    public TMP_Text textBox;
    //public Rigidbody playerRigidbody;


    void Start() 
    {
        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        while (timeStart > 0)
        {
            //playerRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            textBox.text = Mathf.Round(timeStart).ToString();
            yield return new WaitForSeconds(1f);
            timeStart--;
        }
        textBox.text = "JEĆ!";
        yield return new WaitForSeconds(1f);
        //playerRigidbody.constraints = RigidbodyConstraints.None;
        textBox.gameObject.SetActive(false);
    }
}