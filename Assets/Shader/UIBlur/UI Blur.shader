Shader "UI/UI Blur"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Size("Size", Range(0, 10)) = 1
	}
	SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float4 _MainTex_ST;
            
            float _Size;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            half4 frag (v2f input) : COLOR
            {
                float2 res = _MainTex_TexelSize.xy;
                float i = _Size;
    
                half4 col; 
                UNITY_INITIALIZE_OUTPUT(half4, col);
                col.rgb = tex2D( _MainTex, input.uv ).rgb;
                col.rgb += tex2D( _MainTex, input.uv + float2( i, i ) * res ).rgb;
                col.rgb += tex2D( _MainTex, input.uv + float2( i, -i ) * res ).rgb;
                col.rgb += tex2D( _MainTex, input.uv + float2( -i, i ) * res ).rgb;
                col.rgb += tex2D( _MainTex, input.uv + float2( -i, -i ) * res ).rgb;
                col.rgb /= 5.0f;
                
                return col;
            }
            ENDCG
        }
	}
}

