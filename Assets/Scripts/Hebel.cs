using UnityEngine;

public class Hebel : MonoBehaviour
{
    public GameObject targetObject;

    private bool isActivated = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Du kannst eine andere Taste verwenden
        {
            isActivated = !isActivated;
            targetObject.SetActive(isActivated);
        }
    }
}