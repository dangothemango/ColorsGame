// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Custom/ShimmeringShader" {
	Properties {
		_NaturalTint 	("Natural Tint", Color) = (1,1,1,1)
		_BaseColor 		("Base Color", Color) = (1,0,0,1)
		_DistortionMap 	("Distortion Map", 2D) = "bump" {}
		_DistortionScale ("Distortion Scale", Range(0,1)) = 0.0
		_Controller 	("Controller", Range(0,1)) = 0.5
		_RippleScale	("Ripple Scale", Range(0,1)) = 0.0
		_RippleSpeed	("Ripple Speed", Range(0,10)) = 0.0
	}

	SubShader{
		Tags { 
			"Queue"="Transparent"
			"RenderType"="Opaque"
		 }
		LOD 100

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _NaturalTint;
		fixed4 _BaseColor;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			//output
			o.Albedo = _NaturalTint.rgb;
			o.Alpha = 0.1f;
		}
		ENDCG

	}

	SubShader {
		Tags {
			"Queue" = "Transparent"
		 	"RenderType"="Opaque"
		 	"IsEmissive"="true"
		}
		LOD 200
		Cull back
		//Grab a render of the background to shader calling object and turn into 
		// a texture called ScreenGrab
		GrabPass{
			"_ScreenGrab"
		}
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		uniform sampler2D _DistortionMap;
		sampler2D _ScreenGrab;

		struct Input {
			float2 uv_MainTex;
			float4 grabUV;
			float4 screenPos;
		};

		void vert (inout appdata_full v, out Input o) {
			float4 hpos = UnityObjectToClipPos(v.vertex);
			o.grabUV = ComputeGrabScreenPos(hpos);
		}

		half _DistortionScale;
		half _Controller;
		half _RippleScale; 
		half _RippleSpeed;
		fixed4 _NaturalTint;
		fixed4 _BaseColor;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			//input
			float4 screenPosition = IN.screenPos;
			#if UNITY_UV_STARTS_AT_TOP
			float scale = -1.0f;
			#else
			float scale = 1.0f;
			#endif
			float halfPosW = screenPosition.w * 0.5f;
			screenPosition.y = (screenPosition.y - halfPosW) * _ProjectionParams.x * scale + halfPosW;
			screenPosition.w += 1e-11;
			screenPosition.xyzw /= screenPosition.w;
			//build input to o.emission
			float t = _Time.y;
			float4 tempOut;
			float2 uv_DistortionMap;
			float4 uv_ScreenGrab;
			float4 ScaledDistortion;

			//process 
			float4 timedRipple = (t * _RippleSpeed);
			float2 screenRipple = float2((timedRipple+screenPosition).x, (timedRipple+screenPosition).y);
			float4 distortionFactor = float4((UnpackNormal(tex2D(_DistortionMap,(_RippleScale*screenRipple).xy))* _DistortionScale), 0.0);
			tempOut = lerp(tex2Dproj( _ScreenGrab, UNITY_PROJ_COORD((distortionFactor + screenPosition))), _BaseColor, _Controller);

			//output
			o.Albedo = _NaturalTint.rgb;
			o.Emission = tempOut.rgb;
			o.Metallic = tempOut.r;
			o.Smoothness = tempOut.r;
			o.Alpha = 1.0f;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
