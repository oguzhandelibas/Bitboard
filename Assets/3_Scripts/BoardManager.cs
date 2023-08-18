using UnityEngine;
using System;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Bitboard
{
    public class BoardManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] tilePrefabs;
        [SerializeField] private GameObject housePrefab;
        [SerializeField] private GameObject treePrefab;
        [SerializeField] private Text score;

        private CellController _cellController;
        private GameObject[] _tiles;
        private long _desertBB = 0;
        private long _dirtBB = 0;
        private long _pastureBB = 0;
        private long _treeBB = 0;
        private long _waterBB = 0;
        private long _playerBB = 0;

        public void Initialize()
        {
            _cellController = new CellController();
            _tiles = new GameObject[64];
            for (var r = 0; r < 8; r++)
            {
                for (var c = 0; c < 8; c++)
                {
                    int randomTile = Random.Range(0, tilePrefabs.Length);
                    Vector3 pos = new Vector3(c, 0, r);
                    GameObject tile = Instantiate(tilePrefabs[randomTile], pos, Quaternion.identity);

                    tile.name = tile.tag + "_" + r + "_" + c;
                    _tiles[r * 8 + c] = tile;

                    if (tile.tag == "Desert")
                        _desertBB = _cellController.SetCellState(_desertBB, r, c);
                    else if (tile.tag == "Dirt")
                        _dirtBB = _cellController.SetCellState(_dirtBB, r, c);
                    else if (tile.tag == "Pasture")
                        _pastureBB = _cellController.SetCellState(_pastureBB, r, c);
                    else if (tile.tag == "Water")
                        _waterBB = _cellController.SetCellState(_waterBB, r, c);
                }
            }

            InvokeRepeating("PlantTree", 0.5f, 1);
        }
        public void HandleMouseClickAndCreateHouse()
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                int r = (int)hit.collider.gameObject.transform.position.z;
                int c = (int)hit.collider.gameObject.transform.position.x;
                if (_cellController.GetCellState(~_waterBB & ~_dirtBB & ~_treeBB & ~_playerBB, r, c))
                {
                    CreateHouse(hit.collider.gameObject.transform);
                    _playerBB = _cellController.SetCellState(_playerBB, r, c);
                    CalculateScore();
                }

            }
        }
        private void CreateHouse(Transform hitTransform)
        {
            GameObject house = Instantiate(housePrefab);
            house.transform.parent = hitTransform;
            house.transform.localPosition = Vector3.zero;
        }
        private void PlantTree()
        {
            int rr = Random.Range(0, 8);
            int rc = Random.Range(0, 8);
            if (_cellController.GetCellState(_dirtBB | (_pastureBB & ~_playerBB), rr, rc))
            {
                GameObject tree = Instantiate(treePrefab);
                tree.transform.parent = _tiles[rr * 8 + rc].transform;
                tree.transform.localPosition = Vector3.zero;
                _treeBB = _cellController.SetCellState(_treeBB, rr, rc);
            }
        }
        private void CalculateScore()
        {
            score.text = "Score: " + (_cellController.CellCount(_pastureBB & _playerBB) * 5 +
                                      _cellController.CellCount(_desertBB & _playerBB) * 2);
        }
    }
}
