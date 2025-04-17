using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement2D : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Sprite leftFacingSprite;
    public Sprite rightFacingSprite;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Image playerImage;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerImage = GetComponent<Image>();
    }

    private void Update()
    {
        // Get WASD or Arrow Key Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Change sprite based on direction
        if (movement.x > 0)
        {
            playerImage.sprite = rightFacingSprite;
        }
        else if (movement.x < 0)
        {
            playerImage.sprite = leftFacingSprite;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
