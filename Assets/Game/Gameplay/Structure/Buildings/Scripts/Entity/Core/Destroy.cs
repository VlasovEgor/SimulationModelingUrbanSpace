using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] private GameObject _building;

    public void DestroyBuidling()
    {
        Destroy(_building);
    }
}
