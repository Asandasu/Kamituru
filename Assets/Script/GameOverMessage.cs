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
    [SerializeField] private Image panel;
    void Start()
    {
        panel.color = new Color(0,0,0,0);
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
        yield return new WaitForSeconds(0.5f); 
        Scene currentScene = SceneManager.GetActiveScene();

        for(float f = 0; f < 1; f += 0.01f)
        {
            panel.color = new Color(1, 1, 1, f);
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(currentScene.name);
    }

    public void QuitGame()
    {
        gameoverMes.text = "あ・・・マジ？\nなら、少し手伝ってもらおうか・・・\nこっちに来て、資料を取ってきてくれる";
        button[0].SetActive(false);
        button[1].SetActive(false);
        StartCoroutine(CloseMainGame());
    }

    private IEnumerator CloseMainGame()
    {
        yield return new WaitForSeconds(0.5f);
        Scene currentScene = SceneManager.GetActiveScene();

        for (float f = 0; f < 1; f += 0.01f)
        {
            panel.color = new Color(0, 0, 0, f);
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(currentScene.name);
    }
}
