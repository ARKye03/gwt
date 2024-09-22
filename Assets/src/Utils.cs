using UnityEngine;
using System.Collections;

public partial class Board : MonoBehaviour
{

    private void UpdateCount() => roundCount.text = $"Rounds: {Round}";
    public void IncreaseRound()
    {
        Round++;
        UpdateCount();
    }
    public void ResetRound()
    {
        Round = 0;
        UpdateCount();
    }
    public IEnumerator RotateCamera(float duration)
    {
        yield return new WaitForSeconds(0.2f);
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, 180);
        float elapsedTime = 0;

        // float startSize = mainCamera.orthographicSize;
        // float zoomInSize = startSize - 0.1f; // Zoom in
        // float zoomOutSize = startSize; // Zoom out

        Quaternion startTextRotation = roundCount.transform.rotation;
        Quaternion endTextRotation = startTextRotation * Quaternion.Euler(0, 0, 180);

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            t = t * t * (3f - 2f * t); // Ease-in-out

            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            roundCount.transform.rotation = startTextRotation;
            winnerText.transform.rotation = startTextRotation;
            allyWinsText.transform.rotation = startTextRotation;
            enemyWinsText.transform.rotation = startTextRotation;
            allyPower.transform.rotation = startTextRotation;
            enemyPower.transform.rotation = startTextRotation;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;

        // roundCount.transform.rotation = endTextRotation;
        // winnerText.transform.rotation = endRotation;
        // allyWinsText.transform.rotation = endRotation;
        // enemyWinsText.transform.rotation = endRotation;
        // allyPower.transform.rotation = endRotation;
        // enemyPower.transform.rotation = endRotation;
    }
    public void UpdateHandPanelVisibility()
    {
        CanvasGroup allyHandPanelCanvasGroup = allyPlayer.handPanel.GetComponent<CanvasGroup>();
        CanvasGroup enemyHandPanelCanvasGroup = enemyPlayer.handPanel.GetComponent<CanvasGroup>();

        if (allyPlayerIsPlaying)
        {
            allyHandPanelCanvasGroup.alpha = 1;
            allyHandPanelCanvasGroup.interactable = true;
            allyHandPanelCanvasGroup.blocksRaycasts = true;

            enemyHandPanelCanvasGroup.alpha = 0;
            enemyHandPanelCanvasGroup.interactable = false;
            enemyHandPanelCanvasGroup.blocksRaycasts = false;
        }
        else
        {
            allyHandPanelCanvasGroup.alpha = 0;
            allyHandPanelCanvasGroup.interactable = false;
            allyHandPanelCanvasGroup.blocksRaycasts = false;

            enemyHandPanelCanvasGroup.alpha = 1;
            enemyHandPanelCanvasGroup.interactable = true;
            enemyHandPanelCanvasGroup.blocksRaycasts = true;
        }
    }
}