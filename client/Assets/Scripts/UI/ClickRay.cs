using UnityEngine;
using UnityEngine.EventSystems;

public class ClickRay: MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    MajiangChoosePanel CurChoosePanel;

    [SerializeField]
    GameObject clone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        LayerMask layerMask = LayerMask.GetMask("Majiang");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 50, layerMask.value))
        {
            //是否已经结束了
            var isOver = CurChoosePanel.IsOver();

            if (isOver)
            {
                CurChoosePanel.Lose();
                return;
            }

            var IsCanClick = CurChoosePanel.IsCanClick();

            if (!IsCanClick)
            {
                return;
            }

            var meshItem = hit.collider.gameObject.GetComponent<MajiangMeshItem>();

            var mj = GameObject.Instantiate(clone, transform);
            mj.transform.position = Input.mousePosition;
            mj.GetComponent<MajiangItem>().SetMJId(meshItem.GetMJId());
            mj.SetActive(true);
            
            Debug.Log("射线检测：" + hit.collider.name);
            GameObject.Destroy(hit.collider.gameObject);

            mj.GetComponent<MajiangItem>().FlyToTarget(CurChoosePanel);

            //
        }
    }
}
