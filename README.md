# RogueLikeShootingGame2D

![image](https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/88df9c91-314e-4467-8e15-a6cac38279a4)

# 플레이&nbsp;영상&nbsp;보러가기

[![Video Label](http://img.youtube.com/vi/uxYgtaZIq4c/0.jpg)](https://youtu.be/uxYgtaZIq4c)

# 프로젝트 소개

<aside>
💡 엔터 더 건전처럼 총을 이용하여 적을 무찌르는 2D 액션 어드벤처 게임을 만들었습니다
</aside>

- 장르 - 2D 액션 어드벤처
- 플랫폼 - PC
- 제작 인원 - 1명
- 제작 기간 - 3개월
- 사용 언어 - C#
- 사용 엔진 - UnityEngine
- Git 저장소 - https://github.com/SSeadog/RogueLikeShootingGame2D

# 게임 로직

- 플레이어는 게임 시작 시 플레이할 캐릭터를 선택할 수 있습니다!
- 맵을 탐험하며 적을 만나거나 상점에서 쇼핑을 할수 있습니다!
- 총을 쏴서 적을 공격하고 구르기를 통해 회피하고 수류탄을 통해 적 공격을 무마시킬 수 있습니다!
- 맵 제작 툴을 통해 그려진 타일맵에 적 몬스터, 룸 트리거, 상점 등을 편리하게 배치할 수 있습니다!

# 구현 내용

<aside>
💡 프로젝트를 진행하며 기억에 남는 구현 사항을 몇가지만 간단하게 소개해드리겠습니다!

</aside>

## 물 반사 효과 구현
![image](https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/8176cf26-5968-4164-af5b-539991ed5db4)
![image](https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/47b799c2-c030-4c6b-af44-080132b2e791)

- 플레이어를 쫓아다니도록 세컨드 카메라를 두고 플레이어 바로 밑에 렌더 텍스쳐를 두어 물에 반사된 것처럼 보이는 효과를 주었습니다
- Player만 보이도록 컬링 마스크를 설정했습니다
- 자연스러운 효과를 위해 물이 흐르는 듯한 셰이더 그래프를 작성했습니다**(gif도 추가할지말지)**

## UI로직

- 기존 UI로직
    - 변경하려는 UI요소를 찾기 위해 [SerializeField]를 이용해 인스펙터에서 연결하거나transform.Find, GetComponentsInChildren등의 함수를 이용했는데 여러 불편함이 존재했습니다
- 개선시킨 UI로직
    - UI공통 함수를 만들어 사용했습니다
    - Bind<T>(Type enumType)
        - enumType에 있는 이름들을 이용해 T타입의 컴포넌트들을 UI자신에게서 찾아서 Dictionary<Type, Dictionary<string, UnityEngine.Object>> 형태로 담아두었습니다
    - Get<T>(string name)
        - UI자신에게서 name 게임오브젝트의 T타입의 컴포넌트를 가져옵니다
    
- 코드 예시
    
    ```csharp
    // UIBase를 제작. 모든 UI스크립트들은 UIBase를 상속받아 만들도록 했습니다
    public class UIBase : MonoBehaviour
    {
    		// GameObject, Image, Text 등의 UI요소들을 저장할 Dictionary
    		Dictionary<Type, Dictionary<string, UnityEngine.Object>> _objects
    				= new Dictionary<Type, Dictionary<string, UnityEngine.Object>>();
    		
    		// T타입의 요소를 enumType으로 주어진 enum요소의 이름들을 이용해서 찾아서
    		// _objects에 저장하는 함수
    		// GameObject나 Component를 구분하지 않고 모두 저장하기 위해
    		// UnityEngine.Object 제네릭 형식 제약 조건을 두었습니다
    		protected void Bind<T>(Type enumType) where T : UnityEngine.Object
    		{
    		    if (_objects.ContainsKey(typeof(T)) == false)
    		    {
    		        Dictionary<string, UnityEngine.Object> objects
    								= new Dictionary<string, UnityEngine.Object>();
    		
    		        _objects.Add(typeof(T), objects);
    		    }
    		
    		    string[] names = enumType.GetEnumNames();
    		
    		    foreach (string name in names)
    		    {
    		        T bindObject = null;
    		
    		        if (typeof(T) == typeof(GameObject))
    		            bindObject = Util.FindChild(gameObject, name) as T;
    		        else
    		            bindObject = Util.FindChild<T>(gameObject, name);
    		
    		        if (bindObject != null)
    		            _objects[typeof(T)].Add(name, bindObject);
    		    }
    		}
    		
    		// 저장했던 요소를 찾는 함수
    		protected T Get<T>(string name) where T : UnityEngine.Object
    		{
    		    return _objects[typeof(T)][name] as T;
    		}
    }
    ```
    

## 몬스터 상태 구현

<aside>
💡 몬스터 로직 구현은 FSM 패턴을 통해 상태마다 특정 행동을 하도록 구현했습니다

</aside>

![image](https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/5892b57a-4485-4e39-b615-03090c8bc2e0)

각 상태는 EnemyState 클래스를 상속받아 구현했습니다

![image](https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/c83613fe-e89f-4c40-941f-55b66c4ef1db)

### 상태 목록

- SpawnState - 스폰 상태로 스폰될 때 1초 정도 자리에서 대기하는 상태
- IdleState - (현재 게임에선 필요 없는 상태)바로 MoveState 상태로 전환하는 상태
- MoveState - 플레이어를 쫓아가는 상태
- AttackState - 공격 쿨타임에 맞게 기다리며 공격하는 상태
- AttackedState - 공격받아 잠깐 경직되었다가 이전 상태로 돌아가는 상태
- DieState - 죽는 애니메이션 실행 후 파괴되는 상태

![image](https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/4194fcab-7c79-421b-afc5-b40535d32eba)

초기에 상태들을 모두 생성하여 저장하는 코드

![image](https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/40b58c0c-0bfc-414a-8830-b4838e1daf9b)

상태를 전환하는 함수 부분의 코드

- 몬스터 생성 시 상태들을 모두 생성하여 저장해두었습니다
- 공격 상태에서 공격 쿨타임을 계산할 때 상태가 전환되면 쿨타임이 초기화되는 문제가 있어서 상태를 재사용하여 해결하였습니다

![image](https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/f833d57d-2781-4770-9b47-24d4c2643091)

- 각 상태는 enum타입으로 두어 사용이 용이하도록 했습니다

## 스테이지 요소 배치 툴

### 영상&nbsp;보러가기

[![Video Label](http://img.youtube.com/vi/rRj5Mx-59Gg/0.jpg)](https://youtu.be/rRj5Mx-59Gg)

![image](https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/3bd975bd-498e-4a86-899a-b4a87032f958)

- 동작 방식
    - 불러오기 기능을 통해 Json파일에 저장해뒀던 스폰 정보를 불러올 수 있도록 했습니다
    - 게임 스테이지를 Room이라는 개념으로 구역을 나누고 제가 만든 스테이지의 요소들은 Room 범위 위에 배치할 수 있도록 했습니다
    - 배치했던 요소를 클릭하여 위치를 옮기거나 크기를 조절할 수 있습니다
    - 저장 기능을 통해 Json파일로 저장할 수 있도록 했습니다

![스테이지 전체 모습
이미지 클릭 후 SpaceBar를 누르면 크게 보실 수 있습니다](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/a679728a-9d22-4793-8f67-73c0d776ff40/Untitled.png)

스테이지 전체 모습
이미지 클릭 후 SpaceBar를 누르면 크게 보실 수 있습니다

![Room 하나의 모습](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/cd5f710a-7b39-4637-b757-5d49a9d51f0d/%EB%B3%B4%EC%8A%A4%EB%A3%B8%EB%A7%88%ED%82%B9.png)

Room 하나의 모습

- 아래 빨간색으로 마킹한 부분은 플레이어가 접촉할 시 해당 Room의 각 요소(몬스터, 문 등)를 스폰시키는 트리거입니다
- 아래 보라색으로 마킹한 부분은 통행을 막을 문입니다
- 초록색으로 마킹한 부분은 몬스터들을 배치해둔 부분입니다
- 스테이지 요소 파일 구조

![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/d55cfb84-dd25-4c62-8765-21f6ec1f5d78/Untitled.png)

- Json형식
- Rooms 이름의 배열을 저장
- Rooms 배열의 각 요소는 이름, 위치, 크기, 트리거 목록, 스폰시킬 오브젝트 목록, 문 목록을 가지도록 하였습니다

# Scene 구조

## 플레이어 선택 씬

![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/2cc31d32-d4e2-4189-862f-e5416f7bff63/Untitled.png)

- 플레이할 캐릭터를 선택하는 씬입니다

![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/ccbe0c7f-9e93-489a-a362-ab12c0d06fe0/Untitled.png)

- 캐릭터 정보는 배열 형태의 Json파일로 저장해두었으며 각 배열 요소는 캐릭터 id, 이름, 체력, 이동속도, 무기id, 썸네일이미지 경로, 캐릭터 색상을 저장해두었습니다

## 게임 씬

![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/e9ba1c84-4cc8-482b-9ca7-007f801b0a51/Untitled.png)

- 키보드의 WASD키를 이용해 이동합니다
- 마우스 좌클릭으로 공격할 수 있습니다
- 마우스 우클릭으로 회피를 할 수 있습니다
- 키보드의 R키를 통해 장전할 수 있습니다
- 키보드의 SpaceBar키를 통해 폭탄을 사용할 수 있습니다
- (추가 획득한 무기가 있다면)숫자키를 통해 무기 교체가 가능합니다

![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/f7e58d61-0555-4e3f-97cb-457a407a16ea/Untitled.png)

- 트리거를 밟음으로써 특정 룸의 요소들을 생성할 수 있습니다
- 맵 요소(Room, 트리거, 몬스터, 상점 등)는 Json파일로 저장해두었으며 아래 스테이지 요소 배치 씬에서 편리하게 배치가 가능하도록 했습니다

![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/84f6b459-0bbf-450d-8910-548016225285/Untitled.png)

- 상점 룸에서는 사려는 요소에 다가가면 해당 아이템의 이름과 가격 확인이 가능합니다
- 접근한 상태에서 키보드의 E키를 통해 구매가 가능합니다

![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/2957f8ff-7231-451c-be61-0d316fc8e2f8/Untitled.png)

- 보스 룸에서는 보스와 전투를 하며 보스를 잡으면 게임에서 승리할 수 있습니다

## 스테이지 요소 배치 씬(맵메이킹 씬)

![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/d13192ca-79d9-478e-9b7b-59a130778b02/Untitled.png)

- 불러오기 기능을 통해 배치했던 요소들을 불러올 수 있습니다
- Room, 트리거, 몬스터, 상점 등을 배치할 수 있습니다
- 배치했던 요소를 클릭하여 위치를 옮기거나 크기를 조절할 수 있습니다
- 저장 기능을 통해 수정한 내용을 Json파일로 저장할 수 있습니다
