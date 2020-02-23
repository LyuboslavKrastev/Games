using UnityEngine;
public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        int muscPlayersCount = FindObjectsOfType<MusicPlayer>().Length;
        if (muscPlayersCount > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(transform.gameObject);
        }
    }
}