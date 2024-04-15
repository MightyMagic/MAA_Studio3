Shader "Custom/OutlineNew" {
    Properties {
        _MainTex ("Base Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0, 0, 1, 1) // Blue outline
        _OutlineThickness ("Outline Thickness", Range(0, 1)) = 0.05
    }

    SubShader {
        Tags { "RenderType"="Opaque" }
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            uniform sampler2D _MainTex;
            float4 _OutlineColor;
            float _OutlineThickness;

            struct appdata_t {
                float4 vertex : POSITION;
            };

            struct v2f {
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata_t v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            half4 frag(v2f i) : SV_Target {
                // Sample the on-screen image
                half4 baseColor = tex2D(_MainTex, i.pos.xy / i.pos.w);

                // Calculate the outline
                half4 outline = baseColor;
                outline.rgb = _OutlineColor.rgb;
                outline.a *= step(_OutlineThickness, 0.5);

 
                return lerp(baseColor, outline, outline.a);
            }
            ENDCG
        }
    }
}
