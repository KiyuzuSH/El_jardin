using UnityEngine;

namespace KiyuzuDev.ITGWDO
{
    public class GlobalDataManager : MonoBehaviour
    {
        #region 控制当前酒馆主题风格

        public WorldStyle PresentWorldStyle { get; private set; }
        public bool JalousieShutDown { get; set; }
        
        private void Start()
        {
            SetStyle(WorldStyle.Utopia);
        }

        public void SetStyle(WorldStyle _style)
        {
            
        }

        #endregion
    }
}
