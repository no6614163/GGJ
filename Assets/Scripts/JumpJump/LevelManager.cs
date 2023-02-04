using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JumpJump
{
    public class LevelManager : LevelManagerBase
    {

        public GameConfig Config { get; private set; }

        [SerializeField] Character characterPrefab;
        [SerializeField] Obstacle obstaclePrefab;
        [SerializeField] RectTransform characterLayer;
        [SerializeField] RectTransform obstacleLayer;
        [SerializeField] RectTransform ground;
        List<Character> characters;
        List<Obstacle> obstacles = new List<Obstacle>();

        float scrolledAmount = 0;
        float nextSpawnDistance;
        bool isGameOver = false;
        protected override void Awake()
        {
            base.Awake();
            Config = GetComponent<GameConfig>();
            InitGame();
        }
        protected override void InitGame()
        {
            characters = new List<Character>();
            for(int i=0; i<Config.Characters.Length; i++)
            {
                Character character = Instantiate(characterPrefab, characterLayer.transform);
                character.Init(this, ground.rect.height, Config.Characters[i]);
                character.Position = new Vector2(character.Position.x - 180 * Config.CharactersRandomness * ((float)i/Config.Characters.Length), ground.rect.height);
                characters.Add(character);
            }
            characters.Sort((a, b) => (int)(b.Position.x - a.Position.x));
            nextSpawnDistance = ground.rect.width;
            StartTimer();
        }

        IEnumerator DelayJumps()
        {
            float totalDelay = 0.17f * Config.CharactersRandomness * Config.DurationScale;
            foreach (Character character in characters)
            {
                character.Jump();
                if(totalDelay > 0f)
                    yield return new WaitForSeconds(totalDelay / characters.Count);
            }
        }
        public override void OnTimerEnd()
        {
            if (isGameOver)
                return;
            base.OnTimerEnd();
            OnClear();
        }
        private void Update()
        {
            if (!isGameOver && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
            {
                StartCoroutine(DelayJumps());
            }
            if(!isGameOver)
            {
                scrolledAmount += Config.ScrollSpeed * Time.deltaTime * Config.TimeScale;
                if (scrolledAmount >= nextSpawnDistance)
                {
                    SpawnObstacle();
                }
            }
            //obstacle 이동
            for (int i=0; i<obstacles.Count; i++)
            {
                obstacles[i].Position += new Vector2(-Config.ScrollSpeed, 0) * Time.deltaTime * Config.TimeScale;
                if (obstacles[i].Position.x < -obstacleLayer.rect.width)
                {
                    Obstacle ob = obstacles[i];
                    obstacles.RemoveAt(i);
                    Destroy(ob.gameObject);
                    i--;
                }
            }
            if (isClear)
                return;
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
            {
                yield return new WaitForSeconds(1);
                ShowResult(false);
            }
            else
            {
                yield return new WaitForSeconds(2);
                ShowResult(true);
            }

        }
        void OnGameOver()
        {
            isGameOver = true;
            StopTimer();
            //TODO : 게임오버 (첫 양 충돌)
            StartCoroutine(SlowScroll(0.5f, true));
        }
        bool isClear = false;
        void OnClear()
        {
            isClear = true;
            isGameOver = true;
            foreach (var ch in characters)
                ch.OnWin();
            SoundManager.Instance.PlaySFX("Win", "JumpJump", 1.2f);
            StartCoroutine(SlowScroll(0.3f, false));

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
