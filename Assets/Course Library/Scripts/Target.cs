using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private float minSpeed = 15f;
    private float maxSpeed = 18f;
    private float maxTorque = 1f;
    private float xRange = 4f;
    private float yRange = 8f;

    private GameManager gameManager;
    public int pointValue;

    public ParticleSystem explosionPartical; //Tham chiếu đến Component ParticalSystem .
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();
        //gameManager = FindAnyObjectByType<GameManager>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); //Lấy scripts của GameManager để truy cập phương thức của nó.
    }

    void Update()
    {

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 2f);

            //Nếu tia chạm kể địch, kẻ địch sẽ bị tiêu diệt
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    if (gameManager.isGameActive)
                    {
                        Destroy(hit.transform.gameObject);
                        //Tạo hiệu ứng ngay tại vị trí bị destroy : 
                        Instantiate(explosionPartical, transform.position, explosionPartical.transform.rotation);
                        gameManager.UpdateScore(pointValue);
                    }
                }
            }           
        }
        if (!gameManager.isGameActive) targetRb.angularVelocity = Vector3.zero; //Gán tốc độ xoay của vật bằng = 0 dừng luôn
    }
    Vector3 RandomForce()
    {
        //Lí do hàm trả về giá trị Vector3 : vì phép tính có Vector3.up là 1 Vector có sẵn = new Vector3(0,1,0) nhân với 1 độ lớn lực ép 
        //vật thể chịu tác động bởi lực đẩy tức thời theo hướng lên trên
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), -yRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DestroyZone"))
        {
            Destroy(this.gameObject);
            if (gameObject.CompareTag("Good"))
            {
                gameManager.GameOver();
                
            }
        }
    }
}
