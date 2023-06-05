using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Checkpoints : MonoBehaviour
{
    public float timeStart = 5;
    private float originalTimeStart;
    public GameObject Player;

    public List<GameObject> UIObjects = new List<GameObject>();

    public TMP_Text textBox;
    public GameObject GameOverPanel;
    public Rigidbody playerRigidbody;
    private bool hasReachedCheckpoint = false;
    private bool isRaceFinished = false;

    public LapTimeManager lapTimeManager;

    void Start()
    {
        originalTimeStart = timeStart;
        GameOverPanel.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint") && !hasReachedCheckpoint && !isRaceFinished)
        {
            StartCoroutine(CheckPointCountdownCoroutine());
            hasReachedCheckpoint = true;
        }
        else if (other.gameObject.CompareTag("Checkpoint") && hasReachedCheckpoint && !isRaceFinished)
        {
            timeStart = originalTimeStart + 1;
        }
        else if (other.gameObject.CompareTag("FinishLine") && !isRaceFinished)
        {
            isRaceFinished = true;
            lapTimeManager.IsLapStarted = false;
        }
    }

    IEnumerator CheckPointCountdownCoroutine()
    {
        while (timeStart >= 0)
        {
            textBox.text = Mathf.Round(timeStart).ToString();
            yield return new WaitForSeconds(1f);
            timeStart--;
        }

        if (!isRaceFinished)
        {
            GameOverPanel.gameObject.SetActive(true);

            playerRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            lapTimeManager.IsLapStarted = false;

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
        timeStart = originalTimeStart;
        hasReachedCheckpoint = false;
        isRaceFinished = false;
    }
}
