using UnityEngine;
using System.Collections;
using UnityEditor;


namespace ACR.TimeOfDayFree
{
	[CustomEditor(typeof(TimeOfDayManager))] 
	public class TimeOfDayManagerEditor : TimeOfDayEditor
	{


		new SerializedObject serializedObject; 
		TimeOfDayManager timeOfDayManager;
		//____________________________________________


		#region SerializeProperties

		// Resources.
		SerializedProperty autoAssignSky;
		SerializedProperty skyMaterial;

		SerializedProperty directionalLight;
		SerializedProperty sunTransform;
		SerializedProperty moonTransform;

		SerializedProperty moonTexture;
		SerializedProperty starsCubemap;
		SerializedProperty starsNoiseCubemap;
		//_____________________________________________


		// World and time.
		SerializedProperty playTime;
		SerializedProperty worldLongitude;
		SerializedProperty useWorldLongitudeCurve;
		SerializedProperty worldLongitudeCurve;
		SerializedProperty dayInSeconds;
		SerializedProperty currentTime;
		//_____________________________________________


		// Sun.
		SerializedProperty useSunColorGradient;
		SerializedProperty sunColor;
		SerializedProperty sunColorGradient;
		SerializedProperty useSunSizeCurve;
		SerializedProperty sunSize;
		SerializedProperty sunSizeCurve;
		SerializedProperty useSunLightIntensityCurve;
		SerializedProperty sunLightIntensity;
		SerializedProperty sunLightIntensityCurve;
		//_____________________________________________

		// Atmosphere
		SerializedProperty useSkyTintGradient;
		SerializedProperty skyTint;
		SerializedProperty skyTintGradient;

		SerializedProperty useAtmosphereThicknessCurve;
		SerializedProperty atmosphereThickness;
		SerializedProperty atmosphereThicknessCurve;

		SerializedProperty groundColor;

		SerializedProperty useNightColor;
		SerializedProperty useNightColorGradient;
		SerializedProperty nightColor;
		SerializedProperty nightColorGradient;

		SerializedProperty useHorizonFade;
		SerializedProperty horizonFade;
		SerializedProperty useHorizonFadeCurve;
		SerializedProperty horizonFadeCurve;
		//_______________________________________________

		// Moon.
		SerializedProperty useMoon;

		SerializedProperty moonRotationMode;
		SerializedProperty moonLongitude;
		SerializedProperty moonLatitude;
		SerializedProperty useMoonLongitudeCurve;
		SerializedProperty moonLongitudeCurve;
		SerializedProperty useMoonLatitudeCurve;
		SerializedProperty moonLatitudeCurve;

		SerializedProperty useMoonLightColorGradient;
		SerializedProperty moonLightColor;
		SerializedProperty moonLightColorGradient;
		SerializedProperty useMoonLightIntensityCurve;
		SerializedProperty moonLightIntensity;
		SerializedProperty moonLightIntensityCurve;

		SerializedProperty useMoonColorGradient;
		SerializedProperty moonColor;
		SerializedProperty moonColorGradient;

		SerializedProperty useMoonIntensityCurve;
		SerializedProperty moonIntensity;
		SerializedProperty moonIntensityCurve;

		SerializedProperty useMoonSizeCurve;
		SerializedProperty moonSize;
		SerializedProperty moonSizeCurve;

		//------------------------------------------------
		SerializedProperty useMoonHalo;
		SerializedProperty useMoonHaloColorGradient;
		SerializedProperty moonHaloColor;
		SerializedProperty moonHaloColorGradient;

		SerializedProperty useMoonHaloSizeCurve;
		SerializedProperty moonHaloSize;
		SerializedProperty moonHaloSizeCurve;

		SerializedProperty useMoonHaloIntensityCurve;
		SerializedProperty moonHaloIntensity;
		SerializedProperty moonHaloIntensityCurve;
		//_________________________________________________

		// Stars.
		SerializedProperty useStars;
		SerializedProperty starsRotationMode;
		SerializedProperty starsOffsets;
		SerializedProperty useStarsColorGradient;
		SerializedProperty starsColor;
		SerializedProperty starsColorGradient;
		SerializedProperty useStarsIntensityCurve;

		SerializedProperty starsIntensity;
		SerializedProperty starsIntensityCurve;


		SerializedProperty useStarsTwinkle;
		SerializedProperty useStarsTwinkleCurve;
		SerializedProperty starsTwinkle;
		SerializedProperty starsTwinkleCurve;
		SerializedProperty useStarsTwinkleSpeedCurve;
		SerializedProperty starsTwinkleSpeed;
		SerializedProperty starsTwinkleSpeedCurve;
		//_________________________________________________


		// Ambient.
		SerializedProperty ambientMode;
		SerializedProperty useAmbientSkyColorGradient;
		SerializedProperty ambientSkyColor;
		SerializedProperty ambientSkyColorGradient;
		SerializedProperty useAmbientEquatorColorGradient;
		SerializedProperty ambientEquatorColor;
		SerializedProperty ambientEquatorColorGradient;

		SerializedProperty useAmbientGroundColorGradient;
		SerializedProperty ambientGroundColor;
		SerializedProperty ambientGroundColorGradient;
		SerializedProperty useAmbientIntensityCurve;

