using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    private int hitPoint = 50;
    private float widthHalf = 0;
    private float heightHalf = 0;
    private bool isActive = true;
    private TextMeshProUGUI hitPointText = null;


    public void Initialize()
    {
        widthHalf = transform.localScale.x / 2;
        heightHalf = transform.localScale.y / 2;
    }


    void Update()
    {

    }

    public bool CheckBallHit(Ball ballObject)
    {
        Vector2 ballPosition = ballObject.transform.position;
        if (ballPosition.x + ballObject.GetRadius() < transform.position.x - widthHalf) return false;
        if (ballPosition.x - ballObject.GetRadius() > transform.position.x + widthHalf) return false;
        if (ballPosition.y + ballObject.GetRadius() < transform.position.y - heightHalf) return false;
        if (ballPosition.y - ballObject.GetRadius() > transform.position.y + heightHalf) return false;

        return true;
    }

    public void Hit()
    {
        hitPoint--;
        hitPointText.text = hitPoint.ToString();
        if (hitPoint > 0) return;
        isActive = false;
        gameObject.SetActive(isActive);
        hitPointText.gameObject.SetActive(isActive);
    }

    public void SetHitPoint(int newHitPoint)
    {
        hitPoint = newHitPoint;
    }

    public void SetIsActive(bool newIsActive)
    {
        isActive = newIsActive;
    }

    public bool GetIsActive()
    {
        return isActive;
    }

    public Vector2 GetSideHalf()
    {
        return new Vector2(widthHalf, heightHalf);
    }

    public void SetTextMeshPro(TextMeshProUGUI newTextMeshPro)
    {
        hitPointText = newTextMeshPro;
        hitPointText.text = hitPoint.ToString();
        hitPointText.transform.position = transform.position;
    }

    public void SetHitPointTextPosition(Vector2 position)
    {
        hitPointText.transform.position = position;
    }
}
