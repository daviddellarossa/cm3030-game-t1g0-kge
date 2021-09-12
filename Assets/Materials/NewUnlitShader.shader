Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _Color ( "Color", Color ) = (1, 1, 1, 0)
        _Gloss ( "Gloss", Float ) = 1
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
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
                float3 worldPos : TEXCOOD2;
            };

            float4 _Color;
            float _Gloss;
            

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = v.normal;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 normal = normalize(i.normal);

                // Lighting
                float3 lightDir = _WorldSpaceLightPos0.xyz; // the light direction
                float3 lightColor = _LightColor0.rgb;
                
                // Ambient Light
                float3 ambientLight = float3(0.2, 0.2, 0.2);
                
                // Direct diffuse light
                float diffuseFalloff = max(0, dot( lightDir, i.normal)); // the dot product between the light and the normal, minimum 0
                float3 directDiffuseLight = lightColor * diffuseFalloff; // multiplied by the color (assuming intensity is 1)

                // Direct specular light
                float3 camPos = _WorldSpaceCameraPos;
                float3 fragToCam = camPos - i.worldPos;
                float3 viewDir = normalize(fragToCam);
                float3 viewReflect = reflect(-viewDir, normal);
                float specularFalloff = max(0, dot(viewReflect, lightDir));
                specularFalloff = pow(specularFalloff, _Gloss);
                float3 directSpecularLight = lightColor * specularFalloff;

                // Composite
                // float3 finalColor = (ambientLight + directDiffuseLight) * _Color.rgb + directSpecularLight;
                float3 finalColor = (ambientLight + directDiffuseLight) * _Color.rgb;

                return float4(finalColor, 0);
            }
            ENDCG
        }
    }
}
