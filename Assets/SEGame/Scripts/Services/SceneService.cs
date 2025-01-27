
using System.Collections;
using SkyForge.Extention;

namespace SEGame
{
    public class SceneService : BaseSceneService
    {
        public const string BOOTSTRAP_SCENE = "Bootstrap";
        public const string MAIN_MENU_SCENE = "MainMenu";
        
        public IEnumerator LoadMainMenuScene()
        {
            yield return LoadScene(BOOTSTRAP_SCENE);
            yield return LoadScene(MAIN_MENU_SCENE);   
        }        
    }
}


