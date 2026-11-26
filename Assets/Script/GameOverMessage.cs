using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMessage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameoverMes;
    [SerializeField] private GameObject[] button;
    void Start()
    {
        button[0].SetActive(false);
        button[1].SetActive(false);
    }

    public void MessageSpawn()
    {
        StartCoroutine(GameOverMes());
    }

    private IEnumerator GameOverMes()
    {
        yield return new WaitForSeconds(0.5f);
        gameoverMes.text = "どうやら死んだみたいだね\n小野篁の末裔よ・・・\nどうする？";
        yield return new WaitForSeconds(0.5f);
        button[0].SetActive(true);
        button[1].SetActive(true);
    }

    public void ContinueReStart()
    {
        gameoverMes.text = "よろしい・・・\nなら、近いところに置いてあげよう";
        button[0].SetActive(false);
        button[1].SetActive(false);
        StartCoroutine(ReStart());

    }
    private IEnumerator ReStart()
    {
        yield return new WaitForSeconds(2f); 
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void QuitGame()
    {
        gameoverMes.text = "あ・・・マジ？\nなら、少し手伝ってもらおうか・・・\nこっちに来て、資料を取ってきてくれる";
        button[0].SetActive(false);
        button[1].SetActive(false);
    }
}
