using UnityEngine;
using System.Collections;

namespace ACR.TimeOfDayFree
{

	// TOD- Manager.
	[ExecuteInEditMode]
	[AddComponentMenu("ACR/Time Of Day Free/Time Of Day Manager")]
	public class TimeOfDayManager : MonoBehaviour 
	{



#region Resources

		// Material.
		//---------------------------------------------------------

		// Autoassign sky material?.
		[SerializeField] private bool m_AutoAssignSky = true;

		// Sky material.
		public Material skyMaterial = null;
		//_________________________________________________________


		// Sun and moon.
		//---------------------------------------------------------

		// Sun and moon light.
		[SerializeField] private Light m_DirectionalLight = null;

		private Transform m_DirectionalLightTransform = null;

		// Sun transform.
		[SerializeField] private Transform m_SunTransform = null;

		// Moon transform.
		[SerializeField] private Transform m_MoonTransform = null;

		// Moon texture.
		public Texture2D moonTexture = null;
		//_________________________________________________________

		// Stars.
		//---------------------------------------------------------
		// Stars cubemap.
		public  Cubemap starsCubemap = null;

		// Stars noise cubemap.
		public  Cubemap starsNoiseCubemap = null;
		//_________________________________________________________

#endregion

#region World And Time

		public bool playTime;
		//-------------------------------------------------------------------------------------

		// Longitude.
		//--------------------------------------------------------------------------------------
		[SerializeField]
		[Range(-180f,180f)] private float m_WorldLongitude = 25f;

		public bool  useWorldLongitudeCurve = false;

		public AnimationCurve worldLongitudeCurve = AnimationCurve.Linear(0, 0f, 1f, 360f);

		public float WorldLongitude
		{ 
			get{ return m_WorldLongitude;  } 
			set{ m_WorldLongitude = value; }
		}
		//--------------------------------------------------------------------------------------

		public Vector3 WorldRotation{ get; private set; }

		// Day in seconds.
		public float dayInSeconds = 60f;

		// Start time.
		//public float startTime = 7;

		// Current time.
		public float currentTime   = 7f; 

		// Day duration in the earth.
		private const float k_DayDuration = 24f;

		// Time references.
		public float  Hour{ get; private set; }
		public float  Minute{ get; private set; }
		public string TimeString { get; private set; }
		//----------------------------------------------------------------------------------------

		// Used to evaluate the curves and gradients.
		public float CGTime{get{return (currentTime / k_DayDuration);}}

		// Const degrees.
		private const int k_RightAngle = 90;
		private const int k_270deg     = 270; 
		//----------------------------------------------------------------------------------------

#endregion

#region Sun

		public bool useSunColorGradient = true;

		[SerializeField]
		private Color m_SunColor = new Color (1f, .851f, .722f, 1f);

		public Gradient sunColorGradient = new Gradient()
		{
			colorKeys = new GradientColorKey[]
			{
				new GradientColorKey(new Color(1f, .639f, .482f, 1f), .25f),
				new GradientColorKey(new Color(1f, .725f, .482f, 1f), .30f),
				new GradientColorKey(new Color(1f, .851f, .722f, 1f), .50f),
				new GradientColorKey(new Color(1f, .725f, .482f, 1f), .70f),
				new GradientColorKey(new Color(1f, .639f, .482f, 1f), .75f)
			},
			alphaKeys = new GradientAlphaKey[] 
			{
				new GradientAlphaKey(1f, 0f),
				new GradientAlphaKey(1f, 1f)
			}
		};

		// Get sun color.
		public Color SunColor
		{
			get{ return m_SunColor;  }
			set{ m_SunColor = value; }
		}
		//---------------------------------------------------------------------------------------

		public bool useSunSizeCurve = false;

		[SerializeField]
		[Range(0,1f)] private float m_SunSize = .07f;

		public AnimationCurve sunSizeCurve = AnimationCurve.Linear(0, .07f, 1f, .07f);

		// Get sun size.
		public float SunSize
		{
			get{ return m_SunSize;  }
			set{ m_SunSize = value; }
		}
		//----------------------------------------------------------------------------------------

		public bool useSunLightIntensityCurve = true; 

		[SerializeField]
		[Range(0,8f)] private float m_SunLightIntensity = 0f;

		public AnimationCurve sunLightIntensityCurve = new AnimationCurve()
		{
			keys = new Keyframe[]
			{
				new Keyframe(  0f, 0f), 
				new Keyframe(.25f, 0f), 
				new Keyframe(.30f, 1f), 
				new Keyframe(.70f, 1f), 
				new Keyframe(.75f, 0f), 
				new Keyframe(  1f, 0f) 
			}

		};

		// Get sun light intensity.
		public float SunLightIntensity
		{
			get{ return m_SunLightIntensity;  }
			set{ m_SunLightIntensity = value; }
		} 
		//----------------------------------------------------------------------------------------

		// Sun direction.
		public Vector3 SunDirection{ get{ return -m_SunTransform.forward; } }

		// Sun light state.
		public bool SunLightEnable{ get; set; }

#endregion

#region Atmosphere.

		public bool useSkyTintGradient = false;

