Shader "Custom/earth" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Clouds("Clouds (RGB)", 2D) = "black" {}
		_Cloudiness ("Cloudiness", Range(1, 5)) = 1
		_SpinDirection("Spin Direction", Vector) = (1, 0, 0, 0)
		_SpinSpeed("Spin Speed", float) = 0.25
		_WindDirection("Wind Direction", Vector) = (-1, 0, 0, 0)
		_WindSpeed("Wind Speed", float) = 0.5
		_Glossiness ("Smoothness", Range(0,1)) = 0.0
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Clouds;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Cloudiness;
		half _Metallic;
		half4 _SpinDirection;
		half4 _WindDirection;
		half _WindSpeed;
		half _SpinSpeed;

		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o)
		{
			// Albedo comes from a texture tinted by color
			half2 earthUV = IN.uv_MainTex;
			earthUV.x += _SpinDirection.x * _SpinSpeed * _Time.x;

			fixed4 c = tex2D (_MainTex, earthUV);

			half2 cloudUV = IN.uv_MainTex;
			cloudUV.x += _WindDirection.x * _Time.x * _WindSpeed;

			fixed4 d = tex2D(_Clouds, cloudUV) * _Color * _Cloudiness;

			o.Albedo = lerp(c.rgb, d.rgb, d.r);
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
