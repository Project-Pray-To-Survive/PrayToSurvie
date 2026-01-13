using System.Collections.Generic;

public class InputQueue
{
    private Queue<NetWorkMessage> _queue = new Queue<NetWorkMessage>();
    public void Enqueue(NetWorkMessage data)
    {
        _queue.Enqueue(data);
    }
    public NetWorkMessage Dequeue()
    {
        return _queue.Dequeue();
    }
}
