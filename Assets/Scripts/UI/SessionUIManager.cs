using UnityEngine;

public sealed class SessionUIManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameObject createPanel;
    [SerializeField] private GameObject joinPanel;

    private void Awake()
    {
        ValidateReferences();
    }

    private void Start()
    {
        ShowOptionPanel();
    }

    #region Panel Controls

    public void ShowOptionPanel()
    {
        SetActivePanel(option: true, create: false, join: false);
    }

    public void ShowCreatePanel()
    {
        SetActivePanel(option: false, create: true, join: false);
    }

    public void ShowJoinPanel()
    {
        SetActivePanel(option: false, create: false, join: true);
    }

    #endregion

    #region Helpers

    private void SetActivePanel(bool option, bool create, bool join)
    {
        optionPanel?.SetActive(option);
        createPanel?.SetActive(create);
        joinPanel?.SetActive(join);
    }

    private void ValidateReferences()
    {
        if (!optionPanel || !createPanel || !joinPanel)
        {
            Debug.LogError("SessionUIManager: One or more panel references are missing!");
        }
    }

    #endregion
}
