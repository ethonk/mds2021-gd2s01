Shader "Custom/RunningWaterSimple" {
	Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_Transparency ("Transparency", Range (-0.5, 0.5)) = 0.1
	_WaveSpeed ("wave velocity", Range (-10, 10)) = 1
	_SplashTex ("Splash Texture", 2D) = "Black" {}

}
SubShader {
	Tags { "Queue"="Transparent""IgnoreProjector"="True" "RenderType"="Transparent" }
	Cull off Zwrite off

CGPROGRAM
#pragma surface surf Lambert alpha nolightmap nodirlightmap
//#pragma target 3.0
//input limit (8) exceeded, shader uses 9
//#pragma exclude_renderers d3d11_9x

sampler2D _SplashTex;
fixed4 _Color;
float _WaveSpeed;
half _Transparency;

struct Input {
	float2 uv_SplashTex;
	float4 color : Color;
	float _Time;
	INTERNAL_DATA
};

void surf (Input IN, inout SurfaceOutput o) {
	float2 uv1 = float2(IN.uv_SplashTex.x,IN.uv_SplashTex.y + _Time.x * _WaveSpeed);
	float2 uv2 = float2(IN.uv_SplashTex.x + 0.5,IN.uv_SplashTex.y + _Time.x * _WaveSpeed*0.5);
	fixed4 fc = tex2D(_SplashTex, uv1) + tex2D(_SplashTex, uv2);	
	o.Albedo = fc.rgb;
	
	
	
	o.Alpha =  _Transparency+0.5;
}
ENDCG
}

FallBack "Bumped Specular"
}