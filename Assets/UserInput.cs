using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFruitNinja
{
    public class UserInput : MonoBehaviour
    {
        #region EVENTMETHODS
        //when the user presses and drags on the screen. if the user removes too quickly then its considered as tap
        public delegate void PanBeganAction(Touch t);
        public static event PanBeganAction OnPanBegan;

        public delegate void PanHeldAction(Touch t);
        public static event PanHeldAction OnPanHeld;

        public delegate void PanEndedAction(Touch t);
        public static event PanEndedAction OnPanEnded;

        //To check accelerometer action
        public delegate void AccelerometerChangedAction(Vector3 acceleration);
        public static event AccelerometerChangedAction OnAccelerometerChanged;
        #endregion

        #region PUBLIC VARIABLES
        public float tapMaxMovement = 50; //Maximum pixel tap can move
        public float panMinTime = 0.4f;//tap gesture lasts more than minumum time
        #endregion

        #region PRIVATE VARAIBLES
        private Vector2 movement;           //Movement vector will track who far you move.

        private bool tapGestureFailed = false;  //tap Gesture will become,
        private float startTime;//will keep time when our gesture begins
        private Vector3 defaultAcceleration;
        private bool panGestureRecognized = false;// when we recognize gesture we gone make true
        #endregion

        #region MONOBEHAVIOUR METHODS
        #endregion
        void Start()
        {


        }

        // Update is called once per frame
        void Update()
        {
            if (OnAccelerometerChanged != null)
            {
                Vector3 acceleration = new Vector3(Input.acceleration.x, Input.acceleration.y, -1 * Input.acceleration.z);
                acceleration -= defaultAcceleration;
                OnAccelerometerChanged(acceleration);
            }
            if (Input.touchCount > 0) //To finding out,no.of touches greater than 0 or not. If no touches, then no movement.
            {
                Touch touch = Input.touches[0]; //Need to find out, number of touches on screen. If there are more no.oc touches, need to call array.
                if (touch.phase == TouchPhase.Began) // We have several touch phases, began enters the first frame of the touch.
                {
                   // movement = Vector2.zero; //We made our movement to zero.
                    startTime = Time.time;

                }
                else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                   
                    if (!panGestureRecognized && Time.time - startTime > panMinTime)//if current time and start time greater than min time 
                    {
                        Debug.Log("Pan Gesture Enabled");
                        panGestureRecognized = true;
                       // tapGestureFailed = true;

                        if (OnPanBegan != null)
                            OnPanBegan(touch);
                    }
                    else if (panGestureRecognized)
                    {
                        if (OnPanHeld != null)
                            OnPanHeld(touch);
                    }
                   

                }


                else  // if finger is removed from screen, then we are calling if tapgesture is not failed.
                {
                    if (panGestureRecognized)
                    {
                        if (OnPanEnded != null)
                            OnPanEnded(touch);
                    }
                   
                   
                    panGestureRecognized = false; // ready for the next pan gesture
                }


            }
            void OnEnable()
            {
                defaultAcceleration = new Vector3(Input.acceleration.x, Input.acceleration.y, -1 * Input.acceleration.z);
            }




        }


    }
}
