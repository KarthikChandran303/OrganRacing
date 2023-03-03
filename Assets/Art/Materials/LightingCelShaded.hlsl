#ifndef LIGHTING_CEL_SHADED
#define LIGHTING_CEL_SHADED

#ifndef SHADERGRAPH_PREVIEW
struct SurfaceVars
{
    float3 normal;
    float smoothness;
    float shininess;
    float3 view;
};

float3 calculateCelShading(Light l, SurfaceVars s)
{
    float attenuation = l.shadowAttenuation;
    float diffuse = saturate(dot(s.normal, l.direction));
    float3 halfDir = normalize(l.direction + s.view);
    float specular = saturate(dot(s.normal, halfDir));
    specular = pow(specular, s.shininess);
    specular *= diffuse * s.smoothness;
    return attenuation;
}
#endif

void CelShaded_float(float smoothness, float3 normal, float3 view, float3 pos, out float3 color)
{
    #if defined(SHADERGRAPH_PREVIEW)
    color = float3(normal.x, normal.y, normal.z);
    #else
        SurfaceVars s;
        s.normal = normalize(normal);
        s.smoothness = smoothness;
        s.shininess = exp2(10 * smoothness + 1);
        s.view = normalize(view);
        #if SHADOWS_SCREEN
            float4 clipPos = TransformWorldToHClip(pos);
            float4 shadowCoord = ComputeScreenPos(clipPos);
        #else
            float4 shadowCoord = TransformWorldToShadowCoord(pos); 
        #endif
        Light light = GetMainLight(shadowCoord);
        color = calculateCelShading(light, s);
    #endif
}

#endif 