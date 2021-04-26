using ComponentProcessingAPIProject.Models;
using ComponentProcessingAPIProject.Test.view_model;
using System.Collections.Generic;

namespace ComponentProcessingAPIProject.Repository
{
    public interface IRepo
    {
        //ProcessDetail ProcessDetails(string name1, string contactNumber1, long creditCardNumber1, string componentType1, string componentName1, int quantity1);

        IEnumerable<ProcessDetail> GetAllDetails();

        ProcessResponse ProcessResponse(string Name, string ContactNumber, string ComponentType, string ComponentName, int Quantity, string IsPriorityRequest);

        string CompleteProcess(int RequestId);
    }
}
