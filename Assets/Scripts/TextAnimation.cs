using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textMeshPro;

    public string[] stringArray; 
    [SerializeField] float timeBtwnChars; 
    [SerializeField] float timeBtwnWords; 

    [SerializeField] GameObject nextButton; 
    private int currentStringIndex = 0; 

    void Start()
    {
        nextButton.SetActive(false); 
        DisplayNextString();
    }

    public void DisplayNextString()
    {
        if (currentStringIndex < stringArray.Length)
        {
            _textMeshPro.text = stringArray[currentStringIndex];
            StartCoroutine(RevealText());
        }
        else
        {
            nextButton.SetActive(true);
        }
    }

    private IEnumerator RevealText()
    {
        _textMeshPro.ForceMeshUpdate();
        int totalVisibleCharacters = _textMeshPro.textInfo.characterCount;
        int counter = 0;

        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            _textMeshPro.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters)
            {
                currentStringIndex++; 
                yield return new WaitForSeconds(timeBtwnWords); 
                DisplayNextString();
                break;
            }

            counter++;
            yield return new WaitForSeconds(timeBtwnChars); 
        }
    }

    public void OnNextButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

