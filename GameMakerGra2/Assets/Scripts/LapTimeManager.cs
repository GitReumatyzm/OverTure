using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LapTimeManager : MonoBehaviour
{
    public float TimeStart;

    public TMP_Text LapTime;
    public TMP_Text BestLapTime;

    public GameObject StartLap;
    public GameObject FinishLap;
    public GameObject Player;

    bool IsLapStarted = false;
    
    void Start()
    {
        LapTime.text = TimeStart.ToString("F2");
        BestLapTime.text = PlayerPrefs.GetFloat("BestLapTime", 0f).ToString("0.00");
        PlayerPrefs.DeleteKey("BestLapTime");
    }

    void Update()
    {
        if(IsLapStarted == true)
        {
            TimeStart += Time.deltaTime;
            LapTime.text = TimeStart.ToString("F2");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == FinishLap && other == FinishLap.GetComponent<Collider>())
        {
            IsLapStarted = false; 
            float totalTime = TimeStart; 
            TimeStart = 0; 
            LapTime.text = totalTime.ToString("F2"); 

            if (totalTime < PlayerPrefs.GetFloat("BestLapTime", 0f) || PlayerPrefs.GetFloat("BestLapTime", 0f) == 0f)
            {
                PlayerPrefs.SetFloat("BestLapTime", totalTime);
                BestLapTime.text = (PlayerPrefs.GetFloat("BestLapTime", 0f) == 0f) ? "0.00" : PlayerPrefs.GetFloat("BestLapTime", 0f).ToString("F2");
            }
        }
        else if (other.gameObject == StartLap && other == StartLap.GetComponent<Collider>())
        {
            IsLapStarted = true;
        }
    }
}