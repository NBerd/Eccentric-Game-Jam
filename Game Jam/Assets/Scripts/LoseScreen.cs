using UnityEngine;
using TMPro;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private GameObject _container;
    [SerializeField] private Page _page;

    public void Open(string cause) 
    {
        _descriptionText.text = cause;
        _page.Reload();
        _container.SetActive(true);
    }
}