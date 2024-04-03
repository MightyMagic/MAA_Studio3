Shader "Custom/CubeOutlineAndTexture"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _EdgeColor ("Edge Color", Color) = (1,1,1,1)
        _OutlineWidth ("Outline Width", Range(0.0, 0.1)) = 0.05
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 100
        
        CGPROGRAM
        #pragma surface surf Lambert
        
        struct Input
        {
            float2 uv_MainTex;
            float3 worldNormal;
        };
        
        sampler2D _MainTex;
        fixed4 _EdgeColor;
        float _OutlineWidth;
        
        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 texColor = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = texColor.rgb;
            o.Alpha = texColor.a;
            
            // Calculate edge
            float edge = saturate(1.0 - length(IN.worldNormal));
            o.Albedo = lerp(o.Albedo, _EdgeColor.rgb, edge * _OutlineWidth);
            o.Alpha = lerp(o.Alpha, _EdgeColor.a, edge * _OutlineWidth);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
