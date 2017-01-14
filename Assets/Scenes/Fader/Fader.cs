/*******************************************************************************
* �^�C�g��   : �t�F�[�h�p�X�N���v�g
* �t�@�C���� : Fader.cs
* �쐬��     : �u�� ����
* �쐬��     : 2016/07/26
********************************************************************************
* �X�V����	- 2016/07/26
*			-V0.01 Initial Version
*******************************************************************************/

/*******************************************************************************
* �}�N����`
*******************************************************************************/
#if UNITY_4_6 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2
#define UNITY_OLD_SCENE_MANAGEMENT
#endif

/*******************************************************************************
* �X�e�[�g�����g�ݒ�( �������\�[�X�J���@�\ )
*******************************************************************************/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if !UNITY_OLD_SCENE_MANAGEMENT
using UnityEngine.SceneManagement;
#endif

/*******************************************************************************
* �N���X
*******************************************************************************/
public class Fader : MonoBehaviour 
{
	// ----------------------------------------------------------------------
	// [ �񋓌^ : �t�F�[�h�̎�� ]
	// ----------------------------------------------------------------------
	public enum Style
	{
		BlackOut,
		BlackIn,
		WhiteOut,
		WhiteIn,
		BlackOutHalf,
		Clear,
		Fill
	};

	// ----------------------------------------------------------------------
	// [ �萔 : �t�F�[�h�̊�{���� ]
	// ----------------------------------------------------------------------
	const float DefaultFadeTime = 1f;

	// ----------------------------------------------------------------------
	// [ �ϐ��錾 ]
	// ----------------------------------------------------------------------
	public static Fader instance;	// singleton instance
	[SerializeField] Image fadeImg;	// empty image uses for fading
	private float fadeAlpha;
	private string nextSceneName;
	private Vector3 fadeColor;
	private float fadeTime;
	private int fadeDir;
	private bool isPaused;
	private Style fadeStyle;
	private System.Action callback;
	private bool isFading;

	public bool IsFading 
	{
		get
		{
			return isFading; 
		}
		private set
		{
			isFading = value; 
		}
	}

