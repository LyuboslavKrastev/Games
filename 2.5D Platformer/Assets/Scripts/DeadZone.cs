using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Transform _startPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.ReduceLives();
                CharacterController controller = player.GetComponent<CharacterController>();
                if (controller != null)
                {
                    controller.enabled = false; // the falling is too fast for the respawn to occur
                    StartCoroutine(ControllerEnableRoutine(controller));
                }
                player.transform.position = _startPoint.position;
            }
        }
    }

    IEnumerator ControllerEnableRoutine(CharacterController controller)
    {
        yield return new WaitForSeconds(1.0f);

        controller.enabled = true;
    }
}
