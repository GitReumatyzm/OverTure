using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Checkpoints : MonoBehaviour
{
    public float timeStart = 5;
    private float OriginalTimeStart;
    public GameObject Player;

    public List<GameObject> UIObjects = new List<GameObject>();

    public TMP_Text textBox;
    public GameObject GameOverPanel;
    public Rigidbody playerRigidbody;
    private bool HasReachedCheckpoint = false;
    private bool IsGameOver = false;
    public GameObject FinishLap;
    private Coroutine checkPointCoroutine;
    private bool isRaceFinished = false;



    private LapTimeManager lapTimeManager;

    void Start()
    {
        OriginalTimeStart = timeStart;
        GameOverPanel.gameObject.SetActive(false);

        lapTimeManager = Player.GetComponent<LapTimeManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint") && !HasReachedCheckpoint)
        {
            StartCoroutine(CheckPointCountdownCoroutine());
            HasReachedCheckpoint = true;
        }
        else if (other.gameObject.CompareTag("Checkpoint") && HasReachedCheckpoint)
        {
            timeStart = OriginalTimeStart + 1;
        }
    }

    public IEnumerator CheckPointCountdownCoroutine()
    {
        while (timeStart >= 0)
        {
            textBox.text = Mathf.Round(timeStart).ToString();
            yield return new WaitForSeconds(1f);
            timeStart--;
        }

        if (!IsGameOver)
        {
            GameOverPanel.gameObject.SetActive(true);

            playerRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            lapTimeManager.IsLapStarted = false;
        
            foreach (GameObject uiObjects in UIObjects)
            {
                uiObjects.SetActive(false);
            }

            float totalTime = lapTimeManager.TimeStart;
            lapTimeManager.TimeStart = 0;
            lapTimeManager.LapTime.text = totalTime.ToString("F2");

            if (totalTime < PlayerPrefs.GetFloat("BestLapTime", 0f) || PlayerPrefs.GetFloat("BestLapTime", 0f) == 0f)
            {
                PlayerPrefs.SetFloat("BestLapTime", totalTime);
                lapTimeManager.BestLapTime.text = PlayerPrefs.GetFloat("BestLapTime", 0f).ToString("F2");
            }

        }

        textBox.text = "KUNIC";
        yield return new WaitForSeconds(1f);
        textBox.gameObject.SetActive(false);
        timeStart = OriginalTimeStart;
        HasReachedCheckpoint = false;
        IsGameOver = false;
    }

}