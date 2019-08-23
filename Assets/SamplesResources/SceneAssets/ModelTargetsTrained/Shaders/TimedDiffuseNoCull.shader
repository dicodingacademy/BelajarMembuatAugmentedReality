Shader "Custom/TimedDiffuseNoCull" {
    Properties{
        _BlinkColor("Blink Color", Color) = (0.0,1.0,0.0,1.0)
        _BlinkFrequency("Blink Frequency", Float) = 2.0
    }

    SubShader{
        Tags{ "Queue" = "Geometry" "RenderType" = "Transparent" }

        Cull Off

        CGPROGRAM

        #pragma surface surf Lambert alpha:fade
        struct Input {
            float2 uv_MainTex;
        };

        half4 _BlinkColor;
        float _BlinkFrequency;

        void surf(Input IN, inout SurfaceOutput o) {
            half wave = 0.7 + 0.3 * sin(_BlinkFrequency * 6.28 * _Time.y);
            o.Albedo = _BlinkColor.rgb * wave;
            o.Emission = 0.5 * o.Albedo;
            o.Alpha = _BlinkColor.a;
        }

        ENDCG
    }
        
    Fallback "Diffuse"
}