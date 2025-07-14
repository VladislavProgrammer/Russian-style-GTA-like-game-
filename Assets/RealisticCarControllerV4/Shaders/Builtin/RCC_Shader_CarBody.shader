// Made with Amplify Shader Editor v1.9.6.3
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "RCC Car Body Shader"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_Metallic("Metallic", Range( 0 , 1)) = 0.5
		_Glossiness("Glossiness", Range( 0 , 1.5)) = 0.925
		_MainTex("Diffuse Texture", 2D) = "white" {}
		_MetallicGlossMap("MetallicGlossMap", 2D) = "white" {}
		[Normal]_BumpMap("BumpMap", 2D) = "bump" {}
		_BumpScale("BumpScale", Range( 0 , 1)) = 0.5
		_Flakes("Flakes", 2D) = "white" {}
		_FlakesIntensity("Flakes Intensity", Range( 0 , 2)) = 1.75
		_FlakesRimPower("Flakes Rim Power", Range( 0 , 1)) = 0.75
		_FlakesPower("Flakes Power", Range( 0 , 10)) = 6
		_FresnelColor("Fresnel Color", Color) = (1,0,0,1)
		_FresnelPower("Fresnel Power", Range( 0 , 1)) = 0.35
		_CloudsReflection("Clouds Reflection", 2D) = "white" {}
		_CloudsSpeed("Clouds Speed", Range( 0 , 1)) = 0.02
		_CloudsIntensity("Clouds Intensity", Range( 0 , 1)) = 0
		_CloudsTiling("Clouds Tiling", Range( 0 , 10)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityStandardUtils.cginc"
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
			float3 worldNormal;
			INTERNAL_DATA
			float3 worldRefl;
		};

		uniform sampler2D _BumpMap;
		uniform float4 _BumpMap_ST;
		uniform float _BumpScale;
		uniform sampler2D _CloudsReflection;
		uniform float _CloudsTiling;
		uniform float _CloudsSpeed;
		uniform float _CloudsIntensity;
		uniform float4 _FresnelColor;
		uniform float4 _Color;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform float _FresnelPower;
		uniform float _FlakesIntensity;
		uniform sampler2D _Flakes;
		uniform float4 _Flakes_ST;
		uniform float _FlakesRimPower;
		uniform float _FlakesPower;
		uniform float _Metallic;
		uniform float _Glossiness;
		uniform sampler2D _MetallicGlossMap;
		uniform float4 _MetallicGlossMap_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_BumpMap = i.uv_texcoord * _BumpMap_ST.xy + _BumpMap_ST.zw;
			o.Normal = UnpackScaleNormal( tex2D( _BumpMap, uv_BumpMap ), _BumpScale );
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float fresnelNdotV305 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode305 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV305, 1.5 ) );
			float3 ase_worldReflection = WorldReflectionVector( i, float3( 0, 0, 1 ) );
			float2 temp_cast_1 = (1.0).xx;
			float mulTime292 = _Time.y * _CloudsSpeed;
			float cos276 = cos( mulTime292 );
			float sin276 = sin( mulTime292 );
			float2 rotator276 = mul( ase_worldReflection.xy - temp_cast_1 , float2x2( cos276 , -sin276 , sin276 , cos276 )) + temp_cast_1;
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			float fresnelNdotV223 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode223 = ( 0.1 + 1.0 * pow( 1.0 - fresnelNdotV223, _FresnelPower ) );
			float4 lerpResult221 = lerp( _FresnelColor , ( _Color * tex2D( _MainTex, uv_MainTex ) ) , fresnelNode223);
			o.Albedo = ( pow( ( pow( fresnelNode305 , 1.0 ) * ( tex2D( _CloudsReflection, ( _CloudsTiling * rotator276 ) ).a * _CloudsIntensity ) ) , 0.5 ) + lerpResult221 ).rgb;
			float2 uv_Flakes = i.uv_texcoord * _Flakes_ST.xy + _Flakes_ST.zw;
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float clampResult228 = clamp( ( 1.0 - _FlakesRimPower ) , 0.0 , 1.0 );
			float fresnelNdotV32 = dot( ase_worldNormal, ase_worldlightDir );
			float fresnelNode32 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV32, clampResult228 ) );
			float clampResult229 = clamp( ( 1.0 - fresnelNode32 ) , 0.0 , 1.0 );
			float4 temp_cast_3 = (_FlakesPower).xxxx;
			float4 temp_output_86_0 = pow( ( ( _FlakesIntensity * tex2D( _Flakes, uv_Flakes ) ) * clampResult229 ) , temp_cast_3 );
			o.Emission = temp_output_86_0.rgb;
			o.Metallic = _Metallic;
			float2 uv_MetallicGlossMap = i.uv_texcoord * _MetallicGlossMap_ST.xy + _MetallicGlossMap_ST.zw;
			o.Smoothness = ( temp_output_86_0 + ( _Glossiness * tex2D( _MetallicGlossMap, uv_MetallicGlossMap ) ) ).r;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float4 tSpace0 : TEXCOORD2;
				float4 tSpace1 : TEXCOORD3;
				float4 tSpace2 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.worldRefl = -worldViewDir;
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19603
Node;AmplifyShaderEditor.RangedFloatNode;33;-2046.282,-956.7606;Inherit;True;Property;_FlakesRimPower;Flakes Rim Power;9;0;Create;True;0;0;0;False;0;False;0.75;0.75;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;291;-1067.765,-3176.189;Inherit;False;Property;_CloudsSpeed;Clouds Speed;14;0;Create;True;0;0;0;False;0;False;0.02;0.02;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;292;-789.7668,-3175.189;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldReflectionVector;329;-815.2418,-3449.19;Inherit;False;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;227;-1754.272,-806.52;Inherit;False;Constant;_Float2;Float 2;11;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;226;-1755.273,-879.3205;Inherit;False;Constant;_Float1;Float 1;11;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;192;-1756.324,-956.1327;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;312;-764.2814,-3268.976;Inherit;False;Constant;_Float9;Float 9;16;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;276;-480.1846,-3394.473;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ClampOpNode;228;-1581.688,-935.7367;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;358;-576.9609,-3485.717;Inherit;False;Property;_CloudsTiling;Clouds Tiling;16;0;Create;True;0;0;0;False;0;False;1;2;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;357;-288.7709,-3484.624;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;300;-531.1687,-3657.64;Inherit;False;Constant;_Float7;Float 7;16;0;Create;True;0;0;0;False;0;False;1.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;32;-1396.743,-1031.487;Inherit;True;Standard;WorldNormal;LightDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;310;24.35742,-3499.132;Inherit;False;Constant;_Float8;Float 8;16;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;214;-1085.932,-1031.882;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;231;-1082.304,-877.1657;Inherit;False;Constant;_Float4;Float 2;11;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;230;-1084.311,-952.9845;Inherit;False;Constant;_Float3;Float 1;11;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;259;-103.7089,-3421.104;Inherit;True;Property;_CloudsReflection;Clouds Reflection;13;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.RangedFloatNode;295;-102.341,-3224.892;Inherit;False;Property;_CloudsIntensity;Clouds Intensity;15;0;Create;True;0;0;0;False;0;False;0;0.1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;305;-361.7883,-3754.686;Inherit;True;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;36;-1268.973,-1472.72;Inherit;False;Property;_FlakesIntensity;Flakes Intensity;8;0;Create;True;0;0;0;False;0;False;1.75;1.5;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;189;-1276.84,-1360.867;Inherit;True;Property;_Flakes;Flakes;7;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;209;-974.4586,-1378.722;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;309;268.3829,-3487.984;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;294;219.5316,-3378.551;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;229;-919.3292,-978.109;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;225;-952.6707,-2083.878;Inherit;False;Constant;_Float0;Float 0;11;0;Create;True;0;0;0;False;0;False;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;38;-924.879,-2317.658;Inherit;True;Property;_MainTex;Diffuse Texture;3;0;Create;False;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.RangedFloatNode;224;-1080.037,-2014.139;Inherit;False;Property;_FresnelPower;Fresnel Power;12;0;Create;True;0;0;0;False;0;False;0.35;0.2;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;58;-855.0099,-2489.855;Inherit;False;Property;_Color;Color;0;0;Create;True;0;0;0;False;0;False;1,1,1,1;0,0,0,1;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;308;449.0874,-3460.345;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;254;-602.306,-2448.745;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;220;-857.2408,-2661.021;Inherit;False;Property;_FresnelColor;Fresnel Color;11;0;Create;True;0;0;0;False;0;False;1,0,0,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.RangedFloatNode;356;450.9876,-3362.413;Inherit;False;Constant;_Float10;Float 10;16;0;Create;True;0;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;223;-794.012,-2130.765;Inherit;True;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;87;-975.869,-1599.086;Inherit;True;Property;_FlakesPower;Flakes Power;10;0;Create;True;0;0;0;False;0;False;6;10;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;213;-755.5118,-1198.859;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;60;-458.3636,-769.4246;Inherit;False;Property;_Glossiness;Glossiness;2;0;Create;True;0;0;0;False;0;False;0.925;0.925;0;1.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;359;-464.5139,-693.3469;Inherit;True;Property;_MetallicGlossMap;MetallicGlossMap;4;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.LerpOp;221;-416.7394,-2535.243;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;355;599.7495,-3451.12;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;360;-171.5829,-716.5071;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;86;-534.0361,-1428.468;Inherit;True;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;236;308.7722,-1373.638;Inherit;False;Property;_BumpScale;BumpScale;6;0;Create;True;0;0;0;False;0;False;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;284;-509.8345,-3229.387;Inherit;True;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;296;361.1226,-2558.786;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;370;-201.0568,-1232.023;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;183;-461.068,-847.4991;Inherit;False;Property;_Metallic;Metallic;1;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;242;586.2443,-1421.812;Inherit;True;Property;_BumpMap;BumpMap;5;1;[Normal];Create;True;0;0;0;False;0;False;-1;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;74.66112,-1447.005;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;RCC Car Body Shader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;;0;False;;False;0;False;;0;False;;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;0;0;False;;0;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;17;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;16;FLOAT4;0,0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;292;0;291;0
WireConnection;192;0;33;0
WireConnection;276;0;329;0
WireConnection;276;1;312;0
WireConnection;276;2;292;0
WireConnection;228;0;192;0
WireConnection;228;1;226;0
WireConnection;228;2;227;0
WireConnection;357;0;358;0
WireConnection;357;1;276;0
WireConnection;32;3;228;0
WireConnection;214;0;32;0
WireConnection;259;1;357;0
WireConnection;305;3;300;0
WireConnection;209;0;36;0
WireConnection;209;1;189;0
WireConnection;309;0;305;0
WireConnection;309;1;310;0
WireConnection;294;0;259;4
WireConnection;294;1;295;0
WireConnection;229;0;214;0
WireConnection;229;1;230;0
WireConnection;229;2;231;0
WireConnection;308;0;309;0
WireConnection;308;1;294;0
WireConnection;254;0;58;0
WireConnection;254;1;38;0
WireConnection;223;1;225;0
WireConnection;223;3;224;0
WireConnection;213;0;209;0
WireConnection;213;1;229;0
WireConnection;221;0;220;0
WireConnection;221;1;254;0
WireConnection;221;2;223;0
WireConnection;355;0;308;0
WireConnection;355;1;356;0
WireConnection;360;0;60;0
WireConnection;360;1;359;0
WireConnection;86;0;213;0
WireConnection;86;1;87;0
WireConnection;296;0;355;0
WireConnection;296;1;221;0
WireConnection;370;0;86;0
WireConnection;370;1;360;0
WireConnection;242;5;236;0
WireConnection;0;0;296;0
WireConnection;0;1;242;0
WireConnection;0;2;86;0
WireConnection;0;3;183;0
WireConnection;0;4;370;0
ASEEND*/
//CHKSM=92CABE3877661BAF12847560A55842666669F54F