using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float LimitTime;
    public bool Timeover = false;
    public TextMeshProUGUI text_Timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(LimitTime > 0)
        {
            LimitTime -= Time.deltaTime;
            if(LimitTime < 0)
            {
                SceneManager.LoadScene("LoseEnd");
            }
        }
        
        text_Timer.text = "Time:" + Mathf.Round(LimitTime);
    }
}
