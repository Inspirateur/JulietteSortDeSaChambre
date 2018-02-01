Shader "VFX/Glow"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Highlight("Highlight", 2D) = "black" {}
		_HighlightColor("Highlight Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _Highlight;
			float4 _HighlightColor;
			float4 _Highlight_TexelSize;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
			float sum = 0.0;
			for (int x = -3; x < 3; x++)
				for (int y = -3; y < 3; y++) {
					float2 offset = float2(x * _Highlight_TexelSize.x, y * _Highlight_TexelSize.y);
					sum += tex2D(_Highlight, i.uv + offset).r;
				}
			sum /= 36.0;
			if (sum > 0.95)
				sum = 0.0;
				col.rgb = col.rgb + _HighlightColor.rgb * sum;
				return col;
			}
			ENDCG
		}
	}
}
