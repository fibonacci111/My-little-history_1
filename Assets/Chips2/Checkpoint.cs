using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static List<Vector3> collectedCheckpointPositions = new List<Vector3>();
    private bool hasTeleported = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTeleported)
        {
            if (!IsCheckpointCollected(transform.position))
            {
                collectedCheckpointPositions.Add(transform.position);
                SaveCheckpoints();
                TeleportToLastCheckpoint(other.transform);
                hasTeleported = true;
            }
        }
    }

    public static void TeleportToLastCheckpoint(Transform playerTransform)
    {
        if (collectedCheckpointPositions.Count > 0)
        {
            playerTransform.position = collectedCheckpointPositions[collectedCheckpointPositions.Count - 1];
        }
    }

    bool IsCheckpointCollected(Vector3 checkpointPosition)
    {
        return collectedCheckpointPositions.Contains(checkpointPosition);
    }

    void SaveCheckpoints()
    {
        PlayerPrefs.SetInt("CheckpointCount", collectedCheckpointPositions.Count);
        for (int i = 0; i < collectedCheckpointPositions.Count; i++)
        {
            PlayerPrefs.SetFloat("CheckpointPosX_" + i, collectedCheckpointPositions[i].x);
            PlayerPrefs.SetFloat("CheckpointPosY_" + i, collectedCheckpointPositions[i].y);
            PlayerPrefs.SetFloat("CheckpointPosZ_" + i, collectedCheckpointPositions[i].z);
        }
        PlayerPrefs.Save();
    }
}
