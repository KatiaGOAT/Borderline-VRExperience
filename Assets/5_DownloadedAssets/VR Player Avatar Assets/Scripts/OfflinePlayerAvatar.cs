using Unity.XR.CoreUtils;
using Unity.XR.CoreUtils.Bindings.Variables;
using UnityEngine;
using UnityEngine.Android;

namespace XRMultiplayer
{
    /// <summary>
    /// Represents the offline player avatar.
    /// </summary>
    public class OfflinePlayerAvatar : MonoBehaviour
    {
        public static BindableVariable<float> voiceAmp = new BindableVariable<float>();

        /// <summary>
        /// Gets or sets a value indicating whether the player is muted.
        /// </summary>
        public static bool muted
        {
            get => s_Muted;
            set
            {
                if (Permission.HasUserAuthorizedPermission(Permission.Microphone))
                    s_Muted = value;
            }
        }

        /// <summary>
        /// A value indicating whether the player is muted.
        /// </summary>
        static bool s_Muted;

        /// <summary>
        /// The head transform.
        /// </summary>
        [SerializeField] Transform m_HeadTransform;

        /// <summary>
        /// The head renderer.
        /// </summary>
        [SerializeField] SkinnedMeshRenderer m_HeadRend;

        /// <summary>
        /// The voice amplitude curve.
        /// </summary>
        [SerializeField] AnimationCurve m_VoiceCurve;

        /// <summary>
        /// The head origin.
        /// </summary>
        Transform m_HeadOrigin;

        /// <summary>
        /// The mouth blend smoothing.
        /// </summary>
        [SerializeField] float m_MouthBlendSmoothing = 5.0f;

        /// <summary>
        /// The microphone loudness.
        /// </summary>
        float m_MicLoudness;

        /// <summary>
        /// The microphone device name.
        /// </summary>
        string m_Device;

        /// <summary>
        /// The sample window.
        /// </summary>
        int m_SampleWindow = 128;

        /// <summary>
        /// The clip record.
        /// </summary>
        AudioClip m_ClipRecord;

        /// <summary>
        /// The voice destination volume.
        /// </summary>
        float m_VoiceDestinationVolume;

        bool m_MicInitialized = false;

        /// <inheritdoc/>
        void Start()
        {
            XROrigin rig = FindFirstObjectByType<XROrigin>();
            m_HeadOrigin = rig.Camera.transform;

        }
    }
}