		[SerializeField]
		private Color m_SkyTint = new Color(.5f, .5f, .5f, 1f); 

		public Gradient skyTintGradient = new Gradient()
		{
			colorKeys = new GradientColorKey[]
			{
				new GradientColorKey(new Color(.5f, .5f, .5f, 1f), .25f),
				new GradientColorKey(new Color(.5f, .5f, .5f, 1f), .30f),
				new GradientColorKey(new Color(.5f, .5f, .5f, 1f), .70f),
				new GradientColorKey(new Color(.5f, .5f, .5f, 1f), .75f)
			},
			alphaKeys = new GradientAlphaKey[] 
			{
				new GradientAlphaKey(1f, 0f),
				new GradientAlphaKey(1f, 1f)
			}
		};

		// Get sky tint.
		public Color SkyTint
		{
			get{ return m_SkyTint;  }
			set{ m_SkyTint = value; }
		}
		//----------------------------------------------------------------------------------------

		public bool useAtmosphereThicknessCurve = false;

		[SerializeField]
		[Range(0,5f)]private float m_AtmosphereThickness = 1;

		public AnimationCurve atmosphereThicknessCurve = AnimationCurve.Linear(0, 1f, 1f, 1f); 

		// Get atmosphere thickness.
		public float AtmosphereThickness
		{
			get{ return m_AtmosphereThickness;  }
			set{ m_AtmosphereThickness = value; }
		}
		//----------------------------------------------------------------------------------------


		// Ground skybox color.
		public Color groundColor = new Color(.412f, .384f, .365f, 1f); 
		//----------------------------------------------------------------------------------------

		// Night color.
		public bool useNightColor = true;

		public bool useNightColorGradient = false;

		[SerializeField]
		private Color m_NightColor = new Color(.0f, .009f, .176f, 1f);

		public Gradient nightColorGradient = new Gradient ()
		{
			colorKeys = new GradientColorKey[]
			{
				new GradientColorKey(new Color(.0f, .009f, .176f, 1f), .20f),
				new GradientColorKey(new Color( 0f,    0f,    0f, 1f), .30f),
				new GradientColorKey(new Color( 0f,    0f,    0f, 1f), .70f),
				new GradientColorKey(new Color(.0f, .009f, .176f, 1f), .80f)
			},
			alphaKeys = new GradientAlphaKey[] 
			{
				new GradientAlphaKey(1f, 0f),
				new GradientAlphaKey(1f, 1f)
			}
		};

		// Get night horizon color.
		public Color NightColor
		{
			get{ return m_NightColor;  }
			set{ m_NightColor = value; }
		}
		//----------------------------------------------------------------------------------------

		public bool useHorizonFade = true;

		[SerializeField]
		[Range(.03f,2f)]private float m_HorizonFade = 1.5f;
		public  bool useHorizonFadeCurve = false;
		public  AnimationCurve horizonFadeCurve =  AnimationCurve.Linear(0, 1.5f, 0, 1.5f);

		public float HorizonFade
		{
			get{ return m_HorizonFade;  }  
			set{ m_HorizonFade = value; } 
		}


#endregion

#region Moon

		public bool useMoon = true; 

		public enum MoonRotationMode{Automatic, Custom}
		public MoonRotationMode moonRotationMode = MoonRotationMode.Automatic;
		//---------------------------------------------------------------------------------------

		[Range(-180f,180f)]
		[SerializeField]
		private float m_MoonLongitude = 0;

		public bool useMoonLongitudeCurve  = false;

		public AnimationCurve moonLongitudeCurve =  AnimationCurve.Linear (0, 1f, 1f, 1f); 

		public float MoonLongitude
		{
			get { return m_MoonLongitude; } 
			set { m_MoonLongitude = value;} 
		}
		//---------------------------------------------------------------------------------------

		[Range(-180f,180f)]
		[SerializeField]
		protected float m_MoonLatitude = 0;

		public bool useMoonLatitudeCurve  = false;

		public AnimationCurve moonLatitudeCurve =  AnimationCurve.Linear (0, 1f, 1f, 360f); 

		public float MoonLatitude
		{
			get { return m_MoonLatitude; } 
			set { m_MoonLatitude = value;} 
		}
		//---------------------------------------------------------------------------------------


		public bool useMoonLightColorGradient;

		[SerializeField]
		private Color m_MoonLightColor = new Color (.345f, .459f, .533f, 1f);

		public Gradient moonLightColorGradient = new Gradient()
		{
			colorKeys = new GradientColorKey[]
			{
				new GradientColorKey(new Color(.345f, .459f, .533f, 1f), 0f),
				new GradientColorKey(new Color(.345f, .459f, .533f, 1f), 1f)
			},
			alphaKeys = new GradientAlphaKey[] 
			{
				new GradientAlphaKey(1f, 0f),
				new GradientAlphaKey(1f, 1f)
			}
		};

		// Get moon light color.
		public Color MoonLightColor
		{
			get{ return m_MoonLightColor;  }
			set{ m_MoonLightColor = value; }
		}
		//---------------------------------------------------------------------------------------

		public bool useMoonLightIntensityCurve = true;

		[SerializeField]
		[Range(0,5f)]protected float m_MoonLightIntensity = .2f;

