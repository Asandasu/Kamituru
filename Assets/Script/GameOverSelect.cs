
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameOverSelect : MonoBehaviour
{
    [Header("最初に選択するボタン")]
    [SerializeField] private Button firstButton;

    private void OnEnable()
    {
        StartCoroutine(SelectFirstButton());
    }

    private IEnumerator SelectFirstButton()
    {
        yield return null;

        if (EventSystem.current == null || firstButton == null)
            yield break;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(
            firstButton.gameObject
        );
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.K))
            return;

        if (EventSystem.current == null)
            return;

        GameObject selected =
            EventSystem.current.currentSelectedGameObject;

        if (selected == null)
            return;

        Button button = selected.GetComponent<Button>();

        if (button != null && button.interactable)
        {
            button.onClick.Invoke();
        }
    }
}
