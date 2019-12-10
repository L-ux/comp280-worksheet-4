Shader "Custom/Radar Shader"
{
    Properties
    {
        _Color ("Floor Color", Color) = (1,1,1,1)
        _LitColor ("Lit Radar Color", Color) = (1,1,1,1)
        _DullColor ("Dull Radar Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _RadarDistance ("Radar Distance", Range(0,7)) = 1.0
        _RadarWidth ("Radar Width", Range(0,1)) = 0.3
        _RadarLocation ("Radar Location", Vector) = (0,0,0,0)
        _EnemyDirection ("Enemy Direction", Vector) = (1,0,0,0)
        _RadarChunkWidth ("Radar Segment Width", Range(0,1)) = 0.2
        _EnemyAngle ("Enemy Angle", float) = 0
        _EnemyIsRight ("Enemy Rotation Direction", float ) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        fixed4 _LitColor;
        fixed4 _DullColor;
        float _RadarDistance;
        float _RadarWidth;
        fixed4 _RadarLocation;
        fixed4 _EnemyDirection;
        float _RadarChunkWidth;
        float _EnemyAngle;
        float _EnemyIsRight;


        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        fixed4 colorBlend(fixed4 firstColour, fixed4 secondColour, float LerpValue)
        {
            fixed4 outcolor = (0,0,0,1);
            outcolor.r = firstColour.r * (1-LerpValue ) + secondColour.r * LerpValue;
            outcolor.g = firstColour.g * (1-LerpValue ) + secondColour.g * LerpValue;
            outcolor.b = firstColour.b * (1-LerpValue ) + secondColour.b * LerpValue;
            return outcolor;
        }

        float doDotProduct(fixed4 vec1, fixed4 vec2)
        {
            return (vec1.x * vec2.x + vec1.z * vec2.z);
        }

        bool doCrossRight(fixed4 vec1, fixed4 vec2) // return 1 if true, 0 is false
        {
            fixed4 cross = ((vec1.y * vec2.z - vec2.y * vec1.z),
                            (vec1.z * vec2.x - vec2.z - vec1.x),
                            (vec1.x * vec2.y - vec2.x - vec1.y),
                            0);
            return cross.y > 1;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float dist = length(IN.worldPos.xyz - _RadarLocation.xyz) - _RadarDistance;
            float upper = dist + (_RadarWidth/2);
            float lower = dist - (_RadarWidth/2);

            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

            // to find whether it is within the radar or not
            bool isWithinRadar = (lower < 0 && upper > 0);

            // to find whether the direction to the enemy is correct

            fixed4 wat = (IN.worldPos.x,0,IN.worldPos.y,0);
            float indotdot = doDotProduct(wat, _EnemyDirection);

            bool isWithinDot = (indotdot < _EnemyAngle + 0.5 && indotdot > _EnemyAngle - 0.5);
            bool isSameDirection = doCrossRight(wat, _EnemyDirection);

            bool totalRadarDir = /*isWithinDot **/ isSameDirection;


            c.rgb = colorBlend(_Color,  colorBlend(_DullColor, _LitColor, totalRadarDir), isWithinRadar);

            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }

        ENDCG
    }
    FallBack "Diffuse"
}
