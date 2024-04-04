using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;

public class TennisBall : MonoBehaviour
{
    private Vector3 initialBallPosition; // Initial position of the ball
    public string hitter;
    int characterScore;
    int botScore;
    public bool playing = true;
    [SerializeField] private TextMeshProUGUI characterTMPScore;
    [SerializeField] private TextMeshProUGUI botTMPScore;

    void Start()
    {
        initialBallPosition = transform.position; // Save the initial position of the ball
        characterScore = 0;
        botScore = 0;
        UpdateScores();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            //transform.position = initialBallPosition;

            GameObject.Find("Character").GetComponent<PlayerHitting>().Reset();

            if (playing)
            {
                if (hitter == "Character")
                {
                    characterScore++;
                }
                else if (hitter == "Bot")
                {
                    botScore++;
                }

                playing = false;
                UpdateScores();
            }
        }
        else if (collision.gameObject.CompareTag("Out"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            //transform.position = initialBallPosition;

            GameObject.Find("Character").GetComponent<PlayerHitting>().Reset();

            if (playing)
            {
                if (hitter == "Character")
                {
                    botScore++;
                }
                else if (hitter == "Bot")
                {
                    characterScore++;
                }

                playing = false;
                UpdateScores();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Out") && playing)
        {
            if(hitter == "Character")
            {
                botScore++;
            }
            else if (hitter == "Bot")
            {
                characterScore++;
            }

            playing = false;
            UpdateScores();
        }
    }

    private void UpdateScores()
    {
        characterTMPScore.text = "Character: " + characterScore;
        botTMPScore.text = "Bot: " + botScore;
    }
}
