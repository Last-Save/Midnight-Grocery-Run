Shader "Custom/URP_SurfaceWithBakedSkinnedAnimation"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _BumpScale("Normal Scale", Float) = 1.0
        _Specular ("Specular", 2D) = "black" {}
        _Smoothness ("Smoothness", Range(0,1)) = 0.5
        _AnimationPos("Baked Position Animation Texture", 2D) = "black" {}
        _AnimationNm("Baked Normal Animation Texture", 2D) = "black" {}
        _Speed("Animation Speed", float) = 60
        _Length("Animation Length", float) = 300
        _ManualFrame("Animation Frame", float) = 300
        [Toggle(MANUAL)] _UseManual("Manual", Float) = 0

        _NoiseTiling("Offset noise tiling", Vector) = (1,1,1,1)

        _SpeedNoiseTiling("Speed noise tiling", Vector) = (1,1,1,1)
        _SpeedMinMax("Speed min max", Vector) = (1,1,1,1)

        _ScaleNoiseTiling("Scale noise tiling", Vector) = (1,1,1,1)
        _ScaleMinMax("Scale min max", Vector) = (1,1,1,1)
        
        // URP Standard properties
        [HideInInspector] _Surface("__surface", Float) = 0.0
        [HideInInspector] _Blend("__blend", Float) = 0.0
        [HideInInspector] _AlphaClip("__clip", Float) = 0.0
        [HideInInspector] _SrcBlend("__src", Float) = 1.0
        [HideInInspector] _DstBlend("__dst", Float) = 0.0
        [HideInInspector] _ZWrite("__zw", Float) = 1.0
        [HideInInspector] _Cull("__cull", Float) = 2.0
    }
    
    SubShader
    {
        Tags 
        { 
            "RenderType" = "Opaque" 
            "RenderPipeline" = "UniversalPipeline"
            "Queue" = "Geometry"
        }
        
        LOD 200
        
        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }
            
            Blend[_SrcBlend][_DstBlend]
            ZWrite[_ZWrite]
            Cull[_Cull]
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile _ _SHADOWS_SOFT
            #pragma multi_compile _ _SCREEN_SPACE_OCCLUSION
            #pragma multi_compile _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
            #pragma multi_compile _ _REFLECTION_PROBE_BLENDING
            #pragma multi_compile _ _REFLECTION_PROBE_BOX_PROJECTION
            #pragma multi_compile _ _LIGHT_LAYERS
            #pragma multi_compile _ _LIGHT_COOKIES
            #pragma multi_compile _ _CLUSTERED_RENDERING
            
            #pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
            #pragma multi_compile _ SHADOWS_SHADOWMASK
            #pragma multi_compile _ DIRLIGHTMAP_COMBINED
            #pragma multi_compile _ LIGHTMAP_ON
            #pragma multi_compile _ DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            
            #pragma multi_compile_instancing
            #pragma instancing_options assumeuniformscaling
            
            #pragma shader_feature_local MANUAL
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
            
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            
            TEXTURE2D(_NormalMap);
            SAMPLER(sampler_NormalMap);
            
            TEXTURE2D(_Specular);
            SAMPLER(sampler_Specular);
            
            TEXTURE2D(_AnimationPos);
            SAMPLER(sampler_AnimationPos);
            
            TEXTURE2D(_AnimationNm);
            SAMPLER(sampler_AnimationNm);
            
            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _NormalMap_ST;
                float4 _Color;
                float4 _AnimationPos_TexelSize;
                float4 _NoiseTiling;
                float4 _SpeedNoiseTiling;
                float4 _SpeedMinMax;
                float4 _ScaleNoiseTiling;
                float4 _ScaleMinMax;
                float _Speed;
                float _Length;
                float _ManualFrame;
                float _Smoothness;
                float _BumpScale;
            CBUFFER_END
            
            UNITY_INSTANCING_BUFFER_START(Props)
                UNITY_DEFINE_INSTANCED_PROP(float4, _LocalPosition)
                UNITY_DEFINE_INSTANCED_PROP(float4, _InstancedColor)
            UNITY_INSTANCING_BUFFER_END(Props)
            
            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
                float2 texcoord : TEXCOORD0;
                float2 lightmapUV : TEXCOORD1;
                uint vertexID : SV_VertexID;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 positionWS : TEXCOORD1;
                float3 normalWS : TEXCOORD2;
                float4 tangentWS : TEXCOORD3;
                float4 shadowCoord : TEXCOORD4;
                DECLARE_LIGHTMAP_OR_SH(lightmapUV, vertexSH, 5);
                float fogFactor : TEXCOORD6;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };
            
            float remap(float s, float a1, float a2, float b1, float b2)
            {
                return b1 + (s-a1)*(b2-b1)/(a2-a1);
            }
            
            void ApplyBakedAnimation(inout Attributes input)
            {
                float framesCount = _AnimationPos_TexelSize.z;
                float verticesCount = _AnimationPos_TexelSize.w;
                
                float3 randomOffset = abs(UNITY_ACCESS_INSTANCED_PROP(Props, _LocalPosition).xyz);
                
                float _frame;
                #if MANUAL
                    _frame = _ManualFrame;
                #else
                    float3 speedOffset = (randomOffset * _SpeedNoiseTiling.xyz);
                    float speedRandomOffset = (speedOffset.x + speedOffset.y + speedOffset.z) % (_SpeedMinMax.y - _SpeedMinMax.x);
                    speedRandomOffset += _SpeedMinMax.x;
                    
                    _frame = (_Speed * speedRandomOffset) * _Time.y;
                    
                    float3 frameOffset = (randomOffset * _NoiseTiling.xyz);
                    _frame += frameOffset.x + frameOffset.y + frameOffset.z;
                    
                    _frame = _frame % _Length;
                #endif
                
                float3 scaleOffset = randomOffset * _ScaleNoiseTiling.xyz;
                float scaleRandomOffset = (scaleOffset.x + scaleOffset.y + scaleOffset.z) % (_ScaleMinMax.y - _ScaleMinMax.x);
                scaleRandomOffset += _ScaleMinMax.x;
                
                float _vertexId = (float)input.vertexID;
                
                float3 offset = SAMPLE_TEXTURE2D_LOD(_AnimationPos, sampler_AnimationPos, 
                    float2(_frame / framesCount, (_vertexId + 0.5) / verticesCount), 0).xyz;
                float3 normal = SAMPLE_TEXTURE2D_LOD(_AnimationNm, sampler_AnimationNm, 
                    float2(_frame / framesCount, (_vertexId + 0.5) / verticesCount), 0).xyz;
                
                input.positionOS.xyz = offset * scaleRandomOffset;
                input.normalOS.xyz = normal;
            }
            
            Varyings vert(Attributes input)
            {
                Varyings output = (Varyings)0;
                
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_TRANSFER_INSTANCE_ID(input, output);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
                
                ApplyBakedAnimation(input);
                
                VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
                VertexNormalInputs normalInput = GetVertexNormalInputs(input.normalOS, input.tangentOS);
                
                output.positionCS = vertexInput.positionCS;
                output.positionWS = vertexInput.positionWS;
                output.uv = TRANSFORM_TEX(input.texcoord, _MainTex);
                output.normalWS = normalInput.normalWS;
                output.tangentWS = float4(normalInput.tangentWS.xyz, input.tangentOS.w * GetOddNegativeScale());
                
                OUTPUT_LIGHTMAP_UV(input.lightmapUV, unity_LightmapST, output.lightmapUV);
                OUTPUT_SH(output.normalWS.xyz, output.vertexSH);
                
                output.fogFactor = ComputeFogFactor(vertexInput.positionCS.z);
                output.shadowCoord = GetShadowCoord(vertexInput);
                
                return output;
            }
            
            half4 frag(Varyings input) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
                
                half4 albedoTexture = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv);
                half4 specularTexture = SAMPLE_TEXTURE2D(_Specular, sampler_Specular, input.uv);
                
                half4 color = albedoTexture * _Color * UNITY_ACCESS_INSTANCED_PROP(Props, _InstancedColor);
                
                half3 normalTS = UnpackNormalScale(SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, input.uv), _BumpScale);
                float3 bitangent = cross(input.normalWS, input.tangentWS.xyz) * input.tangentWS.w;
                float3x3 tangentToWorld = float3x3(input.tangentWS.xyz, bitangent, input.normalWS);
                float3 normalWS = TransformTangentToWorld(normalTS, tangentToWorld);
                normalWS = normalize(normalWS);
                
                half smoothness = _Smoothness * specularTexture.a;
                half3 specular = specularTexture.rgb;
                
                InputData inputData = (InputData)0;
                inputData.positionWS = input.positionWS;
                inputData.normalWS = normalWS;
                inputData.viewDirectionWS = GetWorldSpaceNormalizeViewDir(input.positionWS);
                inputData.shadowCoord = input.shadowCoord;
                inputData.fogCoord = input.fogFactor;
                inputData.vertexLighting = half3(0, 0, 0);
                inputData.bakedGI = SAMPLE_GI(input.lightmapUV, input.vertexSH, inputData.normalWS);
                inputData.normalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(input.positionCS);
                inputData.shadowMask = SAMPLE_SHADOWMASK(input.lightmapUV);
                
                SurfaceData surfaceData = (SurfaceData)0;
                surfaceData.albedo = color.rgb;
                surfaceData.specular = specular;
                surfaceData.metallic = 0;
                surfaceData.smoothness = smoothness;
                surfaceData.normalTS = normalTS;
                surfaceData.emission = 0;
                surfaceData.occlusion = 1;
                surfaceData.alpha = color.a;
                
                half4 finalColor = UniversalFragmentBlinnPhong(inputData, surfaceData);
                finalColor.rgb = MixFog(finalColor.rgb, inputData.fogCoord);
                
                return finalColor;
            }
            ENDHLSL
        }
        
        Pass
        {
            Name "ShadowCaster"
            Tags{"LightMode" = "ShadowCaster"}

            ZWrite On
            ZTest LEqual
            ColorMask 0
            Cull[_Cull]

            HLSLPROGRAM
            #pragma target 2.0

            #pragma vertex vert
            #pragma fragment frag
            
            #pragma multi_compile_instancing
            #pragma shader_feature_local MANUAL

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
            
            TEXTURE2D(_AnimationPos);
            SAMPLER(sampler_AnimationPos);
            
            TEXTURE2D(_AnimationNm);
            SAMPLER(sampler_AnimationNm);
            
            CBUFFER_START(UnityPerMaterial)
                float4 _AnimationPos_TexelSize;
                float4 _NoiseTiling;
                float4 _SpeedNoiseTiling;
                float4 _SpeedMinMax;
                float4 _ScaleNoiseTiling;
                float4 _ScaleMinMax;
                float _Speed;
                float _Length;
                float _ManualFrame;
            CBUFFER_END
            
            UNITY_INSTANCING_BUFFER_START(Props)
                UNITY_DEFINE_INSTANCED_PROP(float4, _LocalPosition)
                UNITY_DEFINE_INSTANCED_PROP(float4, _InstancedColor)
            UNITY_INSTANCING_BUFFER_END(Props)
            
            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                uint vertexID : SV_VertexID;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };
            
            void ApplyBakedAnimation(inout Attributes input)
            {
                float framesCount = _AnimationPos_TexelSize.z;
                float verticesCount = _AnimationPos_TexelSize.w;
                
                float3 randomOffset = abs(UNITY_ACCESS_INSTANCED_PROP(Props, _LocalPosition).xyz);
                
                float _frame;
                #if MANUAL
                    _frame = _ManualFrame;
                #else
                    float3 speedOffset = (randomOffset * _SpeedNoiseTiling.xyz);
                    float speedRandomOffset = (speedOffset.x + speedOffset.y + speedOffset.z) % (_SpeedMinMax.y - _SpeedMinMax.x);
                    speedRandomOffset += _SpeedMinMax.x;
                    
                    _frame = (_Speed * speedRandomOffset) * _Time.y;
                    
                    float3 frameOffset = (randomOffset * _NoiseTiling.xyz);
                    _frame += frameOffset.x + frameOffset.y + frameOffset.z;
                    
                    _frame = _frame % _Length;
                #endif
                
                float3 scaleOffset = randomOffset * _ScaleNoiseTiling.xyz;
                float scaleRandomOffset = (scaleOffset.x + scaleOffset.y + scaleOffset.z) % (_ScaleMinMax.y - _ScaleMinMax.x);
                scaleRandomOffset += _ScaleMinMax.x;
                
                float _vertexId = (float)input.vertexID;
                
                float3 offset = SAMPLE_TEXTURE2D_LOD(_AnimationPos, sampler_AnimationPos, 
                    float2(_frame / framesCount, (_vertexId + 0.5) / verticesCount), 0).xyz;
                float3 normal = SAMPLE_TEXTURE2D_LOD(_AnimationNm, sampler_AnimationNm, 
                    float2(_frame / framesCount, (_vertexId + 0.5) / verticesCount), 0).xyz;
                
                input.positionOS.xyz = offset * scaleRandomOffset;
                input.normalOS.xyz = normal;
            }
            
            Varyings vert(Attributes input)
            {
                Varyings output = (Varyings)0;
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_TRANSFER_INSTANCE_ID(input, output);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

                ApplyBakedAnimation(input);

                VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
                VertexNormalInputs normalInput = GetVertexNormalInputs(input.normalOS, float4(0, 0, 0, 0));

                Light mainLight = GetMainLight();
                float3 lightDirectionWS = mainLight.direction;

                output.positionCS = TransformWorldToHClip(ApplyShadowBias(vertexInput.positionWS, normalInput.normalWS, lightDirectionWS));

                #if UNITY_REVERSED_Z
                    output.positionCS.z = min(output.positionCS.z, UNITY_NEAR_CLIP_VALUE);
                #else
                    output.positionCS.z = max(output.positionCS.z, UNITY_NEAR_CLIP_VALUE);
                #endif

                return output;
            }
            
            half4 frag(Varyings input) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
                return 0;
            }
            ENDHLSL
        }

        Pass
        {
            Name "DepthOnly"
            Tags{"LightMode" = "DepthOnly"}

            ZWrite On
            ColorMask 0
            Cull[_Cull]

            HLSLPROGRAM
            #pragma target 2.0

            #pragma vertex vert
            #pragma fragment frag
            
            #pragma multi_compile_instancing
            #pragma shader_feature_local MANUAL

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            TEXTURE2D(_AnimationPos);
            SAMPLER(sampler_AnimationPos);
            
            TEXTURE2D(_AnimationNm);
            SAMPLER(sampler_AnimationNm);
            
            CBUFFER_START(UnityPerMaterial)
                float4 _AnimationPos_TexelSize;
                float4 _NoiseTiling;
                float4 _SpeedNoiseTiling;
                float4 _SpeedMinMax;
                float4 _ScaleNoiseTiling;
                float4 _ScaleMinMax;
                float _Speed;
                float _Length;
                float _ManualFrame;
            CBUFFER_END
            
            UNITY_INSTANCING_BUFFER_START(Props)
                UNITY_DEFINE_INSTANCED_PROP(float4, _LocalPosition)
                UNITY_DEFINE_INSTANCED_PROP(float4, _InstancedColor)
            UNITY_INSTANCING_BUFFER_END(Props)
            
            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                uint vertexID : SV_VertexID;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };
            
            void ApplyBakedAnimation(inout Attributes input)
            {
                float framesCount = _AnimationPos_TexelSize.z;
                float verticesCount = _AnimationPos_TexelSize.w;
                
                float3 randomOffset = abs(UNITY_ACCESS_INSTANCED_PROP(Props, _LocalPosition).xyz);
                
                float _frame;
                #if MANUAL
                    _frame = _ManualFrame;
                #else
                    float3 speedOffset = (randomOffset * _SpeedNoiseTiling.xyz);
                    float speedRandomOffset = (speedOffset.x + speedOffset.y + speedOffset.z) % (_SpeedMinMax.y - _SpeedMinMax.x);
                    speedRandomOffset += _SpeedMinMax.x;
                    
                    _frame = (_Speed * speedRandomOffset) * _Time.y;
                    
                    float3 frameOffset = (randomOffset * _NoiseTiling.xyz);
                    _frame += frameOffset.x + frameOffset.y + frameOffset.z;
                    
                    _frame = _frame % _Length;
                #endif
                
                float3 scaleOffset = randomOffset * _ScaleNoiseTiling.xyz;
                float scaleRandomOffset = (scaleOffset.x + scaleOffset.y + scaleOffset.z) % (_ScaleMinMax.y - _ScaleMinMax.x);
                scaleRandomOffset += _ScaleMinMax.x;
                
                float _vertexId = (float)input.vertexID;
                
                float3 offset = SAMPLE_TEXTURE2D_LOD(_AnimationPos, sampler_AnimationPos, 
                    float2(_frame / framesCount, (_vertexId + 0.5) / verticesCount), 0).xyz;
                float3 normal = SAMPLE_TEXTURE2D_LOD(_AnimationNm, sampler_AnimationNm, 
                    float2(_frame / framesCount, (_vertexId + 0.5) / verticesCount), 0).xyz;
                
                input.positionOS.xyz = offset * scaleRandomOffset;
                input.normalOS.xyz = normal;
            }
            
            Varyings vert(Attributes input)
            {
                Varyings output = (Varyings)0;
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_TRANSFER_INSTANCE_ID(input, output);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

                ApplyBakedAnimation(input);

                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                return output;
            }
            
            half4 frag(Varyings input) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
                return 0;
            }
            ENDHLSL
        }
    }
    
    FallBack "Hidden/Universal Render Pipeline/FallbackError"
}