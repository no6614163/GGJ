using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JumpJump
{
    public class LevelManager : MonoBehaviour
    {

        public GameConfig Config;

        [SerializeField] Character characterPrefab;
        [SerializeField] Obstacle obstaclePrefab;
        [SerializeField] RectTransform characterLayer;
        [SerializeField] RectTransform obstacleLayer;
        [SerializeField] RectTransform ground;
        List<Character> characters;
        List<Obstacle> obstacles = new List<Obstacle>();

        float elapsedTime = 0f;
        float scrolledAmount = 0;
        float nextSpawnDistance;
        bool isGameOver = false;
        private void Awake()
        {
            InitGame();
        }
        void InitGame()
        {
            characters = new List<Character>();
            for(int i=0; i<Config.Characters.Length; i++)
            {
                Character character = Instantiate(characterPrefab, characterLayer.transform);
                character.Init(this, ground.rect.height, Config.Characters[i]);
                character.Position = new Vector2(character.Position.x + 100 * Config.CharactersRandomness * Random.Range(-0.5f, 0.5f), ground.rect.height);
                characters.Add(character);
            }
            characters.Sort((a, b) => (int)(b.Position.x - a.Position.x));
            nextSpawnDistance = ground.rect.width;
        }

        IEnumerator DelayJumps()
        {
            float totalDelay = 0.1f * Config.CharactersRandomness;
            foreach (Character character in characters)
            {
                character.Jump();
                if(totalDelay > 0f)
                    yield return new WaitForSeconds(totalDelay / characters.Count);
            }
        }
        private void Update()
        {
            if (!isGameOver && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
            {
                StartCoroutine(DelayJumps());
            }
            if (!isGameOver)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime > Config.TimeLimit)
                {
                    OnClear();
                }
            }
            if(!isGameOver)
            {
                scrolledAmount += Config.ScrollSpeed * Time.deltaTime;
                if (scrolledAmount >= nextSpawnDistance)
                {
                    SpawnObstacle();
                }
            }
            //obstacle 이동
            for (int i=0; i<obstacles.Count; i++)
            {
                obstacles[i].Position += new Vector2(-Config.ScrollSpeed, 0) * Time.deltaTime;
                if (obstacles[i].Position.x < -obstacleLayer.rect.width)
                {
                    Obstacle ob = obstacles[i];
                    obstacles.RemoveAt(i);
                    Destroy(ob.gameObject);
                    i--;
                }
            }
            //충돌체크
            foreach(Character character in characters)
            {
                foreach(Obstacle obstacle in obstacles)
                {
                    if (Vector2.Distance(character.Position, obstacle.Position) < character.Radius + obstacle.Radius)
                    {
                        character.OnCollision();
                        if(!isGameOver)
                            OnGameOver();
                    }
                }
            }
        }
        IEnumerator SlowScroll(float duration, bool isLose)
        {
            float eTime = 0f;
            float originalSpeed = Config.ScrollSpeed;
            while(eTime < duration)
            {
                Config.ScrollSpeed = Mathf.Lerp(originalSpeed, 0, eTime / duration);
                yield return null;
                eTime += Time.deltaTime;
            }
            Config.ScrollSpeed = 0f;
            if(isLose)
                UI_Manager.Instance.ShowPopupUI<UI_FailedPopup>();
            else
                UI_Manager.Instance.ShowPopupUI<UI_ClearPopup>();

        }
        void OnGameOver()
        {
            isGameOver = true;
            //TODO : 게임오버 (첫 양 충돌)
            Debug.Log("게임오버!!");
            StartCoroutine(SlowScroll(0.5f, true));
        }
        void OnClear()
        {
            isGameOver = true;
            StartCoroutine(SlowScroll(0.3f, false));
            //for (int i = 0; i < obstacles.Count; i++)
            //        Destroy(obstacles[i].gameObject);
            //obstacles.Clear();
        }
        void SpawnObstacle()
        {
            var ob  = Instantiate(obstaclePrefab, obstacleLayer);
            ob.Position = new Vector2(obstacleLayer.rect.width / 2 + ob.Size.x / 2, ground.rect.height);
            obstacles.Add(ob);
            nextSpawnDistance = Random.Range(Config.ObstacleDistanceRange.x, Config.ObstacleDistanceRange.y);
            scrolledAmount = 0f;
        }
    }

}
