using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour {

    public float speed;
    private Rigidbody rb;
    private int count = 0;
    public Text countText;
    public Text resultText;
    public Text levelText;
    public Text timerText;
    private float timeLeft = 60;
    private bool successfull = false;
    private bool replay = false;
    private bool _exit = false;
    private static int level = 1;

    private void Start()
    {
        timeLeft /= level;
        rb = GetComponent<Rigidbody>();
        setCountText();
        resultText.text = "";
        levelText.text = "Level " + level.ToString();
        setTimerText();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f ,moveVertical);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            count++;
            setCountText();
        }
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        setTimerText();
        setResultText();
        if (successfull == true) {
            if(Input.GetKeyDown(KeyCode.Space)) {
                level++;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        if (replay == true)
        {
            level = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (_exit == true)
        {
            Application.Quit();
        }
    }

    private void setCountText() {
        countText.text = "Count " + count.ToString();
    }

    private void setTimerText() {
        timerText.text = "Remaining Time: " + (timeLeft >= 0 ? timeLeft.ToString("0.0") : 0.ToString("0.0"));
    }

    private void setResultText() {
        if (timeLeft >= 0)
        {
            if (count >= 10) {
                resultText.text = "You Win!\nPress Space Button to Start Next Level!";
                successfull = true;
            }
        }
        else
        {
            resultText.text = "Game Over!\nPress R to Replay or E to Exit";
            if (Input.GetKeyDown(KeyCode.R))
            {
                replay = true;
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                _exit = true;
            }
            
        }

    }
}