		SerializedProperty ambientIntensity;
		SerializedProperty ambientIntensityCurve;
		//_________________________________________________


		// fog.
		SerializedProperty fogType;
		SerializedProperty fogMode;
		SerializedProperty useRenderSettingsFog;
		SerializedProperty useFogDensityCurve;

		SerializedProperty fogDensity;
		SerializedProperty fogDensityCurve;
		SerializedProperty useFogStartDistanceCurve;
		SerializedProperty fogStartDistance;

		SerializedProperty fogStartDistanceCurve;

		SerializedProperty useFogEndDistanceCurve;

		SerializedProperty fogEndDistance;
		SerializedProperty fogEndDistanceCurve;

		SerializedProperty useFogColorGradient;
		SerializedProperty fogColor;
		SerializedProperty fogColorGradient;
		//_________________________________________________


		// Other Settings.
		SerializedProperty exposure;
		SerializedProperty useExposureCurve;
		SerializedProperty exposureCurve;
		//_________________________________________________




		#endregion

		#region foldouts
		//-----------------------------------------
		bool m_ResourcesFoldout;
		bool m_WorldAndTimeFoldout;
		bool m_SunFoldout;
		bool m_AtmosphereFoldout;
		bool m_MoonFoldout;
		bool m_StarsFoldout;
		bool m_AmbientFoldout;
		bool m_FogFoldout;
		bool m_OtherSettingsFoldout;
		//_________________________________________
		#endregion



