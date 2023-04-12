using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LapTimeManager : MonoBehaviour
{
    public float TimeStart;
    public TMP_Text TextBox;

    public GameObject StartLap;
    public GameObject FinishLap;

    bool IsLapStarted = false;
    
    void Start()
    {
        TextBox.text = TimeStart.ToString("F2");
    }

    void Update()
    {
        if(IsLapStarted == true)
        {
          TimeStart += Time.deltaTime;
          TextBox.text = TimeStart.ToString("F2");
        }
    }

    public void OnTriggerEnter(Collider other)
{
    if (other.gameObject == FinishLap && other == FinishLap.GetComponent<Collider>())
    {
        IsLapStarted = false; 
        float totalTime = TimeStart; 
        TimeStart = 0; 
        TextBox.text = totalTime.ToString("F2"); 
    }
    else if (other.gameObject == StartLap && other == StartLap.GetComponent<Collider>())
    {
        IsLapStarted = true;
    }
}
    
}