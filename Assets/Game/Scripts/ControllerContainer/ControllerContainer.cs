using System;
using System.Collections.Generic;
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
            if (instance is not null)
            {
                Destroy(instance.gameObject);
            }
            instance = this;
        }

        #endregion

        #region AddVehicle

        public void AddVehicle(Vehicle.Vehicle vehicle)
        {
            if(vehicles.Contains(vehicle))
                return;
            
            vehicles.Add(vehicle);
        }

        #endregion
        
    }
}