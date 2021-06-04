using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using TMPro;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
        public GameObject winTextObject;
        public int counter;
        public TextMeshProUGUI bananacounter;


		public int maxHealth = 3;                           //From Here
		public float timeInvincible = 3.0f;

    
		public int health { get { return currentHealth; }}
		int currentHealth;
    
		bool isInvincible;
		float invincibleTimer;                              //To here are the code from Ruby's Adventure

        private void Start()
        {

            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
            winTextObject.SetActive(false);
            counter = 0;
            SetCountText();
        }

        bool able_to_win = false;
        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            //End Game
            //Keep checking the number of fruits that user collected
            if (counter == 5)
            {
                winTextObject.SetActive(true);
                able_to_win = true;
            }

            if (Input.GetKeyDown("m"))
            {
                SceneManager.LoadScene("Level Select");
                Cursor.lockState = CursorLockMode.None;
            }
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v * m_CamForward + h * m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v * Vector3.forward + h * Vector3.right;
            }
#if !MOBILE_INPUT
            // walk speed multiplier
            if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);
            m_Jump = false;
        }


        void SetCountText()
        {
            bananacounter.text = "Bananas: " + counter.ToString(); //placeholder for counter when working       

        }
        //Player move over fruits, then trigger the event to disable the correspond fruit
        //This block of code is based on the roll a ball tutorial
        //Maybe something wrong with the collider
        public AudioSource src;
        public AudioClip clip;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("fruit"))
            {
                //Display a debug message on console
                Debug.Log("Player has triggered the fruit");

                src = other.GetComponent<AudioSource>();
                src.PlayOneShot(clip);
                counter++; //Increment the counter when the user picked up a fruit.
                other.GetComponent<Collider>().enabled = false;
                Destroy(other.gameObject, other.GetComponent<AudioSource>().clip.length);

                SetCountText();
            }


            if (other.gameObject.CompareTag("endzone") && able_to_win)
            {
                Debug.Log("Player has triggered the ENDZONE");
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("Level Select");
            }
        }
		public void ChangeHealth(int amount)                            // Borrowed from Ruby's adventure
		{
			if (amount < 0)
			{
				if (isInvincible)
					return;
            
				isInvincible = true;
				invincibleTimer = timeInvincible;
			}
        
			currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
			//Debug.Log(currentHealth + "/" + maxHealth);
		}
    }
}
