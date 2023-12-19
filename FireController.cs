using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float speed;
    GameObject Fire;
    // Start is called before the first frame update
    void Start()
    {
        //Invoke("DestroyF", 2f);
        this.Fire = GameObject.Find("Hero");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);
        Vector2 p1 = transform.position;
        Vector2 p2 = this.Fire.transform.position;
        Vector2 dir = p1 - p2;

        float d = dir.magnitude;
        float r1 = 0.1f;
        float r2 = 0.5f;

        if(d<r1+r2)
        {
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().DecreaseHp();
            Destroy(gameObject);
        }
    }

}