		public AnimationCurve moonLightIntensityCurve = new AnimationCurve()
		{
			keys = new Keyframe[]
			{
				new Keyframe(  0f, .2f), new Keyframe(.20f, .2f), 
				new Keyframe(.22f,  0f), new Keyframe(.77f,  0f), 
				new Keyframe(.80f, .2f), new Keyframe( 1f, .2f) 
			}
		}; 

		// Get moon intensity .
		public float MoonLightIntensity 
		{ 
			get{ return m_MoonLightIntensity;  } 
			set{ m_MoonLightIntensity = value; } 
		}
		//---------------------------------------------------------------------------------------

		public bool useMoonColorGradient = false;

		[SerializeField]
		protected Color m_MoonColor = Color.white;

		public Gradient moonColorGradient = new Gradient()
		{
			colorKeys = new GradientColorKey[]
			{
				new GradientColorKey(new Color(1f, 1f, 1f, 1f), 0f),
				new GradientColorKey(new Color(1f, 1f, 1f, 1f), 1f)
			},
			alphaKeys = new GradientAlphaKey[] 
			{
				new GradientAlphaKey(1f, 0f),
				new GradientAlphaKey(1f, 1f)
			}
		};


		// Get moon color .
		public Color MoonColor
		{
			get{ return m_MoonColor;  }
			set{ m_MoonColor = value; }
		}
		//---------------------------------------------------------------------------------------


		public bool useMoonIntensityCurve = true;

		[SerializeField]
		[Range(0,5f)] protected float m_MoonIntensity = 1;

		public AnimationCurve moonIntensityCurve = new AnimationCurve()
		{
			keys = new Keyframe[]
			{
				new Keyframe(  0f, 1f),  new Keyframe(.15f, 1f), 
				new Keyframe(.30f, 0f),  new Keyframe(.70f, 0f), 
				new Keyframe(.85f, 1f),  new Keyframe(  1f, 1f), 
			}

		}; 

		// Get moon intensity .
		public float MoonIntensity 
		{ 
			get{ return m_MoonIntensity;  } 
			set{ m_MoonIntensity = value; } 
		}
		//---------------------------------------------------------------------------------------

		public bool useMoonSizeCurve = false;

		[SerializeField]
		[Range(0,1f)]protected float m_MoonSize = .096f;

		public AnimationCurve moonSizeCurve =  AnimationCurve.Linear (0, .096f, 1f, .096f); 

		// Get moon color .
		public float MoonSize
		{
			get{ return m_MoonSize;  }
			set{ m_MoonSize = value; }
		}
		//---------------------------------------------------------------------------------------


		public bool useMoonHalo = true;

		public bool useMoonHaloColorGradient;

		[SerializeField]
		protected Color m_MoonHaloColor = new Color (.005f, 0f, .134f, 1f);

		public Gradient moonHaloColorGradient = new Gradient()
		{
			colorKeys = new GradientColorKey[]
			{
				new GradientColorKey(new Color(.005f, 0f, .134f, 1f), .20f),
				new GradientColorKey(new Color(.005f, 0f, .134f, 1f), .80f)
			},
			alphaKeys = new GradientAlphaKey[] 
			{
				new GradientAlphaKey(1f, .20f),
				new GradientAlphaKey(1f, .70f)
			}
		};

		// Get moon halo color.
		public Color MoonHaloColor
		{ 
			get{ return m_MoonHaloColor;  } 
			set{ m_MoonHaloColor = value; } 
		}
		//---------------------------------------------------------------------------------------

		public bool useMoonHaloSizeCurve;

		[SerializeField]
		[Range(0, 10f)]private float m_MoonHaloSize = 3f;

		public AnimationCurve moonHaloSizeCurve  =  AnimationCurve.Linear (0, 3f, 1f, 3f);

		// Get moon halo size.
		public float MoonHaloSize
		{ 
			get{ return m_MoonHaloSize;  } 
			set{ m_MoonHaloSize = value; } 
		}
		//---------------------------------------------------------------------------------------

		public bool useMoonHaloIntensityCurve = true;

		[SerializeField]
		[Range(0, 5f)]private float m_MoonHaloIntensity = 1f;

		public AnimationCurve moonHaloIntensityCurve = new AnimationCurve()
		{
			keys = new Keyframe[]
			{
				new Keyframe(  0f, 1f),  new Keyframe(.15f, 1f), 
				new Keyframe(.25f, 0f),  new Keyframe(.75f, 0f), 
				new Keyframe(.85f, 1f),  new Keyframe(  1f, 1f), 
			}

		};  

		// Get moon halo intensity.
		public float MoonHaloIntensity
		{ 
			get{ return m_MoonHaloIntensity;  } 
			set{ m_MoonHaloIntensity = value; } 
		}
		//---------------------------------------------------------------------------------------

		// Moon direction.
		public Vector3 MoonDirection{get{return -m_MoonTransform.forward;}}

		// Moon ligth state.
		public bool MoonLightEnable{ get; set; }
		//---------------------------------------------------------------------------------------

#endregion

#region Stars

		public bool useStars = true;

