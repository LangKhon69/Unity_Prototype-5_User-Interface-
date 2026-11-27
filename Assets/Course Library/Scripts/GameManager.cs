using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public Button restartButton;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText; //Khai báo biến tham chiếu đến G.O Text Mesh trong Scene View -> Điều chỉnh thuộc tính thông qua Scripts
    public TextMeshProUGUI goodText;
    public TextMeshProUGUI badText;

    public List<GameObject> targets;
    private float spawnRate = 1.0f;
    private int score; //Biến lưu trữ điểm hiển thị thông qua Text Mesh pro
    private int bad;
    private int good;
    public bool isGameActive; //Biến kiểm tra game Over hay chưa để tắt các hoạt động khác 

    private Coroutine spawnCoroutine;

    void Start()
    {
        isGameActive = true;
        score = 0;
        spawnCoroutine = StartCoroutine(SpawnTarget());
        UpdateScore(0,0,0);
    }


    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator SpawnTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }

    }
    //Hàm tính toán diểm hiển thị trên giao diện : 
    public void UpdateScore(int scoreToAdd, int g, int b)
    {
        score += scoreToAdd;
        good += g;
        bad += b;
        scoreText.text = "Score: " + score;
        goodText.text = "Good: " + good;
        badText.text = "Bad: " + bad;
    }
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true); //Lệnh bật G.O - GameOverText lên màn hình
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
        StopCoroutine(spawnCoroutine);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
