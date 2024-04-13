Shader "Custom/EyesClosed/EdgeDetection"
{
   Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _EdgeColor ("Edge Color", Color) = (1,0,0,1) // Add this line
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
            fixed4 _EdgeColor; // Add this line

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 size = 1.0 / _ScreenParams.xy;
                float3x3 G[2] = {
                    { -1, 0, 1,
                      -2, 0, 2,
                      -1, 0, 1 },
                    { -1,-2,-1,
                       0, 0, 0,
                       1, 2, 1 }
                };
                float4 tex[3][3];
                for (int y = 0; y < 3; ++y)
                {
                    for (int x = 0; x < 3; ++x)
                    {
                        tex[y][x] = tex2D(_MainTex, i.uv + size * float2(x-1,y-1));
                    }
                }
                float4 edge = 0;
                for (int y = 0; y < 3; ++y)
                {
                    for (int x = 0; x < 3; ++x)
                    {
                        edge.rgb += tex[y][x].rgb * G[0][y][x];
                        edge.a += tex[y][x].a * G[1][y][x];
                    }
                }
                // If an edge is detected, color it with _EdgeColor
                if (length(edge.rgb) > 0.1)
                {
                    return _EdgeColor; // Use the edge color
                }
                else
                {
                    return fixed4(0, 0, 0, 0); // Transparent color
                }
            }
            ENDCG
        }
    }
}