using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleUnit : MonoBehaviour {
    [SerializeField] bool isPlayerUnit;

    public Bokemon Bokemon { get; set; }

    Image image;
    Vector3 originalPosition;
    Color originalColor;

    public void Awake() {
        image = GetComponent<Image>();
        // get local position of the image, which is the position relative to the canvas
        originalPosition = image.transform.localPosition;
        originalColor = image.color;
    }

    // set up the image of the bokemon units
    public void Setup(Bokemon bokemon) {
        Bokemon = bokemon;
        if (isPlayerUnit) {
            image.sprite = Bokemon.Base.BackSprite;
        } else {
            image.sprite = Bokemon.Base.FrontSprite;
        }

        // need to reset the alpha value after fainting, at the start of the battle
        image.color = originalColor;
        PlaEnterAnimation();
    }

    public void PlaEnterAnimation() {
        if (isPlayerUnit) {
            image.transform.localPosition = new Vector3(-500f, originalPosition.y);
        } else {
            image.transform.localPosition = new Vector3(500f, originalPosition.y);
        }

        // move the image to the original position, and time taken is 1 second
        image.transform.DOLocalMoveX(originalPosition.x, 1f);
    }

    public void PlayAttackAnimation() {
        // sequence is used to play multiple animations in a row
        var sequence = DOTween.Sequence();
        if (isPlayerUnit) {
            sequence.Append(image.transform.DOLocalMoveX(originalPosition.x + 50f, 0.25f));
        } else {
            sequence.Append(image.transform.DOLocalMoveX(originalPosition.x - 50f, 0.25f));
        }

        sequence.Append(image.transform.DOLocalMoveX(originalPosition.x, 0.25f));
    }

    public void PlayHitAnimation() {
        var sequence = DOTween.Sequence();
        sequence.Append(image.DOColor(Color.red, 0.1f));
        sequence.Append(image.DOColor(originalColor, 0.1f));
    }

    public void PlayFaintAnimation() {
        var sequence = DOTween.Sequence();
        sequence.Append(image.transform.DOLocalMoveY(originalPosition.y - 100f, 0.5f));
        // use join because append will wait for the previous animation to finish
        sequence.Join(image.DOFade(0f, 0.5f));
    }
}