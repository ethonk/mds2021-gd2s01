Shader "Custom/RunningWater" {
	Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_SpecColor ("Specular Color", Color) = (0.5,0.5,0.5,1)
	_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
	_Transparency ("Transparency", Range (-0.5, 0.5)) = 0.1
	_MaxWaterSpeed ("max water velocity", Range (-100, 100)) = 5
	_WaveSpeed ("wave velocity", Range (-10, 10)) = 1
	_ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)
	_Cube ("Reflection Cubemap", Cube) = "" {}
	_BumpMap ("Normalmap", 2D) = "bump" {}
	_SplashTex ("Splash Texture", 2D) = "Black" {}
}
SubShader {
	Tags { "Queue"="Transparent""IgnoreProjector"="True" "RenderType"="Transparent" }
	Cull off Zwrite off
	LOD 400

CGPROGRAM
#pragma surface surf BlinnPhong alpha vertex:vert nolightmap nodirlightmap decal:blend
#pragma target 3.0
//input limit (8) exceeded, shader uses 9
#pragma exclude_renderers d3d11_9x

sampler2D _BumpMap;
sampler2D _SplashTex;
samplerCUBE _Cube;

fixed4 _Color;
fixed4 _ReflectColor;
float _MaxWaterSpeed;
float _WaveSpeed;
half _Shininess;
half _Transparency;

struct Input {
	float2 uv_BumpMap;
	float2 uv_SplashTex;
	float3 worldRefl;
	float4 color : Color;
	float _Time;
	INTERNAL_DATA
};

void vert (inout appdata_full v) {
    v.vertex.xyz += v.normal * (sin(_Time.x*3.145  +v.vertex.x*50) + sin(_Time.x*2.947  +v.vertex.z*50))*0.004;
    //half offset = sin(_Time*20 + v.vertex.x+ v.vertex.y)*0.1;
   // v.vertex.x += offset;
   // v.vertex.z += offset;
}

void surf (Input IN, inout SurfaceOutput o) {
	float offs1 = _Time.x * _WaveSpeed;
	float offs2 = 0.5;
	float2 uv1 = float2(IN.uv_BumpMap.x + offs1,IN.uv_BumpMap.y + offs1);
	float2 uv2 = float2(IN.uv_BumpMap.x - offs1,IN.uv_BumpMap.y + offs1+ offs2);
	float3 norm1 = UnpackNormal(tex2D(_BumpMap, uv1));
	float3 norm2 = UnpackNormal(tex2D(_BumpMap, uv2));
	float3 norm = (norm1 + norm2) * 0.5;
	uv1 = float2(IN.uv_SplashTex.x,IN.uv_SplashTex.y + _Time.x * _MaxWaterSpeed);
	uv2 = float2(IN.uv_SplashTex.x + 0.5,IN.uv_SplashTex.y + _Time.x * _MaxWaterSpeed*0.5);
	fixed4 fc = tex2D(_SplashTex, uv1) + tex2D(_SplashTex, uv2);
	fc *= 0.5;
	fixed4 c = _Color * (1-IN.color.r) + IN.color.r * fc;
	o.Albedo = c.rgb;
	
	o.Gloss = 1 * (1-IN.color.r);
	o.Specular = _Shininess * (1-IN.color.r);
	
	o.Normal = norm;
	
	float3 worldRefl = WorldReflectionVector (IN, o.Normal);
	fixed4 reflcol = texCUBE (_Cube, worldRefl);
	o.Emission = reflcol.rgb * _ReflectColor.rgb * (1-IN.color.r);
	o.Alpha = ((reflcol.a * _ReflectColor.a + _Transparency) * (1-IN.color.r) + IN.color.r * fc.a)*IN.color.a;
}
ENDCG
}

FallBack "Custom/RunningWaterSimple"
}