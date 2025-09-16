Shader "Custom/NightVisionCCTV"
{
    Properties
    {
        _MainTex("Base (Render Texture)", 2D) = "white" {}
        _NoiseTex("Noise Texture", 2D) = "white" {}
        _NoiseIntensity("Noise Intensity", Range(0, 0.3)) = 0.05
        _Brightness("Brightness", Range(0.5, 2)) = 1
        _GreenTint("Green Tint", Color) = (0,1,0,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }
        LOD 100

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            
            TEXTURE2D(_NoiseTex);
            SAMPLER(sampler_NoiseTex);

            float _NoiseIntensity;
            float _Brightness;
            float4 _GreenTint;

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS);
                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                // Sample scene
                float3 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv).rgb;

                // Grayscale conversion
                float gray = dot(col, float3(0.3,0.59,0.11));

                // Apply green tint
                float3 greenCol = gray * _GreenTint.rgb;

                // Sample noise
                float noise = SAMPLE_TEXTURE2D(_NoiseTex, sampler_NoiseTex, IN.uv).r;
                greenCol += noise * _NoiseIntensity;

                // Apply brightness
                greenCol *= _Brightness;

                return float4(greenCol, 1);
            }
            ENDHLSL
        }
    }
}
