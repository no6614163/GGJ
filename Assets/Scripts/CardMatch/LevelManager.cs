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
        [SerializeField] RectTransform canvas;
        [SerializeField] RectTransform cardParent;
        [SerializeField] CardBehaviour cardPrefab;
        [SerializeField] UIBurstParticle particlePrefab;

        List<CardBehaviour> cards = new List<CardBehaviour>();

        CardBehaviour lastCardClicked = null;

        int wrongCount = 0;
        int matchedCards = 0;

        public void InstantiateParticle(Vector2 pos)
        {
            var particle = Instantiate(particlePrefab, canvas.transform);
            particle.RectTransform.position = pos;
        }

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
            SoundManager.Instance.PlaySFXPitched("Wrong", "GameCommon", 0.05f, 1.2f);
            card1.SetDesiredState(false);
            card2.SetDesiredState(false);
            card1.OnWrong();
            card2.OnWrong();
            wrongCount += 1;
        }
        void OnCorrect(CardBehaviour card1, CardBehaviour card2)
        {
            //TODO : 정답 효과, 점수추가
            SoundManager.Instance.PlaySFXToggle("Correct", "GameCommon");
            InstantiateParticle(card1.RectTransform.position);
            InstantiateParticle(card2.RectTransform.position);
            card1.Clickable = false;
            card2.Clickable = false;
            card1.OnCorrect();
            card2.OnCorrect();
            matchedCards += 2;
            if(matchedCards == cards.Count)
            {
                OnGameClear();
            }
        }
        public override void OnTimerEnd()
        {
            StartCoroutine(EndCoroutine(false));
            SoundManager.Instance.PlaySFX("Wrong", "CardMatch");
        }
        void OnGameClear()
        {
            StopTimer();

            StartCoroutine(EndCoroutine(true));
        }
        IEnumerator EndCoroutine(bool isWin)
        {
            //틀린 직후
            for (int i = 0; i < cards.Count; i++)
                cards[i].Clickable = false;
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < cards.Count; i++)
            {
                if (isWin)
                    cards[i].OnCorrect();
                else
                    cards[i].OnWrong();
            }

            yield return new WaitForSeconds(1f);
            if (isWin)
                ShowResult(true);
            else
                ShowResult(false);
        }
    }
}
