using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private float moveSpeed = 6.0f;
    private float curTime;
    private Rigidbody2D Rgb;
    public float cooltime;
    public Transform pos;
    public Vector2 boxSize;
    void Start()
    {
        animator = GetComponent<Animator>();
        Rgb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0.0f).normalized;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {   
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (moveDirection != Vector3.zero)
        {
            // 움직임 애니메이션 재생
            animator.SetBool("IsMoving", true);
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
        else
        {
            // idle 애니메이션 재생
            animator.SetBool("IsMoving", false);
        }
        if(curTime <= 0)
        {

            if (Input.GetKey(KeyCode.Z))
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.CompareTag("Enemy"))
                    {
                        MonsterController monsterAttackController = collider.GetComponent<MonsterController>();
                        if (monsterAttackController != null)
                        {
                            monsterAttackController.TakeDamage(1);
                        }
                    }
                }
                animator.SetTrigger("IsAttacking");
                curTime = cooltime;
            }

        }
        else
        {
            curTime -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
}
