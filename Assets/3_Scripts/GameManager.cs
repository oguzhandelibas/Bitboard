using UnityEngine;

namespace Bitboard
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private BoardManager boardManager;

        private void Start()
        {
            boardManager.Initialize();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                boardManager.HandleMouseClickAndCreateHouse();
            }
        }

    }
}
