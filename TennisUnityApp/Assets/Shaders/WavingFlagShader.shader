Shader "Custom/HardcodedRomanianFlag"
{
    Properties
    {
        _WaveAmplitude ("Wave Amplitude", Float) = 0.1
        _WaveFrequency ("Wave Frequency", Float) = 2.0
        _WaveSpeed ("Wave Speed", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Transparent" }
        LOD 100

        Cull Off // Turn off face culling

        CGPROGRAM
        #pragma surface surf Standard vertex:vert
        #pragma target 3.0

        struct Input
        {
            float2 uv_MainTex;
        };

        float _WaveAmplitude;
        float _WaveFrequency;
        float _WaveSpeed;

        void vert (inout appdata_full v)
        {
            float wave = sin(_WaveFrequency * (v.vertex.x + _Time.y * _WaveSpeed));
            v.vertex.y += wave * _WaveAmplitude;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float3 color = float3(1, 0, 0); // Default to red
            if (IN.uv_MainTex.x < 1.0 / 3.0)
                color = float3(0, 0, 1); // Blue
            else if (IN.uv_MainTex.x < 2.0 / 3.0)
                color = float3(1, 1, 0); // Yellow

            o.Albedo = color;
            o.Alpha = 1.0;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
