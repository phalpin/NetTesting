using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MultiBool
{
    private Dictionary<Guid, bool> _boolState;

    public bool Truthiness
    {
        get
        {
            return _boolState.Values.Count == 0;
        }
    }

    public MultiBool()
    {
        _boolState = new Dictionary<Guid, bool>();
    }

    /// <summary>
    /// Adds a new "falsehood" to alter the truthiness.
    /// </summary>
    /// <returns></returns>
    public Guid addFalse()
    {
        Guid retVal = Guid.NewGuid();
        _boolState.Add(retVal, false);
        return retVal;
    }

    /// <summary>
    /// Sets a given guid identifier to true (if it has it)
    /// </summary>
    /// <param name="incoming"></param>
    public void setTrue(Guid incoming)
    {
        if (_boolState.ContainsKey(incoming))
        {
            _boolState.Remove(incoming);
        }
    }
}