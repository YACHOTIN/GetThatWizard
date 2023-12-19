using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    private Image hpImage;  // �̹����� ���� �����մϴ�.
    public GameObject Player;

    private MonoBehaviour[] playerScripts; // Player ���� ������Ʈ�� ��ũ��Ʈ���� �����ϱ� ���� �迭

    private bool pisalive = true;

    public bool Pisalive
    {
        get { return pisalive; }
        set { pisalive = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        // �̹����� ����� GameObject���� ã�� �Ҵ��մϴ�.
        hpImage = GameObject.Find("hpGauge").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DecreaseHp()
    {
        // �̹����� fillAmount�� ���ҽ�ŵ�ϴ�.
        hpImage.fillAmount -= 0.25f;
        if (hpImage.fillAmount <= 0)
        {
            // Player ���� ������Ʈ�� ��Ȱ��ȭ�մϴ�.
            Destroy(Player);
            Pisalive = false;
            SceneManager.LoadScene("LoseEnd");
        }
    }
}
