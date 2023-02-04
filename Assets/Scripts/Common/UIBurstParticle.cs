using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBurstParticle : MonoBehaviour
{
    [SerializeField] bool testMode = true;
    [SerializeField] int emmisionCount;
    [SerializeField] float emmisionRadius;
    [SerializeField] Vector2 shootAngleRange;
    [SerializeField] Vector2 shootPowerRange;
    [SerializeField] Vector2 initialScaleRange;
    [SerializeField] AnimationCurve alphaCurve;
    [SerializeField] AnimationCurve scaleCurve;
    [SerializeField] float gravity;
    [SerializeField] float duration;
    [SerializeField] Sprite sprite;

    List<UIParticleBody> particles = new List<UIParticleBody>();
    RectTransform rectTransform;
    public RectTransform RectTransform { get { return rectTransform; } }
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        for(int i=0; i<emmisionCount; i++)
        {
            GameObject ob = Instantiate(Resources.Load("Prefabs/Common/ParticleBase"), transform) as GameObject;
            UIParticleBody particle = ob.GetComponent<UIParticleBody>();
            float shootAngle = Random.Range(shootAngleRange.x, shootAngleRange.y);
            float shootPower = Random.Range(shootPowerRange.x, shootPowerRange.y);
            float initialScale = Random.Range(initialScaleRange.x, initialScaleRange.y);

            Vector2 velocity = AngleToVector(shootAngle) * shootPower;
            Vector2 position = rectTransform.anchoredPosition + AngleToVector(shootAngle) * emmisionRadius;

            particle.Init(sprite, velocity, position, initialScale, duration, gravity, alphaCurve, scaleCurve);
            particles.Add(particle);
        }
    }

    private void Update()
    {
        for(int i=0; i<particles.Count; i++)
        {
            if (particles[i] == null)
            {
                particles.RemoveAt(i);
                i--;
            }
        }
        if (particles.Count == 0)
        {
            if (testMode)
            {
                particles.Clear();
                gameObject.SetActive(false);
            }
            else
                Destroy(gameObject);

        }
    }

    static Vector2 AngleToVector(float degrees)
    {

        return new Vector2(Mathf.Cos(degrees * Mathf.Deg2Rad), Mathf.Sin(degrees * Mathf.Deg2Rad));
    }
}
