using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CardMatch
{
    public class LevelManager : LevelManagerBase
    {
        GameConfig config;
        public GameConfig Config { get { return config; } }
        [SerializeField] RectTransform cardParent;
        [SerializeField] CardBehaviour cardPrefab;

        List<CardBehaviour> cards = new List<CardBehaviour>();

        CardBehaviour lastCardClicked = null;

        int wrongCount = 0;
        int matchedCards = 0;


        protected override void Awake()
        {
            base.Awake();
            config = GetComponent<GameConfig>();
            //TODO : config 변경
            //--
            InitGame();
        }
        private void Start()
        {
            StartCoroutine(GameStartCoroutine());
        }
        IEnumerator GameStartCoroutine()
        {
            for(int i=0; i<cards.Count; i++)
            {
                StartCoroutine(InitialShowCard(cards[i]));
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(config.InitialShowingTime);

            //TODO : Start!!
            StartTimer();
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].Clickable = true;
            }
        }
        IEnumerator InitialShowCard(CardBehaviour card)
        {
            card.SetDesiredState(true);
            yield return new WaitForSeconds(config.InitialShowingTime);
            card.SetDesiredState(false);
        }
        protected override void InitGame()
        {
            List<Sprite> cardImages = new List<Sprite>();
            cardImages.AddRange(HappyUtils.Random.RandomElements(config.CardImages, config.ColCount * config.rowCount / 2));
            cardImages.AddRange(cardImages);
            HappyUtils.Random.Shuffle(cardImages);
            for(int i=0; i<cardImages.Count; i++)
            {
                CardBehaviour newCard = Instantiate(cardPrefab, cardParent.transform);
                newCard.Clickable = false;
                newCard.Init(cardImages[i],this);
                cards.Add(newCard);
            }
        }
        public void OnCardClicked(CardBehaviour card)
        {
            card.SetDesiredState(true);
            if (lastCardClicked == null)
            {
                lastCardClicked = card;
                lastCardClicked.Clickable = false;
            }
            else
            {
                lastCardClicked.Clickable = true;
                if (lastCardClicked.Sprite == card.Sprite)
                {
                    OnCorrect(lastCardClicked, card);
                }
                else
                {
                    OnWrong(lastCardClicked, card);
                }
                lastCardClicked = null;
            }
        }
        void OnWrong(CardBehaviour card1, CardBehaviour card2)
        {
            //TODO : 삐빅효과
            card1.SetDesiredState(false);
            card2.SetDesiredState(false);
            wrongCount += 1;
        }
        void OnCorrect(CardBehaviour card1, CardBehaviour card2)
        {
            //TODO : 정답 효과, 점수추가
            card1.Clickable = false;
            card2.Clickable = false;
            matchedCards += 2;
            if(matchedCards == cards.Count)
            {
                OnGameClear();
            }
        }
        public override void OnTimerEnd()
        {
            Debug.Log("제한시간 초과로 패배!");
            UI_Manager.Instance.ShowPopupUI<UI_FailedPopup>();
        }
        void OnGameClear()
        {
            StopTimer();
            Debug.Log("게임 클리어!");
            UI_Manager.Instance.ShowPopupUI<UI_ClearPopup>();
        }
    }
}
