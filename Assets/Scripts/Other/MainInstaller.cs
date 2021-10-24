using Zenject;
using Mst.Spawn;
using Mst.UI;

public class MainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //Container.Bind<CoreManager>().AsSingle();
        //Container.Bind<SpawnEnemy>().AsSingle();
        //ontainer.Bind<CursorChanger>().AsTransient().NonLazy();
    }
}