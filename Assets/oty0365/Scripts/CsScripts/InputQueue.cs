using System.Collections.Generic;

public class InputQueue
{
    private Queue<IBehavior> _queue = new Queue<IBehavior>();
    public void Enqueue(IBehavior behavior)
    {
        _queue.Enqueue(behavior);
    }
    public IBehavior Dequeue()
    {
        return _queue.Dequeue();
    }
}
