using Harmony;
using UnityEngine;

namespace NKHook6.Api.Events.InGame
{
    [HarmonyPatch(typeof(Assets.Scripts.Unity.UI_New.InGame.InGame), "Update")]
    public class OnUI_NewInGameUpdate
    {
        [HarmonyPrefix]
        public static bool Prefix(Assets.Scripts.Unity.UI_New.InGame.InGame __instance)
        {
            Camera c = __instance.sceneCamera;
            if (Input.GetMouseButton(1))
                Update(c);

            return true;
        }

        static float mainSpeed = 10f; // Regular speed.
        static float shiftAdd = 25f;  // Multiplied by how long shift is held.  Basically running.
        static float maxShift = 100f; // Maximum speed when holding shift.
        static float camSens = .35f;  // Camera sensitivity by mouse input.
        static private Vector3 lastMouse = new Vector3(Screen.width / 2, Screen.height / 2, 0); // Kind of in the middle of the screen, rather than at the top (play).
        static private float totalRun = 1.0f;

        static void Update(Camera c)
        {

            // Mouse input.
            lastMouse = Input.mousePosition - lastMouse;
            lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
            lastMouse = new Vector3(c.transform.eulerAngles.x + lastMouse.x, c.transform.eulerAngles.y + lastMouse.y, 0);
            c.transform.eulerAngles = lastMouse;
            lastMouse = Input.mousePosition;

            // Keyboard commands.
            Vector3 p = getDirection();
            if (Input.GetKey(KeyCode.LeftShift))
            {
                totalRun += Time.deltaTime;
                p = p * totalRun * shiftAdd;
                p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
                p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
                p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
            }
            else
            {
                totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
                p = p * mainSpeed;
            }
            if (Input.GetKey(KeyCode.W))
            {
                c.orthographicSize *= 0.9f;
            }
            if (Input.GetKey(KeyCode.S))
            {

                c.orthographicSize *= 1.1f;
            }
            p = p * Time.deltaTime;
            Vector3 newPosition = c.transform.position;
            if (Input.GetKey(KeyCode.V))
            { //If player wants to move on X and Z axis only
                c.transform.Translate(p);
                newPosition.x = c.transform.position.x;
                newPosition.z = c.transform.position.z;
                c.transform.position = newPosition;
            }
            else
            {
                c.transform.Translate(p);
            }
        }

        static private Vector3 getDirection()
        {
            Vector3 p_Velocity = new Vector3();

            if (Input.GetKey(KeyCode.A))
            {
                p_Velocity += new Vector3(-1, 0, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                p_Velocity += new Vector3(1, 0, 0);
            }
            if (Input.GetKey(KeyCode.R))
            {
                p_Velocity += new Vector3(0, 1, 0);
            }
            if (Input.GetKey(KeyCode.F))
            {
                p_Velocity += new Vector3(0, -1, 0);
            }
            return p_Velocity;
        }
    }
}
