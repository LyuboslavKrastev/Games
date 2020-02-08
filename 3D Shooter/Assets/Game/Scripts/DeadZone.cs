using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DeadZone : MonoBehaviour
{
    [SerializeField]
    private Transform _startPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                CharacterController controller = player.GetComponent<CharacterController>();
                NavMeshAgent agent = player.GetComponent<NavMeshAgent>();

                if (controller != null)
                {
                    controller.enabled = false; // the falling is too fast for the respawn to occur
                    agent.enabled = false;
                    StartCoroutine(EnableRoutine(controller, agent));
                }
                player.transform.position = _startPoint.position;
            }
        }
        else
        {
            Destroy(other.transform.parent.gameObject);
        }
    }
    IEnumerator EnableRoutine(CharacterController controller, NavMeshAgent agent)
    {
        yield return new WaitForSeconds(1.0f);

        controller.enabled = true;
        agent.enabled = true;
    }
}
