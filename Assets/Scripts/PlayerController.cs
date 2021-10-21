using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player
    [SerializeField]
    private float speed;
    private float normalSpeed;

    private Rigidbody2D rb2;

    private Vector2 desiredMovement = Vector2.zero;

    private float playerSpriteWidth;

    //Bullet
    [SerializeField]
    private GameObject playerBullet;
    [SerializeField]
    private GameObject[] bulletOrigins;
    private float bulletCurrentTime = 0f;
    [SerializeField]
    private float bulletSpawnTime;
    private float normalBulletTime;

    //PowerUp
    [Header("PowerUp Boost")]
    [SerializeField]
    private float boostTime;
    private float boostDeltaTime;

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

        normalBulletTime = bulletSpawnTime;
        normalSpeed = speed;
    }

    // Update is called once per frame
    private void Update()
    {
        desiredMovement = new Vector2(Input.GetAxis("Horizontal"), 0f);

        //Disparo
        Shoot();
    }
    private void FixedUpdate()
    {
        MoveCharacter(desiredMovement);
    }
    private void Shoot()
    {
        //Aumentamos el tiempo para spawnear la bala
        bulletCurrentTime += Time.deltaTime;

        //Comprobamos si ha pasado suficiente tiempo para spawnear una bala
        if(bulletCurrentTime >= bulletSpawnTime)
        {
            bulletCurrentTime = 0f;

            for (int i = 0; i < bulletOrigins.Length; i++)
            {
                Instantiate(playerBullet, bulletOrigins[i].transform.position, Quaternion.identity); 
            }

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
    IEnumerator powerUpBoost()
    {
        if (Random.Range(0f, 1f) >= 0.5f)
        {
            bulletSpawnTime *= 0.5f;
            yield return new WaitForSeconds(10);
            bulletSpawnTime *= 2f;
        }
        else
        {
            speed *= 2f;
            yield return new WaitForSeconds(10);
            speed *= 0.5f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")){
            Destroy(gameObject);
        }
        else if (collision.CompareTag("PowerUp"))
        {
            StartCoroutine(powerUpBoost());
        }
    }
}
