using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TMP_Text turnText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Button playCardButton;
    [SerializeField] private Button endTurnButton;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (TurnManager.Instance == null) return;

        turnText.text = $"Turn {TurnManager.Instance.CurrentTurn.Value}";
        timerText.text = Mathf.CeilToInt(TurnManager.Instance.GetLocalTime()).ToString();

        bool started = TurnManager.Instance.IsTimerRunning.Value;
        bool iEnded = TurnManager.Instance.HaveIEnded();

        playCardButton.interactable = !started;
        endTurnButton.interactable = started && !iEnded;
    }

    public void OnPlayCardClicked()
    {
        TurnManager.Instance.StartTimerServerRpc();
    }

    public void OnEndTurnClicked()
    {
        TurnManager.Instance.EndTurnLocal();
    }
}
