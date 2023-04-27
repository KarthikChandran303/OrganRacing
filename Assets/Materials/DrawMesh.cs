using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMesh : MonoBehaviour
{

    public int population;
    public float range;
    public Material mat;
    public Mesh mesh;

    public GraphicsBuffer positionsBuffer;
    public GraphicsBuffer colorsBuffer;
    public GraphicsBuffer instanceBuffer;
    public ComputeShader compute;
    public Transform pusher;
    
    private Bounds bounds;
    private ComputeBuffer argsBuffer;

    public GameObject testObject;
    
    private struct InstanceData {
        public Matrix4x4 matrix;
        public static int Size() {
            return sizeof(float) * 4 * 4;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        int kernel = compute.FindKernel("CSMain");
        
        argsBuffer = new ComputeBuffer(5, sizeof(int), ComputeBufferType.IndirectArguments);
        positionsBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, population, sizeof(float) * 3);
        colorsBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, population, sizeof(float) * 4);
        instanceBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, population, InstanceData.Size());

        bounds = new Bounds(transform.position, Vector3.one * 500);
        var args = new uint[5];
        
        args[0] = (uint)mesh.GetIndexCount(0);
        args[1] = (uint)population;
        args[2] = (uint)mesh.GetIndexStart(0);
        args[3] = (uint)mesh.GetBaseVertex(0);
        args[4] = 0;
        argsBuffer.SetData(args);
        
        var positions = new Vector3[population];
        var colors = new Color[population];
        var instances = new InstanceData[population];
        Debug.Log("pos:" + transform.position);
        for(int i = 0; i < population; i++)
        {
            InstanceData data = new InstanceData();
            Vector3 pos = new Vector3((Random.Range(-range, range)) - transform.position.x, -transform.position.y, (Random.Range(-range, range) - transform.position.z));
            Vector3 scale = new Vector3(0.3f, Random.Range(0.3f, 3.5f), 0.3f);
            Quaternion rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
            data.matrix = Matrix4x4.TRS(pos, rotation, scale);
            instances[i] = data;
            
            positions[i] = pos;
            colors[i] = Color.Lerp(Color.red, Color.blue, Random.value);
        }

        positionsBuffer.SetData(positions);
        colorsBuffer.SetData(colors);
        instanceBuffer.SetData(instances);
        
        compute.SetBuffer(kernel, "positions", positionsBuffer);
        compute.SetBuffer(kernel, "colors", colorsBuffer);

        mat.SetBuffer("colorsBuffer", colorsBuffer); // Link the buffer to our shadergraph
        mat.SetBuffer("positionsBuffer", positionsBuffer); // Link the buffer to our shadergraph
        mat.SetBuffer("instanceBuffer", instanceBuffer); // Link the buffer to our shadergraph
        
        // set pusher pose
        mat.SetVector("_ForcePos", pusher.position);
        //compute.SetVector("pusherPos", pusher.position);
    }

    // Update is called once per frame
    void Update()
    {
        int kernel = compute.FindKernel("CSMain");
        compute.SetVector("pusherPos", (pusher.position - transform.position));
        compute.Dispatch(kernel, Mathf.CeilToInt(population / 64f), 1, 1);
        mat.SetVector("_ForcePos", pusher.position - transform.position);
        Graphics.DrawMeshInstancedIndirect(mesh, 0, mat, bounds, argsBuffer);
    }
    
    private void OnDestroy()
    {
        argsBuffer.Release();
        positionsBuffer.Release();
        colorsBuffer.Release();
        instanceBuffer.Release();
    }
}