		public enum StarsRotationMode{Automatic, Static}
		public StarsRotationMode starsRotationMode = StarsRotationMode.Automatic;
		//--------------------------------------------------------------------------------------

		// Outer space offsets.
		public Vector3 starsOffsets = Vector3.zero;
		//--------------------------------------------------------------------------------------

		public bool useStarsColorGradient;

		[SerializeField]
		private Color m_StarsColor = Color.white;

		public Gradient starsColorGradient = new Gradient()
		{
			colorKeys = new GradientColorKey[]
			{
				new GradientColorKey(new Color(1f, 1f, 1f, 1f), .20f),
				new GradientColorKey(new Color(1f, 1f, 1f, 1f), .80f)
			},
			alphaKeys = new GradientAlphaKey[] 
			{
				new GradientAlphaKey(1f, 0f),
				new GradientAlphaKey(1f, 1f)
			}
		};

		// Get stars Color.
		public Color StarsColor
		{
			get{ return m_StarsColor;  } 
			set{ m_StarsColor = value; } 
		}
		//--------------------------------------------------------------------------------------

		public bool useStarsIntensityCurve = true;

		[SerializeField]
		[Range(0,5f)] private float m_StarsIntensity = 1;

		public AnimationCurve starsIntensityCurve = new AnimationCurve()
		{
			keys = new Keyframe[]
			{
				new Keyframe(  0f, 1f),  new Keyframe(.20f, 1f), 
				new Keyframe(.25f, 0f),  new Keyframe(.75f, 0f), 
				new Keyframe(.80f, 1f),  new Keyframe(  1f, 1f), 
			}
		};  

		public float StarsIntensity
		{
			get{ return m_StarsIntensity;  } 
			set{ m_StarsIntensity = value; } 
		}
		//--------------------------------------------------------------------------------------

		public bool useStarsTwinkle = true;

		public bool useStarsTwinkleCurve;

		[SerializeField]
		[Range(0,1f)]
		protected float m_StarsTwinkle = .5f;

		public AnimationCurve starsTwinkleCurve = AnimationCurve.Linear (0, .5f, 1f, .5f); 

		public float StarsTwinkle
		{
			get{ return m_StarsTwinkle;  } 
			set{ m_StarsTwinkle = value; } 
		}

		public bool useStarsTwinkleSpeedCurve;

		[SerializeField]
		[Range(0,10f)]
		protected float m_StarsTwinkleSpeed = 7;

		public AnimationCurve starsTwinkleSpeedCurve =  AnimationCurve.Linear (0, 71f, 1f, 7f);

		public float StarsTwinkleSpeed
		{
			get{ return m_StarsTwinkleSpeed;  } 
			set{ m_StarsTwinkleSpeed = value; } 
		}

		private float starsTwinkleSpeed;
		//--------------------------------------------------------------------------------------

#endregion

#region Ambient

		// Ambient Source.
		private enum AmbientMode{Color, Gradient, Skybox}

		[SerializeField]
		private AmbientMode m_AmbientMode = AmbientMode.Gradient;
		//----------------------------------------------------------------------------------

		// Gradient Ambient.
		public bool useAmbientSkyColorGradient = true;

		[SerializeField]
		private Color m_AmbientSkyColor = new Color(.463f, .576f, .769f, 1f);

		public Gradient ambientSkyColorGradient = new Gradient () 
		{
			colorKeys = new GradientColorKey[]
			{
				new GradientColorKey(new Color(   0f, .008f, .149f, 1f), .22f),
				new GradientColorKey(new Color(.435f, .494f, .498f, 1f), .25f),
				new GradientColorKey(new Color(.463f, .576f, .769f, 1f), .30f),
				new GradientColorKey(new Color(.463f, .576f, .769f, 1f), .70f),
				new GradientColorKey(new Color(.435f, .494f, .498f, 1f), .75f),
				new GradientColorKey(new Color(   0f, .008f, .149f, 1f), .78f)
			},
			alphaKeys = new GradientAlphaKey[] 
			{
				new GradientAlphaKey(1f, .20f),
				new GradientAlphaKey(1f, .70f)
			}
		};

		public Color AmbientSkyColor
		{
			get{ return m_AmbientSkyColor;  } 
			set{ m_AmbientSkyColor = value; } 
		}
		//----------------------------------------------------------------------------------

		public bool useAmbientEquatorColorGradient = true;

		[SerializeField]
		protected Color m_AmbientEquatorColor = new Color(.698f, .843f, 1f, 1f);

		public Gradient ambientEquatorColorGradient = new Gradient ()
		{
			colorKeys = new GradientColorKey[]
			{
				new GradientColorKey(new Color(   0f, .008f, .188f, 1f), .22f),
				new GradientColorKey(new Color(.859f, .780f, .561f, 1f), .25f),
				new GradientColorKey(new Color(.698f, .843f,    1f, 1f), .30f),
				new GradientColorKey(new Color(.698f, .843f,    1f, 1f), .70f),
				new GradientColorKey(new Color(.859f, .780f, .561f, 1f), .75f),
				new GradientColorKey(new Color(   0f, .008f, .188f, 1f), .78f)
			},
			alphaKeys = new GradientAlphaKey[] 
			{
				new GradientAlphaKey(1f, 0f),
				new GradientAlphaKey(1f, 1f)
			}
		};

