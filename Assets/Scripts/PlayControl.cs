using TMPro;
using UnityEngine;
using UnityEngine.Audio;
public class PlayerControl : MonoBehaviour
{
    public GameObject GameManagerGo;
    public GameObject PlayerBulletGo;
    public GameObject BulletPosition01;
    public GameObject BulletPosition02;
    public GameObject ExplosionGo;
    public TMP_Text LivesUIText;
    [SerializeField] private AudioSource audioSource;
    const int MaxLives = 3;
    int lives;

    [SerializeField] private float speed = 5f;

    [SerializeField] private bool isInvincible = false;

    public void Init()
    {
        lives = MaxLives;

        LivesUIText.text = lives.ToString();

        transform.position = new Vector2 (0, 0);

        gameObject.SetActive(true);
    }

    private Vector2 min;
    private Vector2 max;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Tính toán giới hạn màn hình một lần duy nhất
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // Trừ đi kích thước nhân vật (giả sử sprite có kích thước khoảng 0.57 x 0.57)
        float halfWidth = 0.285f;
        float halfHeight = 0.285f;

        min.x += halfWidth;
        max.x -= halfWidth;
        min.y += halfHeight;
        max.y -= halfHeight;
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            audioSource.Play();

            GameObject bullet01 = (GameObject)Instantiate (PlayerBulletGo);
            bullet01.transform.position = BulletPosition01.transform.position;

            GameObject bullet02 = (GameObject)Instantiate (PlayerBulletGo);
            bullet02.transform.position = BulletPosition02.transform.position;
        }
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y).normalized;
        Move(direction);

        if (Input.GetKeyDown(KeyCode.M))
        {
            isInvincible = !isInvincible;
            LivesUIText.text = isInvincible ? "∞" : lives.ToString();
        }
    }

    void Move(Vector2 direction)
    {
        Vector3 pos = transform.position;
        pos += (Vector3)direction * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }
     void OnTriggerEnter2D(Collider2D col)
    {
        if((col.tag == "EnemyShipTag") ||  (col.tag == "EnemyBulletTag"))
        {
            PlayExplosion();
            if (!isInvincible)
            {


                lives--;
                LivesUIText.text = lives.ToString();

                if (lives == 0)
                {

                    GameManagerGo.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);

                    //Destroy(gameObject); hide the player's ship
                    gameObject.SetActive(false);
                }
            }
            else 
            {
                LivesUIText.text = "∞";
            }
        }    
    }
    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate (ExplosionGo);

        explosion.transform.position = transform.position;  
    }
}
