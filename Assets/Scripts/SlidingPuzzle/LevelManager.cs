using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SlidingPuzzle
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] Piece piecePrefab;
        [SerializeField] RectTransform grid;
        GameConfig config;

        Vector2Int emptyPos;

        Piece[][] pieces;
        int boardSize;
        float cellSize;

        bool isGameOver = false;

        float elapsedTime = 0f;
        bool isPieceMoving = false;
        private void Awake()
        {
            config = GetComponent<GameConfig>();
            InitGame();
        }
        void InitGame()
        {
            boardSize = (int)config.BoardSize;
            int currentPicture = 0;
            cellSize = grid.rect.width / boardSize;


            Vector2Int emptyPos;
            if (config.EmptyPiecePos[currentPicture] == EmptyPieceType.LeftDown)
                emptyPos = new Vector2Int(0, 0);
            else if (config.EmptyPiecePos[currentPicture] == EmptyPieceType.RightDown)
                emptyPos = new Vector2Int(boardSize-1, 0);
            else if (config.EmptyPiecePos[currentPicture] == EmptyPieceType.LeftUp)
                emptyPos = new Vector2Int(0, boardSize-1);
            else //if (config.EmptyPiecePos[currentPicture] == EmptyPieceType.RightUp)
                emptyPos = new Vector2Int(boardSize-1, boardSize-1);
            float unitlen = config.Pictures[currentPicture].texture.width / (int)config.BoardSize;
            pieces = new Piece[(int)config.BoardSize][];
            for (int i=0; i < (int)config.BoardSize; i++)
            {
                pieces[i] = new Piece[(int)config.BoardSize];
                for (int j = 0; j < (int)config.BoardSize; j++)
                {
                    if (i == emptyPos.y && j == emptyPos.x)
                    {
                        pieces[i][j] = null;
                        emptyPos = new Vector2Int(j, i);
                        continue;
                    }
                    Sprite sprite = Sprite.Create(config.Pictures[currentPicture].texture, 
                        new Rect(j * unitlen, i * unitlen, unitlen, unitlen), Vector2.zero);
                    Piece piece = Instantiate(piecePrefab, grid.transform);
                    piece.Position = new Vector2((j+0.5f) * cellSize, (i+0.5f) * cellSize) - new Vector2(grid.rect.width / 2, grid.rect.height / 2);
                    piece.Size = new Vector2(cellSize, cellSize);
                    piece.Init(this, sprite, i* (int)config.BoardSize + j);
                    piece.PosInGrid = new Vector2Int(j, i);
                    pieces[i][j] = piece;
                }
            }
            Shuffle();
        }
        void Shuffle()
        {
            Vector2Int lastClicked = emptyPos;
            for(int a=0; a<config.ShuffleCount; a++)
            {
                //���� �� �ִ°� ������, �������� ������ �ȴ���.
                //������ �ִ°� : ��ĭ �ֺ�
                int[] dx = { -1, 0, 1, 0 };
                int[] dy = { 0, 1, 0, -1 };
                int[] indicies = { 0, 1, 2, 3 };
                HappyUtils.Random.Shuffle(indicies);
                for (int i = 0; i< 4; i++)
                {
                    int k = indicies[i];
                    Vector2Int checkPos = emptyPos + new Vector2Int(dx[k], dy[k]);
                    if (checkPos.x < 0 || checkPos.y < 0 || checkPos.x > boardSize - 1 || checkPos.y > boardSize - 1)
                        continue;
                    if (checkPos !=lastClicked)
                    {
                        Vector2 targetPos = new Vector2((emptyPos.x + 0.5f) * cellSize, (emptyPos.y + 0.5f) * cellSize) - new Vector2(grid.rect.width / 2, grid.rect.height / 2);

                        Piece piece = pieces[checkPos.y][checkPos.x];
                        piece.Position = targetPos;
                        piece.PosInGrid = emptyPos;
                        pieces[checkPos.y][checkPos.x] = null;
                        pieces[emptyPos.y][emptyPos.x] = piece;
                        lastClicked = checkPos;
                        emptyPos = checkPos;
                        break;
                    }
                }
            }
        }
        private void Update()
        {
            elapsedTime += Time.deltaTime;
            if (!isGameOver && !isPieceMoving && elapsedTime >= config.TimeLimit)
            {
                SetClickable(false);
                GameOver();
            }
        }
        void GameOver()
        {
            isGameOver = true;
            UI_Manager.Instance.ShowPopupUI<UI_FailedPopup>();
        }
        public bool OnPieceClicked(Piece piece)
        {
            int[] dx = { -1, 0, 1, 0 };
            int[] dy = { 0, 1, 0, -1};
            for(int k=0; k<4; k++)
            {
                Vector2Int checkPos = piece.PosInGrid + new Vector2Int(dx[k], dy[k]);
                if (checkPos.x < 0 || checkPos.y < 0 || checkPos.x > boardSize - 1 || checkPos.y > boardSize - 1)
                    continue;
                if (checkPos == emptyPos)
                {
                    emptyPos = piece.PosInGrid;
                    StartCoroutine(MovePiece(piece.PosInGrid, checkPos,config.PieceMoveDuration));
                    return true;
                }
            }
            return false;
        }
        void SetClickable(bool clickable)
        {
            foreach(Piece[] p in pieces)
            {
                foreach (Piece piece in p)
                {
                    if(piece != null)
                        piece.Clickable = clickable;
                }
            }
        }
        IEnumerator OnGameClear()
        {
            foreach (Piece[] p in pieces)
            {
                foreach (Piece piece in p)
                {
                    if (piece != null)
                        StartCoroutine(piece.RemoveBorder(0.5f));
                }
            }
            yield return new WaitForSeconds(1f);
            UI_Manager.Instance.ShowPopupUI<UI_ClearPopup>();
        }
        bool CheckGameClear()
        {
            for(int i=0; i<boardSize; i++)
            {
                for (int j=0; j<boardSize; j++)
                {
                    if (pieces[i][j] != null && pieces[i][j].ID != j + i * boardSize)
                        return false;
                }
            }
            return true;
        }
        IEnumerator MovePiece(Vector2Int from, Vector2Int to, float duration)
        {
            isPieceMoving = true;
            SetClickable(false);
            Piece piece = pieces[from.y][from.x];
            float eTime = 0f;
            Vector2 originalPos = piece.Position;
            Vector2 targetPos = new Vector2((to.x + 0.5f) * cellSize, (to.y + 0.5f) * cellSize) - new Vector2(grid.rect.width / 2, grid.rect.height / 2);
            while (eTime < duration)
            {
                piece.Position = Vector2.Lerp(originalPos, targetPos, eTime / duration);
                yield return null;
                eTime += Time.deltaTime;
            }
            piece.Position = targetPos;
            piece.PosInGrid = to;
            emptyPos = from;
            pieces[from.y][from.x] = null;
            pieces[to.y][to.x] = piece;

            if(CheckGameClear())
            {
                yield return StartCoroutine(OnGameClear());
                yield break;
            }
            SetClickable(true);
            isPieceMoving = false;

        }

    }

}
