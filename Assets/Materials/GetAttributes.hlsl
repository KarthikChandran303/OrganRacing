#include "Matrix.hlsl"

#if defined(UNITY_PROCEDURAL_INSTANCING_ENABLED)
struct InstanceData {
    float4x4 m;
};
StructuredBuffer<float3> positionsBuffer;
StructuredBuffer<float4> colorsBuffer;
StructuredBuffer<InstanceData> instanceBuffer;
#endif


void ConfigureProcedural()
{
}


// More info on how this works:
// https://docs.unity3d.com/Packages/com.unity.shadergraph@6.9/manual/Custom-Function-Node.html
void GetAttributes_float(float In, out float3 Position, out float4 Color, out float4x4 Transform, out float4x4 InverseTransform)
{
    Position = 0;
    Color = 0;
    Transform = 0;
    InverseTransform = 0;
    
    #if defined(UNITY_PROCEDURAL_INSTANCING_ENABLED)
    Position = positionsBuffer[(int) In];
    Color = colorsBuffer[(int) In];
    Transform = instanceBuffer[(int) In].m;
    InverseTransform = inverse(Transform);
    #endif
}
