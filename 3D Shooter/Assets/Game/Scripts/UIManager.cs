using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Text _reloadingText;

    [SerializeField]
    private Text _reloadText;

    [SerializeField]
    private Text _ammoCount;

    [SerializeField]
    private Text _pickUpCoin;

    [SerializeField]
    private Image _coinImg;
    public void UpdateAmmo(int count)
    {
        _ammoCount.text = $"Ammo: {count}";
    }

    public void ShowReloading()
    {
        _reloadingText.gameObject.SetActive(true);
    }

    public void HideReloading()
    {
        _reloadingText.gameObject.SetActive(false);
    }

    public void ShowReloadWarning()
    {
        _reloadText.gameObject.SetActive(true);
    }

    public void HideReloadWarning()
    {
        _reloadText.gameObject.SetActive(false);
    }

    public void UpdateCoins(int coins)
    {
        _coinImg.gameObject.SetActive(true);
    }

    public void ShowPickUpNotification()
    {
        _pickUpCoin.gameObject.SetActive(true);
    }

    public void HidePickUpNotification()
    {
        _pickUpCoin.gameObject.SetActive(false);
    }

}