		void OnEnable()
		{
			
			serializedObject = new SerializedObject (target);
			timeOfDayManager = (TimeOfDayManager)target;
			//__________________________________________________________________________________________________

			// Resources and components.
			autoAssignSky     = serializedObject.FindProperty ("m_AutoAssignSky");
			skyMaterial       = serializedObject.FindProperty ("skyMaterial");
			directionalLight  = serializedObject.FindProperty ("m_DirectionalLight");
			sunTransform      = serializedObject.FindProperty ("m_SunTransform");
			moonTransform     = serializedObject.FindProperty ("m_MoonTransform");
			moonTexture       = serializedObject.FindProperty ("moonTexture");
			starsCubemap      = serializedObject.FindProperty ("starsCubemap");
			starsNoiseCubemap = serializedObject.FindProperty ("starsNoiseCubemap");
			//__________________________________________________________________________________________________

			// World and time.
			playTime               = serializedObject.FindProperty ("playTime");
			worldLongitude         = serializedObject.FindProperty ("m_WorldLongitude");
			useWorldLongitudeCurve = serializedObject.FindProperty ("useWorldLongitudeCurve");
			worldLongitudeCurve    = serializedObject.FindProperty ("worldLongitudeCurve");
			dayInSeconds           = serializedObject.FindProperty ("dayInSeconds");
			currentTime            = serializedObject.FindProperty ("currentTime");
			//__________________________________________________________________________________________________

			// Sun.
			useSunColorGradient           = serializedObject.FindProperty ("useSunColorGradient");
			sunColor                      = serializedObject.FindProperty ("m_SunColor");
			sunColorGradient              = serializedObject.FindProperty ("sunColorGradient");
			useSunSizeCurve               = serializedObject.FindProperty ("useSunSizeCurve");
			sunSize                       = serializedObject.FindProperty ("m_SunSize");
			sunSizeCurve                  = serializedObject.FindProperty ("sunSizeCurve");
			useSunLightIntensityCurve     = serializedObject.FindProperty ("useSunLightIntensityCurve");
			sunLightIntensity             = serializedObject.FindProperty ("m_SunLightIntensity");
			sunLightIntensityCurve        = serializedObject.FindProperty ("sunLightIntensityCurve");
			//__________________________________________________________________________________________________

			// Atmosphere.
			useSkyTintGradient                 = serializedObject.FindProperty ("useSkyTintGradient");
			skyTint                            = serializedObject.FindProperty ("m_SkyTint");
			skyTintGradient                    = serializedObject.FindProperty ("skyTintGradient");
			useAtmosphereThicknessCurve        = serializedObject.FindProperty ("useAtmosphereThicknessCurve");
			atmosphereThickness                = serializedObject.FindProperty ("m_AtmosphereThickness");
			atmosphereThicknessCurve           = serializedObject.FindProperty ("atmosphereThicknessCurve");
			groundColor                        = serializedObject.FindProperty ("groundColor");
			useNightColor                      = serializedObject.FindProperty ("useNightColor");
			useNightColorGradient              = serializedObject.FindProperty ("useNightColorGradient");
			nightColor                         = serializedObject.FindProperty ("m_NightColor");
			nightColorGradient                 = serializedObject.FindProperty ("nightColorGradient");
			useHorizonFade                     = serializedObject.FindProperty ("useHorizonFade");
			horizonFade                        = serializedObject.FindProperty ("m_HorizonFade");
			useHorizonFadeCurve                = serializedObject.FindProperty ("useHorizonFadeCurve");
			horizonFadeCurve                   = serializedObject.FindProperty ("horizonFadeCurve");
			//__________________________________________________________________________________________________

			// Use moon.
			useMoon                       = serializedObject.FindProperty ("useMoon");
			moonRotationMode              = serializedObject.FindProperty ("moonRotationMode");
			moonLongitude                 = serializedObject.FindProperty ("m_MoonLongitude");
			moonLatitude                  = serializedObject.FindProperty ("m_MoonLatitude");
			useMoonLongitudeCurve         = serializedObject.FindProperty ("useMoonLongitudeCurve");
			moonLongitudeCurve            = serializedObject.FindProperty ("moonLongitudeCurve");
			useMoonLatitudeCurve          = serializedObject.FindProperty ("useMoonLatitudeCurve");
			moonLatitudeCurve             = serializedObject.FindProperty ("moonLatitudeCurve");
			useMoonLightColorGradient     = serializedObject.FindProperty ("useMoonLightColorGradient");
			moonLightColor                = serializedObject.FindProperty ("m_MoonLightColor");
			moonLightColorGradient        = serializedObject.FindProperty ("moonLightColorGradient");
			useMoonLightIntensityCurve    = serializedObject.FindProperty ("useMoonLightIntensityCurve");
			moonLightIntensity            = serializedObject.FindProperty ("m_MoonLightIntensity");
			moonLightIntensityCurve       = serializedObject.FindProperty ("moonLightIntensityCurve");
			useMoonColorGradient          = serializedObject.FindProperty ("useMoonColorGradient");
			moonColor                     = serializedObject.FindProperty ("m_MoonColor");
			moonColorGradient             = serializedObject.FindProperty ("moonColorGradient");
			useMoonIntensityCurve         = serializedObject.FindProperty ("useMoonIntensityCurve");
			moonIntensity                 = serializedObject.FindProperty ("m_MoonIntensity");
			moonIntensityCurve            = serializedObject.FindProperty ("moonIntensityCurve");
			useMoonSizeCurve              = serializedObject.FindProperty ("useMoonSizeCurve");
			moonSize                      = serializedObject.FindProperty ("m_MoonSize");
			moonSizeCurve                 = serializedObject.FindProperty ("moonSizeCurve");
			useMoonHalo                   = serializedObject.FindProperty ("useMoonHalo");
			useMoonHaloColorGradient      = serializedObject.FindProperty ("useMoonHaloColorGradient");
			moonHaloColor                 = serializedObject.FindProperty ("m_MoonHaloColor");
			moonHaloColorGradient         = serializedObject.FindProperty ("moonHaloColorGradient");
			useMoonHaloSizeCurve          = serializedObject.FindProperty ("useMoonHaloSizeCurve");
			moonHaloSize                  = serializedObject.FindProperty ("m_MoonHaloSize");
			moonHaloSizeCurve             = serializedObject.FindProperty ("moonHaloSizeCurve");
			useMoonHaloIntensityCurve     = serializedObject.FindProperty ("useMoonHaloIntensityCurve");
			moonHaloIntensity             = serializedObject.FindProperty ("m_MoonHaloIntensity");
			moonHaloIntensityCurve        = serializedObject.FindProperty ("moonHaloIntensityCurve");
			//__________________________________________________________________________________________________

			// Stars.
			useStars                  = serializedObject.FindProperty ("useStars");
			starsRotationMode         = serializedObject.FindProperty ("starsRotationMode");
			starsOffsets              = serializedObject.FindProperty ("starsOffsets");
			useStarsColorGradient     = serializedObject.FindProperty ("useStarsColorGradient");
			starsColor                = serializedObject.FindProperty ("m_StarsColor");
			starsColorGradient        = serializedObject.FindProperty ("starsColorGradient");
			useStarsIntensityCurve    = serializedObject.FindProperty ("useStarsIntensityCurve");
			useStarsTwinkle           = serializedObject.FindProperty ("useStarsTwinkle");
			starsIntensity            = serializedObject.FindProperty ("m_StarsIntensity");
			starsIntensityCurve       = serializedObject.FindProperty ("starsIntensityCurve");
			useStarsTwinkleCurve      = serializedObject.FindProperty ("useStarsTwinkleCurve");
			starsTwinkle              = serializedObject.FindProperty ("m_StarsTwinkle");
			starsTwinkleCurve         = serializedObject.FindProperty ("starsTwinkleCurve");
			useStarsTwinkleSpeedCurve = serializedObject.FindProperty ("useStarsTwinkleSpeedCurve");
			starsTwinkleSpeed         = serializedObject.FindProperty ("m_StarsTwinkleSpeed");
			starsTwinkleSpeedCurve    = serializedObject.FindProperty ("starsTwinkleSpeedCurve");
			//__________________________________________________________________________________________________

			// Ambient.
			ambientMode                      = serializedObject.FindProperty ("m_AmbientMode");
			useAmbientSkyColorGradient       = serializedObject.FindProperty ("useAmbientSkyColorGradient");
			ambientSkyColor                  = serializedObject.FindProperty ("m_AmbientSkyColor");
			ambientSkyColorGradient          = serializedObject.FindProperty ("ambientSkyColorGradient");
			useAmbientEquatorColorGradient   = serializedObject.FindProperty ("useAmbientEquatorColorGradient");
			ambientEquatorColor              = serializedObject.FindProperty ("m_AmbientEquatorColor");
			ambientEquatorColorGradient      = serializedObject.FindProperty ("ambientEquatorColorGradient");
			useAmbientGroundColorGradient    = serializedObject.FindProperty ("useAmbientGroundColorGradient");
			ambientGroundColor               = serializedObject.FindProperty ("m_AmbientGroundColor");
			ambientGroundColorGradient       = serializedObject.FindProperty ("ambientGroundColorGradient");
			useAmbientIntensityCurve         = serializedObject.FindProperty ("useAmbientIntensityCurve");
			ambientIntensity                 = serializedObject.FindProperty ("m_AmbientIntensity");
			ambientIntensityCurve            = serializedObject.FindProperty ("ambientIntensityCurve");
			//__________________________________________________________________________________________________

			// fog.
			fogType                  = serializedObject.FindProperty ("fogType");
			fogMode                  = serializedObject.FindProperty ("fogMode");
			useRenderSettingsFog     = serializedObject.FindProperty ("useRenderSettingsFog");
			useFogDensityCurve       = serializedObject.FindProperty ("useFogDensityCurve");
			fogDensity               = serializedObject.FindProperty ("m_FogDensity");
			fogDensityCurve          = serializedObject.FindProperty ("fogDensityCurve");
			useFogStartDistanceCurve = serializedObject.FindProperty ("useFogStartDistanceCurve");
			fogStartDistance         = serializedObject.FindProperty ("m_FogStartDistance");
			fogStartDistanceCurve    = serializedObject.FindProperty ("fogStartDistanceCurve");
			useFogEndDistanceCurve   = serializedObject.FindProperty ("useFogEndDistanceCurve");
			fogEndDistance           = serializedObject.FindProperty ("m_FogEndDistance");
			fogEndDistanceCurve      = serializedObject.FindProperty ("fogEndDistanceCurve");
			useFogColorGradient      = serializedObject.FindProperty ("useFogColorGradient");
			fogColor                 = serializedObject.FindProperty ("m_FogColor");
			fogColorGradient         = serializedObject.FindProperty ("fogColorGradient");
			//____________________________________________________________________________________________________

			// Other settings.
			exposure             = serializedObject.FindProperty ("m_Exposure");
			useExposureCurve     = serializedObject.FindProperty ("useExposureCurve");
			exposureCurve        = serializedObject.FindProperty ("exposureCurve");
			//____________________________________________________________________________________________________
		}

