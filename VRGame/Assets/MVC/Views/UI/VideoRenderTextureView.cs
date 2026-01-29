using UnityEngine;
using UnityEngine.Video;

namespace Unity.VRTemplate
{
    /// <summary>
    /// View component that creates a RenderTexture for rendering video to a target renderer
    /// Renamed from VideoPlayerRenderTexture.cs to clarify its role as a View in MVC
    /// </summary>
    [RequireComponent(typeof(VideoPlayer))]
    public class VideoRenderTextureView : MonoBehaviour
    {
        const string k_ShaderName = "Unlit/Texture";

        [SerializeField, Tooltip("The target Renderer which will display the video.")]
        Renderer m_Renderer;

        [SerializeField, Tooltip("The width of the RenderTexture which will be created.")]
        int m_RenderTextureWidth = 1920;

        [SerializeField, Tooltip("The height of the RenderTexture which will be created.")]
        int m_RenderTextureHeight = 1080;

        [SerializeField, Tooltip("The bit depth of the depth channel for the RenderTexture which will be created.")]
        int m_RenderTextureDepth;

        VideoPlayer m_VideoPlayer;
        RenderTexture m_RenderTexture;
        Material m_Material;

        void Start()
        {
            m_VideoPlayer = GetComponent<VideoPlayer>();
            SetupRenderTexture();
        }

        void OnDestroy()
        {
            CleanupRenderTexture();
        }

        /// <summary>
        /// Setup the render texture and apply to video player and renderer
        /// </summary>
        void SetupRenderTexture()
        {
            m_RenderTexture = new RenderTexture(m_RenderTextureWidth, m_RenderTextureHeight, m_RenderTextureDepth);
            m_RenderTexture.Create();

            m_Material = new Material(Shader.Find(k_ShaderName));
            m_Material.mainTexture = m_RenderTexture;

            if (m_VideoPlayer != null)
            {
                m_VideoPlayer.targetTexture = m_RenderTexture;
            }

            if (m_Renderer != null)
            {
                m_Renderer.material = m_Material;
            }
        }

        /// <summary>
        /// Cleanup render texture resources
        /// </summary>
        void CleanupRenderTexture()
        {
            if (m_RenderTexture != null)
            {
                m_RenderTexture.Release();
                Destroy(m_RenderTexture);
            }

            if (m_Material != null)
            {
                Destroy(m_Material);
            }
        }

        /// <summary>
        /// Update the render texture dimensions
        /// </summary>
        public void UpdateRenderTextureDimensions(int width, int height)
        {
            m_RenderTextureWidth = width;
            m_RenderTextureHeight = height;
            
            CleanupRenderTexture();
            SetupRenderTexture();
        }
    }
}
