using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MonsterController : MonoBehaviour
{
    public int Hp;
    public bool IsAlive = true;
    public float trackShotCoolTime = 2.0f;
    public float circleShotCoolTime = 3.0f;
    public float circleTime = 5.0f;
    private float curTrackTime;
    private float curCircleTime;
    private float reloadTime = 5.0f;
    private bool canTrackFire = true;
    private bool canCircleFire = true;
    private int projectilesFiredTrack = 0;

    public GameObject Fire;
    public GameDirector GD;
    public Transform Pos;
    public Transform circle;
    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        GD = FindObjectOfType<GameDirector>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (IsAlive)
        {
            FireProjectiles();
        }
    }

    private void FireProjectiles()
    {
        TrackShot();
        CircleShot();
    }
    public void TakeDamage(int damage)
    {
        Hp -= damage;
        if (Hp == 0)
        {
            IsAlive = false;
            Destroy(this.gameObject);
            SceneManager.LoadScene("WinEnd");
        }
    }
    private void TrackShot()
    {
        if (canTrackFire && curTrackTime <= 0)
        {
            List<(Transform, float)> bullets = new List<(Transform, float)>();

            for (int i = 0; i < 45; i += 13)
            {
                GameObject temp = Instantiate(Fire);
                Destroy(temp, 5f);

                float angle = i * Mathf.Deg2Rad;
                float x = Mathf.Cos(angle);
                float y = Mathf.Sin(angle);

                temp.transform.position = new Vector3(x, y, 0) + Pos.position;
                temp.transform.rotation = Quaternion.Euler(0, 0, i);
                projectilesFiredTrack++;
                bullets.Add((temp.transform, 1f));

                if (projectilesFiredTrack >= 32)
                {
                    canTrackFire = false;
                    StartCoroutine(ReloadTrack());
                }
            }

            curTrackTime = trackShotCoolTime;
            StartCoroutine(BulletToTarget(bullets));
        }

        curTrackTime -= Time.deltaTime;
    }

    private void CircleShot()
    {
        if (canCircleFire && curCircleTime <= 0)
        {
            float offset = 1000f;
            int maxProjectilesCircle = 30;
            int projectilesFiredCircle = 0;

            for (int i = 0; i < 90 && projectilesFiredCircle < maxProjectilesCircle; i++)
            {
                GameObject temp1 = Instantiate(Fire);
                Destroy(temp1, 5f);

                float angle = i * Mathf.Deg2Rad + offset * i / 4;
                float x = Mathf.Cos(angle);
                float y = Mathf.Sin(angle);

                temp1.transform.position = new Vector3(x, y, 0) + circle.position;
                temp1.transform.rotation = Quaternion.Euler(0, 0, i);
                projectilesFiredCircle++;

                if (projectilesFiredCircle >= maxProjectilesCircle)
                {
                    canCircleFire = false;
                    StartCoroutine(ReloadCircle());
                }
            }

            curCircleTime = circleShotCoolTime;
        }

        curCircleTime -= Time.deltaTime;
    }

    private IEnumerator ReloadTrack()
    {
        yield return new WaitForSeconds(reloadTime);
        canTrackFire = true;
        projectilesFiredTrack = 0;
    }

    private IEnumerator ReloadCircle()
    {
        yield return new WaitForSeconds(reloadTime);
        canCircleFire = true;
    }

    private IEnumerator BulletToTarget(List<(Transform, float)> objects)
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < objects.Count; i++)
        {
            Vector3 targetDirection = Player.transform.position - objects[i].Item1.position;
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            objects[i].Item1.rotation = Quaternion.Euler(0, 0, angle);
        }

        objects.Clear();
    }
}
