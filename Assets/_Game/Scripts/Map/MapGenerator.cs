using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Map
{
    public class MapGenerator : MonoBehaviour
    {
        [Inject] private IMapController _mapController;
        public int width = 6;
        public int height = 6;

        public HexCell[] cells;

        public MapConfig MapConfig;

        public NoiseGenerator noiseGenerator;

        public MeshFilter combinedMeshFilter;
        public MeshRenderer combinedMeshRenderer;
        void Awake () {
            cells = new HexCell[height * width];

            for (int z = 0, i = 0; z < height; z++) {
                for (int x = 0; x < width; x++) {
                    CreateCell(x, z, noiseGenerator.GetNoiseValue(x, z), i++);
                }
            }

            foreach (var cell in cells)
            {
                _mapController.CreateCell(cell);
            }

            // AdvancedMerge();
        }

        public void AdvancedMerge()
         {
              // All our children (and us)
              MeshFilter[] filters = GetComponentsInChildren<MeshFilter>(false);

              // All the meshes in our children (just a big list)
              List<Material> materials = new List<Material>();
              MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>(false); // <-- you can optimize this
              foreach (MeshRenderer renderer in renderers)
              {
               if (renderer.transform == transform)
                continue;
               Material[] localMats = renderer.sharedMaterials;
               foreach (Material localMat in localMats)
                if (!materials.Contains (localMat))
                 materials.Add (localMat);
              }

              // Each material will have a mesh for it.
              List<Mesh> submeshes = new List<Mesh>();
              foreach (Material material in materials)
              {
                   // Make a combiner for each (sub)mesh that is mapped to the right material.
                   List<CombineInstance> combiners = new List <CombineInstance>();
                   foreach (MeshFilter filter in filters)
                   {
                        if (filter.transform == transform) continue;
                        // The filter doesn't know what materials are involved, get the renderer.
                        MeshRenderer renderer = filter.GetComponent<MeshRenderer>();  // <-- (Easy optimization is possible here, give it a try!)
                        if (renderer == null)
                        {
                             Debug.LogError (filter.name + " has no MeshRenderer");
                             continue;
                        }

                        // Let's see if their materials are the one we want right now.
                        Material[] localMaterials = renderer.sharedMaterials;
                        for (int materialIndex = 0; materialIndex < localMaterials.Length; materialIndex++)
                        {
                            if (localMaterials [materialIndex] != material)
                                continue;
                            // This submesh is the material we're looking for right now.
                            CombineInstance ci = new CombineInstance();
                            ci.mesh = filter.sharedMesh;
                            ci.subMeshIndex = materialIndex;
                            Quaternion rotation = Quaternion.Euler(
                                filter.transform.eulerAngles.x, 
                                filter.transform.eulerAngles.y, 
                                filter.transform.eulerAngles.z);

                            // Set the translation, rotation and scale parameters.
                            Matrix4x4 m = Matrix4x4.TRS(
                                filter.transform.position, 
                                    rotation, 
                                    filter.transform.localScale);

                            
                            ci.transform = m;//Matrix4x4.identity;
                            combiners.Add (ci);
                        }
                   }
                   // Flatten into a single mesh.
                   Mesh mesh = new Mesh ();
                   mesh.CombineMeshes (combiners.ToArray(), true);
                   submeshes.Add (mesh);
              }

              // The final mesh: combine all the material-specific meshes as independent submeshes.
              List<CombineInstance> finalCombiners = new List<CombineInstance> ();
              foreach (Mesh mesh in submeshes)
              {
               CombineInstance ci = new CombineInstance ();
               ci.mesh = mesh;
               ci.subMeshIndex = 0;
               ci.transform = Matrix4x4.identity;
               finalCombiners.Add (ci);
              }
              Mesh finalMesh = new Mesh();
              finalMesh.CombineMeshes (finalCombiners.ToArray(), false);
              combinedMeshFilter.sharedMesh = finalMesh;
              combinedMeshRenderer.sharedMaterials = materials.ToArray();
              Debug.Log ("Final mesh has " + submeshes.Count + " materials.");

              foreach (var filter in filters)
              {
                  filter.gameObject.SetActive(false);
              }
         }
	
        void CreateCell (int x, int z, float y, int i) {
            Vector3 position;
            position.x = x;
            position.y = y;
            position.z = z;

            HexCell cell = cells[i] = new HexCell(x, z, y);
        }
    }
}