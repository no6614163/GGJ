using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIParticleBody : MonoBehaviour
{
   AnimationCurve alphaCurve;
    AnimationCurve scaleCurve;
    float initialScale;
   float gravity;
   float duration;
    float elapsedTime = 0f;

    Vector2 velocity;
    Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    public void Init(Sprite sprite, Vector2 velocity, Vector2 position, float initialScale, float duration, float gravity, AnimationCurve alphaCurve, AnimationCurve scaleCurve)
    {
        image.sprite = sprite;
        this.velocity = velocity;
        image.rectTransform.anchoredPosition = position;
        image.rectTransform.localScale = new Vector3(initialScale, initialScale, 1);
        this.initialScale = initialScale;
        this.duration = duration;
        this.gravity = gravity;
        this.alphaCurve = alphaCurve;
        this.scaleCurve = scaleCurve;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime > duration)
        {
            Destroy(gameObject);
            return;
        }
        velocity += new Vector2(0, -gravity) * Time.deltaTime;
        image.rectTransform.anchoredPosition += velocity * Time.deltaTime;
        float xScale = 1;
        if (velocity.x < 0)
            xScale = -1;

        image.rectTransform.localScale = scaleCurve.Evaluate(elapsedTime / duration) * new Vector3(xScale*initialScale, initialScale, 1);


        float a = alphaCurve.Evaluate(elapsedTime / duration);
        image.color = new Color(image.color.r, image.color.g, image.color.b, a);
        Vector2 norm = velocity.normalized;
        float rotZ = Mathf.Atan2(norm.y, norm.x)* Mathf.Rad2Deg + 90;
        image.rectTransform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
