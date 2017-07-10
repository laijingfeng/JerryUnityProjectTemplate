using UnityEngine;
using UnityEngine.EventSystems;

public class MySR : UIBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public enum MovementType
    {
        /// <summary>
        /// Restricted but flexible -- can go past the edges, but springs back in place
        /// </summary>
        Elastic,
        /// <summary>
        /// Restricted movement where it's not possible to go past the edges
        /// </summary>
        Clamped,
    }

    [SerializeField]
    private RectTransform m_Content;
    [SerializeField]
    private RectTransform m_ViewRect;
    [SerializeField]
    private bool m_Horizontal = true;
    [SerializeField]
    private bool m_Vertical = true;
    [SerializeField]
    private MovementType m_MovementType = MovementType.Elastic;

    private Vector2 m_Velocity;
    private bool m_Dragging;

    // The offset from handle position to mouse down position
    private Vector2 m_PointerStartLocalCursor = Vector2.zero;
    private Vector2 m_ContentStartPosition = Vector2.zero;

    private Bounds m_ContentBounds;
    private Bounds m_ViewBounds;

    protected override void Awake()
    {
        base.Awake();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Vec3ToString(m_ViewBounds.size);
            Vec3ToString(m_ContentBounds.size);

            //Vector2 localCursor;
            //if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(m_ViewRect, Input.mousePosition, null, out localCursor))
            //{
            //    Debug.LogWarning("xxxx");
            //    return;
            //}
            //Vec3ToString(localCursor);
        }
    }

    public static void Vec3ToString(Vector3 vec, string tag = "")
    {
        Debug.LogWarning(string.Format("{0}={1},{2},{3}", tag, vec.x, vec.y, vec.z));
    }

    public virtual void OnInitializePotentialDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        m_Velocity = Vector2.zero;
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        if (!IsActive())
        {
            return;
        }

        UpdateBounds();

        m_PointerStartLocalCursor = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_ViewRect, eventData.position, eventData.pressEventCamera, out m_PointerStartLocalCursor);
        m_ContentStartPosition = m_Content.anchoredPosition;
        m_Dragging = true;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        m_Dragging = false;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        if (!IsActive())
        {
            return;
        }

        Vector2 localCursor;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(m_ViewRect, eventData.position, eventData.pressEventCamera, out localCursor))
        {
            return;
        }
        //Vec3ToString(localCursor);

        UpdateBounds();

        var pointerDelta = localCursor - m_PointerStartLocalCursor;
        Vector2 position = m_ContentStartPosition + pointerDelta;

        // Offset to get content into place in the view.
        Vector2 offset = CalculateOffset(position - m_Content.anchoredPosition);
        position += offset;
        if (m_MovementType == MovementType.Elastic)
        {
            if (offset.x != 0)
            {
                position.x = position.x - RubberDelta(offset.x, m_ViewBounds.size.x);
            }
            if (offset.y != 0)
            {
                position.y = position.y - RubberDelta(offset.y, m_ViewBounds.size.y);
            }
        }

        SetContentAnchoredPosition(position);
    }

    protected virtual void SetContentAnchoredPosition(Vector2 position)
    {
        if (!m_Horizontal)
        {
            position.x = m_Content.anchoredPosition.x;
        }
        if (!m_Vertical)
        {
            position.y = m_Content.anchoredPosition.y;
        }

        if (position != m_Content.anchoredPosition)
        {
            m_Content.anchoredPosition = position;
            UpdateBounds();
        }
    }

    private static float RubberDelta(float overStretching, float viewSize)
    {
        return (1 - (1 / ((Mathf.Abs(overStretching) * 0.55f / viewSize) + 1))) * viewSize * Mathf.Sign(overStretching);
    }

    private void UpdateBounds()
    {
        m_ViewBounds = new Bounds(m_ViewRect.rect.center, m_ViewRect.rect.size);
        m_ContentBounds = new Bounds(m_Content.rect.center, m_Content.rect.size);

        Vec3ToString(m_ContentBounds.min, "m_ContentBounds");
    }

    private Vector2 CalculateOffset(Vector2 delta)
    {
        Vector2 offset = Vector2.zero;

        Vector2 min = m_ContentBounds.min;
        Vector2 max = m_ContentBounds.max;

        if (m_Horizontal)
        {
            min.x += delta.x;
            max.x += delta.x;
            if (min.x > m_ViewBounds.min.x)
            {
                offset.x = m_ViewBounds.min.x - min.x;
            }
            else if (max.x < m_ViewBounds.max.x)
            {
                offset.x = m_ViewBounds.max.x - max.x;
            }
        }

        if (m_Vertical)
        {
            min.y += delta.y;
            max.y += delta.y;
            if (max.y < m_ViewBounds.max.y)
            {
                offset.y = m_ViewBounds.max.y - max.y;
                Debug.LogWarning("Maxxxx");
            }
            else if (min.y > m_ViewBounds.min.y)
            {
                offset.y = m_ViewBounds.min.y - min.y;
                Debug.LogWarning("Minnnn");
            }
        }

        Vec3ToString(min, "min");
        Vec3ToString(m_ViewBounds.min, "viewBounds");

        return offset;
    }
}