using UnityEngine;

[CreateAssetMenu(fileName = "new Library", menuName = "Library")]
public class Library : ScriptableObject
{
    [SerializeField] private string[] _words;

    public string[] Words => _words;
}