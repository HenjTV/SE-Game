
using System.Collections;
using SkyForge.Extention;

namespace SEGame
{
    public class SceneService : BaseSceneService
    {
        public const string BOOTSTRAP_SCENE = "Bootstrap";
        public const string MAIN_MENU_SCENE = "MainMenu";
        public const string GAMEPLAY_SCENE = "Gameplay";
        
        public IEnumerator LoadMainMenuScene(MainMenuEnterParams mainMenuEnterParams)
        {
            yield return LoadScene(BOOTSTRAP_SCENE);
            yield return LoadScene(MAIN_MENU_SCENE, mainMenuEnterParams);   
        }

        public IEnumerator LoadGameplayScene(GameplayEnterParams gameplayEnterParams)
        {
            yield return LoadScene(BOOTSTRAP_SCENE);
            yield return LoadScene(GAMEPLAY_SCENE, gameplayEnterParams);
        }
    }
}


