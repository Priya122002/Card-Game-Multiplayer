//using Unity.Netcode;
//using UnityEngine;

//public class JsonMessageSender : NetworkBehaviour
//{
//    public static JsonMessageSender Instance;

//    private void Awake()
//    {
//        Instance = this;
//    }

//    // Client → Server
//    public void Send(string json)
//    {
//        if (!IsOwner) return;
//        SendMessageServerRpc(json);
//    }

//    // Server receives JSON
//    [ServerRpc(RequireOwnership = false)]
//    void SendMessageServerRpc(string json)
//    {
//        ProcessMessage(json);
//        ReceiveMessageClientRpc(json);
//    }

//    // All Clients receive JSON
//    [ClientRpc]
//    void ReceiveMessageClientRpc(string json)
//    {
//        ProcessMessage(json);
//    }

//    private void ProcessMessage(string json)
//    {
//        Debug.Log("JSON Received: " + json);
//        // You will add routing by action later
//    }
//}
using UnityEngine;

public class JsonMessageSender : MonoBehaviour
{
    public static JsonMessageSender Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Send(string json)
    {
        TurnManager.Instance.SendJsonToServer(json);
    }

   
    private string GetValue(string json, string key)
    {
        int index = json.IndexOf(key) + key.Length + 3;
        int end = json.IndexOfAny(new char[] { ',', '}' }, index);
        return json.Substring(index, end - index).Replace("\"", "");
    }
}
