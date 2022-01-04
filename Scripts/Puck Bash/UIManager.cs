using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace AirHockey
{
    using Transitions;
    namespace Managers
    {
        public class UIManager : MonoBehaviour
        {
            [SerializeField] private GameObject[] scores;
            [SerializeField] private TextMeshProUGUI timer;
            [SerializeField] private GameObject pauseMenu;
            [SerializeField] private Image panel;
            [SerializeField] private float flashSpeed;

            [SerializeField] private float freezeFrameTimer;
            [Header("CountDown")] [SerializeField] private GameObject counterPrefab;
            public static UIManager Instance;
            private static float appliedFreezeTimer;
            private static bool isFlashing;
            private GameObject canvas;

            [SerializeField] private GameObject winnerPanel;
            [SerializeField] private GameObject winnerPanelBlue;
            [SerializeField] private GameObject winnerPanelRed;

            [SerializeField] public GameObject menuBody;

            public float waitingSeconds = 2f;

            


            void Start()
            {
                if (!Instance)
                    Instance = this;

                appliedFreezeTimer = freezeFrameTimer;
                canvas = GameObject.FindGameObjectWithTag("Canvas");

                StartCoroutine(CountDown());
                isFlashing = false;

            }
            IEnumerator CountDown()
            {
                    
                while ((TransitionScript.Instance != null && TransitionScript.Instance.IsTransitioning) || !AudioManager.Instance)
                {
                    yield return null;
                }

                if (!TransitionScript.Instance.IsTransitioning)
                {
                    Transform counterTransform = Instantiate(counterPrefab, canvas.transform.position, Quaternion.identity).transform;
                    counterTransform.SetParent(canvas.transform);
                    counterTransform.localScale = Vector3.one * 3f;
                    Time.timeScale = 0f;
                    yield return new WaitForSecondsRealtime(AudioManager.SFX[(int)SoundEffects.Counter].clip.length);
                    Time.timeScale = 1f;
                    FindObjectOfType<ArenaManager>().Inititialize();
                    Destroy(counterTransform.gameObject);
                }

            }
            public void UpdateScore(int index, int newScore)
            {
                if (SceneManager.GetActiveScene().name == "Arena fatty Shape Colors AI" ||
                    SceneManager.GetActiveScene().name == "Arena fatty Shape Colors" || SceneManager.GetActiveScene().name == "CoopArena")
                {
                    scores[index].GetComponent<TextMeshProUGUI>().text = newScore.ToString();
                }
                else
                {
                    if (newScore > 0 && newScore < 7)
                    {
                        scores[index].transform.GetChild(newScore).gameObject.SetActive(false);

                    }
                }
                
            }

            public void ShowWinner(Peddle peddle)
            {
                /*                TextMeshProUGUI winnerText = winnerPanel.GetComponentInChildren<TextMeshProUGUI>();
                                winnerText.text = $"{peddle.GetName().Substring(0, 3)} {winnerText.text}";
                                winnerText.color = peddle.GetComponent<SpriteRenderer>().color;*/

                
                if (peddle.GetName().Equals("Blue"))
                {
                    winnerPanelBlue.SetActive(true);
                    winnerPanelBlue.GetComponent<Animator>().Play("New Blue Win") ;

                }
                else
                {
                    winnerPanelRed.SetActive(true);
                    winnerPanelBlue.GetComponent<Animator>().Play("New Red Win");

                }

                
                winnerPanel.SetActive(true);
                StartCoroutine(PreventUserInputWin());



            }
            public void ShowHidePauseMenu(bool showPauseMenu)
            {
                pauseMenu.SetActive(showPauseMenu);
                Time.timeScale = (showPauseMenu) ? 0f : 1f;
            }

            public void ReturnToMainMenu()
            {
                TransitionScript.Instance.StartTransition("MainMenu");
                //SceneManager.LoadScene("MainMenu");
            }

            public void QuitGame()
            {
                Application.Quit();
            }


            public static IEnumerator FreezeTime()
            {
                float clipLength = AudioManager.PlayOneSFX((int)SoundEffects.PerfectTime, 2f);
                Time.timeScale = 0.01f;
                Instance.FlashIt();
                yield return new WaitForSecondsRealtime(clipLength + appliedFreezeTimer);
                Time.timeScale = 1f;
            }

            public void FlashIt()
            {
                StartCoroutine(FlashInOut());
            }
            public IEnumerator FlashInOut()
            {
                yield return Flash(0.75f);
                yield return Flash(0);
            }

            private IEnumerator Flash(float alpha)
            {
                while (panel.color.a != alpha)
                {
                    var newAlpha = Mathf.MoveTowards(panel.color.a, alpha, Time.deltaTime * flashSpeed);
                    panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, newAlpha);
                    yield return null;
                }
            }

            public IEnumerator PreventUserInputWin()
            {
                menuBody.SetActive(false);
                yield return new WaitForSecondsRealtime(waitingSeconds);
                menuBody.SetActive(true);
                Vector2 v2 = new Vector2(0, 0);

            } 


        }



    }
}
