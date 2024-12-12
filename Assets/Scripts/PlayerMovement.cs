using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Transform targetTrans;
    public LayerMask UnwalkableLayer;
    public LayerMask MoveableLayer;
    public Animator animator;
    public LayerMask ButtonLayer;  

    
    public GameObject[] boxes;     
    public GameObject[] buttons;   

    private Vector2 movement;

    private void Awake()
    {
        targetTrans.position = transform.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTrans.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetTrans.position) < .01f)
        {
            Vector3 nextPosition = targetTrans.position + new Vector3(movement.x, movement.y, 0f);

            if (!Physics2D.OverlapCircle(nextPosition, .1f, UnwalkableLayer))
            {
                Collider2D hitCollider = Physics2D.OverlapCircle(nextPosition, .1f, MoveableLayer);
                if (hitCollider != null)
                {
                    Vector3 pushPosition = nextPosition + new Vector3(movement.x, movement.y, 0f);

                    if (!Physics2D.OverlapCircle(pushPosition, .1f, UnwalkableLayer))
                    {
                        Rigidbody2D boxRigidbody = hitCollider.GetComponent<Rigidbody2D>();
                        if (boxRigidbody != null)
                        {
                            boxRigidbody.MovePosition(boxRigidbody.position + new Vector2(movement.x, movement.y));
                        }
                    }
                }
                else
                {
                    targetTrans.position = nextPosition;
                }
            }
        }

        CheckLevelComplete(); 
    }

    void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    private void UpdateAnimator()
    {
        animator.SetBool("Walk", movement.magnitude > 0);
    }

    void CheckLevelComplete()
    {
        if (boxes.Length != buttons.Length)
        {
            Debug.LogError("The number of boxes and buttons must match!");
            return;
        }

        for (int i = 0; i < boxes.Length; i++)
        {
            if (!IsBoxOnButton(boxes[i], buttons[i]))
            {
                return; 
            }
        }

        Debug.Log("All boxes are on their buttons! Level complete.");
        EndLevel();
    }

    bool IsBoxOnButton(GameObject box, GameObject button)
    {
        if (box == null || button == null) return false;

        Collider2D buttonCollider = Physics2D.OverlapCircle(box.transform.position, 0.1f, ButtonLayer);
        return buttonCollider != null && buttonCollider.gameObject == button;
    }

    void EndLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

