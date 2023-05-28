using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Checkpoints : MonoBehaviour
{
    public float timeStart = 5;
    private float OriginalTimeStart;
    public GameObject Player; 

    public TMP_Text textBox;
    public TMP_Text GameOverText;
    public Rigidbody playerRigidbody;
    private bool HasReachedCheckpoint = false;
    private bool IsGameOver = false;

    void Start()
    {
        OriginalTimeStart = timeStart;
        GameOverText.gameObject.SetActive(false);
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
            //playerRigidbody.constraints = RigidbodyConstraints.None;
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
        
        if (!IsGameOver)
        {
            GameOverText.gameObject.SetActive(true);
            playerRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        textBox.text = "KUNIC";
        playerRigidbody.constraints = RigidbodyConstraints.None;
        yield return new WaitForSeconds(1f);
        textBox.gameObject.SetActive(false);
        timeStart = OriginalTimeStart;
        HasReachedCheckpoint = false;
        IsGameOver = false;
    }
}