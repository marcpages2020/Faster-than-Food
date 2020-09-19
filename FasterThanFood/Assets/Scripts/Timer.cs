using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float minutes;
    public float seconds;
    private Text text;

    [SerializeField] GameManager gameManager;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        seconds -= Time.deltaTime;

        if(seconds < 0)
        {
            minutes--;

            if(minutes < 0)
            {
                minutes = 0;
                if(seconds < 0)
                {
                    gameManager.timeOver.Invoke();
                }
            }
            else
            {
                seconds = 59.0f;
            }
        }

        if(seconds < 9.5f)
        {
            text.text = minutes.ToString() + " : 0" + Mathf.Round(seconds).ToString();
        }
        else
        {
            text.text = minutes.ToString() + " : " + Mathf.Round(seconds).ToString();
        }
    }
}
