
using System.Collections;
using SkyForge.Extention;

namespace SEGame
{
    public class SceneService : BaseSceneService
    {
#if DEDICATED_SERVER
        public const string SERVER_SCENE = "Server";
#else
        public const string BOOTSTRAP_SCENE = "Bootstrap";
        public const string MAIN_MENU_SCENE = "MainMenu";
        public const string GAMEPLAY_SCENE = "Gameplay";
#endif
        
#if DEDICATED_SERVER
        public IEnumerator LoadServerScene(ServerEnterParams serverEnterParams)
        {
            yield return LoadScene(SERVER_SCENE, serverEnterParams);
        }
#else
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
#endif
    }
}


