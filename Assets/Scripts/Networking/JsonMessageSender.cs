using Unity.Netcode;
using UnityEngine;

public class JsonMessageSender : NetworkBehaviour
{
    public static JsonMessageSender Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Client → Server
    public void Send(string json)
    {
        if (!IsOwner) return;
        SendMessageServerRpc(json);
    }

    // Server receives JSON
    [ServerRpc(RequireOwnership = false)]
    void SendMessageServerRpc(string json)
    {
        ProcessMessage(json);
        ReceiveMessageClientRpc(json);
    }

    // All Clients receive JSON
    [ClientRpc]
    void ReceiveMessageClientRpc(string json)
    {
        ProcessMessage(json);
    }

    private void ProcessMessage(string json)
    {
        Debug.Log("JSON Received: " + json);
        // You will add routing by action later
    }
}
