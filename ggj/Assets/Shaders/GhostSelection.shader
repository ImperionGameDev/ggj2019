Shader "Tower/GhostSelection"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_TintColor("Tint Color", Color) = (1, 1, 1, 0)
		_Transparency("Transparency", Range(0.0, 1)) = 0.25
		_Speed("Speed", Float) = 1
	}
	SubShader
	{
		LOD 100
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"			

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _TintColor;
			float _Transparency;
			float _Speed;

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 texcol = tex2D(_MainTex, i.uv) + _TintColor;
				float4 t = _Time * _Speed;
				texcol.a = clamp(abs(sin(t)) + _Transparency, 0, 1);
				return texcol;
			}
			ENDCG
		}
	}
}
