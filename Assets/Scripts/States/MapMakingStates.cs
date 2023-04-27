public class MapSelectState : GameState
{
    // ���� ����� ���� ������ �Ǵ� ���� �����ϴ� ����
    // �ϴ��� ���� �ϳ��⿡ ���� �����ؼ� MapMakingState�� �ѱ�
    public override void OnStart()
    {
        Managers.Game.SetState(new MapInstanceEditState());
    }
}

// �� ��ȯ ���
public class MapRoomSpawnState : GameState
{
    // ���� ��ġ ��ҵ� ��ġ�� �� �ִ� ����
    public override void OnStart()
    {
        // �ʿ��� UI�� ��ġ -> ���¸��� UI�� �����ϰ� �������� �ڵ� ��ġ
        // �ʿ��� �̺�Ʈ ���
        Managers.Scene.GetCurrentScene<MapMakingScene>()._inputController.SetRoomSpawnMouseEvent();
    }
}

// ������Ʈ(Ʈ����, ��, ������Ʈ, ����) ��ȯ ���
public class MapObjectSpawnState : GameState
{
    public override void OnStart()
    {
        // �ʿ��� UI�� ��ġ -> ���¸��� UI�� �����ϰ� �������� �ڵ� ��ġ
        // �ʿ��� �̺�Ʈ ���
        Managers.Scene.GetCurrentScene<MapMakingScene>()._inputController.SetObjectSpawnMouseEvent();
    }
}

public class MapSpawnState : GameState
{
    // ���� ��ġ ��ҵ� ��ġ�� �� �ִ� ����
    public override void OnStart()
    {
        // �ʿ��� UI�� ��ġ -> ���¸��� UI�� �����ϰ� �������� �ڵ� ��ġ
        // �ʿ��� �̺�Ʈ ���
        Managers.Scene.GetCurrentScene<MapMakingScene>()._inputController.SetObjectSpawnMouseEvent();
    }
}

// ��ȯ�� �ν��Ͻ� ���� & ���� ���
public class MapInstanceEditState : GameState
{
    public override void OnStart()
    {
        Managers.Scene.GetCurrentScene<MapMakingScene>()._inputController.SetInstanceEditMouseEvent();
    }

    public override void OnEnd()
    {
        Managers.Scene.GetCurrentScene<MapMakingScene>().CurSelectInstance = null;
    }
}

public class MapMakingDoneState : GameState
{
    // ��ġ�� �Ϸ��ϰ� �����ϴ� ����. �� ���´� �ʿ� ���� ���ɼ��� ����
}