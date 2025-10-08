using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace ldjam_58
{
    public class CutoutMaskUI : Image
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public override Material materialForRendering
        {
            get
            {   
                var newMat = new Material(base.materialForRendering);
                newMat.SetFloat("_StencilComp", (float)CompareFunction.NotEqual);
                return newMat;
            }
        }
    }
}
