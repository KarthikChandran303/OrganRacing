#ifndef ADDITIONAL_LIGHTS
#define ADDITIONAL_LIGHTS

void AdditionalLights_float(float3 WorldPos, float Index, out float3 Direction, out float3 Color, 
    out float DistanceAtten, out float ShadowAtten, out bool IsSpot)
{
    Direction = normalize(float3(0.0f, 0.0f, 0.0f));
    Color = float3(0.0f, 0.0f, 0.0f);
    DistanceAtten = 1.0f;
    ShadowAtten = 1.0f;
    IsSpot = false;
    #ifdef SHADERGRAPH_PREVIEW
    Direction = normalize(float3(0.5f, 0.5f, 0.25f));
    Color = float3(1.0f, 1.0f, 1.0f);
    DistanceAtten = 1.0f;
    ShadowAtten = 1.0f;
    #else
    int pixelLightCount = GetAdditionalLightsCount();
    if(Index < pixelLightCount)
    {
        Light light = GetAdditionalLight(Index, WorldPos);
        Direction = light.direction;
        Color = light.color;
        DistanceAtten = light.distanceAttenuation;
        ShadowAtten = light.shadowAttenuation;
        IsSpot = true;
    }
    #endif
}
#endif 