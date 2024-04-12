Shader "Custom/GreyNoise"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseIntensity ("Noise Intensity", Range(0, 10)) = 0.5
        _Sharpness ("Sharpness", Range(1, 20)) = 1
        _Speed ("Speed", Range(0.001, 0.1)) = 0.02
    }
    SubShader
    {
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
            float4 _MainTex_ST;
            float _NoiseIntensity;
            float _Sharpness;
            float _Speed;
            float _Seed = 43758.5453;
            //float _Time;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Scale the UV coordinates to zoom in on the noise
                float2 scaledUV = i.uv * 0.1;

                _Seed = 43758.5453 * (2 + _SinTime) * _Speed;

                // Generate noise based on UV coordinates and time

               // float noise = frac(sin(dot(scaledUV ,float2(12.9898,78.233))) * _Seed);

                //float noise = frac(sin(dot(scaledUV ,float2(12.9898,78.233))) * 43758.5453);
               float noise = frac(sin(dot(scaledUV + (_SinTime / 10) ,float2(12.9898,78.233))) * 43758.5453);
                //float noise = frac(sin(dot(scaledUV + unity_DeltaTime ,float2(12.9898,78.233))) * 43758.5453);

                // Scale the noise by the intensity
                noise *= _NoiseIntensity;

                // Adjust the sharpness of the noise
                noise = pow(noise, _Sharpness);

                return fixed4(noise, noise, noise, 1.0);
            }
            ENDCG
        }
    }
}