		public override void OnInspectorGUI()
		{

			serializedObject.Update ();

			HorizontalSeparator (Color.white, 2);
			TexTitle ("Time of Day Manager [Free]");
			HorizontalSeparator (Color.white, 2);

			ResourcesAndComponents(); 
			WorldAndTime();
			Sun(); 
			Atmosphere();
			Moon();
			Stars();   
			Ambient();  
			Fog();
			OtherSettings();

			serializedObject.ApplyModifiedProperties();

		}


		void ResourcesAndComponents()
		{

			m_ResourcesFoldout = EditorGUILayout.Foldout (m_ResourcesFoldout, "Resources");
			if (m_ResourcesFoldout) 
			{

				HorizontalSeparator (Color.white, 2);
				TexTitle ("Resources");
				HorizontalSeparator (Color.white, 2);

				EditorGUILayout.PropertyField(autoAssignSky, new GUIContent("Auto Assign Sky?"));

				EditorGUILayout.PropertyField(skyMaterial, new GUIContent("Sky Material"));
				if (skyMaterial.objectReferenceValue == null)
				{
					EditorGUILayout.HelpBox ("Please Assign Sky Material", MessageType.Warning);
				}

				EditorGUILayout.PropertyField(sunTransform, new GUIContent("Sun Transform"));
				if (sunTransform.objectReferenceValue == null) 
				{
					EditorGUILayout.HelpBox ("Please Assign Sun Transform", MessageType.Warning);
				} 
					

				EditorGUILayout.PropertyField(moonTransform, new GUIContent("Moon Transform"));
				if (moonTransform.objectReferenceValue == null) 
				{
					EditorGUILayout.HelpBox ("Please Assign Moon Light", MessageType.Warning);
				} 

				EditorGUILayout.PropertyField(directionalLight, new GUIContent("Directional Light"));


				EditorGUILayout.PropertyField(moonTexture, new GUIContent("Moon Texture"));
				if (moonTexture.objectReferenceValue == null) 
				{
					EditorGUILayout.HelpBox ("Please Assign Moon Texture", MessageType.Warning);
				} 


				EditorGUILayout.PropertyField(starsCubemap, new GUIContent("Stars Cubemap")); 
				if (starsCubemap.objectReferenceValue == null) 
				{
					EditorGUILayout.HelpBox ("Please Assign Stars Cubemap", MessageType.Warning);
				}


				EditorGUILayout.PropertyField(starsNoiseCubemap, new GUIContent("Stars Noise Cubemap")); 
				if (starsNoiseCubemap.objectReferenceValue == null) 
				{
					EditorGUILayout.HelpBox ("Please Assign Stars Noise Cubemap", MessageType.Warning);
				}
			}
		}
			
