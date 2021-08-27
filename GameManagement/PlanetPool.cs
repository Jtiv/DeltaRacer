using System;

public class PlanetPool : GenericPool<Planet>
{
    public event Action PlanetReturned;
    
    public void AddPlanetToPool(Planet objectToAdd)
    {
        base.ReturnToPool(objectToAdd);
        PlanetReturned?.Invoke();
    }

}
