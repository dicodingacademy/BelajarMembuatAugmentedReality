/*========================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/
Shader "Custom/Scan" {
    Properties{
        _Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _MainTex("Texture", 2D) = "white" {}
        _ScanTime("Scan Time", Float) = 0.0
        _ScanSpeed("Scan Speed", Float) = 0.2
        _WaveLength("Wave Length", Float) = 0.2
    }

    SubShader {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        Cull Back
        ZWrite On
        ZTest LEqual
        
        CGPROGRAM
        #pragma surface surf Lambert alpha:fade
        struct Input {
            float2 uv_MainTex;
            float3 worldPos;
        };

        uniform sampler2D _MainTex;
        uniform half4 _Color;
        uniform float _ScanTime;
        uniform float _ScanSpeed;
        uniform float _WaveLength;

        void surf(Input IN, inout SurfaceOutput o) 
        {
            float vel = _ScanSpeed;
            float wave = 0.5 + 0.5*cos((6.28 / _WaveLength) * (IN.worldPos.y - vel*_ScanTime));
            float lum = smoothstep(0.0, 1.0, (wave - 0.995) / 0.005);
            o.Albedo = _Color.rgb * (1.0 + lum) * tex2D(_MainTex, IN.uv_MainTex).rgb;
            o.Alpha = _Color.a * lum;
        }
        ENDCG
    }

    Fallback "Diffuse"
}