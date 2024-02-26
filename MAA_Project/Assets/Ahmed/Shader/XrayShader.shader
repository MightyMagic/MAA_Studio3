Shader "Custom/UnlitXRayWithGlowAndSmokeAndMovementAndWater"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _XRayColor ("X-Ray Color", Color) = (1,0,0,1)
        _XRayStrength ("X-Ray Strength", Range(0,1)) = 0.5
        _GlowColor ("Glow Color", Color) = (0,1,1,1)
        _GlowStrength ("Glow Strength", Range(0,1)) = 0.5
        _SmokeColor ("Smoke Color", Color) = (0.5,0.5,0.5,1)
        _SmokeStrength ("Smoke Strength", Range(0,1)) = 0.5
        _Speed ("Movement Speed", float) = 1.0
        _WaterStrength ("Water Strength", float) = 0.1
        _WaterFrequency ("Water Frequency", float) = 2.0
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
            };
            
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            
            sampler2D _MainTex;
            float4 _Color;
            float4 _XRayColor;
            float _XRayStrength;
            float4 _GlowColor;
            float _GlowStrength;
            float4 _SmokeColor;
            float _SmokeStrength;
            float _Speed;
            float _WaterStrength;
            float _WaterFrequency;
            
            v2f vert (appdata v)
            {
                v2f o;
                // Apply movement to UV coordinates
                o.uv = v.uv + float2(sin(_Time.y * _WaterFrequency + v.vertex.x * _WaterStrength),
                                     cos(_Time.y * _WaterFrequency + v.vertex.y * _WaterStrength)) * _WaterStrength;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, i.uv) * _Color;
                fixed4 xRayColor = _XRayColor * _XRayStrength;
                fixed4 glowColor = _GlowColor * _GlowStrength;
                
                // Simulate smoke effect
                float smoke = smoothstep(0.5, 0.0, length(i.uv - 0.5)) * _SmokeStrength;
                fixed4 smokeColor = _SmokeColor * smoke;
                
                // Combine all effects
                fixed4 finalColor = texColor + xRayColor + glowColor + smokeColor;
                
                // Apply glow intensity
                finalColor.rgb *= (1.0 + _GlowStrength);
                
                // Clamp the color to maintain HDR range
                finalColor = clamp(finalColor, 0.0, 1.0);
                
                return finalColor;
            }
            ENDCG
        }
    }
}
