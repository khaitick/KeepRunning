using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform startingPoint;
    public TextMeshProUGUI score_text;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverPanel_finalScore;
    public float scoreBoostRate = 1;

    private float score = 0f;
    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        playerControllerScript.gameOver = true;
        StartCoroutine("StartAnimation");
        gameOverPanel.SetActive(false);

    }

    void Update()
    {
        if (!playerControllerScript.gameOver)
        {
            score += Time.deltaTime * scoreBoostRate;
            score_text.text = score.ToString("#") + "00";
        }
        else if(playerControllerScript.gameOver && score > 0)
        {
            score_text.gameObject.SetActive(false);
            gameOverPanel_finalScore.text = score.ToString("#") + "00";
            score_text.color = Color.white;
            gameOverPanel.SetActive(true);
        }
    }

    IEnumerator StartAnimation()
    {
        float lerpSpeed = 2f;
        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_f", 0.26f);
        playerControllerScript.GetComponent<Animator>().SetBool("Static_b", false);
        Vector3 startPos = playerControllerScript.transform.position;
        Vector3 endPos = startingPoint.position;

        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;

        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;

        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            playerControllerScript.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            yield return null;
        }
        playerControllerScript.gameOver = false;
        playerControllerScript.StartRunning();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Prototype 3");
    }
}
