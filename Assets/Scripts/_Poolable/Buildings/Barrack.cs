using GridSystem;
using UI;

namespace _Poolable.Buildings
{
    public class Barrack : BuildingBase
    {
        private void Start()
        {
            OnClick.AddListener(OpenInfoPanel);
        }
    
        private void OpenInfoPanel()
        {
            if (!CheckClick()&&GridTester.Instance.GetBuildingMode()) return;
            InfoPanel.Instance.OpenCloseInfoPanel();
            InfoPanel.Instance.CreateProductionUIItem();
        }
    }
}