		public Color AmbientEquatorColor
		{
			get{ return m_AmbientEquatorColor;  } 
			set{ m_AmbientEquatorColor = value; } 
		}
		//----------------------------------------------------------------------------------

		public bool useAmbientGroundColorGradient = true;

		[SerializeField]
		protected Color m_AmbientGroundColor = new Color (.467f, .435f, .416f, 1f);

		public Gradient ambientGroundColorGradient  = new Gradient ()
		{
			colorKeys = new GradientColorKey[]
			{
				new GradientColorKey(new Color(0f, 0f, 0f, 1f), .22f),
				new GradientColorKey(new Color(.227f, .157f, .102f, 1f), .25f),
				new GradientColorKey(new Color(.467f, .435f, .416f, 1f), .30f),
				new GradientColorKey(new Color(.467f, .435f, .416f, 1f), .70f),
				new GradientColorKey(new Color(.227f, .157f, .102f, 1f), .75f),
				new GradientColorKey(new Color(0f, 0f, 0f, 1f), .78f)
			},
			alphaKeys = new GradientAlphaKey[] 
			{
				new GradientAlphaKey(1f, 0f),
				new GradientAlphaKey(1f, 1f)
			}
		};

		public Color AmbientGroundColor
		{
			get{ return m_AmbientGroundColor;  } 
			set{ m_AmbientGroundColor = value; } 
		}
		//----------------------------------------------------------------------------------

		public bool useAmbientIntensityCurve = false;

		[SerializeField]
		[Range(0,8f)] protected float m_AmbientIntensity = 1f;

		public AnimationCurve ambientIntensityCurve = AnimationCurve.Linear (0, 1f, 1f, 1f);  

		public float AmbientIntensity
		{
			get{ return m_AmbientIntensity;  } 
			set{ m_AmbientIntensity = value; } 
		}

		//----------------------------------------------------------------------------------

#endregion

#region Fog

		private enum FogType{RenderSettings, EvaluateOnly, Off}
		[SerializeField]private FogType fogType = FogType.RenderSettings;
		//--------------------------------------------------------------------------------------

		public FogMode fogMode = FogMode.ExponentialSquared;
		//--------------------------------------------------------------------------------------

		public bool useRenderSettingsFog = false;

		public bool useFogDensityCurve;

		[SerializeField]
		[Range(0,1f)]protected float m_FogDensity = 0.001f;

		public AnimationCurve fogDensityCurve = AnimationCurve.Linear(0, 0.0016f, 1f, 0.0016f);  

		public float FogDensity
		{
			get{ return m_FogDensity;  }  
			set{ m_FogDensity = value; } 
		}
		//--------------------------------------------------------------------------------------

		public bool useFogStartDistanceCurve;

		[SerializeField]
		private float m_FogStartDistance = 0f;

		public AnimationCurve fogStartDistanceCurve = AnimationCurve.Linear(0, 0f, 1f, 0f);  

		public float FogStartDistance
		{
			get{ return m_FogStartDistance;  }  
			set{ m_FogStartDistance = value; } 
		}
		//--------------------------------------------------------------------------------------

		public bool useFogEndDistanceCurve;

		[SerializeField]
		private float m_FogEndDistance = 300f;

		public AnimationCurve fogEndDistanceCurve = AnimationCurve.Linear(0, 300f, 1f, 300f);  

		public float FogEndDistance
		{
			get{ return m_FogEndDistance;  }  
			set{ m_FogEndDistance = value; } 
		}
		//--------------------------------------------------------------------------------------

		public bool useFogColorGradient = true;

		[SerializeField]
		private Color m_FogColor = new Color(.576f, .706f, .878f, 1f); 

		public Gradient fogColorGradient  = new Gradient ()
		{
			colorKeys = new GradientColorKey[]
			{
				new GradientColorKey(new Color(   0f, .008f, .188f, 1f), .22f),
				new GradientColorKey(new Color(.682f, .655f, .584f, 1f), .25f),
				new GradientColorKey(new Color(.576f, .706f, .878f, 1f), .30f),
				new GradientColorKey(new Color(.576f, .706f, .878f, 1f), .70f),
				new GradientColorKey(new Color(.682f, .655f, .584f, 1f), .75f),
				new GradientColorKey(new Color(   0f, .008f, .188f, 1f), .78f)
			},
			alphaKeys = new GradientAlphaKey[] 
			{
				new GradientAlphaKey(1f, 0f),
				new GradientAlphaKey(1f, 1f)
			}
		};

		public Color FogColor
		{
			get{ return m_FogColor;  }  
			set{ m_FogColor = value; } 
		}
		//--------------------------------------------------------------------------------------

#endregion

#region Other Settings

		//--------------------------------------------------------------------------------------
		[SerializeField]private float m_Exposure = 1.3f;
		public  bool useExposureCurve = false;
		public  AnimationCurve exposureCurve = AnimationCurve.Linear(0, 1, 0, 1);

		public float Exposure
		{
			get{ return m_Exposure;  }  
			set{ m_Exposure = value; } 
		}
		//---------------------------------------------------------------------------------------
#endregion






