using UnityEngine;
using UnityEngine.InputSystem;

namespace MyHerbagnole
{
    /// <summary>
    /// Represents the player's input in my herbagnole
    /// </summary>
    public class BagnolePlayer : MonoBehaviour
    {
        public int id { get; private set; }
        private PlayerInput input;
        private CarController carController;
        public GameObject prefabCar { get;  private set;}
        public GameObject prefabDriver { get; private set; }

        public void Init(int id, PlayerInput input)
        {
            this.id = id;
            this.input = input;
        }

        public void SetPrefabs(GameObject prefabCar, GameObject prefabDriver)
        {
            this.prefabCar = prefabCar;
            this.prefabDriver = prefabDriver;
        }

        public void SetDriver(GameObject driver)
        {
            prefabDriver = driver;
        }

        public void SetCar(GameObject car)
        {
            prefabCar = car;
        }

        public CarController CreateCar()
        {
            carController = Instantiate(prefabCar).GetComponent<CarController>();
            carController.SetDriver(prefabDriver);
            input.camera = carController.GetCamera();
            return carController;
        }

        public void DestroyCar()
        {
            if (carController != null) Destroy(carController.gameObject);
            carController = null;
            input.camera = null;
        }

        void OnMove(InputValue inputValue)
        {
            if (carController && BagnoleManager.instance.state == BagnoleManager.GameState.RACE)
            {
                carController.Move(inputValue.Get<Vector2>());
            }
            else if (BagnoleManager.instance.state == BagnoleManager.GameState.MAINMENU)
            {
                BagnoleMenuManager.instance.ForwardOnMove(id, inputValue.Get<Vector2>());
            }
        }

        void OnUse(InputValue inputValue)
        {
            if (carController && BagnoleManager.instance.state == BagnoleManager.GameState.RACE)
            {
                // Do something I guess
            }
            else if (BagnoleManager.instance.state == BagnoleManager.GameState.MAINMENU)
            {
                BagnoleMenuManager.instance.ForwardOnSubmit(id);
            } 
        }
    }
}

