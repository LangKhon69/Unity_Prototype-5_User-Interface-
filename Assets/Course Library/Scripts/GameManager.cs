using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText; //Khai báo biến tham chiếu đến G.O Text Mesh trong Scene View -> Điều chỉnh thuộc tính thông qua Scripts
    public TextMeshProUGUI goodText;
    public TextMeshProUGUI badText;

    public List<GameObject> targets;
    private float spawnRate = 1.0f;
    private int score; //Biến lưu trữ điểm hiển thị thông qua Text Mesh pro
    private int good;
    private int bad;
   


    private Coroutine spawnCoroutine;

    void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnTarget());
        UpdateScore(0,0,0);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnTarget()
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
}
