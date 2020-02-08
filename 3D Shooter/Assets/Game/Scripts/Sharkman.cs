using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharkman : MonoBehaviour
{
    private UIManager _UIManager;

    private Player player;

    private bool _isInTrigger;

    AudioSource _audioSource;

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
            if (player.GetCoins() > 0)
            {
                _UIManager.ShowInteractionNotification("Would you like to purchase some fish, buddy?");
            }
            else
            {
                _UIManager.ShowInteractionNotification("Come back when you can afford my fish!");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _isInTrigger = false;
            _UIManager.HideInteractionNotification();
        }
    }

    void Update()
    {
        if (_isInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (player != null && player.GetCoins() > 0)
                {
                    _UIManager.HideInteractionNotification();
                    _audioSource.Play();
                    player.BuyWeapon();
                }
            }
        }
    }
}
