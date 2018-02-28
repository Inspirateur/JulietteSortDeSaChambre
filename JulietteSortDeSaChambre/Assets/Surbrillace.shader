

Shader "Custom/Surbrillace" {
      Properties {
        _Color ("Main Color", Color) = (0.4235294117647059,0.4745098039215686,0.7568627450980392)
        _Start ("Start", Float) = 0.0
        _End ("End", Float) = 1.0
        _Alpha ("Alpha", Float) = 1.0
    }
	SubShader {
        Pass {

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 _Color;
            float _Start;
            float _End;
            float _Alpha;

            struct v2f {
                float4 pos : SV_POSITION;
                float3 normal : NORMAL;
            };

            v2f vert (appdata_base v)
            {
                v2f o;
                float4 postemp;
                o.normal = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, v.normal));
                postemp = UnityObjectToClipPos(v.vertex.xyz);
                o.pos = postemp;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float3 normal = normalize(i.normal);
                float3 eye = normalize(-i.pos.xyz);
                float rim = smoothstep( _Start, _End,1.0 - dot(normal, eye));
                return float4 ( clamp(rim, 0.0,1.0) * _Alpha * _Color.xyz,1.0);
            }
            ENDCG

        }
    }
}
