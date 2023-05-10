public class MapSelectState : GameState
{
    // 맵을 만들기 위해 기준이 되는 맵을 선택하는 상태
    // 일단은 맵이 하나기에 맵을 선택해서 MapMakingState로 넘김
    public override void OnStart()
    {
        Managers.Game.SetState(new MapInstanceEditState());
    }
}

// 룸 소환 모드
public class MapRoomSpawnState : GameState
{
    // 각종 배치 요소들 배치할 수 있는 상태
    public override void OnStart()
    {
        // 필요한 UI를 배치 -> 상태마다 UI는 동일하게 가져가고 자동 배치
        // 필요한 이벤트 등록
        Managers.Scene.GetCurrentScene<MapMakingScene>().inputController.SetRoomSpawnMouseEvent();
    }
}

// 오브젝트(트리거, 문, 오브젝트, 몬스터) 소환 모드
public class MapObjectSpawnState : GameState
{
    public override void OnStart()
    {
        // 필요한 UI를 배치 -> 상태마다 UI는 동일하게 가져가고 자동 배치
        // 필요한 이벤트 등록
        Managers.Scene.GetCurrentScene<MapMakingScene>().inputController.SetObjectSpawnMouseEvent();
    }
}

public class MapSpawnState : GameState
{
    // 각종 배치 요소들 배치할 수 있는 상태
    public override void OnStart()
    {
        // 필요한 UI를 배치 -> 상태마다 UI는 동일하게 가져가고 자동 배치
        // 필요한 이벤트 등록
        Managers.Scene.GetCurrentScene<MapMakingScene>().inputController.SetObjectSpawnMouseEvent();
    }
}

// 소환한 인스턴스 선택 & 수정 모드
public class MapInstanceEditState : GameState
{
    public override void OnStart()
    {
        Managers.Scene.GetCurrentScene<MapMakingScene>().inputController.SetInstanceEditMouseEvent();
    }

    public override void OnEnd()
    {
        Managers.Scene.GetCurrentScene<MapMakingScene>().CurSelectInstance = null;
    }
}

public class MapMakingDoneState : GameState
{
    // 배치를 완료하고 저장하는 상태. 이 상태는 필요 없을 가능성도 있음
}