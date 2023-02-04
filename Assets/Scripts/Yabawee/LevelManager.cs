using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yabawee
{
    public class LevelManager : LevelManagerBase
    {
        int currentItemIndex;
        float cupYPos;
        float itemYPos;

        bool isWrong;

        int itemShuffledCount = 0;
        CupBehaviour chosenCup = null;
        bool [] doFalseShuffle;

        Vector2[] handOriginalPos = new Vector2[2];
        Vector2[] cupOriginalPos;

         CupBehaviour[] cups;
        [SerializeField] Hand [] hands;
        [SerializeField] RectTransform referenceCup;
        [SerializeField] RectTransform showStage;
        [SerializeField] Item item;
        [SerializeField] CupBehaviour cupPrefab;
        [SerializeField] RectTransform canvas;
        [SerializeField] UIBurstParticle particlePrefab;
        public void InstantiateParticle(Vector2 pos)
        {
            var particle = Instantiate(particlePrefab, canvas.transform);
            particle.RectTransform.position = pos;
        }

        GameConfig config;
        public GameConfig Config { get { return config; } }

        public void OnCupClicked(CupBehaviour cup)
        {
            chosenCup = cup;
        }
        protected override void Awake()
        {
            base.Awake();
            //TODO : config 변경
            //--
            config = GetComponent<GameConfig>();
            InitGame();
        }
        protected override void InitGame()
        {
            cupOriginalPos = new Vector2[config.CupCount];
            cupYPos = referenceCup.anchoredPosition.y;
            itemYPos = item.RectTransform.anchoredPosition.y;
            cups = new CupBehaviour[config.CupCount];
            float leftmostX = -(config.CupCount - 1) * 0.5f * config.CupSpacing;
            for(int i=0; i< config.CupCount; i++)
            {
                CupBehaviour newCup = Instantiate(cupPrefab, showStage);
                cups[i] = newCup;
                newCup.Init(this, i);
                float x = leftmostX + i * config.CupSpacing;
                newCup.RectTransform.anchoredPosition = new Vector2(x, cupYPos);
                cupOriginalPos[i] = new Vector2(x, cupYPos);
            }
            currentItemIndex = Random.Range(0, config.CupCount);
            item.RectTransform.anchoredPosition = new Vector2(cups[currentItemIndex].RectTransform.anchoredPosition.x, itemYPos);

            for(int i=0; i<2; i++)
            {
                handOriginalPos[i] = hands[i].RectTransform.anchoredPosition;
                hands[i].RectTransform.SetAsLastSibling();
            }
            Destroy(referenceCup.gameObject);
        }
        private void Start()
        {
            StartCoroutine(GameCoroutine());
        }
        IEnumerator GameCoroutine()
        {
            for (int i = 0; i < config.RoundCount; i++)
            {
                yield return new WaitForSeconds(config.RoundInterval);
                yield return StartCoroutine(RoundCoroutine(i));
                chosenCup = null;
                if(isWrong)
                {
                    //TODO : 게임 오버
                    ShowResult(false);
                    yield break;
                }
            }
            //게임 클리어
            ShowResult(true);
        }
        IEnumerator RoundCoroutine(int round)
        {
            float handMoveDuration = 1f * config.DurationScale;
            float[] dur = new float[] { 0.3f, 0.4f, 0.3f };
            foreach (var cup in cups)
                cup.Clickable = false; 
            itemShuffledCount = 0;
            doFalseShuffle = new bool[config.ShufflePerRound[round]];
            int falseShuffleCount = Random.Range(config.FalseShuffleRangePerRound[round].x, config.FalseShuffleRangePerRound[round].y + 1);
            for(int i=0; i<doFalseShuffle.Length; i++)
            {
                if (i < falseShuffleCount)
                    doFalseShuffle[i] = true;
                else
                    doFalseShuffle[i] = false;
            }
            HappyUtils.Random.Shuffle(doFalseShuffle);
            ArrangeCups();
            ArrangeHands();
            for (int i=0; i<config.CupCount; i+=2)
            {
                //컵쪽으로 손 감
                if (i + 1 < config.CupCount)
                {
                    StartCoroutine(GrabCup(hands[0], i, handMoveDuration * dur[0]));
                    yield return StartCoroutine(GrabCup(hands[1], i + 1, handMoveDuration * dur[0]));
                }
                else
                {
                    yield return StartCoroutine(GrabCup(hands[1], i, handMoveDuration * dur[0]));
                }
                //컵 앞으로 놓음
                if (i + 1 < config.CupCount)
                {
                    StartCoroutine(CupToFront(i, handMoveDuration * dur[1], handMoveDuration * dur[2]));
                    yield return StartCoroutine(CupToFront(i + 1, handMoveDuration * dur[1], handMoveDuration * dur[2]));
                }
                else
                {
                    yield return StartCoroutine(CupToFront(i, handMoveDuration * dur[1], handMoveDuration * dur[2]));
                }
                hands[1].transform.SetAsLastSibling();
                hands[0].transform.SetAsLastSibling();
            }

            item.gameObject.SetActive(false);
            //섞기
            int[] indicies = new int[config.CupCount];
            for (int i = 0; i < config.CupCount; i++)
                indicies[i] = i;
            for (int i=0; i<config.ShufflePerRound[round]; i++)
            {
                int[] shuffleTargets = HappyUtils.Random.RandomElements(indicies, 2);
                if (config.ShufflePerRound[round] - 1 - i < config.MinItemMoveCount[round] - itemShuffledCount)
                {
                    shuffleTargets[0] = currentItemIndex;
                    List<int> candidates = new List<int>();
                    candidates.AddRange(indicies);
                    candidates.Remove(currentItemIndex);
                    shuffleTargets[1] = HappyUtils.Random.RandomElement(candidates);
                }
                if (shuffleTargets[0] == itemShuffledCount || shuffleTargets[1] == itemShuffledCount)
                    itemShuffledCount++;
                yield return StartCoroutine(Shuffle(new int[]{ shuffleTargets[0], shuffleTargets[1]}, doFalseShuffle[i]));
                ArrangeHands();
                if (i < config.ShufflePerRound[round] - 1)
                    yield return new WaitForSeconds(config.ShuffleInterval * config.DurationScale);
            }
            item.transform.SetAsFirstSibling();
            yield return StartCoroutine(ReturnHands());

            foreach (var cup in cups)
                cup.Clickable = true;

            //플레이어가 선택
            while (chosenCup == null)
                yield return null;

            foreach (var cup in cups)
                cup.Clickable = false;
            item.gameObject.SetActive(true);
            item.RectTransform.anchoredPosition = new Vector2(cups[currentItemIndex].RectTransform.anchoredPosition.x, itemYPos);
            if (chosenCup.ID == currentItemIndex)
                OnCorrect();
            else
                OnWrong();

            int hand = GetCloseHandIndex(chosenCup);
            yield return StartCoroutine(GrabCup(hands[hand], chosenCup.ID, handMoveDuration * dur[0]));
            item.Shake();
            yield return StartCoroutine(CupToBack(chosenCup.ID, handMoveDuration * dur[1], handMoveDuration * dur[2]));
            yield return new WaitForSeconds(1f);

            List<int> leftCups = new List<int>();
            for(int i=0; i<config.CupCount; i++)
            {
                if (i == chosenCup.ID) continue;
                leftCups.Add(i);
            }
            leftCups.Sort((x, y) => (int)(cups[x].RectTransform.anchoredPosition.x - cups[y].RectTransform.anchoredPosition.x));

            ArrangeHands();
            for (int i = 0; i < leftCups.Count; i += 2)
            {
                //컵쪽으로 손 감

                if (i + 1 < leftCups.Count)
                {
                    StartCoroutine(GrabCup(hands[0], leftCups[i], handMoveDuration * dur[0]));
                    yield return StartCoroutine(GrabCup(hands[1], leftCups[i + 1], handMoveDuration * dur[0]));
                }
                else
                {
                    yield return StartCoroutine(GrabCup(hands[1], leftCups[i], handMoveDuration * dur[0]));
                }
                //컵 앞으로 놓음
                if (i + 1 < leftCups.Count)
                {
                    StartCoroutine(CupToBack(leftCups[i], handMoveDuration * dur[1], handMoveDuration * dur[2]));
                    yield return StartCoroutine(CupToBack(leftCups[i + 1], handMoveDuration * dur[1], handMoveDuration * dur[2]));
                }
                else
                {
                    yield return StartCoroutine(CupToBack(leftCups[i], handMoveDuration * dur[1], handMoveDuration * dur[2]));
                }
            }
            yield return StartCoroutine(ReturnHands());
        }

        void ArrangeHands()
        {
            if (hands[0].RectTransform.anchoredPosition.x > hands[1].RectTransform.anchoredPosition.x)
            {
                var t = hands[0];
                hands[0] = hands[1];
                hands[1] = t;

                var t2 = handOriginalPos[0];
                handOriginalPos[0] = handOriginalPos[1];
                handOriginalPos[1] = t2;
            }
        }
        void ArrangeCups()
        {
            List<CupBehaviour> sortedCups = new List<CupBehaviour>();
            sortedCups.AddRange(cups);
            sortedCups.Sort((x, y) => (int)(x.RectTransform.anchoredPosition.x - y.RectTransform.anchoredPosition.x));

            currentItemIndex = sortedCups.FindIndex(x => x.ID == currentItemIndex);
            for (int i = 0; i < sortedCups.Count; i++)
            {
                cups[i] = sortedCups[i];
                cups[i].Init(this, i);
            }
        }
        IEnumerator ReturnHands(float duration = 1f)
        {
            duration *= config.DurationScale;
            StartCoroutine(MoveHand(hands[0], handOriginalPos[0], duration));
            yield return StartCoroutine(MoveHand(hands[1], handOriginalPos[1], duration));
        }
        void OnCorrect()
        {
            InstantiateParticle(item.RectTransform.position);
            SoundManager.Instance.PlaySFXToggle("Correct", "GameCommon");
        }
        void OnWrong()
        {
            SoundManager.Instance.PlaySFXPitched("Wrong", "Yabawee", 0.05f, 1.2f);
            isWrong = true;
        }
        IEnumerator CupToFront(int cupIndex, float duration1, float duration2)
        {
            yield return StartCoroutine(MoveCup(cupIndex, cups[cupIndex].RectTransform.anchoredPosition + new Vector2(0, 20), duration1));
            yield return StartCoroutine(MoveCup(cupIndex, new Vector2(cups[cupIndex].RectTransform.anchoredPosition.x, itemYPos), duration2));
            cups[cupIndex].SetRelease();
            SoundManager.Instance.PlaySFXPitched("Land", "Yabawee", 0.05f);
        }
        IEnumerator CupToBack(int cupIndex, float duration1, float duration2)
        {
            float x = cups[cupIndex].RectTransform.anchoredPosition.x;
            yield return StartCoroutine(MoveCup(cupIndex, new Vector2(x, cupOriginalPos[cupIndex].y) + new Vector2(0, 20), duration1));
            yield return StartCoroutine(MoveCup(cupIndex, new Vector2(x, cupOriginalPos[cupIndex].y), duration2));
            cups[cupIndex].SetRelease();
            SoundManager.Instance.PlaySFXPitched("Land", "Yabawee", 0.05f);
        }
        IEnumerator GrabCup(Hand hand, int cupIndex, float duration)
        {
            yield return StartCoroutine(MoveHandToCup(hand, cupIndex, duration));
            if(hand.transform.parent != cups[cupIndex].transform)
                SoundManager.Instance.PlaySFXPitched("Grab", "Yabawee", 0.05f);
            cups[cupIndex].SetGrabbed(hand);

        }

        IEnumerator Shuffle(int [] cupIndicies, bool falseShuffle)
        {
            float handMoveDuration = 0.2f *config.DurationScale;
            if(cupIndicies[0] > cupIndicies[1])
            {
                int tem = cupIndicies[0];
                cupIndicies[0] = cupIndicies[1];
                cupIndicies[1] = tem;
            }    
            int handIdx = 0;
            StartCoroutine(GrabCup(hands[handIdx], cupIndicies[0], handMoveDuration));
            StartCoroutine(GrabCup(hands[1-handIdx], cupIndicies[1], handMoveDuration));
            yield return new WaitForSeconds(handMoveDuration);

            SoundManager.Instance.PlaySFXToggle("Shuffle", "Yabawee");

            cups[cupIndicies[0]].transform.SetAsFirstSibling();
            hands[handIdx].transform.SetSiblingIndex(1);
            cups[cupIndicies[1]].transform.SetAsLastSibling();
            hands[1-handIdx].transform.SetAsLastSibling();

            Vector2 cup0pos = cups[cupIndicies[0]].RectTransform.anchoredPosition;
            Vector2 cup1pos = cups[cupIndicies[1]].RectTransform.anchoredPosition;
            Vector2 center = (cup0pos + cup1pos) / 2;
            StartCoroutine(MoveCup(cupIndicies[0], center + new Vector2(0, config.YMovement), config.ShuffleDuration/ 2 * config.DurationScale));
            StartCoroutine(MoveCup(cupIndicies[1], center - new Vector2(0, config.YMovement), config.ShuffleDuration/ 2 * config.DurationScale));
            yield return new WaitForSeconds(config.ShuffleDuration * 0.5f * config.DurationScale);

            if (falseShuffle)
            {
                StartCoroutine(MoveCup(cupIndicies[0], cup0pos, config.ShuffleDuration / 2 * config.DurationScale));
                StartCoroutine(MoveCup(cupIndicies[1], cup1pos, config.ShuffleDuration / 2 * config.DurationScale));
            }
            else
            {
                StartCoroutine(MoveCup(cupIndicies[0], cup1pos, config.ShuffleDuration / 2 * config.DurationScale));
                StartCoroutine(MoveCup(cupIndicies[1], cup0pos, config.ShuffleDuration / 2 * config.DurationScale));
            }
            yield return new WaitForSeconds(config.ShuffleDuration * 0.5f * config.DurationScale);

            cups[cupIndicies[0]].SetRelease();
            cups[cupIndicies[1]].SetRelease();

            hands[0].transform.SetAsLastSibling();
            hands[1].transform.SetAsLastSibling();

        }
        int GetCloseHandIndex(CupBehaviour cup)
        {
            float dist1 = Vector2.Distance(cup.RectTransform.anchoredPosition, hands[0].RectTransform.anchoredPosition);
            float dist2 = Vector2.Distance(cup.RectTransform.anchoredPosition, hands[1].RectTransform.anchoredPosition);
            if (dist1 < dist2)
                return 0;
            else
                return 1;
        }
        IEnumerator MoveCup(int cupIndex, Vector2 cupTargetPosition, float duration)
        {
            float eTime = 0f;
            Vector2 originalPosition = cups[cupIndex].RectTransform.anchoredPosition;
            while (eTime < duration)
            {
                cups[cupIndex].RectTransform.anchoredPosition = Vector2.Lerp(originalPosition, cupTargetPosition, eTime / duration);
                yield return null;
                eTime += Time.deltaTime;
            }
            cups[cupIndex].RectTransform.anchoredPosition = cupTargetPosition;

        }
        IEnumerator MoveHandToCup(Hand hand, int cupIndex, float duration)
        {
            float eTime = 0f;
            Vector2 originalPosition = hand.RectTransform.anchoredPosition;
            Vector2 targetPosition = cups[cupIndex].HandPosition;
            while (eTime < duration)
            {
                hand.RectTransform.anchoredPosition = Vector2.Lerp(originalPosition, targetPosition, eTime / duration);
                yield return null;
                eTime += Time.deltaTime;
            }
            hand.RectTransform.anchoredPosition = targetPosition;
        }
        IEnumerator MoveHand(Hand hand, Vector2 targetPosition, float duration)
        {
            float eTime = 0f;
            Vector2 originalPosition = hand.RectTransform.anchoredPosition;
            while (eTime < duration)
            {
                hand.RectTransform.anchoredPosition = Vector2.Lerp(originalPosition, targetPosition, eTime / duration);
                yield return null;
                eTime += Time.deltaTime;
            }
            hand.RectTransform.anchoredPosition = targetPosition;
        }
    }

}