		void Start()
		{

			if (Application.isPlaying)
			{

				if (m_AutoAssignSky)
					RenderSettings.skybox = skyMaterial;

				InitComponents();
			}
		}


		void InitComponents()
		{
			if(m_DirectionalLight != null)
				m_DirectionalLightTransform  = m_DirectionalLight.transform;
		}


		void GetTimeString()
		{
			string h   = Hour   < 10 ? "0" + Hour.ToString()   : Hour.ToString();
			string m   = Minute < 10 ? "0" + Minute.ToString() : Minute.ToString();
			TimeString = h + ":" + m;
		}

		void GetWorldRotation()
		{

			//----------------------------------------------------------------------------------
			if(useWorldLongitudeCurve)	
				WorldLongitude = worldLongitudeCurve.Evaluate(CGTime) - k_RightAngle;

			//WorldRotation.y = worldLatitudeCurve.Evaluate (CGTime) - k_RightAngle;

			WorldRotation = new Vector3 () 
			{
				x =	currentTime * (360 / k_DayDuration) - k_RightAngle,
				y = WorldLongitude,
				z = 0
			};
			//___________________________________________________________________________________
		}

		void Update()
		{


			if (skyMaterial == null || m_SunTransform == null)
				return;

#if UNITY_EDITOR
			if (!Application.isPlaying)
			{

				if (m_AutoAssignSky)
					RenderSettings.skybox = skyMaterial;

				InitComponents (); 

				currentTime = Mathf.Clamp (currentTime, 0 - .0001f, k_DayDuration + .0001f);
			}
#endif


			#region Time.

			// Time.
			//--------------------------------------------------------------------------------
			// Prevent the current time exceeds the day duration.
			if (currentTime > k_DayDuration) 
				currentTime = 0; 

			if (currentTime < 0)
				currentTime = k_DayDuration; 
			//--------------------------------------------------------------------------------


			// Play time.
			if (playTime && Application.isPlaying) 
			{

				currentTime  += (Time.deltaTime / dayInSeconds) * k_DayDuration;
			}
			//---------------------------------------------------------------------------------

			Hour   = Mathf.Floor (currentTime);
			Minute = Mathf.Floor ((currentTime - Hour)*60);

			GetTimeString();

			if (currentTime <= 5.50f || currentTime >= 18.50f)
				SunLightEnable = false;
			else
				SunLightEnable = true;


			if (!SunLightEnable)
				MoonLightEnable = true; 
			else
				MoonLightEnable = false; 

			//__________________________________________________________________________________
			#endregion

			GetWorldRotation ();

			#region Sun.

			// Sun.
			//----------------------------------------------------------------------------------
			m_SunTransform.localEulerAngles = new Vector3(WorldRotation.x, WorldRotation.y, 0);
		

			// Evaluate sun properties.
			if (useSunSizeCurve) 
				SunSize = sunSizeCurve.Evaluate(CGTime);


			// Sun light intensity.
			if (useSunLightIntensityCurve)
				SunLightIntensity = sunLightIntensityCurve.Evaluate(CGTime);

			// Sun Color.
			if (useSunColorGradient) 
				SunColor = sunColorGradient.Evaluate(CGTime);

			// SkyTint.
			if (useSkyTintGradient) 
				SkyTint = skyTintGradient.Evaluate(CGTime);
			//---------------------------------------------------------------------------------

			// Set Sun Properties.
			skyMaterial.SetFloat ("_SunSize",  SunSize);
			skyMaterial.SetColor ("_SunColor", SunColor);
			skyMaterial.SetVector("_SunDir",   SunDirection);
			//_________________________________________________________________________________
			#endregion

			#region Atmosphere

			// Atmosphere thickness.
			if (useAtmosphereThicknessCurve) 
				AtmosphereThickness = atmosphereThicknessCurve.Evaluate(CGTime);



			skyMaterial.SetColor("_SkyTint", SkyTint);
			skyMaterial.SetFloat("_AtmosphereThickness", AtmosphereThickness);
			skyMaterial.SetColor("_GroundColor", groundColor);

			if (useNightColor) 
			{

				if (useNightColorGradient)
					NightColor = nightColorGradient.Evaluate(CGTime);

				skyMaterial.EnableKeyword("NIGHTCOLOR");
				skyMaterial.SetColor("_NightColor", NightColor); 
			}
			else skyMaterial.DisableKeyword("NIGHTCOLOR");

			if(useHorizonFade)
			{
				skyMaterial.EnableKeyword ("HORIZONFADE"); 

				if (useHorizonFadeCurve)
					HorizonFade = horizonFadeCurve.Evaluate (CGTime);

				skyMaterial.SetFloat("_HorizonFade", HorizonFade);

			}
			else skyMaterial.DisableKeyword ("HORIZONFADE"); 

			//____________________________________________________________________________________
			#endregion


			#region Moon
			if (useMoon) 
			{


				if (m_MoonTransform != null) 
				{

					// Moon light color.
					if (useMoonLightColorGradient)
						MoonLightColor = moonLightColorGradient.Evaluate(CGTime);

					// Moon light intensity.
					if (useMoonLightIntensityCurve)
						MoonLightIntensity = moonLightIntensityCurve.Evaluate(CGTime); 

					// Moon color.
					if (useMoonColorGradient)
						MoonColor = moonColorGradient.Evaluate(CGTime);

					// Moon Size.
					if(useMoonSizeCurve)
						MoonSize = moonSizeCurve.Evaluate(CGTime);

					// Moon Intensity.
					if (useMoonIntensityCurve)
						MoonIntensity = moonIntensityCurve.Evaluate(CGTime); 

					// Moon Halo Color.
					if (useMoonHaloColorGradient)
						MoonHaloColor = moonHaloColorGradient.Evaluate(CGTime);


					// Moon Halo Size.
					if (useMoonHaloSizeCurve)
						MoonHaloSize = moonHaloSizeCurve.Evaluate(CGTime);

					// Moon Halo Intensity.
					if (useMoonHaloIntensityCurve)
						MoonHaloIntensity = moonHaloIntensityCurve.Evaluate(CGTime);

					//===============================================================================


					if (useMoonLongitudeCurve) 
						MoonLongitude = moonLongitudeCurve.Evaluate (CGTime);

					if (useMoonLatitudeCurve) 
						MoonLatitude  = moonLatitudeCurve.Evaluate (CGTime)-270;



					switch (moonRotationMode)
					{

					case MoonRotationMode.Automatic:

						m_MoonTransform.parent        = m_SunTransform;
						m_MoonTransform.localRotation = Quaternion.Euler(0, 180f, -180f);

						//m_MoonTransform.forward =  m_SunTransform.InverseTransformDirection(Vector3.forward);

						m_MoonTransform.localScale    = new Vector3(-1, 1, 1);
						 
					break;

					case MoonRotationMode.Custom:

						m_MoonTransform.localEulerAngles = new Vector3(MoonLatitude, MoonLongitude, 0f);
						m_MoonTransform.localScale       = new Vector3(-1, 1, 1);
						m_MoonTransform.parent           = this.transform;

					break;

					}


					skyMaterial.EnableKeyword("MOON");

					skyMaterial.SetVector("_MoonDir", MoonDirection); 
					if (moonTexture != null)
					{
						skyMaterial.SetMatrix  ("_MoonMatrix",    m_MoonTransform.worldToLocalMatrix);
						skyMaterial.SetTexture ("_MoonTexture",   moonTexture);
						skyMaterial.SetFloat   ("_MoonSize",      MoonSize);
						skyMaterial.SetColor   ("_MoonColor",     MoonColor);
						skyMaterial.SetFloat   ("_MoonIntensity", MoonIntensity);
					} 
					else 
					{
						skyMaterial.DisableKeyword("MOON");
					}

					if (useMoonHalo) 
					{
						skyMaterial.EnableKeyword("MOONHALO");
						skyMaterial.SetColor("_MoonHaloColor", MoonHaloColor); 
						skyMaterial.SetFloat("_MoonHaloSize", MoonHaloSize); 
						skyMaterial.SetFloat("_MoonHaloIntensity", MoonHaloIntensity); 


					} 
					else
						skyMaterial.DisableKeyword("MOONHALO");

				}
				else 
				{
					//m_MoonLight.enabled = false;
					skyMaterial.DisableKeyword("MOON");
					skyMaterial.DisableKeyword("MOONHALO");

				}

			}
			else 
			{
				skyMaterial.DisableKeyword("MOON");
				skyMaterial.DisableKeyword("MOONHALO");

			}

			#endregion


			#region Stars
			if ((starsCubemap != null && starsNoiseCubemap != null) && useStars)
			{

				// Used to rotate stars. 
				Matrix4x4 sunMatrix = starsRotationMode == StarsRotationMode.Automatic ? m_SunTransform.worldToLocalMatrix : Matrix4x4.identity; 

				Matrix4x4 starsMatrix = Matrix4x4.TRS (Vector3.zero, Quaternion.Euler(starsOffsets), Vector3.one); 

				skyMaterial.SetMatrix("_SunMatrix", sunMatrix);
				skyMaterial.SetMatrix("_StarsMatrix", starsMatrix);


				// Stars color.
				if (useStarsColorGradient)
					StarsColor = starsColorGradient.Evaluate (CGTime); 

				// Stars color.
				if (useStarsIntensityCurve)
					StarsIntensity = starsIntensityCurve.Evaluate (CGTime); 


				// Stars twinkle.
				if (useStarsTwinkleCurve)
					StarsTwinkle = starsTwinkleCurve.Evaluate (CGTime); 

				// Stars twinkle speed curve.
				if (useStarsTwinkleSpeedCurve)
					StarsTwinkleSpeed = starsTwinkleSpeedCurve.Evaluate (CGTime);


				skyMaterial.EnableKeyword ("STARS"); 


				if(useStarsTwinkle)
					skyMaterial.EnableKeyword("STARSTWINKLE");
				else
					skyMaterial.DisableKeyword("STARSTWINKLE");


				starsTwinkleSpeed += Time.deltaTime * StarsTwinkleSpeed;
				Matrix4x4 starsNoiseMatrix = Matrix4x4.TRS (Vector3.zero, Quaternion.Euler (starsTwinkleSpeed, 0, 0), Vector3.one);

				skyMaterial.SetTexture("_StarsCubemap", starsCubemap);
				skyMaterial.SetTexture("_StarsNoiseCubemap", starsNoiseCubemap);
				skyMaterial.SetMatrix("_StarsNoiseMatrix", starsNoiseMatrix);
				skyMaterial.SetColor("_StarsColor", StarsColor);
				skyMaterial.SetFloat("_StarsIntensity", StarsIntensity);
				skyMaterial.SetFloat("_StarsTwinkle", StarsTwinkle);
			}
			else 
			{
				skyMaterial.DisableKeyword("STARS");
				skyMaterial.DisableKeyword("STARSTWINKLE");
			}


			#endregion


			#region Other Settings
			// Exposure.
			if (useExposureCurve)
				Exposure = exposureCurve.Evaluate (CGTime);

			skyMaterial.SetFloat("_Exposure", Exposure);
			#endregion

			if(m_DirectionalLight != null)
				DirLightRot();

			UpdateAmbientColor();
			UpdateFog ();

		}