		void WorldAndTime()
		{

			m_WorldAndTimeFoldout = EditorGUILayout.Foldout (m_WorldAndTimeFoldout , "World And Time");
			if(m_WorldAndTimeFoldout)
			{

				HorizontalSeparator (Color.white, 2);
				TexTitle ("World And Time");
				HorizontalSeparator (Color.white, 2);

			
				// World Longitude.
				EditorGUILayout.BeginHorizontal ();

				if(useWorldLongitudeCurve.boolValue)
					CurveField ("Longitude", worldLongitudeCurve, Color.white, new Rect (0, 0, 1, 360f), 75);
				else
					EditorGUILayout.PropertyField(worldLongitude, new GUIContent("Longitude"));

				ToggleButton(useWorldLongitudeCurve, "C");

				EditorGUILayout.EndHorizontal ();

				EditorGUILayout.Separator ();

				//_____________________________________________________________________________________________________________

				EditorGUILayout.PropertyField (playTime, new GUIContent ("Play Time"));

				if(playTime.boolValue)
					EditorGUILayout.PropertyField(dayInSeconds, new GUIContent("Day In Seconds"));

				EditorGUILayout.PropertyField(currentTime, new GUIContent("Current Time"));

				HorizontalSeparator (Color.white, 2);

				EditorGUILayout.BeginHorizontal ();

				EditorGUILayout.LabelField("Time" + " " + timeOfDayManager.TimeString, EditorStyles.miniLabel);

				EditorGUILayout.EndHorizontal ();

				HorizontalSeparator (Color.white, 2);

				//_____________________________________________________________________________________________________________
			}

		}
			
		void Sun()
		{

			m_SunFoldout = EditorGUILayout.Foldout (m_SunFoldout, "Sun");
			if(m_SunFoldout)
			{

				HorizontalSeparator (Color.white, 2);
				TexTitle ("Sun");
				HorizontalSeparator (Color.white, 2);

				// Sun Color.
				EditorGUILayout.BeginHorizontal ();

				if(useSunColorGradient.boolValue)
					ColorField (sunColorGradient, "Sun Color",75);
				else
					ColorField (sunColor, "Sun Color",75);

				ToggleButton(useSunColorGradient, "G");

				EditorGUILayout.EndHorizontal ();
				//EditorGUILayout.Separator();
				//_____________________________________________________________________________________________________________

				// Sun Size.
				EditorGUILayout.BeginHorizontal ();

				if(useSunSizeCurve.boolValue)
					CurveField ("Sun Size", sunSizeCurve, Color.white, new Rect (0, 0, 1, 0.3f), 75);
				else
					EditorGUILayout.PropertyField(sunSize, new GUIContent("Sun Size"));

				ToggleButton(useSunSizeCurve, "C");

				EditorGUILayout.EndHorizontal ();
				//_____________________________________________________________________________________________________________

				// Sun Light Intensity.
				EditorGUILayout.BeginHorizontal ();

				if(useSunLightIntensityCurve.boolValue)
					CurveField ("Sun Light Intensity", sunLightIntensityCurve, Color.white, new Rect (0, 0, 1, 8f), 75);
				else
					EditorGUILayout.PropertyField(sunLightIntensity, new GUIContent("Sun Light Intensity"));

				ToggleButton(useSunLightIntensityCurve, "C");


				EditorGUILayout.EndHorizontal ();

				//_____________________________________________________________________________________________________________

			}

		}

		void Atmosphere()
		{

			m_AtmosphereFoldout = EditorGUILayout.Foldout (m_AtmosphereFoldout, "Atmosphere");
			if (m_AtmosphereFoldout) 
			{

				HorizontalSeparator (Color.white, 2);
				TexTitle ("Atmosphere");
				HorizontalSeparator (Color.white, 2);

			
				// Sky Tint.
				EditorGUILayout.BeginHorizontal ();

				if(useSkyTintGradient.boolValue)
					ColorField (skyTintGradient, "Sky Tint",75);
				else
					ColorField (skyTint, "Sky Tint",75);

				ToggleButton(useSkyTintGradient, "G");

				EditorGUILayout.EndHorizontal ();
				//_____________________________________________________________________________________________________________


				// Atmosphere Thickness.
				EditorGUILayout.BeginHorizontal ();

				if(useAtmosphereThicknessCurve.boolValue)
					CurveField ("Atmosphere Thickness", atmosphereThicknessCurve, Color.white, new Rect (0, 0, 1, 7f), 75);
				else
					EditorGUILayout.PropertyField(atmosphereThickness, new GUIContent("Atmosphere Thickness"));

				ToggleButton(useAtmosphereThicknessCurve, "C");

				EditorGUILayout.EndHorizontal ();
				//_____________________________________________________________________________________________________________

				ColorField(groundColor, "Ground Color",99);
				EditorGUILayout.Separator();
				//_____________________________________________________________________________________________________________


				// Night color.
				EditorGUILayout.PropertyField(useNightColor, new GUIContent("Night Color"));
				if(useNightColor.boolValue)
				{


					EditorGUILayout.BeginHorizontal ();

					if(useNightColorGradient.boolValue)
						ColorField (nightColorGradient, "Night Color",75);
					else
						ColorField (nightColor, "Night Color",75);

					ToggleButton(useNightColorGradient, "G");

					EditorGUILayout.EndHorizontal ();
					EditorGUILayout.Separator ();
				}
			
				//_____________________________________________________________________________________________________________
			

				// Horizon fade.
				EditorGUILayout.PropertyField (useHorizonFade, new GUIContent ("Use Horizon Fade"));
				if (useHorizonFade.boolValue) 
				{

					// Horizon Fade.
					EditorGUILayout.BeginHorizontal ();
					if (useHorizonFadeCurve.boolValue)
						CurveField ("Horizon Fade", horizonFadeCurve, Color.white, new Rect (0, 0, 1, 2f), 75);
					else
						EditorGUILayout.PropertyField (horizonFade, new GUIContent ("Horizon Fade"));

					ToggleButton (useHorizonFadeCurve, "C");

					EditorGUILayout.EndHorizontal ();
				}
				//_____________________________________________________________________________________________________________

			}
		}
			
