Shader "Unlit/FresnelOutliner"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _RimLightColor("RimLight Color", Color) = (1, 1, 1, 1)
        _RimRange("Rim Range", Range(0,10)) = 2.5
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


            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float3 viewDir : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _RimLightColor;
            float _RimRange;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = WorldSpaceViewDir(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                fixed3 normalizedNormal = normalize(i.normal);
                float3 normalizedViewDir = normalize(i.viewDir);

                float4 rimDot = _RimRange - dot(normalizedNormal, normalizedViewDir);

                //float4 out = 

                rimDot = rimDot > 0 ? 1 : 0;

                float rimLight = _RimLightColor * rimDot;

                return col - rimLight;
            }
            ENDCG
        }
    }
}