		void DirLightRot()
		{

			if (SunLightEnable) 
			{
				m_DirectionalLightTransform.parent           = this.transform;
				m_DirectionalLightTransform.localEulerAngles = m_SunTransform.localEulerAngles;
				m_DirectionalLight.intensity = SunLightIntensity;
				m_DirectionalLight.color = SunColor;

			} 
			else if (useMoon) 
			{

				switch (moonRotationMode)
				{

					case MoonRotationMode.Automatic:

					m_DirectionalLightTransform.parent        = m_SunTransform;
					m_DirectionalLightTransform.localRotation = Quaternion.Euler(0, 180f, -180f);

					break;

					case MoonRotationMode.Custom:

					m_DirectionalLightTransform.localEulerAngles = m_MoonTransform.localEulerAngles;
					m_DirectionalLightTransform.parent           = this.transform;
					break;

				}


				m_DirectionalLight.intensity =  MoonLightIntensity;
				m_DirectionalLight.color     =  MoonLightColor;
			}
		}


		void UpdateAmbientColor()
		{

			// Ambient Sky Color.
			if (useAmbientSkyColorGradient)
				AmbientSkyColor = ambientSkyColorGradient.Evaluate(CGTime);

			// Ambient Equator Color
			if (useAmbientEquatorColorGradient)
				AmbientEquatorColor = ambientEquatorColorGradient.Evaluate(CGTime);

			// Ambient Ground Color
			if (useAmbientGroundColorGradient)
				AmbientGroundColor = ambientGroundColorGradient.Evaluate(CGTime);

			// Ambient Intensity
			if (useAmbientIntensityCurve)
				AmbientIntensity = ambientIntensityCurve.Evaluate(CGTime);
			//======================================================================================

			switch (m_AmbientMode) 
			{

			case AmbientMode.Skybox:

				RenderSettings.ambientMode      = UnityEngine.Rendering.AmbientMode.Skybox;
				RenderSettings.ambientIntensity = AmbientIntensity;

			break;

			case AmbientMode.Color: 

				RenderSettings.ambientMode     = UnityEngine.Rendering.AmbientMode.Flat;
				RenderSettings.ambientSkyColor = AmbientSkyColor;

			break;

			case AmbientMode.Gradient:

				RenderSettings.ambientMode         = UnityEngine.Rendering.AmbientMode.Trilight;
				RenderSettings.ambientSkyColor     =  AmbientSkyColor; 
				RenderSettings.ambientEquatorColor =  AmbientEquatorColor;
				RenderSettings.ambientGroundColor  =  AmbientGroundColor;

			break;

			}
		}


