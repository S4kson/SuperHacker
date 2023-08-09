using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Скорость передвижения персонажа

    private Rigidbody2D _rb;
    private Vector2 movement;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        
    }   
    private void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        //float horizontal = movement.x * speed * Time.fixedDeltaTime;
        //float vertical = movement.y * speed * Time.fixedDeltaTime;

        //_rb.velocity = transform.TransformDirection(new Vector2(horizontal,vertical));
        _rb.MovePosition(_rb.position + movement * speed * Time.fixedDeltaTime);

    }
}