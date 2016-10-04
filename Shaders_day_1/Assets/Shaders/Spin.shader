Shader "Custom/Spin" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows alpha:fade

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			// Albedo comes from a texture tinted by color
			half2 uv = IN.uv_MainTex;

			// Offset relative to center
			uv.x -= 0.5;
			uv.y -= 0.5;

			// To Polar
			half r = sqrt((uv.x*uv.x)+(uv.y*uv.y));
			half t = atan2(uv.x, uv.y);

			// Rotation
			t += _Time.w;

			// To Cartecian
			uv.x = r * cos(t);
			uv.y = r * sin(t);

			// Offset back from center
			uv.x += 0.5;
			uv.y += 0.5;

			fixed4 c = tex2D (_MainTex, uv) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
