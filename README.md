# RogueLikeShootingGame2D

![image](https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/88df9c91-314e-4467-8e15-a6cac38279a4)

# 목차
- [플레이 영상 보러가기](#플레이-영상-보러가기)
- [프로젝트 소개](#프로젝트-소개)
- [게임 로직](#게임-로직)
- [구현 내용](#구현-내용)
  * [물 반사 효과 구현](#물-반사-효과-구현)
  * [UI로직](#ui로직)
  * [몬스터 상태 구현](#몬스터-상태-구현)
    + [상태 목록](#상태-목록)
  * [스테이지 요소 배치 툴](#스테이지-요소-배치-툴)
    + [영상 보러가기](#영상-보러가기)
- [Scene 구조](#scene-구조)
  * [플레이어 선택 씬](#플레이어-선택-씬)
  * [게임 씬](#게임-씬)
  * [스테이지 요소 배치 씬](#스테이지-요소-배치-씬)

# 빌드 파일
- 빌드 파일은 크기가 커서 노션 문서에 업로드해두었습니다
- https://better-cherry-6e2.notion.site/RogueLikeShootingGame2D-dc9cf6672d9943c3813ee37f3895de01

# 플레이 영상 보러가기

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
<img src="https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/22266ed4-f25e-478b-8290-78e6d5ed0d4d" width="600"/>
<br>
<img src="https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/8176cf26-5968-4164-af5b-539991ed5db4" width="400"/>
<img src="https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/47b799c2-c030-4c6b-af44-080132b2e791" width="400"/>

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

<img src="https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/5892b57a-4485-4e39-b615-03090c8bc2e0" width="500"/>

각 상태는 EnemyState 클래스를 상속받아 구현했습니다

<img src="https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/c83613fe-e89f-4c40-941f-55b66c4ef1db" width="500"/>

### 상태 목록

- SpawnState - 스폰 상태로 스폰될 때 1초 정도 자리에서 대기하는 상태
- IdleState - (현재 게임에선 필요 없는 상태)바로 MoveState 상태로 전환하는 상태
- MoveState - 플레이어를 쫓아가는 상태
- AttackState - 공격 쿨타임에 맞게 기다리며 공격하는 상태
- AttackedState - 공격받아 잠깐 경직되었다가 이전 상태로 돌아가는 상태
- DieState - 죽는 애니메이션 실행 후 파괴되는 상태

<img src="https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/4194fcab-7c79-421b-afc5-b40535d32eba" width="500"/>

초기에 상태들을 모두 생성하여 저장하는 코드

<img src="https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/40b58c0c-0bfc-414a-8830-b4838e1daf9b" width="500"/>
상태를 전환하는 함수 부분의 코드

- 몬스터 생성 시 상태들을 모두 생성하여 저장해두었습니다
- 공격 상태에서 공격 쿨타임을 계산할 때 상태가 전환되면 쿨타임이 초기화되는 문제가 있어서 상태를 재사용하여 해결하였습니다

<img src="https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/f833d57d-2781-4770-9b47-24d4c2643091" width="500"/>
 
- 각 상태는 enum타입으로 두어 사용이 용이하도록 했습니다

## 스테이지 요소 배치 툴

### 영상 보러가기

[![Video Label](http://img.youtube.com/vi/rRj5Mx-59Gg/0.jpg)](https://youtu.be/rRj5Mx-59Gg)

<img src="https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/3bd975bd-498e-4a86-899a-b4a87032f958" width="500"/>
 
### 동작 방식
    - 불러오기 기능을 통해 Json파일에 저장해뒀던 스폰 정보를 불러올 수 있도록 했습니다
    - 게임 스테이지를 Room이라는 개념으로 구역을 나누고 제가 만든 스테이지의 요소들은 Room 범위 위에 배치할 수 있도록 했습니다
    - 배치했던 요소를 클릭하여 위치를 옮기거나 크기를 조절할 수 있습니다
    - 저장 기능을 통해 Json파일로 저장할 수 있도록 했습니다

<img src="https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/b3e2ae05-a6a2-40a4-bfd2-8396ddd43c43" width="500"/>

<b>스테이지 전체 모습</b>

<img src="https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/3972c127-9f0f-48e8-948c-42337bd92e92" width="500"/>
 
<b>Room 하나의 모습</b>
- 아래 빨간색으로 마킹한 부분은 플레이어가 접촉할 시 해당 Room의 각 요소(몬스터, 문 등)를 스폰시키는 트리거입니다
- 아래 보라색으로 마킹한 부분은 통행을 막을 문입니다
- 초록색으로 마킹한 부분은 몬스터들을 배치해둔 부분입니다

### 스테이지 요소 파일 구조
<img src="https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/9918191f-3d8c-4d69-980a-ffd9d11a8518" width="300"/>
 
- Json형식
- Rooms 이름의 배열을 저장
- Rooms 배열의 각 요소는 이름, 위치, 크기, 트리거 목록, 스폰시킬 오브젝트 목록, 문 목록을 가지도록 하였습니다

# Scene 구조

## 플레이어 선택 씬

<img src="https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/327a25d4-7715-4d73-b7cf-c2b96a8c35f7" width="500"/>
 
<b>플레이할 캐릭터를 선택하는 씬입니다</b>

<img src="https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/3da01837-e88c-4aab-a180-8be889f02e1f" width="500"/>
 
- 캐릭터 정보는 배열 형태의 Json파일로 저장해두었으며 각 배열 요소는 캐릭터 id, 이름, 체력, 이동속도, 무기id, 썸네일이미지 경로, 캐릭터 색상을 저장해두었습니다

## 게임 씬

<img src="https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/561c66d5-5e91-4fa0-b672-6a676a16d359" width="500"/>
 
- 키보드의 WASD키를 이용해 이동합니다
- 마우스 좌클릭으로 공격할 수 있습니다
- 마우스 우클릭으로 회피를 할 수 있습니다
- 키보드의 R키를 통해 장전할 수 있습니다
- 키보드의 SpaceBar키를 통해 폭탄을 사용할 수 있습니다
- (추가 획득한 무기가 있다면)숫자키를 통해 무기 교체가 가능합니다

<img src="https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/efa28c4a-3044-448b-bdaf-e49fc406f02a" width="500"/>
 
- 트리거를 밟음으로써 특정 룸의 요소들을 생성할 수 있습니다
- 맵 요소(Room, 트리거, 몬스터, 상점 등)는 Json파일로 저장해두었으며 아래 스테이지 요소 배치 씬에서 편리하게 배치가 가능하도록 했습니다

<img src="https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/995a4790-9a72-4ee5-9278-73925f537b3e" width="500"/>
 
- 상점 룸에서는 사려는 요소에 다가가면 해당 아이템의 이름과 가격 확인이 가능합니다
- 접근한 상태에서 키보드의 E키를 통해 구매가 가능합니다

<img src="https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/a9092a4e-4248-45da-86e7-814fd46587c9" width="500"/>
 
- 보스 룸에서는 보스와 전투를 하며 보스를 잡으면 게임에서 승리할 수 있습니다

## 스테이지 요소 배치 씬

<img src="https://github.com/SSeadog/RogueLikeShootingGame2D/assets/58541838/7b56e03b-296a-4a51-b595-ef03cc436809" width="500"/>
 
- 불러오기 기능을 통해 배치했던 요소들을 불러올 수 있습니다
- Room, 트리거, 몬스터, 상점 등을 배치할 수 있습니다
- 배치했던 요소를 클릭하여 위치를 옮기거나 크기를 조절할 수 있습니다
- 저장 기능을 통해 수정한 내용을 Json파일로 저장할 수 있습니다
