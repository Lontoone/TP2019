// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SuSu/Pixel_Shake"
{
	Properties
	{
		_PixelateValue("PixelateValue", Range( 0 , 1)) = 0.82
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Speed("Speed", Float) = 1
		_NoiseScale("NoiseScale", Float) = 6.24
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit alpha:fade keepalpha noshadow 
		struct Input
		{
			float4 vertexColor : COLOR;
			float2 uv_texcoord;
			float3 worldNormal;
			INTERNAL_DATA
			float3 worldPos;
		};

		uniform sampler2D _TextureSample0;
		uniform float _PixelateValue;
		uniform float _Speed;
		uniform float _NoiseScale;


		inline float noise_randomValue (float2 uv) { return frac(sin(dot(uv, float2(12.9898, 78.233)))*43758.5453); }

		inline float noise_interpolate (float a, float b, float t) { return (1.0-t)*a + (t*b); }

		inline float valueNoise (float2 uv)
		{
			float2 i = floor(uv);
			float2 f = frac( uv );
			f = f* f * (3.0 - 2.0 * f);
			uv = abs( frac(uv) - 0.5);
			float2 c0 = i + float2( 0.0, 0.0 );
			float2 c1 = i + float2( 1.0, 0.0 );
			float2 c2 = i + float2( 0.0, 1.0 );
			float2 c3 = i + float2( 1.0, 1.0 );
			float r0 = noise_randomValue( c0 );
			float r1 = noise_randomValue( c1 );
			float r2 = noise_randomValue( c2 );
			float r3 = noise_randomValue( c3 );
			float bottomOfGrid = noise_interpolate( r0, r1, f.x );
			float topOfGrid = noise_interpolate( r2, r3, f.x );
			float t = noise_interpolate( bottomOfGrid, topOfGrid, f.y );
			return t;
		}


		float SimpleNoise(float2 UV)
		{
			float t = 0.0;
			float freq = pow( 2.0, float( 0 ) );
			float amp = pow( 0.5, float( 3 - 0 ) );
			t += valueNoise( UV/freq )*amp;
			freq = pow(2.0, float(1));
			amp = pow(0.5, float(3-1));
			t += valueNoise( UV/freq )*amp;
			freq = pow(2.0, float(2));
			amp = pow(0.5, float(3-2));
			t += valueNoise( UV/freq )*amp;
			return t;
		}


		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			o.Normal = float3(0,0,1);
			float temp_output_4_0 = ( 200.0 * ( 1.0 - _PixelateValue ) );
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 ase_worldPos = i.worldPos;
			float3 temp_output_16_0_g1 = ( ase_worldPos * 100.0 );
			float3 crossY18_g1 = cross( ase_worldNormal , ddy( temp_output_16_0_g1 ) );
			float3 worldDerivativeX2_g1 = ddx( temp_output_16_0_g1 );
			float dotResult6_g1 = dot( crossY18_g1 , worldDerivativeX2_g1 );
			float crossYDotWorldDerivX34_g1 = abs( dotResult6_g1 );
			float2 appendResult40 = (float2(0.0 , ( _Time.y * _Speed )));
			float2 uv_TexCoord11 = i.uv_texcoord + appendResult40;
			float simpleNoise10 = SimpleNoise( uv_TexCoord11*_NoiseScale );
			float temp_output_20_0_g1 = simpleNoise10;
			float3 crossX19_g1 = cross( ase_worldNormal , worldDerivativeX2_g1 );
			float3 break29_g1 = ( sign( crossYDotWorldDerivX34_g1 ) * ( ( ddx( temp_output_20_0_g1 ) * crossY18_g1 ) + ( ddy( temp_output_20_0_g1 ) * crossX19_g1 ) ) );
			float3 appendResult30_g1 = (float3(break29_g1.x , -break29_g1.y , break29_g1.z));
			float3 normalizeResult39_g1 = normalize( ( ( crossYDotWorldDerivX34_g1 * ase_worldNormal ) - appendResult30_g1 ) );
			float3 ase_worldTangent = WorldNormalVector( i, float3( 1, 0, 0 ) );
			float3 ase_worldBitangent = WorldNormalVector( i, float3( 0, 1, 0 ) );
			float3x3 ase_worldToTangent = float3x3( ase_worldTangent, ase_worldBitangent, ase_worldNormal );
			float3 worldToTangentDir42_g1 = mul( ase_worldToTangent, normalizeResult39_g1);
			float4 tex2DNode9 = tex2D( _TextureSample0, ( float3( ( floor( ( temp_output_4_0 * i.uv_texcoord ) ) / temp_output_4_0 ) ,  0.0 ) + worldToTangentDir42_g1 ).xy );
			o.Emission = ( i.vertexColor * tex2DNode9 ).rgb;
			o.Alpha = ( tex2DNode9.a * i.vertexColor.a );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18300
201;73;612;655;161.864;235.829;1.351968;True;False
Node;AmplifyShaderEditor.RangedFloatNode;1;-1869.387,127.8036;Inherit;False;Property;_PixelateValue;PixelateValue;0;0;Create;True;0;0;False;0;False;0.82;0.769;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;13;-1446.051,540.7581;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;15;-1384.922,728.1703;Inherit;False;Property;_Speed;Speed;2;0;Create;True;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;2;-1570.992,-97.7963;Inherit;False;Constant;_Float0;Float 0;1;0;Create;True;0;0;False;0;False;200;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;3;-1545.993,98.20366;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-1235.725,582.5136;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;5;-1358.994,245.2037;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-1374.994,-38.79629;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;40;-1051.808,547.0182;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;11;-865.4425,439.6353;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;19;-654.2565,656.4872;Inherit;False;Property;_NoiseScale;NoiseScale;3;0;Create;True;0;0;False;0;False;6.24;6.24;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-1074.994,96.20365;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FloorOpNode;7;-872.0192,85.28964;Inherit;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;10;-594.1514,328.6764;Inherit;True;Simple;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;17.78;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;8;-628.0177,58.28966;Inherit;True;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;18;-325.9993,397.5423;Inherit;True;Normal From Height;-1;;1;1942fe2c5f1a1f94881a33d532e4afeb;0;1;20;FLOAT;0;False;2;FLOAT3;40;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;17;-282.3741,138.5701;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;9;42.40062,38.18007;Inherit;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;41;98.771,-185.5342;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;363.1471,-61.42501;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;43;362.878,177.8734;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;521,25;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;SuSu/Pixel_Shake;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;1;0
WireConnection;14;0;13;2
WireConnection;14;1;15;0
WireConnection;4;0;2;0
WireConnection;4;1;3;0
WireConnection;40;1;14;0
WireConnection;11;1;40;0
WireConnection;6;0;4;0
WireConnection;6;1;5;0
WireConnection;7;0;6;0
WireConnection;10;0;11;0
WireConnection;10;1;19;0
WireConnection;8;0;7;0
WireConnection;8;1;4;0
WireConnection;18;20;10;0
WireConnection;17;0;8;0
WireConnection;17;1;18;40
WireConnection;9;1;17;0
WireConnection;42;0;41;0
WireConnection;42;1;9;0
WireConnection;43;0;9;4
WireConnection;43;1;41;4
WireConnection;0;2;42;0
WireConnection;0;9;43;0
ASEEND*/
//CHKSM=8A9B84955B163EF6D19E332DD2DE823CA864AFC3