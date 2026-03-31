using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Burst;

public struct CubeData
{
    public float3 position;
    public float angle;
    public float randomOffset;
    public float speed;
}

public struct MoveResult
{
    public float3 newPosition;
}

public class GoodCubeSystem : MonoBehaviour
{
    [Header("Settings")]
    public Mesh cubeMesh;
    public Material cubeMaterial;
    public int count = 1000;

    private NativeArray<CubeData> cubeDatas;
    private NativeArray<MoveResult> moveResults;
    private Transform[] transforms;
    
    private JobHandle jobHandle;

    void Start()
    {
        cubeDatas = new NativeArray<CubeData>(count, Allocator.Persistent);
        moveResults = new NativeArray<MoveResult>(count, Allocator.Persistent);
        transforms = new Transform[count];

        for (int i = 0; i < count; i++)
        {
            GameObject go = new GameObject("Cube_" + i);
            go.AddComponent<MeshFilter>().mesh = cubeMesh;
            go.AddComponent<MeshRenderer>().material = cubeMaterial;

            float3 startPos = new float3(
                UnityEngine.Random.Range(-20f, 20f),
                UnityEngine.Random.Range(-20f, 20f),
                UnityEngine.Random.Range(-20f, 20f)
            );

            cubeDatas[i] = new CubeData
            {
                position = startPos,
                angle = 0f,
                randomOffset = UnityEngine.Random.Range(0f, Mathf.PI * 2),
                speed = 5f
            };

            go.transform.position = startPos;
            transforms[i] = go.transform;
        }
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;

        var moveJob = new MoveCubesJob
        {
            DeltaTime = deltaTime,
            CubeDatas = cubeDatas,
            Results = moveResults
        };

        jobHandle = moveJob.Schedule(count, 10); 
    }

    void LateUpdate()
    {
        jobHandle.Complete();

        for (int i = 0; i < count; i++)
        {
            transforms[i].position = moveResults[i].newPosition;
            transforms[i].Rotate(Vector3.up, cubeDatas[i].speed * 20 * Time.deltaTime);
        }
    }

    void OnDestroy()
    {
        if (cubeDatas.IsCreated) cubeDatas.Dispose();
        if (moveResults.IsCreated) moveResults.Dispose();
    }

    [BurstCompile]
    struct MoveCubesJob : IJobParallelFor
    {
        public float DeltaTime;
        public NativeArray<CubeData> CubeDatas; 
        public NativeArray<MoveResult> Results;

        public void Execute(int index)
        {
            CubeData data = CubeDatas[index];

            data.angle += DeltaTime * data.speed;

            float3 newPos = data.position;
            newPos.x += math.sin(data.angle + data.randomOffset) * DeltaTime * data.speed;
            newPos.y += math.cos(data.angle + data.randomOffset) * DeltaTime * data.speed;
            newPos.z += math.sin(data.angle * 0.5f) * DeltaTime * data.speed;

            if (newPos.x > 50f) newPos.x = -50f;
            if (newPos.x < -50f) newPos.x = 50f;
            if (newPos.y > 50f) newPos.y = -50f;
            if (newPos.y < -50f) newPos.y = 50f;
            if (newPos.z > 50f) newPos.z = -50f;
            if (newPos.z < -50f) newPos.z = 50f;

            data.position = newPos;
            CubeDatas[index] = data;

            Results[index] = new MoveResult { newPosition = newPos };
        }
    }
}