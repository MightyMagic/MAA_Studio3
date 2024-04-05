Shader "Custom/EdgeDetectionWithTexture"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _EdgeColor ("Edge Color", Color) = (1,0,0,1)
        _EdgeWidth ("Edge Width", Range(0, 1)) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            uniform sampler2D _MainTex;
            uniform float4 _EdgeColor;
            uniform float _EdgeWidth;
            
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
            
            fixed4 frag (v2f i) : SV_Target
            {
                float2 texelSize = 1.0 / _ScreenParams.xy;
                
                float2 ddxUV = ddx(i.uv);
                float2 ddyUV = ddy(i.uv);
                
                float3 color = tex2D(_MainTex, i.uv).rgb;
                
                float edge = length(tex2D(_MainTex, i.uv + texelSize) - tex2D(_MainTex, i.uv - texelSize));
                edge += length(tex2D(_MainTex, i.uv + ddxUV) - tex2D(_MainTex, i.uv - ddxUV));
                edge += length(tex2D(_MainTex, i.uv + ddyUV) - tex2D(_MainTex, i.uv - ddyUV));
                
                edge /= 3.0;
                
                float mask = saturate(edge * _EdgeWidth);
                
                return half4(lerp(color, _EdgeColor.rgb, mask), 1.0);
            }
            ENDCG
        }
    }
}
