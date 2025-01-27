
using UnityEngine.SceneManagement;
using System.Collections;
using SkyForge.Extention;
using UnityEngine;
using SkyForge;
using System;

namespace SEGame
{
    public class GameEntryPoint
    {
        private static GameEntryPoint m_instance;
        private DIContainer m_rootContainer;
        private Coroutines m_coroutines;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Run()
        {
            Application.targetFrameRate = 60;
            
            if(m_instance is not null)
                throw new Exception("Game is already running.");
            
            m_instance = new GameEntryPoint();
            m_instance.Init();
        }
        
        private GameEntryPoint()
        {
            m_rootContainer = new DIContainer();
        }


        private void Init()
        {
            GameService.RegisterService(m_rootContainer);
            
            //init Coroutines
            m_coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
            m_rootContainer.RegisterInstance(m_coroutines);
            UnityEngine.Object.DontDestroyOnLoad(m_coroutines);
            
            RegisterViewModel(m_rootContainer);
            BindView(m_rootContainer);
            
            var sceneService = m_rootContainer.Resolve<SceneService>();
            sceneService.LoadSceneEvent += OnLoadScene;
            
            var defaultMainMenuEnterParams = new MainMenuEnterParams();
            m_coroutines.StartCoroutine(LoadAndStartMainMenu(defaultMainMenuEnterParams));
        }
        
        private void RegisterViewModel(DIContainer container)
        {
            
        }

        private void BindView(DIContainer container)
        {
            var loadService = container.Resolve<LoadService>();
        }
        
        private void OnLoadScene(Scene scene, LoadSceneMode loadSceneMode, SceneEnterParams sceneEnterParams)
        {
            var sceneName = scene.name;
            
            if (sceneName.Equals(SceneService.BOOTSTRAP_SCENE))
                LoadBootstrapScene();
            else if (sceneName.Equals(SceneService.MAIN_MENU_SCENE))
                m_coroutines.StartCoroutine(LoadMainMenu(sceneEnterParams));

        }

        private void LoadBootstrapScene()
        {
            
        }

        private IEnumerator LoadMainMenu(SceneEnterParams sceneEnterParams)
        {
            var mainMenuContainer = new DIContainer(m_rootContainer);

            var mainMenuEntryPoint = UnityExtention.GetEntryPoint<MainMenuEntryPoint>();
            
            yield return mainMenuEntryPoint.Intialization(mainMenuContainer, sceneEnterParams);
            
            mainMenuEntryPoint.Run();
        }
        
        private IEnumerator LoadAndStartMainMenu(MainMenuEnterParams mainMenuEnterParams)
        {   
            var sceneService = m_rootContainer.Resolve<SceneService>();
            yield return sceneService.LoadMainMenuScene();
        }
        
    }
}
