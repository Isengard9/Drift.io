using System;
using System.Collections.Generic;
using Game.Scripts.Manager.General;
using UnityEngine;
namespace Game.Scripts.ControllerContainer
{
    public class ControllerContainer: MonoBehaviour
    {
        
        #region Variables

        private static ControllerContainer instance;
        public static ControllerContainer Instance => instance;

        private List<Vehicle.Vehicle> vehicles = new List<Vehicle.Vehicle>();
        public List<Vehicle.Vehicle> Vehicles => vehicles;

        

        #endregion

        #region MonoBehaviour

        private void Awake()
        {
            instance = this;
        }

        #endregion

        #region ListProcess

        public void AddVehicle(Vehicle.Vehicle vehicle)
        {
            if(vehicles.Contains(vehicle))
                return;
            
            vehicles.Add(vehicle);
        }

        public void RemoveVehicle(Vehicle.Vehicle vehicle)
        {
            if (vehicles.Contains(vehicle))
            {
                vehicles.Remove(vehicle);
                ControlCheckLevelEnd();
            }
        }

        private void ControlCheckLevelEnd()
        {
            if (!GameManager.IsGameStarted || GameManager.IsGameEnded) return;
            
            if (vehicles.Count > 1) return;

            else
            {
                ManagerContainer.Instance.UIManager.OnLevelSuccess();
            }
        }

        #endregion
        
    }
}