using ChronosFall.Scripts.Configs;
using ChronosFall.Scripts.Interfaces;
using UnityEngine;

namespace ChronosFall.Scripts.Characters.Player.PlayerControls
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private float searchRadius = 1f; // インタラクトの半径

        private void Update()
        {
            if (Input.GetKey(CharacterInputKey.Interact))
            {
                ExecuteInteraction();
            }
        }

        private void ExecuteInteraction()
        {
            //searchRadiusの範囲内のコライダーを取得
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, searchRadius);

            //最も近いオブジェクトを探す
            Collider closestObject = null;
            float closestDistance = Mathf.Infinity;

            //forEachでコライダーを検索していく
            foreach (var hitCollider in hitColliders)
            {
                //タグ:Interactableがついているかどうか
                if (hitCollider.CompareTag("Interactable"))
                {
                    //距離計算
                    float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        //最も近いコライダーを保存
                        closestObject = hitCollider;
                    }
                }
            }
            if (!closestObject) return;
            
            // InterfaceScriptを実装しているコンポーネントを探す
            IInterface interactable = closestObject.GetComponent<IInterface>();
            if (interactable != null)
            {
                interactable.Interact();
            }
            else
            {
                Debug.LogError($"{closestObject.gameObject.name} は IInteractable を実装していません！");
            }
        }
    }
}