		void Moon()
		{

			m_MoonFoldout = EditorGUILayout.Foldout (m_MoonFoldout, "Moon");
			if (m_MoonFoldout)
			{
				HorizontalSeparator (Color.white, 2);
				TexTitle ("Moon");
				HorizontalSeparator (Color.white, 2);

				EditorGUILayout.PropertyField (useMoon, new GUIContent ("Use Moon"));

				if (useMoon.boolValue) 
				{

					EditorGUILayout.PropertyField (moonRotationMode, new GUIContent ("Moon Rotation Mode"));


					if (moonRotationMode.intValue != 0) 
					{

						// Moon Longitude.
						EditorGUILayout.BeginHorizontal ();

						if (useMoonLongitudeCurve.boolValue)
							CurveField ("Moon Longitude", moonLongitudeCurve, Color.white, new Rect (0, 0, 1, 360f), 75);
						else
							EditorGUILayout.PropertyField (moonLongitude, new GUIContent ("Moon Longitude"));

						ToggleButton (useMoonLongitudeCurve, "C");

						EditorGUILayout.EndHorizontal ();



						// Moon Latitude.
						EditorGUILayout.BeginHorizontal ();

						if (useMoonLatitudeCurve.boolValue)
							CurveField ("Moon Latitude", moonLatitudeCurve, Color.white, new Rect (0, 0, 1, 360f), 75);
						else
							EditorGUILayout.PropertyField (moonLatitude, new GUIContent ("Moon Latitude"));

						ToggleButton (useMoonLatitudeCurve, "C");

						EditorGUILayout.EndHorizontal ();

					}
					EditorGUILayout.Separator ();
					//_____________________________________________________________________________________________________________


					// Moon Light Color.
					EditorGUILayout.BeginHorizontal ();

					if(useMoonLightColorGradient.boolValue)
						ColorField (moonLightColorGradient, "Moon Light Color",75);
					else
						ColorField (moonLightColor, "Moon Light Color",75);

					ToggleButton(useMoonLightColorGradient, "G");

					EditorGUILayout.EndHorizontal ();

					//_____________________________________________________________________________________________________________


					// Moon Light Intensity.
					EditorGUILayout.BeginHorizontal ();

					if(useMoonLightIntensityCurve.boolValue)
						CurveField ("Moon Light Intensity", moonLightIntensityCurve, Color.white, new Rect (0, 0, 1, 1f), 75);
					else
						EditorGUILayout.PropertyField(moonLightIntensity, new GUIContent("Moon Light Intensity"));

					ToggleButton(useMoonLightIntensityCurve, "C");

					EditorGUILayout.EndHorizontal ();
					EditorGUILayout.Separator ();
					//_____________________________________________________________________________________________________________


					// Moon Color.
					EditorGUILayout.BeginHorizontal ();

					if(useMoonColorGradient.boolValue)
						ColorField (moonColorGradient, "Moon Color",75);
					else
						ColorField (moonColor, "Moon Color",75);

					ToggleButton(useMoonColorGradient, "G");

					EditorGUILayout.EndHorizontal ();

					//_____________________________________________________________________________________________________________


					// Moon Intensity.
					EditorGUILayout.BeginHorizontal ();

					if(useMoonIntensityCurve.boolValue)
						CurveField ("Moon Intensity", moonIntensityCurve, Color.white, new Rect (0, 0, 1, 3f), 75);
					else
						EditorGUILayout.PropertyField(moonIntensity, new GUIContent("Moon Intensity"));

					ToggleButton(useMoonIntensityCurve, "C");

					EditorGUILayout.EndHorizontal ();
					//_____________________________________________________________________________________________________________


					// Moon Size.
					EditorGUILayout.BeginHorizontal ();

					if(useMoonSizeCurve.boolValue)
						CurveField ("Moon Size", moonSizeCurve, Color.white, new Rect (0, 0, 1, 1f), 75);
					else
						EditorGUILayout.PropertyField(moonSize, new GUIContent("Moon Size"));

					ToggleButton(useMoonSizeCurve, "C");

					EditorGUILayout.EndHorizontal ();
					EditorGUILayout.Separator ();

					//_____________________________________________________________________________________________________________


					EditorGUILayout.PropertyField(useMoonHalo, new GUIContent("Use Moon Halo"));
					if (useMoonHalo.boolValue) 
					{
						// Moon Halo Color.
						EditorGUILayout.BeginHorizontal ();

						if(useMoonHaloColorGradient.boolValue)
							ColorField (moonHaloColorGradient, "Moon Halo Color",75);
						else
							ColorField (moonHaloColor, "Moon Halo Color",75);

						ToggleButton(useMoonHaloColorGradient, "G");

						EditorGUILayout.EndHorizontal ();

						//_____________________________________________________________________________________________________________

						// Moon Halo Intensity.
						EditorGUILayout.BeginHorizontal ();

						if(useMoonHaloIntensityCurve.boolValue)
							CurveField ("Moon Halo Intensity", moonHaloIntensityCurve, Color.white, new Rect (0, 0, 1, 5f), 75);
						else
							EditorGUILayout.PropertyField(moonHaloIntensity, new GUIContent("Moon Halo Intensity"));

						ToggleButton(useMoonHaloIntensityCurve, "C");

						EditorGUILayout.EndHorizontal ();
						//_____________________________________________________________________________________________________________


						// Moon Halo Size.
						EditorGUILayout.BeginHorizontal ();

						if(useMoonHaloSizeCurve.boolValue)
							CurveField ("Moon Halo Size", moonHaloSizeCurve, Color.white, new Rect (0, 0, 1, 10f), 75);
						else
							EditorGUILayout.PropertyField(moonHaloSize, new GUIContent("Moon Halo Size"));

						ToggleButton(useMoonHaloSizeCurve, "C");

						EditorGUILayout.EndHorizontal ();
						//_____________________________________________________________________________________________________________
					}
				}

			}

		}

