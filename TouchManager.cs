using UnityEngine;

public enum MoveDirection {
    LEFT, RIGHT, UP, DOWN
}

public class TouchManager : MonoBehaviour {

    private float fingerStartTime = 0.0f;
    private Vector2 fingerStartPos = Vector2.zero;
    private Vector2 fingerEndPos = Vector2.zero;


    private bool isSwipe = false;
    private float minSwipeDist = 50.0f;
    private float maxSwipeTime = 1.5f;
  
    public GameObject changeStyleBtn;
    public GameObject undoBtn;
    public GameObject panelSwype;

    private GameManager gm;

    void Awake() {
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    void Start() {
        UIEventListener.Get(changeStyleBtn).onClick += ChangeStyle;
        UIEventListener.Get(undoBtn).onClick += Undo;
        UIEventListener.Get(panelSwype).onKey += MoveKey;
        UIEventListener.Get(panelSwype).onPress += MoveTouch;
    }

    private void MoveTouch(GameObject go, bool isPressed) {
        if (isPressed && !isSwipe) {
            isSwipe = true;
            fingerStartTime = Time.time;
            fingerStartPos = UICamera.currentTouch.pos;
        } else {
            float gestureTime = Time.time - fingerStartTime;
            float gestureDist = (UICamera.currentTouch.totalDelta - fingerStartPos).sqrMagnitude;

            if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist) {
                Vector2 direction = UICamera.currentTouch.pos - fingerStartPos;
                Vector2 swipeType = Vector2.zero;

                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
                    // the swipe is horizontal:
                    swipeType = Vector2.right * Mathf.Sign(direction.x);
                } else {
                    // the swipe is vertical:
                    swipeType = Vector2.up * Mathf.Sign(direction.y);
                }
                if (swipeType.x != 0.0f) {
                    if (swipeType.x > 0.0f) {
                        // MOVE RIGHT
                        gm.Move(MoveDirection.RIGHT);
                    } else {
                        // MOVE LEFT
                        gm.Move(MoveDirection.LEFT);
                    }
                }
                if (swipeType.y != 0.0f) {
                    if (swipeType.y > 0.0f) {
                        // MOVE UP
                        gm.Move(MoveDirection.UP);
                    } else {
                        // MOVE DOWN
                        gm.Move(MoveDirection.DOWN);
                    }
                }
            }
        }
    }

    private void ChangeStyle(GameObject sender) {
        gm.ChangeStyle(sender);
    }

    private void Undo(GameObject sender) {
        gm.Undo(sender);
    }

    private void MoveKey(GameObject sender, KeyCode keyCode) {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
           // Debug.Log("Right");
            gm.Move(MoveDirection.RIGHT);
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
          //  Debug.Log("LEFT");
            gm.Move(MoveDirection.LEFT);
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
         //   Debug.Log("DOWN");
            gm.Move(MoveDirection.DOWN);
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
           // Debug.Log("UP");
            gm.Move(MoveDirection.UP);
        }
    }


}
