using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapSelectPopup : UIBase
{
    private enum Transforms
    {
        Content
    }

    public void ShowPopup() {
        gameObject.SetActive(true);

        // 스테이지 목록 불러와서
        List<int> stageList = new List<int>();
        {
            // Resources 폴더 경로 설정
            string stageDataPath = Application.persistentDataPath;
            
            // 디렉터리가 존재하는지 확인
            if (Directory.Exists(stageDataPath))
            {
                // 모든 파일 경로 가져오기
                string[] files = Directory.GetFiles(stageDataPath, "Stage*.*", SearchOption.TopDirectoryOnly);

                // 파일 리스트에 담기
                foreach (string file in files)
                {
                    // 파일 경로를 Resources 폴더 기준으로 상대 경로로 변환
                    string stageId = file.Substring(stageDataPath.Length).Replace("Stage", "").Replace("/", "").Split(".")[0];
                    Debug.Log("stageId: " + stageId);
                    stageList.Add(int.Parse(stageId));
                }
            }
            else
            {
                Debug.LogError("Resources 폴더가 존재하지 않습니다.");
            }

            Transform gridPanel = Get<Transform>(Transforms.Content.ToString());

            foreach (int stageId in stageList)
            {
                GameObject itemUI = Managers.Resource.Instantiate("Prefabs/MapMaking/MapItem", gridPanel);
                itemUI.GetComponent<MapItem>().SetData(stageId);
            }
        }
    }

    public void HidePopup() {
        gameObject.SetActive(false);
    }

    protected override void InitImediately()
    {
        base.InitImediately();

        Bind<Transform>(typeof(Transforms));
    }
}
