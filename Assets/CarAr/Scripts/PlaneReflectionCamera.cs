using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneReflectionCamera : MonoBehaviour
{
    // Start is called before the first frame update
    Camera m_ReflectionCamera;
    Camera m_MainCamera;
    public GameObject m_ReflectionFloor;
    public Material m_FloorMaterial;
    RenderTexture  m_RenderTarget;
    void Start()
    {
        GameObject reflectionCamera =  new GameObject("ReflectionCamera");
        m_ReflectionCamera = reflectionCamera.AddComponent<Camera>();
        m_ReflectionCamera.enabled = false;
        m_MainCamera = Camera.main;
        m_RenderTarget = new RenderTexture(Screen.width, Screen.height,24);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnPostRender() {
        RenderReflection();
    }
    void RenderReflection(){
        m_ReflectionCamera.CopyFrom(m_MainCamera);
        Vector3 cameraDirectionWorldSpace = m_MainCamera.transform.forward;
        Vector3 cameraUpWorldSpace = m_MainCamera.transform.up;
        Vector3 cameraPositionWorldSpace = m_MainCamera.transform.position;

        //Transform vector to floor space

        Vector3 cameraDirectionPlaneSpace = m_ReflectionFloor.transform.InverseTransformDirection(cameraDirectionWorldSpace);
        Vector3 cameraUpPlaneSpace = m_ReflectionFloor.transform.InverseTransformDirection(cameraUpWorldSpace);
        Vector3 cameraPositionPlaneSpace = m_ReflectionFloor.transform.InverseTransformPoint(cameraPositionWorldSpace);
        //Mirror the vectors
        cameraDirectionPlaneSpace.y *= -1.0f;
        cameraUpPlaneSpace.y *= -1.0f;
        cameraPositionPlaneSpace.y *= -1.0f;

        //transform vector bacl world space 
        cameraDirectionWorldSpace = m_ReflectionFloor.transform.TransformDirection(cameraDirectionPlaneSpace);
        cameraUpWorldSpace = m_ReflectionFloor.transform.TransformDirection(cameraUpPlaneSpace);
        cameraPositionWorldSpace = m_ReflectionFloor.transform.TransformPoint(cameraPositionPlaneSpace);
        //set camera position and rotation
        m_ReflectionCamera.transform.position = cameraPositionWorldSpace;
        m_ReflectionCamera.transform.LookAt(cameraPositionWorldSpace+cameraDirectionWorldSpace,cameraUpWorldSpace);
        //set rendre target
        m_ReflectionCamera.targetTexture = m_RenderTarget;

        // Render the reglection camera
        m_ReflectionCamera.Render();

        DrawQuad();
    }

    void DrawQuad(){
        GL.PushMatrix();
        m_FloorMaterial.SetPass(0);
        m_FloorMaterial.SetTexture("_ReflectionTex",m_RenderTarget);
        GL.LoadOrtho();
        GL.Begin(GL.QUADS);
        GL.TexCoord2(1.0f,0.0f);
        GL.Vertex3(0.0f,0.0f,0.0f); 
        GL.TexCoord2(1.0f,1.0f);
        GL.Vertex3(0.0f,1.0f,0.0f); 
        GL.TexCoord2(0.0f,1.0f);
        GL.Vertex3(1.0f,1.0f,0.0f); 
        GL.TexCoord2(0.0f,0.0f);
        GL.Vertex3(1.0f,0.0f,0.0f); 
        GL.End();
        GL.PopMatrix();

    }
}
