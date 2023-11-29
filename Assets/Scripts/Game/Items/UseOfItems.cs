using UnityEngine;

public class UseOfItems : MonoBehaviour
{
    public static UseOfItems instance;

    private void Start()
    {
        instance = this;
    }

    public void Use(AssetItem item)
    {
        // здесь прописывается механика свойств. То есть если предмет хилит, это прописывается здесь
    }
}