	    void UpdateFog()
		{

			if (fogType == FogType.Off && fogType != FogType.EvaluateOnly)
				return;

			//======================================================================================

			// Fog Density
			if (useFogDensityCurve)
				FogDensity = fogDensityCurve.Evaluate(CGTime);

			// Fog Start Distance
			if (useFogStartDistanceCurve)
				FogStartDistance = fogStartDistanceCurve.Evaluate(CGTime);

			// Fog End Distance
			if (useFogEndDistanceCurve)
				FogEndDistance = fogEndDistanceCurve.Evaluate(CGTime);

			// Fog Color.
			if (useFogColorGradient)
				FogColor= fogColorGradient.Evaluate(CGTime);
			//======================================================================================


			if (fogType == FogType.RenderSettings) 
			{

				RenderSettings.fog      = useRenderSettingsFog;
				RenderSettings.fogMode  = fogMode;
				RenderSettings.fogColor = FogColor;

				switch (fogMode) 
				{

				case FogMode.Exponential:
					RenderSettings.fogDensity = FogDensity;
				break;

				case FogMode.ExponentialSquared:
					RenderSettings.fogDensity = FogDensity;
				break;

				case FogMode.Linear:
					RenderSettings.fogStartDistance = FogStartDistance;
					RenderSettings.fogEndDistance = FogEndDistance;
				break;

				}
			}
		}



	}




}
