using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    private Image hpImage;  // 이미지를 직접 선언합니다.
    public GameObject Player;

    private MonoBehaviour[] playerScripts; // Player 게임 오브젝트의 스크립트들을 저장하기 위한 배열

    private bool pisalive = true;

    public bool Pisalive
    {
        get { return pisalive; }
        set { pisalive = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        // 이미지를 연결된 GameObject에서 찾아 할당합니다.
        hpImage = GameObject.Find("hpGauge").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DecreaseHp()
    {
        // 이미지의 fillAmount를 감소시킵니다.
        hpImage.fillAmount -= 0.25f;
        if (hpImage.fillAmount <= 0)
        {
            // Player 게임 오브젝트를 비활성화합니다.
            Destroy(Player);
            Pisalive = false;
            SceneManager.LoadScene("LoseEnd");
        }
    }
}
