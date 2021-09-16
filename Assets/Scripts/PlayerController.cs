using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player
    [SerializeField]
    private float speed;

    private Rigidbody2D rb2;

    private Vector2 desiredMovement = Vector2.zero;

    private float playerSpriteWidth;

    //Bullet
    private GameObject bullet;
    private float timeToShoot;
    private float fireRate;

    //Camara
    [SerializeField]
    Camera camara;

    private Vector2 cameraXBounds = Vector2.zero;

    // Start is called before the first frame update
    private void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();

        cameraXBounds.x = camara.ViewportToWorldPoint(new Vector2(0f, 1f)).x;
        cameraXBounds.y = camara.ViewportToWorldPoint(new Vector2(1f, 1f)).x;

        playerSpriteWidth = GetComponent<SpriteRenderer>().bounds.size.x * 0.5f;
    }

    // Update is called once per frame
    private void Update()
    {
        desiredMovement = new Vector2(Input.GetAxis("Horizontal"), 0f);
    }
    private void FixedUpdate()
    {
        MoveCharacter(desiredMovement);
    }
    private void Shooting()
    {
        float delta = Time.deltaTime * 1000;
        bool canShoot = false;
        timeToShoot += delta;
        if (timeToShoot > fireRate)
        {
            canShoot = true;
            timeToShoot = 0;
        }

        if (canShoot)
        {
            timeToShoot = 0;
            GameObject temporalBullet = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, 1), transform.rotation);
            Destroy(temporalBullet, 3);
        }
    }
    private void MoveCharacter(Vector2 direction)
    {
        //Dada la direccion deseada del jugador calculamos la posicion final de la nave en su eje horizontal
        float finalPositionX = transform.position.x + (direction.x * speed * Time.fixedDeltaTime);

        //Hacemos un clamp para que se mantenga dentro de los limites de la camara.
        finalPositionX = Mathf.Clamp(finalPositionX, cameraXBounds.x + playerSpriteWidth, cameraXBounds.y - playerSpriteWidth);

        rb2.MovePosition(new Vector2(finalPositionX, rb2.position.y));
    }
}
