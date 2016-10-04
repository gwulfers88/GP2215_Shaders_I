Shader "Custom/water" 
{
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Direction("Direction", Vector) = (1, 0, 0, 0)
		_Speed("Speed", float) = 0
	}
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input 
		{
			float2 uv_MainTex;
		};

		fixed4 _Color;
		half4 _Direction;
		half _Speed;

		void surf (Input IN, inout SurfaceOutputStandard o)
		{
			// Albedo comes from a texture tinted by color
			half2 uv = IN.uv_MainTex;
			half2 dir = normalize(_Direction.xy);

			// x 1/20xSpeed y : 1xNormalSpeed z : 2xSpeed w: 3xSpeed;
			uv.x += _Time.y * dir * _Speed;
			uv.y += _Time.y * dir * _Speed;

			fixed4 c = tex2D (_MainTex, uv) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
