
using UnityEngine.SceneManagement;
using SkyForge.Reactive.Extention;
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
            GameServiceRegistration.RegisterService(m_rootContainer);
            
            //Init Coroutines
            m_coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
            m_rootContainer.RegisterInstance(m_coroutines);
            UnityEngine.Object.DontDestroyOnLoad(m_coroutines);
            
            var sceneService = m_rootContainer.Resolve<SceneService>();
            sceneService.LoadSceneEvent += OnLoadScene;

#if DEDICATED_SERVER

            var defaultServerEnterParams = new ServerEnterParams();
            m_coroutines.StartCoroutine(LoadAndStartServer(defaultServerEnterParams));
#else

            RegisterViewModel(m_rootContainer);
            BindView(m_rootContainer);
            
            var defaultMainMenuEnterParams = new MainMenuEnterParams();
            m_coroutines.StartCoroutine(LoadAndStartMainMenu(defaultMainMenuEnterParams));
#endif
        }
        
        private void OnLoadScene(Scene scene, LoadSceneMode loadSceneMode, SceneEnterParams sceneEnterParams)
        {
            var sceneName = scene.name;
            
#if DEDICATED_SERVER
            if(sceneName.Equals(SceneService.SERVER_SCENE))
                m_coroutines.StartCoroutine(LoadServer(sceneEnterParams));
#else
            if (sceneName.Equals(SceneService.BOOTSTRAP_SCENE))
                LoadBootstrapScene();
            else if (sceneName.Equals(SceneService.MAIN_MENU_SCENE))
                m_coroutines.StartCoroutine(LoadMainMenu(sceneEnterParams));
            else if (sceneName.Equals(SceneService.GAMEPLAY_SCENE))
                m_coroutines.StartCoroutine(LoadGameplay(sceneEnterParams));
#endif
        }
        
#if DEDICATED_SERVER
        private IEnumerator LoadServer(SceneEnterParams sceneEnterParams)
        {
            var serverContainer = new DIContainer(m_rootContainer);

            var serverEntryPoint = UnityExtention.GetEntryPoint<ServerEntryPoint>();
            
            yield return serverEntryPoint.Intialization(serverContainer, sceneEnterParams);

            serverEntryPoint.Run();
        }

        private IEnumerator LoadAndStartServer(ServerEnterParams serverEnterParams)
        {
            var sceneService = m_rootContainer.Resolve<SceneService>();
            yield return sceneService.LoadServerScene(serverEnterParams);
        }
#else 
        private void RegisterViewModel(DIContainer container)
        {
            container.RegisterSingleton<IUIRootViewModel>(factory => new UIRootViewModel());
        }

        private void BindView(DIContainer container)
        {
            var loadService = container.Resolve<LoadService>();
            
            //Init UIRoot
            var prefab = loadService.LoadPrefab<UIRootView>(LoadService.PREFAB_UI_ROOT);
            var uIRootView = UnityEngine.Object.Instantiate(prefab);
            UnityEngine.Object.DontDestroyOnLoad(uIRootView);
            
            var uIRootViewModel = container.Resolve<IUIRootViewModel>();
            uIRootView.Bind(uIRootViewModel);
        }
        
        private void LoadBootstrapScene()
        {
            var uIRootViewModel = m_rootContainer.Resolve<IUIRootViewModel>();
            
            uIRootViewModel.ShowLoadingScreen();
        }

        private IEnumerator LoadMainMenu(SceneEnterParams sceneEnterParams)
        {
            var mainMenuContainer = new DIContainer(m_rootContainer);

            var mainMenuEntryPoint = UnityExtention.GetEntryPoint<MainMenuEntryPoint>();
            
            yield return mainMenuEntryPoint.Intialization(mainMenuContainer, sceneEnterParams);
            
            mainMenuEntryPoint.Run().Subscribe(sceneExitParams =>
            {
                if (sceneExitParams.TargetEnterParams is GameplayEnterParams gameplayEnterParams)
                {
                    m_coroutines.StartCoroutine(LoadAndStartGameplay(gameplayEnterParams));
                }
            });
            
            var uIRootViewModel = m_rootContainer.Resolve<IUIRootViewModel>();
            uIRootViewModel.HideLoadingScreen();
        }

        private IEnumerator LoadGameplay(SceneEnterParams sceneEnterParams)
        {
            var gameplayContainer = new DIContainer(m_rootContainer);

            var gameplayEntryPoint = UnityExtention.GetEntryPoint<GameplayEntryPoint>();
            
            Debug.Log("Test game 1");
            
            yield return gameplayEntryPoint.Intialization(gameplayContainer, sceneEnterParams);
            
            gameplayEntryPoint.Run();
            Debug.Log("Test game 2");
            var uIRootViewModel = m_rootContainer.Resolve<IUIRootViewModel>();
            uIRootViewModel.HideLoadingScreen();
        }
        
        private IEnumerator LoadAndStartMainMenu(MainMenuEnterParams mainMenuEnterParams)
        {   
            var sceneService = m_rootContainer.Resolve<SceneService>();
            yield return sceneService.LoadMainMenuScene(mainMenuEnterParams);
        }

        private IEnumerator LoadAndStartGameplay(GameplayEnterParams gameplayEnterParams)
        {
            var sceneService = m_rootContainer.Resolve<SceneService>();
            yield return sceneService.LoadGameplayScene(gameplayEnterParams);
        }
        
#endif
    }
}
