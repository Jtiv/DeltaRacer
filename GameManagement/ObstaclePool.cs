public class ObstaclePool : GenericPool<Obstacle>
{
    public void BatchPool(int count)
    {
        SpawnToPool(count);
    }
}
