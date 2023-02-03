using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yabawee
{
    public class LevelManager : MonoBehaviour
    {
        int currentItemIndex;
        float cupYPos;
        float itemYPos;

        Vector2[] handOriginalPos = new Vector2[2];

        CupBehaviour[] cups;
        [SerializeField] Hand [] hands;
        [SerializeField] RectTransform referenceCup;
        [SerializeField] RectTransform showStage;
        [SerializeField] Item item;
        [SerializeField] CupBehaviour cupPrefab;

        GameConfig config;
        public GameConfig Config { get { return config; } }


        private void Awake()
        {
            //TODO : config 변경
            //--
            config = GetComponent<GameConfig>();
            InitGame();
        }
        void InitGame()
        {
            cupYPos = referenceCup.anchoredPosition.y;
            itemYPos = item.RectTransform.anchoredPosition.y;
            cups = new CupBehaviour[config.CupCount];
            float leftmostX = -(config.CupCount - 1) * 0.5f * config.CupSpacing;
            for(int i=0; i< config.CupCount; i++)
            {
                CupBehaviour newCup = Instantiate(cupPrefab, showStage);
                cups[i] = newCup;
                float x = leftmostX + i * config.CupSpacing;
                newCup.RectTransform.anchoredPosition = new Vector2(x, cupYPos);
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
            StartCoroutine(RoundCoroutine(0));
        }
        IEnumerator RoundCoroutine(int round)
        {
            float handMoveDuration = 1f;
            float[] dur = new float[] { 0.3f, 0.4f, 0.3f };
            for(int i=0; i<config.CupCount; i+=2)
            {
                //컵쪽으로 손 감
                StartCoroutine(GrabCup(hands[0], i, handMoveDuration * dur[0]));
                if (i+1 < config.CupCount)
                    StartCoroutine(GrabCup(hands[1], i+1, handMoveDuration * dur[0]));
                yield return new WaitForSeconds(handMoveDuration * dur[0]);
                //컵 앞으로 놓음
                StartCoroutine(CupToFront(i, handMoveDuration * dur[1], handMoveDuration * dur[2]));
                if (i + 1 < config.CupCount)
                    StartCoroutine(CupToFront(i+1, handMoveDuration * dur[1], handMoveDuration * dur[2]));
                yield return new WaitForSeconds(handMoveDuration * (1-dur[0]));
            }
            item.gameObject.SetActive(false);
            //섞기
            int[] indicies = new int[config.CupCount];
            for (int i = 0; i < config.CupCount; i++)
                indicies[i] = i;
            for (int i=0; i<config.ShufflePerRound[round]; i++)
            {
                int[] shuffleTargets = HappyUtils.Random.RandomElements(indicies, 2);
                if (currentItemIndex == shuffleTargets[0])
                    currentItemIndex = shuffleTargets[0];
                else if (currentItemIndex == shuffleTargets[1])
                    currentItemIndex = shuffleTargets[1];
                yield return StartCoroutine(Shuffle(new int[]{ shuffleTargets[0], shuffleTargets[1]}));
                if (i < config.ShufflePerRound[round] - 1)
                    yield return new WaitForSeconds(config.ShuffleInterval);
            }
            item.gameObject.SetActive(true);
            //손 원래 자리로
            StartCoroutine(MoveHand(hands[0], handOriginalPos[0], handMoveDuration));
            StartCoroutine(MoveHand(hands[1], handOriginalPos[1], handMoveDuration));
        }
        IEnumerator CupToFront(int cupIndex, float duration1, float duration2)
        {
            yield return StartCoroutine(MoveCup(cupIndex, cups[cupIndex].RectTransform.anchoredPosition + new Vector2(0,20),duration1));
            yield return StartCoroutine(MoveCup(cupIndex, new Vector2(cups[cupIndex].RectTransform.anchoredPosition.x, itemYPos), duration2));
            cups[cupIndex].SetRelease();
        }
        IEnumerator GrabCup(Hand hand, int cupIndex, float duration)
        {
            yield return StartCoroutine(MoveHandToCup(hand, cupIndex, duration));
            cups[cupIndex].SetGrabbed(hand);
        }

        IEnumerator Shuffle(int [] cupIndicies)
        {
            float handMoveDuration = 0.2f;

            int handIdx = GetCloseHandIndex(cups[cupIndicies[0]]);
            StartCoroutine(GrabCup(hands[handIdx], cupIndicies[0], handMoveDuration));
            StartCoroutine(GrabCup(hands[1-handIdx], cupIndicies[1], handMoveDuration));
            yield return new WaitForSeconds(handMoveDuration);

            cups[cupIndicies[0]].transform.SetAsFirstSibling();
            cups[cupIndicies[1]].transform.SetAsLastSibling();

            Vector2 cup0pos = cups[cupIndicies[0]].RectTransform.anchoredPosition;
            Vector2 cup1pos = cups[cupIndicies[1]].RectTransform.anchoredPosition;
            Vector2 center = (cup0pos + cup1pos) / 2;
            StartCoroutine(MoveCup(cupIndicies[0], center + new Vector2(0, config.YMovement), config.ShuffleDuration/2));
            StartCoroutine(MoveCup(cupIndicies[1], center - new Vector2(0, config.YMovement), config.ShuffleDuration/2));
            yield return new WaitForSeconds(config.ShuffleDuration * 0.5f);

            StartCoroutine(MoveCup(cupIndicies[0], cup1pos, config.ShuffleDuration/2));
            StartCoroutine(MoveCup(cupIndicies[1], cup0pos, config.ShuffleDuration/2));
            yield return new WaitForSeconds(config.ShuffleDuration * 0.5f);

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