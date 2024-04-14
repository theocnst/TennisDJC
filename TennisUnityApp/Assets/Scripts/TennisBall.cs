using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;
//using System.Diagnostics;

public class TennisBall : MonoBehaviour
{
    private Vector3 initialBallPosition; // Initial position of the ball
    public string hitter;
    int characterScore;
    int botScore;
    public bool playing = true;
    int characterBounceCount = 0;
    int botBounceCount = 0;
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
        Debug.Log("collision");
        if (collision.gameObject.CompareTag("PlayerGround"))
        {
            Debug.Log("playerGround");

            if (hitter == "Bot")
            {
                botBounceCount++;
                characterBounceCount = 0; //reset opponent's bounce count
            }

            CheckBounceCount();
        }
        else if(collision.gameObject.CompareTag("BotGround"))
        {
            if (hitter == "Character")
            {
                characterBounceCount++;
                botBounceCount = 0; //reset opponent's bounce count
            }

            CheckBounceCount();
        }
        else if (collision.gameObject.CompareTag("Wall"))
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

    private void CheckBounceCount()
    {
        if (characterBounceCount >= 2)
        {
            characterScore++;
            ResetGameAndCounts();
        }
        else if(botBounceCount >= 2)
        {
            botScore++;
            ResetGameAndCounts();
        }
    }

    private void ResetGameAndCounts()
    {
        characterBounceCount = 0;
        botBounceCount = 0;
        playing = false;
        UpdateScores();
    }

    private void UpdateScores()
    {
        characterTMPScore.text = "Character: " + characterScore;
        botTMPScore.text = "Bot: " + botScore;
    }
}