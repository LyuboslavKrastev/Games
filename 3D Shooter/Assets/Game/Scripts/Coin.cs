using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private AudioSource _audioSource;

    private bool _isInTrigger = false;

    UIManager _UIManager;

    Player player;
    
    private void Start()
    {
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_UIManager == null)
        {
            Debug.LogError("UIManager is NULL!");
        }

        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.LogError("AudioSource is NULL!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
      
        if (other.tag == "Player")
        {
            player = other.GetComponent<Player>();
            _isInTrigger = true;
            _UIManager.ShowPickUpNotification();
        }
    }

    void OnTriggerExit(Collider other)
    {    
        if (other.tag == "Player")
        {
            Debug.Log("EXITING");
            _isInTrigger = false;
            _UIManager.HidePickUpNotification();
        }
    }

    void Update()
    {
        if (_isInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Player is pressing E");

                if (player != null)
                {
                    _audioSource.Play();
                    _UIManager.HidePickUpNotification();
                    player.TakeCoin();
                    // remove the renderer, but do not destroy yet so the pickup audio is not stopped
                    Destroy(GetComponent<MeshRenderer>()); 
                    Destroy(this.gameObject, 1.0f);
                }
            }
        }
    }
}
