using TMPro;
using UnityEngine;

public class HostNameInput : MonoBehaviour
{
    public TMP_InputField input;

    public void OnNameChanged()
    {
        if (LocalPlayerCache.Instance != null)
        {
            LocalPlayerCache.Instance.PlayerName = input.text.Trim();
        }
    }
}
