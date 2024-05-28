using UnityEngine;
using Unity.Netcode;

public class PlayerSpawnManager : NetworkBehaviour
{
    public GameObject role1Prefab;
    public GameObject role2Prefab;
    public Vector3 hostSpawnPoint;
    public Vector3 clientSpawnPoint;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;

            if (IsHost)
            {
                SpawnHostPlayer();
            }
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        if (clientId != NetworkManager.Singleton.LocalClientId)
        {
            SpawnClientPlayer(clientId);
        }
    }

    private void SpawnHostPlayer()
    {
        GameObject hostPlayer = Instantiate(role1Prefab, hostSpawnPoint, Quaternion.identity);
        hostPlayer.GetComponent<NetworkObject>().SpawnAsPlayerObject(NetworkManager.Singleton.LocalClientId);
    }

    private void SpawnClientPlayer(ulong clientId)
    {
        GameObject clientPlayer = Instantiate(role2Prefab, clientSpawnPoint, Quaternion.identity);
        clientPlayer.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
    }

    public override void OnDestroy()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        }

        base.OnDestroy();
    }
}
