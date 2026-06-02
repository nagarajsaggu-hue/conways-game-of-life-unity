using UnityEngine;

public class GridLines : MonoBehaviour
{
    private Material lineMaterial;

    public bool showMain = true;
    public bool showSub = false;
    public int gridSizeX;
    public int gridSizeY;
    public float startX;
    public float startY;
    public float startZ;
    public float smallStep;
    public float largeStep;
    public Color mainColor = new Color(0f, 1f, 0f, 1f);
    public Color subColor = new Color(0f, 0.5f, 0f, 1f);

    void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            lineMaterial = CreateMaterialWithShader();
            ConfigureMaterialForAlphaBlending(lineMaterial);
            ConfigureMaterialForDepthWriting(lineMaterial);
            ConfigureMaterialForBackfaceCulling(lineMaterial);
        }
    }

    Material CreateMaterialWithShader()
    {
        var shader = Shader.Find("Hidden/Internal-Colored");
        var material = new Material(shader) { hideFlags = HideFlags.HideAndDontSave };
        return material;
    }

    void ConfigureMaterialForAlphaBlending(Material material)
    {
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
    }

    void ConfigureMaterialForDepthWriting(Material material)
    {
        material.SetInt("_Zwrite", 0);
    }

    void ConfigureMaterialForBackfaceCulling(Material material)
    {
        material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
    }

    private void OnDisable()
    {
        DestroyImmediate(lineMaterial);
    }

    public void OnPostRender()
    {
        CreateLineMaterial();
        lineMaterial.SetPass(0);
        GL.Begin(GL.LINES);

        if (showSub) DrawGridLines(subColor, smallStep);
        if (showMain) DrawGridLines(mainColor, largeStep);

        GL.End();
    }

    private void DrawGridLines(Color color, float step)
    {
        GL.Color(color);

        for (float y = 0; y <= gridSizeY; y += step)
        {
            DrawHorizontalLine(y);
            DrawVerticalLine(y);
        }
    }

    private void DrawHorizontalLine(float y)
    {
        GL.Vertex3(startX, startY + y, startZ);
        GL.Vertex3(startX + gridSizeX, startY + y, startZ);
    }

    private void DrawVerticalLine(float x)
    {
        GL.Vertex3(startX + x, startY, startZ);
        GL.Vertex3(startX + x, startY + gridSizeY, startZ);
    }
}