	// ----------------------------------------------------------------------
	// [ ������ ]
	// ----------------------------------------------------------------------
	void Awake () 
	{
		if(instance == null)
		{
			instance = this;
			//DontDestroyOnLoad(gameObject);
			// set the image as tranparent
			Color c = fadeImg.color;
			fadeImg.color = new Color(c.r, c.g, c.b, 0);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	// ----------------------------------------------------------------------
	// [ �X�V���� ]
	// ----------------------------------------------------------------------
	void Update()
	{
		// run this function on fading only
		if (!isFading) return;
		
		// Update fading
		fadeAlpha += Time.deltaTime / fadeTime * fadeDir;
		fadeImg.color = new Color(fadeColor.x, fadeColor.y, fadeColor.z, fadeAlpha);
		
		if (fadeStyle == Style.BlackOutHalf && fadeAlpha > 0.5f)
		{
			isFading = false;
		}
	}

	// ----------------------------------------------------------------------
	// [ �֐� : �t�F�[�h�J�n ]
	// ���� : fade style, fade time
	// ----------------------------------------------------------------------
	public void Begin(Style style, float time)
	{
		Begin(style, time, (string)null);
	}

	// ----------------------------------------------------------------------
	// [ �֐� : �t�F�[�h�J�n�ƃV�[���؂�ւ� ]
	// ���� : fade style, fade time, next scene name
	// ----------------------------------------------------------------------
	public void Begin(Style style, float time, string sceneName)
	{
		// dont start new fade while fading
		if (isFading || isPaused) return;

		// determine if goto next scene or not
		nextSceneName = sceneName;
		callback = null;

		BeginInner(style, time);
	}

	// ----------------------------------------------------------------------
	// [ �֐� : �t�F�[�h�J�n�Ɗ֐��̓ǂݍ��� ]
	// ���� : fade style, fade time, function which will be called when the fade is finished
	// ----------------------------------------------------------------------
	public void Begin(Style style, float time, System.Action action)
	{
		// dont start new fade while fading
		if (isFading || isPaused) return;

		// determine if goto next scene or not
		callback = action;
		nextSceneName = null;

		BeginInner(style, time);
	}

	// ----------------------------------------------------------------------
	// [ �֐� : Fader�N���X�����Ŏg���t�F�[�h�J�n ]
	// ���� : fade style, fade time,
	// ----------------------------------------------------------------------
	void BeginInner(Style style, float time)
	{
		// prevent 0 divide
		if (time < 0.001f) time = 0.001f;

		if (style != Style.BlackOutHalf)
		{
			Invoke("OnFadeFinish", time);
		}

		fadeStyle = style;
		fadeTime = time;
		SetFaderParams(style);
		isFading = true;
	}

	// ----------------------------------------------------------------------
	// [ �֐� : �t�F�[�h���I��������Ɏ��s�����֐� ]
	// ----------------------------------------------------------------------
	void OnFadeFinish()
	{
		isFading = false;
		if (nextSceneName != null)
		{
#if UNITY_OLD_SCENE_MANAGEMENT
			Application.LoadLevel(nextSceneName);
#else
			SceneManager.LoadScene(nextSceneName);
#endif
		}
		else if (callback != null)
		{
			callback();
		}
	}

	// ----------------------------------------------------------------------
	// [ �֐� : �v�����ꂽ�X�^�C���ɑ΂���e��p�����[�^�ݒ� ]
	// ���� : fade style
	// ----------------------------------------------------------------------
	void SetFaderParams(Style style)
	{
		// set fade color
		switch (style)
		{
		case Style.BlackOut:
			{
				fadeColor = new Vector3(0, 0, 0);
				fadeAlpha = 0;
				fadeDir = +1;
				break;
			}
		case Style.BlackIn:
			{
				fadeColor = new Vector3(0, 0, 0);
				fadeAlpha = 1;
				fadeDir = -1;
				break;
			}
		case Style.WhiteOut:
			{
				fadeColor = new Vector3(1, 1, 1);
				fadeAlpha = 0;
				fadeDir = +1;
				break;
			}
		case Style.WhiteIn:
			{
				fadeColor = new Vector3(1, 1, 1);
				fadeAlpha = 1;
				fadeDir = -1;
				break;
			}
		case Style.BlackOutHalf:
			{
				fadeColor = new Vector3(0, 0, 0);
				fadeAlpha = 0;
				fadeDir = +1;
				break;
			}
		case Style.Clear:
			{
				fadeDir = -1;
				fadeTime /= fadeAlpha;
				break;
			}
		case Style.Fill:
			{
				fadeDir = +1;
				fadeTime /= 1 - fadeAlpha;
				break;
			}
		default:	// should not reach here
				break;
		}
	}

	// ----------------------------------------------------------------------
	// [ ���b�p�[�֐� ]
	// ----------------------------------------------------------------------
	public void BlackIn(float time = DefaultFadeTime)    { Begin(Style.BlackIn, time, (string)null); }
	public void BlackIn(string sceneName)                { Begin(Style.BlackIn, DefaultFadeTime, sceneName); }
	public void BlackIn(System.Action action)            { Begin(Style.BlackIn, DefaultFadeTime, action); }	
	public void BlackIn(float time, string sceneName)    { Begin(Style.BlackIn, time, sceneName); }	
	public void BlackIn(float time, System.Action action){ Begin(Style.BlackIn, time, action); }

	public void BlackOut(float time = DefaultFadeTime)    { Begin(Style.BlackOut, time, (string)null); }
	public void BlackOut(string sceneName)                { Begin(Style.BlackOut, DefaultFadeTime, sceneName); }
	public void BlackOut(System.Action action)            { Begin(Style.BlackOut, DefaultFadeTime, action); }	
	public void BlackOut(float time, string sceneName)    { Begin(Style.BlackOut, time, sceneName); }	
	public void BlackOut(float time, System.Action action){ Begin(Style.BlackOut, time, action); }

	public void WhiteIn(float time = DefaultFadeTime)    { Begin(Style.WhiteIn, time, (string)null); }
	public void WhiteIn(string sceneName)                { Begin(Style.WhiteIn, DefaultFadeTime, sceneName); }
	public void WhiteIn(System.Action action)            { Begin(Style.WhiteIn, DefaultFadeTime, action); }	
	public void WhiteIn(float time, string sceneName)    { Begin(Style.WhiteIn, time, sceneName); }	
	public void WhiteIn(float time, System.Action action){ Begin(Style.WhiteIn, time, action); }

	public void WhiteOut(float time = DefaultFadeTime)    { Begin(Style.WhiteOut, time, (string)null); }
	public void WhiteOut(string sceneName)                { Begin(Style.WhiteOut, DefaultFadeTime, sceneName); }
	public void WhiteOut(System.Action action)            { Begin(Style.WhiteOut, DefaultFadeTime, action); }	
	public void WhiteOut(float time, string sceneName)    { Begin(Style.WhiteOut, time, sceneName); }	
	public void WhiteOut(float time, System.Action action){ Begin(Style.WhiteOut, time, action); }

	public void BlackOutHalf(float time = DefaultFadeTime)    { Begin(Style.BlackOutHalf, time, (string)null); }
	public void BlackOutHalf(string sceneName)                { Begin(Style.BlackOutHalf, DefaultFadeTime, sceneName); }
	public void BlackOutHalf(System.Action action)            { Begin(Style.BlackOutHalf, DefaultFadeTime, action); }	
	public void BlackOutHalf(float time, string sceneName)    { Begin(Style.BlackOutHalf, time, sceneName); }	
	public void BlackOutHalf(float time, System.Action action){ Begin(Style.BlackOutHalf, time, action); }

	public void Clear(float time = DefaultFadeTime)    { Begin(Style.Clear, time, (string)null); }
	public void Clear(string sceneName)                { Begin(Style.Clear, DefaultFadeTime, sceneName); }
	public void Clear(System.Action action)            { Begin(Style.Clear, DefaultFadeTime, action); }	
	public void Clear(float time, string sceneName)    { Begin(Style.Clear, time, sceneName); }	
	public void Clear(float time, System.Action action){ Begin(Style.Clear, time, action); }

	public void Fill(float time = DefaultFadeTime)    { Begin(Style.Fill, time, (string)null); }
	public void Fill(string sceneName)                { Begin(Style.Fill, DefaultFadeTime, sceneName); }
	public void Fill(System.Action action)            { Begin(Style.Fill, DefaultFadeTime, action); }	
	public void Fill(float time, string sceneName)    { Begin(Style.Fill, time, sceneName); }	
	public void Fill(float time, System.Action action){ Begin(Style.Fill, time, action); }


	// ----------------------------------------------------------------------
	// [ �t�F�[�h�x�~ ]
	// ----------------------------------------------------------------------
	public void Pause()
	{
		if (isFading)
		{
			isFading = false;
			isPaused = true;
		}
	}

	// ----------------------------------------------------------------------
	// [ �t�F�[�h�ĊJ ]
	// ----------------------------------------------------------------------
	public void Resume()
	{
		if (isPaused)
		{
			isFading = true;
		}
	}
}
