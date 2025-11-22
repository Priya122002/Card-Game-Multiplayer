using UnityEngine;

public class LocalPlayerCache : MonoBehaviour
{
    public static LocalPlayerCache Instance;
    public string PlayerName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
