using System.Collections;
using UnityEngine;
using UnityEngine.Video;

namespace Unity.VRTemplate
{
    /// <summary>
    /// Controller for video scrubbing - handles video playback control logic
    /// Extracted from VideoTimeScrubControl.cs to separate controller logic from view
    /// </summary>
    [RequireComponent(typeof(VideoPlayer))]
    public class VideoScrubController : MonoBehaviour
    {
        [SerializeField, Tooltip("The model that stores video player state")]
        VideoPlayerStateModel m_StateModel;

        [SerializeField, Tooltip("The view that handles UI updates")]
        VideoPlayerView m_PlayerView;

        [SerializeField, Tooltip("If checked, the slider will fade off after a few seconds.")]
        bool m_HideSliderAfterFewSeconds;

        VideoPlayer m_VideoPlayer;

        void Start()
        {
            m_VideoPlayer = GetComponent<VideoPlayer>();

            if (m_StateModel == null)
            {
                m_StateModel = new VideoPlayerStateModel();
            }

            SetupVideoPlayer();
            SetupViewListeners();
        }

        void OnEnable()
        {
            if (m_VideoPlayer != null)
            {
                m_VideoPlayer.frame = 0;
                PlayVideo();
            }

            if (m_PlayerView != null && m_PlayerView.Slider != null)
            {
                m_PlayerView.UpdateSliderValue(0.0f);
                m_PlayerView.Slider.onValueChanged.AddListener(OnSliderValueChanged);
            }

            if (m_HideSliderAfterFewSeconds)
                StartCoroutine(HideSliderAfterSeconds());
        }

        void OnDisable()
        {
            if (m_PlayerView != null && m_PlayerView.Slider != null)
            {
                m_PlayerView.Slider.onValueChanged.RemoveListener(OnSliderValueChanged);
            }
        }

        void Update()
        {
            if (m_VideoPlayer == null)
                return;

            // Update state from video player
            m_StateModel.CurrentFrame = m_VideoPlayer.frame;
            m_StateModel.CurrentTime = (float)m_VideoPlayer.time;
            m_StateModel.TotalLength = (float)m_VideoPlayer.length;
            m_StateModel.FrameCount = (long)m_VideoPlayer.frameCount;

            // Check if video jump has completed
            if (m_StateModel.VideoJumpPending)
            {
                if (m_StateModel.LastFrameBeforeScrub == m_VideoPlayer.frame)
                    return;

                m_StateModel.LastFrameBeforeScrub = long.MinValue;
                m_StateModel.VideoJumpPending = false;
            }

            // Update view if not dragging
            if (!m_StateModel.IsDragging && !m_StateModel.VideoJumpPending && m_PlayerView != null)
            {
                var progress = m_StateModel.GetNormalizedProgress();
                m_PlayerView.UpdateSliderValue(progress);
            }
        }

        void SetupVideoPlayer()
        {
            if (!m_VideoPlayer.playOnAwake)
            {
                m_VideoPlayer.playOnAwake = true;
                m_VideoPlayer.Play();
                StopVideo();
            }
            else
            {
                PlayVideo();
            }
        }

        void SetupViewListeners()
        {
            // View will call controller methods via UnityEvents or direct references
        }

        void OnSliderValueChanged(float sliderValue)
        {
            if (m_PlayerView != null)
            {
                m_PlayerView.UpdateTimeText(m_StateModel.GetFormattedTimeDisplay());
            }
        }

        public void OnPointerDown()
        {
            m_StateModel.VideoJumpPending = true;
            StopVideo();
            JumpToSliderPosition();
        }

        public void OnRelease()
        {
            m_StateModel.IsDragging = false;
            PlayVideo();
            JumpToSliderPosition();
        }

        public void OnDrag()
        {
            m_StateModel.IsDragging = true;
            m_StateModel.VideoJumpPending = true;
        }

        void JumpToSliderPosition()
        {
            if (m_PlayerView == null || m_VideoPlayer == null)
                return;

            m_StateModel.VideoJumpPending = true;
            var targetFrame = m_StateModel.CalculateTargetFrame(m_PlayerView.GetSliderValue());
            m_StateModel.LastFrameBeforeScrub = m_VideoPlayer.frame;
            m_VideoPlayer.frame = targetFrame;
        }

        public void PlayOrPauseVideo()
        {
            if (m_StateModel.IsPlaying)
                StopVideo();
            else
                PlayVideo();
        }

        void StopVideo()
        {
            if (m_VideoPlayer == null)
                return;

            m_StateModel.IsPlaying = false;
            m_VideoPlayer.Pause();

            if (m_PlayerView != null)
            {
                m_PlayerView.ShowPlayButton();
            }
        }

        void PlayVideo()
        {
            if (m_VideoPlayer == null)
                return;

            m_StateModel.IsPlaying = true;
            m_VideoPlayer.Play();

            if (m_PlayerView != null)
            {
                m_PlayerView.ShowPauseButton();
            }
        }

        IEnumerator HideSliderAfterSeconds(float duration = 1f)
        {
            yield return new WaitForSeconds(duration);
            
            if (m_PlayerView != null && m_PlayerView.Slider != null)
            {
                m_PlayerView.Slider.gameObject.SetActive(false);
            }
        }
    }
}
