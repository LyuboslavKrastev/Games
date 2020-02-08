using UnityEngine;
public class DestroyedCrate : MonoBehaviour
{
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.LogError("AudioSource is NULL!");
        }
        else
        {
            _audioSource.Play();
        }
    }
}
