using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementZ : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Interaction Settings")]
    [SerializeField] private float interactRadius = 1.5f;
    [SerializeField] private LayerMask npcLayer;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool canMove = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!canMove) return;

        // 8-Directional / Top-Down Movement Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        // Press 'E' to interact with nearby NPCs
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteractNPC();
        }
    }

    void FixedUpdate()
    {
        if (!canMove)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        rb.velocity = movement * moveSpeed;
    }

    private void TryInteractNPC()
    {
        // Check for nearby colliders on the NPC layer
        Collider2D hit = Physics2D.OverlapCircle(transform.position, interactRadius, npcLayer);
        
        if (hit != null)
        {
            NPCInteractionZ npc = hit.GetComponent<NPCInteractionZ>();
            if (npc != null)
            {
                npc.TriggerDialogueScene();
            }
        }
    }

    public void SetMovementState(bool state)
    {
        canMove = state;
        if (!state) rb.velocity = Vector2.zero;
    }

    void OnDrawGizmosSelected()
    {
        // Visualize interaction range in the Editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}