		void Stars()
		{


			m_StarsFoldout = EditorGUILayout.Foldout (m_StarsFoldout, "Stars");
			if (m_StarsFoldout) 
			{

				HorizontalSeparator (Color.white, 2);
				TexTitle ("Stars");
				HorizontalSeparator (Color.white, 2);

				EditorGUILayout.PropertyField (useStars, new GUIContent ("Use Stars"));
				if (useStars.boolValue) 
				{

					EditorGUILayout.PropertyField (starsRotationMode, new GUIContent ("Stars Rotation Mode"));
					EditorGUILayout.PropertyField (starsOffsets, new GUIContent ("Stars Offsets"));

					EditorGUILayout.Separator ();

					// Stars Color.
					EditorGUILayout.BeginHorizontal ();

					if(useStarsColorGradient.boolValue)
						ColorField (starsColorGradient, "Stars Color",75);
					else
						ColorField (starsColor, "Stars Color",75);

					ToggleButton(useStarsColorGradient, "G");

					EditorGUILayout.EndHorizontal ();

					//_____________________________________________________________________________________________________________

				

					// Stars Intensity.
					EditorGUILayout.BeginHorizontal ();

					if(useStarsIntensityCurve.boolValue)
						CurveField ("Stars Intensity", starsIntensityCurve, Color.white, new Rect (0, 0, 1, 5f), 75);
					else
						EditorGUILayout.PropertyField(starsIntensity, new GUIContent("Stars Intensity"));

					ToggleButton(useStarsIntensityCurve, "C");

					EditorGUILayout.EndHorizontal ();
					//_____________________________________________________________________________________________________________

					EditorGUILayout.Separator ();

					// Stars Twinkle.
					EditorGUILayout.PropertyField (useStarsTwinkle, new GUIContent ("Use Stars Twinkle"));

					if (useStarsTwinkle.boolValue) 
					{
						EditorGUILayout.BeginHorizontal ();

						if (useStarsTwinkleCurve.boolValue)
							CurveField ("Stars Twinkle", starsTwinkleCurve, Color.white, new Rect (0, 0, 1, 5f), 75);
						else
							EditorGUILayout.PropertyField (starsTwinkle, new GUIContent ("Stars Twinkle"));

						ToggleButton (useStarsTwinkleCurve, "C");

						EditorGUILayout.EndHorizontal ();
					
						//_____________________________________________________________________________________________________________

						// Stars Twinkle Speed.
						EditorGUILayout.BeginHorizontal ();

						if (useStarsTwinkleSpeedCurve.boolValue)
							CurveField ("Stars Twinkle Speed", starsTwinkleSpeedCurve, Color.white, new Rect (0, 0, 1, 5f), 75);
						else
							EditorGUILayout.PropertyField (starsTwinkleSpeed, new GUIContent ("Stars Twinkle Speed"));

						ToggleButton (useStarsTwinkleSpeedCurve, "C");

						EditorGUILayout.EndHorizontal ();
						//_____________________________________________________________________________________________________________
					}

				}

			}
		}

