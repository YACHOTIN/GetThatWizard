using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Transform playerTransform;
    public Image healthbar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if(playerTransform != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(playerTransform.position);
            healthbar.transform.position = new Vector3(screenPos.x, screenPos.y + 105, 0);
        }
    }
}
