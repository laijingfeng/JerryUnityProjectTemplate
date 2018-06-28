using UnityEngine;

namespace Jerry
{
    public class DontDestroy : MonoBehaviour
    {
        void Awake()
        {
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
    }
}