		void Ambient()
		{

			m_AmbientFoldout = EditorGUILayout.Foldout (m_AmbientFoldout, "Ambient");
			if (m_AmbientFoldout) 
			{

				HorizontalSeparator (Color.white, 2);
				TexTitle ("Ambient");
				HorizontalSeparator (Color.white, 2);


				EditorGUILayout.PropertyField (ambientMode, new GUIContent ("Ambient Mode"));


				string ambientColorName = (ambientMode.enumValueIndex == 0) ? "Ambient Color" : "Sky Color";


				if (ambientMode.enumValueIndex != 2) 
				{

					// Ambient Color.
					EditorGUILayout.BeginHorizontal ();

					if (useAmbientSkyColorGradient.boolValue)
						ColorField (ambientSkyColorGradient, ambientColorName, 75);
					else
						ColorField (ambientSkyColor, ambientColorName, 75);

					ToggleButton (useAmbientSkyColorGradient, "G");

					EditorGUILayout.EndHorizontal ();

					//_____________________________________________________________________________________________________________

				} 
				else 
				{


					// Ambient Intensity.
					EditorGUILayout.BeginHorizontal ();

					if(useAmbientIntensityCurve.boolValue)
						CurveField ("Ambient Intensity", ambientIntensityCurve, Color.white, new Rect (0, 0, 1, 8f), 75);
					else
						EditorGUILayout.PropertyField(ambientIntensity, new GUIContent("Ambient Intensity"));

					ToggleButton(useAmbientIntensityCurve, "C");

					EditorGUILayout.EndHorizontal ();
					//_____________________________________________________________________________________________________________
				}


				if (ambientMode.enumValueIndex == 1) 
				{

					// Ambient Equator Color.
					EditorGUILayout.BeginHorizontal ();

					if (useAmbientEquatorColorGradient.boolValue)
						ColorField (ambientEquatorColorGradient, "Ambient Equator Color", 75);
					else
						ColorField (ambientEquatorColor, "Ambient Equator Color", 75);

					ToggleButton (useAmbientEquatorColorGradient, "G");

					EditorGUILayout.EndHorizontal ();

					//_____________________________________________________________________________________________________________


					// Ambient Ground Color.
					EditorGUILayout.BeginHorizontal ();

					if (useAmbientGroundColorGradient.boolValue)
						ColorField (ambientGroundColorGradient, "Ambient Ground Color", 75);
					else
						ColorField (ambientGroundColor, "Ambient Ground Color", 75);

					ToggleButton (useAmbientGroundColorGradient, "G");

					EditorGUILayout.EndHorizontal ();

					//_____________________________________________________________________________________________________________


				}

			}

		}

		void Fog()
		{

			m_FogFoldout = EditorGUILayout.Foldout (m_FogFoldout , "Fog");
			if (m_FogFoldout) 
			{
				HorizontalSeparator (Color.white, 2);
				TexTitle ("Fog");
				HorizontalSeparator (Color.white, 2);

				EditorGUILayout.PropertyField (fogType, new GUIContent ("Fog Type"));

				if (fogType.intValue == 0)
					EditorGUILayout.PropertyField (useRenderSettingsFog, new GUIContent ("Use Fog"));

				if (fogType.intValue != 2) 
				{

					EditorGUILayout.PropertyField (fogMode, new GUIContent ("Fog Mode"));

					if (fogMode.enumValueIndex == 0) {

						// Fog Start Distance.
						EditorGUILayout.BeginHorizontal ();

						if (useFogStartDistanceCurve.boolValue)
							CurveField ("Start Distance", fogStartDistanceCurve, Color.white, new Rect (0, 0, 1, 1000f), 75);
						else
							EditorGUILayout.PropertyField (fogStartDistance, new GUIContent ("Fog Start Distance"));

						ToggleButton (useFogStartDistanceCurve, "C");

						EditorGUILayout.EndHorizontal ();
						//_____________________________________________________________________________________________________________

						// Fog End Distance.
						EditorGUILayout.BeginHorizontal ();

						if (useFogEndDistanceCurve.boolValue)
							CurveField ("End Distance", fogEndDistanceCurve, Color.white, new Rect (0, 0, 1, 1000f), 75);
						else
							EditorGUILayout.PropertyField (fogEndDistance, new GUIContent ("Fog End Distance"));

						ToggleButton (useFogEndDistanceCurve, "C");

						EditorGUILayout.EndHorizontal ();
						//_____________________________________________________________________________________________________________

					} 
					else 
					{

						// Fog Density.
						EditorGUILayout.BeginHorizontal ();

						if (useFogDensityCurve.boolValue)
							CurveField ("Fog Density", fogDensityCurve, Color.white, new Rect (0, 0, 1, 1f), 75);
						else
							EditorGUILayout.PropertyField (fogDensity, new GUIContent ("Fog Density"));

						ToggleButton (useFogDensityCurve, "C");

						EditorGUILayout.EndHorizontal ();
						//_____________________________________________________________________________________________________________
					}

					// Fog Color.
					EditorGUILayout.BeginHorizontal ();

					if (useFogColorGradient.boolValue)
						ColorField (fogColorGradient, "Fog Color", 75);
					else
						ColorField (fogColor, "Fog Color", 75);

					ToggleButton (useFogColorGradient, "G");

					EditorGUILayout.EndHorizontal ();

					//_____________________________________________________________________________________________________________
				}
			}
		}

		void OtherSettings()
		{

			m_OtherSettingsFoldout = EditorGUILayout.Foldout (m_OtherSettingsFoldout , "Other Settings");
			if (m_OtherSettingsFoldout) 
			{

				HorizontalSeparator (Color.white, 2);
				TexTitle ("Other Settings");
				HorizontalSeparator (Color.white, 2);


				// Exposure.
				EditorGUILayout.BeginHorizontal ();

				if(useExposureCurve.boolValue)
					CurveField ("Exposure", exposureCurve, Color.white, new Rect (0, 0, 1, 5f), 75);
				else
					EditorGUILayout.PropertyField(exposure, new GUIContent("Exposure"));

				ToggleButton(useExposureCurve, "C");

				EditorGUILayout.EndHorizontal ();
			}
			//_____________________________________________________________________________________________________________
		}


	}
}
